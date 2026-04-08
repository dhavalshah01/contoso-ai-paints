using System.Net;
using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ContosoSearchSkills
{
    /// <summary>
    /// Azure AI Search custom skill that receives document content,
    /// calls Azure OpenAI to extract product data, and returns individual fields.
    /// </summary>
    public class ExtractProductData
    {
        private readonly ILogger _logger;
        private static readonly HttpClient _httpClient = new();

        private static readonly string OpenAIEndpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? "https://contoso-paint-openai.openai.azure.com";
        private static readonly string DeploymentId = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT") ?? "gpt-4.1";
        private static readonly string ApiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? throw new InvalidOperationException("AZURE_OPENAI_API_KEY environment variable is not set");

        public ExtractProductData(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExtractProductData>();
        }

        [Function("ExtractProductData")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("ExtractProductData function triggered");

            try
            {
                // Read and parse the incoming request
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<RequestBody>(requestBody);

                if (data?.Values == null || data.Values.Count == 0)
                {
                    var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                    await badResponse.WriteStringAsync("Invalid request: 'values' array is required");
                    return badResponse;
                }
                // Initialize response structure
                var response = new ResponseBody { Values = new List<ResponseRecord>() };

                foreach (var record in data.Values)
                {
                    try
                    {
                        var content = record.Data.Content;
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            // If no content provided, return an error for this record
                            response.Values.Add(new ResponseRecord
                            {
                                RecordId = record.RecordId,
                                Data = new ExtractedProductFields(),
                                Errors = new List<ErrorWarning> { new() { Message = "No content provided" } },
                                Warnings = null
                            });
                            continue;
                        }

                        // Call Azure OpenAI to extract product data
                        var jsonResult = await CallOpenAI(content);

                        // Parse the JSON response into individual fields
                        var productData = JObject.Parse(jsonResult);
                        var extracted = new ExtractedProductFields
                        {
                            Sku = productData["sku"]?.Value<string>(),
                            Upc = productData["upc"]?.Value<string>(),
                            ProductName = productData["productName"]?.Value<string>(),
                            Brand = productData["brand"]?.Value<string>(),
                            Finish = productData["finish"]?.Value<string>(),
                            Sheen = productData["sheen"]?.Value<string>(),
                            Base = productData["base"]?.Value<string>(),
                            Color = productData["color"]?.Value<string>(),
                            IntendedUse = productData["intendedUse"]?.Value<string>(),
                            VocValue = productData["vocValue"]?.Value<double?>(),
                            VocUnit = productData["vocUnit"]?.Value<string>(),
                            CoverageMin = productData["coverageMin"]?.Value<int?>(),
                            CoverageMax = productData["coverageMax"]?.Value<int?>(),
                            CoverageUnit = productData["coverageUnit"]?.Value<string>(),
                            DryTimeTouchMinutes = productData["dryTimeTouchMinutes"]?.Value<int?>(),
                            DryTimeRecoatMinutes = productData["dryTimeRecoatMinutes"]?.Value<int?>(),
                            DryTimeCureDays = productData["dryTimeCureDays"]?.Value<int?>(),
                            RecommendedCoats = productData["recommendedCoats"]?.Value<int?>(),
                            Cleanup = productData["cleanup"]?.Value<string>(),
                            WarrantyYears = productData["warrantyYears"]?.Value<int?>(),
                            WarrantyType = productData["warrantyType"]?.Value<string>(),
                            ResinType = productData["resinType"]?.Value<string>(),
                            SolidsByVolume = productData["solidsByVolume"]?.Value<double?>(),
                            SolidsByWeight = productData["solidsByWeight"]?.Value<double?>(),
                            ViscosityMin = productData["viscosityMin"]?.Value<int?>(),
                            ViscosityMax = productData["viscosityMax"]?.Value<int?>(),
                            ViscosityUnit = productData["viscosityUnit"]?.Value<string>(),
                            RecommendedDFT = productData["recommendedDFT"]?.Value<int?>(),
                            RecommendedWFT = productData["recommendedWFT"]?.Value<int?>(),
                            FilmThicknessUnit = productData["filmThicknessUnit"]?.Value<string>(),
                            StorageTempMinF = productData["storageTempMinF"]?.Value<int?>(),
                            StorageTempMaxF = productData["storageTempMaxF"]?.Value<int?>(),
                            ShelfLifeMonths = productData["shelfLifeMonths"]?.Value<int?>(),
                            ProductSummary = productData["productSummary"]?.Value<string>(),
                            ApplicationPrep = productData["applicationPrep"]?.Value<string>(),
                            SafetyHandling = productData["safetyHandling"]?.Value<string>(),
                            Disclaimer = productData["disclaimer"]?.Value<string>()
                        };

                        response.Values.Add(new ResponseRecord
                        {
                            RecordId = record.RecordId,
                            Data = extracted,
                            Errors = null,
                            Warnings = null
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error processing record {record.RecordId}: {ex.Message}");
                        response.Values.Add(new ResponseRecord
                        {
                            RecordId = record.RecordId,
                            Data = new ExtractedProductFields(),
                            Errors = new List<ErrorWarning> { new() { Message = $"Processing error: {ex.Message}" } },
                            Warnings = null
                        });
                    }
                }

                var successResponse = req.CreateResponse(HttpStatusCode.OK);
                successResponse.Headers.Add("Content-Type", "application/json");
                await successResponse.WriteStringAsync(JsonConvert.SerializeObject(response));
                return successResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Function error: {ex.Message}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync("Internal server error");
                return errorResponse;
            }
        }

        private async Task<string> CallOpenAI(string documentContent)
        {
            var url = $"{OpenAIEndpoint}/openai/deployments/{DeploymentId}/chat/completions?api-version=2024-08-01-preview";

            var payload = new
            {
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = "You are a data extraction assistant. Extract structured product data from paint data sheets and return ONLY valid JSON. No markdown, no code fences. Use null for missing values."
                    },
                    new
                    {
                        role = "user",
                        content = $"Extract product data from this text and return ONLY a JSON object with these fields: sku, upc, productName, brand, finish, sheen, base, color, intendedUse, vocValue (number only), vocUnit, coverageMin (number), coverageMax (number), coverageUnit, dryTimeTouchMinutes (number - dry to touch time in minutes), dryTimeRecoatMinutes (number - recoat time in minutes), dryTimeCureDays (number - full cure time in days), recommendedCoats (number), cleanup, warrantyYears (number), warrantyType, resinType, solidsByVolume (number - percent solids by volume), solidsByWeight (number - percent solids by weight), viscosityMin (number - minimum viscosity), viscosityMax (number - maximum viscosity), viscosityUnit (e.g. KU or cP), recommendedDFT (number - dry film thickness), recommendedWFT (number - wet film thickness), filmThicknessUnit (e.g. mils or microns), storageTempMinF (number - min storage temp in Fahrenheit), storageTempMaxF (number - max storage temp in Fahrenheit), shelfLifeMonths (number), productSummary, applicationPrep (surface preparation instructions), safetyHandling (safety and handling instructions), disclaimer (warranty or legal disclaimers).\n\nText:\n{documentContent}"
                    }
                },
                temperature = 0.0,
                max_tokens = 3000
            };

            // Log the payload being sent to OpenAI (without sensitive info)
            var json = JsonConvert.SerializeObject(payload);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("api-key", ApiKey);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var httpResponse = await _httpClient.SendAsync(request);
            var responseBody = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.LogError($"OpenAI API error: {httpResponse.StatusCode} - {responseBody}");
                throw new Exception($"OpenAI API returned {httpResponse.StatusCode}");
            }
            // Log the raw response from OpenAI
            var result = JObject.Parse(responseBody);
            // Extract the content from the response
            var completionText = result["choices"]?[0]?["message"]?["content"]?.Value<string>() ?? "{}";

            // Strip markdown code fences if present
            completionText = completionText.Trim();
            if (completionText.StartsWith("```"))
            {
                var firstNewline = completionText.IndexOf('\n');
                if (firstNewline > 0) completionText = completionText[(firstNewline + 1)..];
                if (completionText.EndsWith("```")) completionText = completionText[..^3];
                completionText = completionText.Trim();
            }

            return completionText;
        }

        // --- Models ---
        private class RequestBody
        {
            [JsonProperty("values")] public List<RequestRecord> Values { get; set; } = new();
        }

        private class RequestRecord
        {
            [JsonProperty("recordId")] public string RecordId { get; set; } = "";
            [JsonProperty("data")] public RecordData Data { get; set; } = new();
        }

        private class RecordData
        {
            [JsonProperty("content")] public string Content { get; set; } = "";
        }

        private class ResponseBody
        {
            [JsonProperty("values")] public List<ResponseRecord> Values { get; set; } = new();
        }

        private class ResponseRecord
        {
            [JsonProperty("recordId")] public string RecordId { get; set; } = "";
            [JsonProperty("data")] public ExtractedProductFields Data { get; set; } = new();
            [JsonProperty("errors")] public List<ErrorWarning>? Errors { get; set; }
            [JsonProperty("warnings")] public List<ErrorWarning>? Warnings { get; set; }
        }

        private class ErrorWarning
        {
            [JsonProperty("message")] public string Message { get; set; } = "";
        }

        private class ExtractedProductFields
        {
            [JsonProperty("sku")] public string? Sku { get; set; }
            [JsonProperty("upc")] public string? Upc { get; set; }
            [JsonProperty("productName")] public string? ProductName { get; set; }
            [JsonProperty("brand")] public string? Brand { get; set; }
            [JsonProperty("finish")] public string? Finish { get; set; }
            [JsonProperty("sheen")] public string? Sheen { get; set; }
            [JsonProperty("base")] public string? Base { get; set; }
            [JsonProperty("color")] public string? Color { get; set; }
            [JsonProperty("intendedUse")] public string? IntendedUse { get; set; }
            [JsonProperty("vocValue")] public double? VocValue { get; set; }
            [JsonProperty("vocUnit")] public string? VocUnit { get; set; }
            [JsonProperty("coverageMin")] public int? CoverageMin { get; set; }
            [JsonProperty("coverageMax")] public int? CoverageMax { get; set; }
            [JsonProperty("coverageUnit")] public string? CoverageUnit { get; set; }
            [JsonProperty("dryTimeTouchMinutes")] public int? DryTimeTouchMinutes { get; set; }
            [JsonProperty("dryTimeRecoatMinutes")] public int? DryTimeRecoatMinutes { get; set; }
            [JsonProperty("dryTimeCureDays")] public int? DryTimeCureDays { get; set; }
            [JsonProperty("recommendedCoats")] public int? RecommendedCoats { get; set; }
            [JsonProperty("cleanup")] public string? Cleanup { get; set; }
            [JsonProperty("warrantyYears")] public int? WarrantyYears { get; set; }
            [JsonProperty("warrantyType")] public string? WarrantyType { get; set; }
            [JsonProperty("resinType")] public string? ResinType { get; set; }
            [JsonProperty("solidsByVolume")] public double? SolidsByVolume { get; set; }
            [JsonProperty("solidsByWeight")] public double? SolidsByWeight { get; set; }
            [JsonProperty("viscosityMin")] public int? ViscosityMin { get; set; }
            [JsonProperty("viscosityMax")] public int? ViscosityMax { get; set; }
            [JsonProperty("viscosityUnit")] public string? ViscosityUnit { get; set; }
            [JsonProperty("recommendedDFT")] public int? RecommendedDFT { get; set; }
            [JsonProperty("recommendedWFT")] public int? RecommendedWFT { get; set; }
            [JsonProperty("filmThicknessUnit")] public string? FilmThicknessUnit { get; set; }
            [JsonProperty("storageTempMinF")] public int? StorageTempMinF { get; set; }
            [JsonProperty("storageTempMaxF")] public int? StorageTempMaxF { get; set; }
            [JsonProperty("shelfLifeMonths")] public int? ShelfLifeMonths { get; set; }
            [JsonProperty("productSummary")] public string? ProductSummary { get; set; }
            [JsonProperty("applicationPrep")] public string? ApplicationPrep { get; set; }
            [JsonProperty("safetyHandling")] public string? SafetyHandling { get; set; }
            [JsonProperty("disclaimer")] public string? Disclaimer { get; set; }
        }
    }
}
