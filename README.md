# Contoso PaintCo AI Search

An end-to-end **AI-powered document intelligence pipeline** that automatically extracts structured product data from unstructured PDF technical data sheets using Azure AI Search, Azure Functions, and Azure OpenAI (GPT-4.1).

![Architecture](Architecture/Contoso-AI-Paint.jpeg)

## Skills Demonstrated

- **Azure AI Search** — indexers, skillsets, custom skills, field mappings, index schema design
- **Azure OpenAI** — prompt engineering for structured data extraction, GPT-4.1 integration
- **Azure Functions** — .NET 8 isolated worker, Flex Consumption plan, HTTP triggers, custom skill contract
- **Document Intelligence** — PDF text extraction, unstructured-to-structured data transformation
- **Cloud Architecture** — serverless design, event-driven pipelines, Azure service integration
- **C# / .NET 8** — async/await, HTTP clients, JSON serialization, dependency injection
- 
## The Problem

Paint manufacturers produce hundreds of product technical data sheets (TDS) as PDFs. These documents contain critical information — SKUs, VOC values, coverage rates, dry times, safety data — buried in unstructured text. Manually extracting this data for searchable catalogs is slow, error-prone, and doesn't scale.

## The Solution

This project builds an **automated AI enrichment pipeline** that:

1. **Ingests** PDF data sheets from Azure Blob Storage
2. **Extracts text** and metadata via Azure AI Search's built-in document cracking
3. **Enriches** documents using a two-stage AI skillset:
   - **Key Phrase Extraction** — identifies important terms using a built-in cognitive skill
   - **Custom Web API Skill** — calls an Azure Function that uses GPT-4.1 to extract **37 structured fields** from raw PDF text
4. **Indexes** all extracted fields into a rich, searchable index with filtering, faceting, and sorting

The result is a fully searchable product catalog where users can query by finish type, filter by VOC compliance, sort by coverage, and find products using natural language — all sourced automatically from raw PDFs.

## Architecture

| Component | Service | Purpose |
|---|---|---|
| **Document Storage** | Azure Blob Storage | Stores product PDF data sheets |
| **Search & Orchestration** | Azure AI Search | Indexer pipeline, skillsets, and search index |
| **AI Enrichment** | Azure Functions (.NET 8, Flex Consumption) | Custom Web API skill for structured extraction |
| **Language Model** | Azure OpenAI (GPT-4.1) | Extracts 37 product fields from unstructured text |
| **AI Platform** | Azure AI Foundry | Model management and deployment |

## Extracted Fields

The custom skill extracts a comprehensive set of product attributes from each PDF:

| Category | Fields |
|---|---|
| **Identity** | SKU, UPC, Product Name, Brand |
| **Product Characteristics** | Finish, Sheen, Base, Color, Intended Use, Resin Type |
| **Performance** | Coverage (min/max), Recommended Coats, VOC Value/Unit |
| **Dry Times** | Touch Dry (minutes), Recoat (minutes), Full Cure (days) |
| **Technical Specs** | Solids by Volume/Weight, Viscosity (min/max/unit), DFT, WFT, Film Thickness Unit |
| **Storage** | Min/Max Storage Temp (°F), Shelf Life (months) |
| **Warranty** | Warranty Years, Warranty Type |
| **Descriptive** | Product Summary, Application Prep, Safety/Handling, Disclaimer, Cleanup |

## Project Structure

```
├── Architecture/                    # Architecture diagram
├── Data/files/                      # Sample product PDF data sheets (10 products)
├── index-files/                     # Azure AI Search resource definitions
│   ├── datasource.json              # Blob storage data source configuration
│   ├── index.json                   # Search index schema (52 fields)
│   ├── skillset.json                # Base skillset definition
│   ├── skillset-extraction.json     # Enhanced skillset with custom AI extraction
│   ├── indexer.json                 # Standard indexer
│   └── indexer-with-extraction.json # Indexer with output field mappings for all 37 extracted fields
├── ProductExtractionFunction/       # Azure Function (.NET 8 Isolated Worker)
│   ├── ParseProductJson.cs          # Core function — OpenAI integration + field extraction
│   ├── Program.cs                   # Function app host configuration
│   └── host.json                    # Function runtime settings
└── contoso-ai-paints.sln            # Solution file
```

## How It Works

```
PDF Upload → Blob Storage → AI Search Indexer triggers
                                    ↓
                        Document Cracking (text + metadata)
                                    ↓
                    ┌───────────────┴───────────────┐
                    ↓                               ↓
          Key Phrase Extraction          Custom Web API Skill
          (built-in cognitive)           (Azure Function → GPT-4.1)
                    ↓                               ↓
                    └───────────────┬───────────────┘
                                    ↓
                        Search Index (52 fields)
                                    ↓
                    Full-text search, filtering, faceting
```

## Key Technical Decisions

- **Azure Functions Flex Consumption** — auto-scales to zero, pay-per-execution, ideal for batch indexing workloads
- **.NET 8 Isolated Worker** — modern hosting model with full dependency injection support
- **GPT-4.1 with temperature 0.0** — deterministic extraction for consistent structured output
- **Batch size 1, degree of parallelism 1** — deliberate throttling to respect OpenAI rate limits during indexing
- **Custom Web API Skill pattern** — follows the Azure AI Search custom skill interface contract (`values[]` request/response format)
- **Environment variables for secrets** — API keys loaded from app settings, not hardcoded

## Getting Started

### Prerequisites

- Azure Subscription with:
  - Azure AI Search service
  - Azure Blob Storage account
  - Azure OpenAI resource with a GPT-4.1 deployment
  - Azure Function App (.NET 8, Flex Consumption)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Azure Functions Core Tools v4](https://learn.microsoft.com/azure/azure-functions/functions-run-local)

### Setup

1. **Deploy the Azure Function:**
   ```bash
   cd ProductExtractionFunction
   func azure functionapp publish <YOUR_FUNCTION_APP_NAME>
   ```

2. **Configure environment variables** on your Function App:
   | Setting | Value |
   |---|---|
   | `AZURE_OPENAI_ENDPOINT` | Your Azure OpenAI endpoint URL |
   | `AZURE_OPENAI_DEPLOYMENT` | Your GPT model deployment name |
   | `AZURE_OPENAI_API_KEY` | Your Azure OpenAI API key |

3. **Upload PDFs** to your Blob Storage `documents` container

4. **Create Azure AI Search resources** using the JSON definitions in `index-files/`:
   - Create the data source (`datasource.json`) — update the connection string
   - Create the index (`index.json`)
   - Create the skillset (`skillset-extraction.json`) — update the function URL
   - Create the indexer (`indexer-with-extraction.json`)

5. **Run the indexer** — documents will be processed automatically



## License

This is a personal portfolio project by [Dhaval Shah](https://github.com/dhavalshah01).
