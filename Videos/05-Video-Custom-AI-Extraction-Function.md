# Video 5: Custom AI Extraction — Deploy the GPT-4.1 Function

**Duration Target:** 10 minutes
**Primary Screen:** VS Code + Azure Portal
**Goal:** Viewer deploys the extraction function to Azure, updates the skillset + indexer with 37 output field mappings, re-runs the indexer, and sees all product fields populated.

---

## TRANSCRIPT + SCREEN DIRECTIONS

---

### [0:00–0:30] Hook — The Before/After Reveal

**SCREEN:** Split-screen (pre-recorded):
- Left: Search Explorer showing `sku: null, color: null, vocValue: null` (from video 4)
- Right: Search Explorer showing `sku: "CTSO-PAINT-EXT-SAT-1G-DB", color: "Deep Blue", vocValue: 45`

**NARRATION:**
> "In video 4, our index had 37 empty product fields. By the end of this video, every single one will be populated — extracted directly from PDF text by GPT-4.1. This is the core of the whole project. Let's do it."

---

### [0:30–1:30] Architecture Recap — Where the Custom Skill Fits

**SCREEN:** Architecture diagram — zoom into the skillset pipeline.

**NARRATION:**
> "Here's what happens during indexing. The indexer reads each PDF from blob storage, extracts the text, and runs a skillset pipeline. In video 4, we had one skill — key phrase extraction. Now we add a second skill: a Custom WebApiSkill."

> "This skill sends the raw PDF text to our Azure Function. The function calls GPT-4.1 with a prompt that says 'extract these 37 fields as JSON.' GPT returns the structured data, the function parses it, and the indexer maps each field into the search index. One skill, 37 fields, fully automated."

```
PDF Text → Azure Function → GPT-4.1 → JSON → 37 Index Fields
```

**TIP:** Use an animated arrow or highlight overlay. Spend max 60 seconds here — viewers saw the full architecture in video 1.

---

### [1:30–3:00] Deploy the Function to Azure

**SCREEN:** Azure Portal (quickly show the pre-created Function App).

**NARRATION:**
> "First we need our function running in Azure. I've already created a Function App in the portal — contoso-paints-functions, .NET 8 isolated, Consumption plan, East US 2. Same resource group as everything else."

**SCREEN:** Azure Portal → Function App → contoso-paints-functions → Overview page (show region, runtime, status).

> "If you haven't created yours yet, pause and create it now — the steps are in the description."

**SCREEN:** VS Code terminal — inside `ProductExtractionFunction/` folder.

> "Now let's deploy. In your terminal, navigate to the ProductExtractionFunction folder and run func azure functionapp publish."

**SCREEN:** Terminal:
```powershell
cd ProductExtractionFunction
func azure functionapp publish contoso-paints-functions
```

> "This builds, packages, and deploys the function to Azure. Takes about 30 seconds."

**SCREEN:** Output showing successful deployment — "Deployment completed successfully."

> "Deployed. Now let's grab the function URL with the access key."

**SCREEN:** Terminal:
```powershell
func azure functionapp list-functions contoso-paints-functions --show-keys
```

> "Copy this URL — it includes the function key as a query parameter. This is what Azure AI Search will call during indexing."

**ON-SCREEN TEXT OVERLAY:** Highlight the URL format:
```
https://contoso-paints-functions-xxx.azurewebsites.net/api/extractproductdata?code=CH-PIk...
```

**TIP:** Have the function app pre-created to skip the portal creation walkthrough. Deploy is the interesting part.

---

### [3:00–4:30] Quick Local Test (Prove It Works)

**SCREEN:** PowerShell — second terminal.

**NARRATION:**
> "Before we wire it into search, let's test it with a sample payload. I'll send a fake product description and see if GPT-4.1 extracts the fields correctly."

**SCREEN:** Run test command (have it pre-typed):
```powershell
$testPayload = @{
    values = @(@{
        recordId = "test1"
        data = @{
            content = "SKU: CTSO-PAINT-EXT-SAT-1G-DB. Product: Premium Exterior Satin Paint. Brand: Contoso PaintCo. Color: Deep Blue. VOC < 45 g/L. Coverage: 350-400 sq ft/gallon."
        }
    })
} | ConvertTo-Json -Depth 4

Invoke-RestMethod -Uri "https://contoso-paints-functions-xxx.azurewebsites.net/api/extractproductdata?code=YOUR_KEY" `
    -Method POST -Body $testPayload -ContentType "application/json" |
    ConvertTo-Json -Depth 5
```

**SCREEN:** Response JSON showing extracted fields:
```json
{
  "values": [{
    "recordId": "test1",
    "data": {
      "sku": "CTSO-PAINT-EXT-SAT-1G-DB",
      "productName": "Premium Exterior Satin Paint",
      "brand": "Contoso PaintCo",
      "color": "Deep Blue",
      "vocValue": 45,
      "vocUnit": "g/L",
      "coverageMin": 350,
      "coverageMax": 400
    }
  }]
}
```

> "Look at that — GPT-4.1 extracted every field from raw text into structured JSON. SKU, product name, brand, color, VOC as a number with its unit, coverage as min/max integers. This is exactly what Azure AI Search Custom Skill format expects."

**TIP:** Pre-run this test so you can show the result instantly. If testing live, cut any loading time in editing.

---

### [4:30–6:00] Update the Skillset — Add the WebApiSkill

**SCREEN:** VS Code — open `index-files/skillset-with-extraction.json`.

**NARRATION:**
> "Now we update the skillset to include our custom function. Open skillset-with-extraction.json — this has both skills."

**SCREEN:** Scroll to the WebApiSkill block. Highlight key parts:

> "Skill 1 is still key phrase extraction — same as before. Skill 2 is new — a Custom WebApiSkill. It points to our deployed function URL."

**(Highlight the URI)**

> "The URI is our function URL with the access key. Timeout is 60 seconds — GPT-4.1 typically responds in 5 to 15 seconds. Batch size is 1 — one document at a time, since each needs its own OpenAI call."

**(Scroll to inputs/outputs)**

> "Input: content — the raw PDF text. Outputs: 37 individual fields. Each one gets a target name prefixed with 'extracted_' — extracted_sku, extracted_vocValue, extracted_dryTimeTouchMinutes, and so on. These intermediate names are what the indexer uses to map into the final index fields."

**SCREEN:** Azure Portal → Search service → Skillsets → click `contoso-paints-skillset` → Edit (JSON editor).

**NARRATION:**
> "Now let's update the skillset in the portal. Go to Skillsets, click our existing contoso-paints-skillset, and click Edit to open the JSON editor."

> "We'll replace the current skillset definition with the enhanced version that includes both skills. Paste in the updated JSON:"

**SCREEN:** Show the updated skillset JSON in the portal editor — highlight the new WebApiSkill block being added after the existing KeyPhraseExtractionSkill. Also update the name to `contoso-paints-skillset-enhanced`.

> "The key addition is the WebApiSkill block — it points to our deployed function URL, with a 60-second timeout and batch size of 1. The 37 outputs map each extracted field to an intermediate name."

**SCREEN:** Click Save.

> "Skillset updated."

---

### [6:00–7:30] Update the Indexer — 37 Output Field Mappings

**SCREEN:** VS Code — open `index-files/indexer-with-extraction.json`. Scroll through the outputFieldMappings.

**NARRATION:**
> "The indexer needs 37 new output field mappings — one for each extracted field. Each mapping says: take the skillset output '/document/extracted_sku' and write it to the index field 'sku'."

**SCREEN:** Slowly scroll the outputFieldMappings array. Don't read every line — let the visual do the work.

> "37 mappings. SKU, UPC, product name, brand, all the way down to disclaimer. Plus the original keyphrase mapping. I won't read them all — the pattern is the same. Source is the extracted intermediate field, target is the index field."

**SCREEN:** Azure Portal → Search service → Indexers → click `contoso-paints-indexer` → Edit (JSON editor).

**NARRATION:**
> "Now let's update the indexer in the portal. Go to Indexers, click our existing contoso-paints-indexer, and open the JSON editor."

> "We update three things: point the skillsetName to 'contoso-paints-skillset-enhanced', and add the 37 new output field mappings. Each mapping takes a skillset output like '/document/extracted_sku' and writes it to the corresponding index field 'sku'. Paste in the updated indexer JSON with all the output field mappings."

**SCREEN:** Show the updated indexer JSON in the portal editor — highlight the new outputFieldMappings array with 37 entries plus the original keyphrase mapping.

> "Click Save."

**SCREEN:** Click Save.

> "Indexer updated. Now let's run it."

---

### [7:30–8:30] Run the Indexer & Watch It Process

**SCREEN:** Azure Portal → Search service → Indexers → contoso-paints-indexer → Run.

**NARRATION:**
> "Click Run. The indexer now processes all 10 PDFs. For each one, it extracts the text, calls key phrase extraction, AND calls our GPT-4.1 function. Each document takes 5 to 15 seconds for the OpenAI call."

**SCREEN:** Click Refresh every few seconds. Show the progress — documents processed count increasing.

> "1 of 10... 3 of 10... 7 of 10..."

**(Speed this up to 4x in editing with background music.)**

> "All 10 documents processed. Status: Success. Let's see the results."

**TIP:** Pre-run the indexer so results are ready. Or speed up the wait at 4x. Don't show 2 minutes of clicking Refresh.

---

### [8:30–9:30] The Big Reveal — All Fields Populated

**SCREEN:** Search Explorer — run the verification query:

```
search=*&$select=sku,productName,brand,color,vocValue,finish,sheen,warrantyYears,dryTimeTouchMinutes,solidsByVolume&$count=true
```

**NARRATION:**
> "Moment of truth. Search Explorer, same query as video 4. And look..."

**SCREEN:** Results appear — all fields populated with real values.

> "SKU: CTSO-PAINT-EXT-SAT-1G-DB. Product Name: Premium Exterior Paint Satin Finish. Brand: Contoso PaintCo. Color: Deep Blue. VOC Value: 45. Warranty: 25 years. Dry time touch: 60 minutes. Solids by volume: 42.5 percent."

> "Every single field is populated. Across all 10 products. GPT-4.1 read the raw PDF text and extracted 37 structured fields per document — automatically, during indexing."

**SCREEN:** Run a filter query to show it's queryable:
```
search=exterior&$filter=vocValue le 50&$select=productName,sku,vocValue&$orderby=vocValue asc
```

> "And we can filter and sort by these fields. Here — all exterior products with VOC under 50, sorted by VOC ascending. This is structured search over unstructured PDF data."

**TIP:** This is the climax of the series. Linger on the results. Zoom in. Let the viewer see every field. 30 seconds of pure payoff.

---

### [9:30–10:00] Wrap-Up & Next Video Teaser

**SCREEN:** Architecture diagram — highlight the final piece: AI Foundry Chat.

**NARRATION:**
> "37 product fields, 10 documents, all extracted by GPT-4.1, all searchable. One video left — we connect this index to Azure AI Foundry's Chat Playground, add a system prompt, and build a RAG agent that answers natural language product questions. See you in the finale — video 6."

**SCREEN:** End card.

---

## CHAPTER MARKERS

```
0:00 - Before/after preview
0:30 - Architecture: where the custom skill fits
1:30 - Deploy function to Azure
3:00 - Test function with sample payload
4:30 - Update skillset with WebApiSkill
6:00 - Update indexer with 37 field mappings
7:30 - Run the indexer
8:30 - The big reveal — all fields populated
9:30 - Next video teaser
```

## SCREEN CHECKLIST

- [ ] Split-screen before/after comparison (pre-recorded)
- [ ] Architecture diagram zoomed into skillset pipeline
- [ ] Azure Portal — Function App overview page
- [ ] VS Code terminal in ProductExtractionFunction folder
- [ ] Successful `func azure functionapp publish` output
- [ ] Function URL with key copied
- [ ] Test payload and response in PowerShell
- [ ] `skillset-with-extraction.json` open in VS Code (WebApiSkill highlighted)
- [ ] `indexer-with-extraction.json` open (outputFieldMappings scrolling)
- [ ] Search Explorer — all fields populated (THE reveal moment)
- [ ] Filter query results proving structured search works
