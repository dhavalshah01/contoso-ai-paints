# Video 4: Azure AI Search — Index, Skillset & Indexer

**Duration Target:** 10 minutes
**Primary Screen:** Azure Portal
**Goal:** Viewer has a search service with a 52-field index, blob data source, keyphrase skillset, and indexer — documents indexed with metadata + key phrases (product fields still NULL).

---

## TRANSCRIPT + SCREEN DIRECTIONS

---

### [0:00–0:20] Recap & Goal

**SCREEN:** Architecture diagram — Azure AI Search block highlighted.

**NARRATION:**
> "We've got our function projects, PDFs in blob storage, and GPT-4.1 deployed. Now we connect the dots. In this video we create Azure AI Search — the index, data source, skillset, and indexer — all from the Azure Portal. By the end, our 10 PDF files will be indexed with full-text search plus AI-extracted key phrases. Let's go."

---

### [0:20–2:00] Create the Azure AI Search Service

**SCREEN:** Azure Portal → Create a resource → search "Azure AI Search" → Create.

**NARRATION:**
> "In the portal, Create a resource, search for Azure AI Search, hit Create."

**SCREEN:** Basics tab form.

**ACTIONS (narrate as you fill):**
> "Resource group: rg-contoso-paints. Service name: contoso-paint-search — globally unique, lowercase. Location: East US 2. Pricing tier: Basic — 75 dollars a month, 15 indexes, 2 gigs of storage. Plenty for a POC."

**SCREEN:** Show filled Basics tab.

> "Review + Create. Create."

**SCREEN:** Deployment screen. (Cut the wait — jump to "Go to resource".)

> "Deployed."

**TIP:** Pre-create the resource. Say "I've already created this — let me navigate to it." Saves 30-60 seconds of wait time.

---

### [2:00–2:30] Navigate to the Search Service

**SCREEN:** Search service → Overview page.

**NARRATION:**
> "Here's our search service. From the left menu we'll create everything we need — the index, data source, skillset, and indexer — all right here in the portal. No scripts, no REST API calls."

---

### [2:30–4:30] Create the Index (52 Fields)

**SCREEN:** Azure Portal → Search service → Indexes → + Add index.

**NARRATION:**
> "First, the index. Go to Indexes in the left menu, then click 'Add index (JSON)'. The portal lets you paste a full index definition as JSON — much faster than adding 52 fields one by one."

**SCREEN:** Index creation — JSON editor view in the portal.

> "We have an index.json file pre-built in our project under the index-files folder. Open it in VS Code, select all, and copy the entire contents."

**SCREEN:** VS Code — open `index-files/index.json`, briefly scroll to show the structure, then Ctrl+A → Ctrl+C.

> "The index defines the schema — every field, its type, and whether it's searchable, filterable, sortable, or facetable. 52 fields total."

**SCREEN:** Switch back to Azure Portal — paste the JSON into the index editor.

> "Paste it into the portal's JSON editor. Let me quickly walk through what's in here."

> "The key field is 'id' — auto-generated from the blob path, base64 encoded. Then we have metadata fields for blob storage — file name, path, size, modified date, content type. These get auto-populated by the indexer."

> "Here's where it gets interesting — 37 product-specific fields. SKU, UPC, product name, brand, finish, sheen, color, VOC value, coverage min and max, dry times in minutes and days, viscosity, film thickness, storage temps, warranty, product summary, application prep, safety handling, and disclaimer."

> "These are all defined now but they'll stay NULL until video 5 when we add the GPT-4.1 extraction."

> "And finally, content_keywords — a Collection of strings. This gets populated by the keyphrase extraction skill in our skillset. There's also a suggester configured for autocomplete on title, productName, brand, and content_keywords."

**SCREEN:** Click 'Save' to create the index.

> "Click Save. Index is ready — all 52 fields created in one shot."

**TIP:** Having the JSON file pre-built saves significant time compared to adding fields manually. Just copy-paste and go.

---

### [4:30–5:30] Create the Blob Data Source

**SCREEN:** Azure Portal → Search service → Data sources → + Add data source.

**NARRATION:**
> "Next, the data source tells Azure AI Search where to find our documents. Click Data sources in the left menu, then 'Add data source'."

**SCREEN:** Data source creation form.

**ACTIONS (narrate as you fill):**
> "Name: contoso-paints-blob-datasource. Type: Azure Blob Storage. Connection string — click 'Choose an existing connection', select your storage account from video 2. Container name: documents. Leave the rest as defaults."

**SCREEN:** Show the filled form with connection string and container name highlighted.

> "The change detection policy automatically detects modified blobs using the last-modified timestamp. That's it — click Save."

**SCREEN:** Click Save.

> "Data source created. The search service now knows where the PDFs live."

---

### [5:30–6:30] Create the Skillset (Key Phrase Extraction)

**SCREEN:** Azure Portal → Search service → Skillsets → + Add skillset.

**NARRATION:**
> "A skillset is a pipeline of AI skills that run during indexing. Click Skillsets in the left menu, then 'Add skillset'. Right now we'll add one skill — Key Phrase Extraction. This is a built-in cognitive skill that pulls important phrases from the document text."

**SCREEN:** Skillset creation form (JSON editor in the portal).

> "Name: contoso-paints-skillset. The portal gives you a JSON editor for the skillset definition. Add a KeyPhraseExtractionSkill:"

**SCREEN:** Show the skillset JSON in the portal editor:
```json
{
  "name": "contoso-paints-skillset",
  "description": "Skillset for extracting key phrases from PDF content",
  "skills": [
    {
      "@odata.type": "#Microsoft.Skills.Text.KeyPhraseExtractionSkill",
      "name": "extract-keyphrases",
      "description": "Extract key phrases from PDF content for enhanced searchability",
      "context": "/document/content",
      "defaultLanguageCode": "en",
      "maxKeyPhraseCount": 25,
      "inputs": [
        { "name": "text", "source": "/document/content" }
      ],
      "outputs": [
        { "name": "keyPhrases", "targetName": "keyphrases" }
      ]
    }
  ],
  "cognitiveServices": null
}
```

> "Input: the document content — the raw text extracted from the PDF. Output: up to 25 key phrases per document. Things like 'exterior satin finish', 'low VOC', 'acrylic latex resin'. These land in the content_keywords field."

> "In video 5, we'll add a second skill here — the custom WebApiSkill that calls our GPT-4.1 function."

**SCREEN:** Click Save.

> "Skillset created."

**TIP:** Don't explain every JSON property. Focus on input → output. 60 seconds for the whole skillset section.

---

### [6:30–7:30] Create the Indexer (Connect Everything)

**SCREEN:** Azure Portal → Search service → Indexers → + Add indexer.

**NARRATION:**
> "The indexer is the engine that ties it all together. It connects data source to skillset to index, and runs the actual document processing. Click Indexers in the left menu, then 'Add indexer'."

**SCREEN:** Indexer creation form (JSON editor in the portal).

> "The portal gives you a JSON editor. Here's what we configure:"

**SCREEN:** Show the indexer JSON in the portal editor:
```json
{
  "name": "contoso-paints-indexer",
  "dataSourceName": "contoso-paints-blob-datasource",
  "targetIndexName": "contoso-paints-index",
  "skillsetName": "contoso-paints-skillset",
  "parameters": {
    "batchSize": 10,
    "maxFailedItems": 5,
    "maxFailedItemsPerBatch": 5,
    "configuration": {
      "dataToExtract": "contentAndMetadata",
      "parsingMode": "default",
      "indexedFileNameExtensions": ".pdf",
      "failOnUnsupportedContentType": false,
      "failOnUnprocessableDocument": false
    }
  },
  "fieldMappings": [
    { "sourceFieldName": "metadata_storage_path", "targetFieldName": "id", "mappingFunction": { "name": "base64Encode" } },
    { "sourceFieldName": "metadata_storage_name", "targetFieldName": "metadata_storage_name" },
    { "sourceFieldName": "metadata_storage_path", "targetFieldName": "metadata_storage_path" },
    { "sourceFieldName": "metadata_storage_size", "targetFieldName": "metadata_storage_size" },
    { "sourceFieldName": "metadata_storage_last_modified", "targetFieldName": "metadata_storage_last_modified" },
    { "sourceFieldName": "metadata_content_type", "targetFieldName": "metadata_content_type" },
    { "sourceFieldName": "metadata_language", "targetFieldName": "metadata_language" },
    { "sourceFieldName": "metadata_title", "targetFieldName": "title" },
    { "sourceFieldName": "metadata_author", "targetFieldName": "author" },
    { "sourceFieldName": "metadata_creation_date", "targetFieldName": "creationDate" }
  ],
  "outputFieldMappings": [
    { "sourceFieldName": "/document/content/keyphrases", "targetFieldName": "content_keywords" }
  ]
}
```

> "Data source: our blob data source. Target index: contoso-paints-index. Skillset: contoso-paints-skillset. Field mappings map blob metadata to index fields automatically — file name, path, size. The 'id' field uses base64Encode on the storage path. Output field mappings take the skillset outputs — key phrases — and write them to the content_keywords field."

> "Configuration: dataToExtract is 'contentAndMetadata' — we want both the PDF text and file metadata. Parsing mode is 'default' — standard PDF text extraction, no OCR."

**SCREEN:** Click Save. The indexer runs immediately.

> "Click Save. The indexer is created and runs immediately."

---

### [7:30–9:00] Verify in Search Explorer

**SCREEN:** Azure Portal → Search service → Indexes → contoso-paints-index → Search Explorer.

**NARRATION:**
> "Let's check the results. Go to Indexes, click contoso-paints-index, open Search Explorer."

**SCREEN:** JSON view — run query:
```json
{ "search": "*", "count": true }
```

> "10 documents indexed. Perfect — one for each PDF."

**SCREEN:** Run a second query:
```json
{
  "search": "*",
  "select": "metadata_storage_name,content_keywords,title",
  "top": 3
}
```

> "Here we see file names, AI-extracted key phrases — 'exterior satin finish', 'acrylic latex', 'low VOC' — and the title from PDF metadata."

**SCREEN:** Run a third query — full field select on one document:
```json
{ "search": "*", "select": "*", "top": 1 }
```

> "Now look at this — expand the full result. You'll see metadata fields are populated, content_keywords has phrases, the full content text is there. But scroll down to the product fields — SKU, brand, color, VOC value — all null."

**(Pause for emphasis)**

> "This is the gap. The PDF text CONTAINS all this information, but Azure AI Search doesn't know how to extract it from unstructured text. That's exactly what we solve in video 5 with our GPT-4.1 custom skill."

**TIP:** This "null fields" reveal is a great dramatic moment. Zoom in on the null values. Let it land for 3 seconds before narrating.

---

### [9:00–9:30] Quick Cost Note

**SCREEN:** Text overlay.

**NARRATION:**
> "Cost check. The Basic Search tier is 75 dollars a month. The keyphrase extraction skill is free for our volume — Azure includes 20 free transactions per indexer run. For 10 documents, we're well under that limit. Total cost added this video: 75 dollars a month for the search service."

**ON-SCREEN:**
```
Azure AI Search (Basic): $75/month
Keyphrase Extraction:    $0 (under free limit)
```

---

### [9:30–10:00] Wrap-Up & Next Video Teaser

**SCREEN:** Architecture diagram — highlight the custom skill / Azure Function arrow.

**NARRATION:**
> "We now have a complete search pipeline: blob storage, index with 52 fields, keyphrase skillset, and a running indexer. But 37 product fields are still empty. In the next video, we deploy our custom Azure Function, add it as a WebApiSkill in the skillset, and watch GPT-4.1 fill in every single field. That's the magic moment. See you in video 5."

**SCREEN:** End card.

---

## CHAPTER MARKERS

```
0:00 - Recap & goal
0:20 - Create Azure AI Search service
2:00 - Navigate to search service
2:30 - Create index (52 fields walkthrough)
4:30 - Create blob data source
5:30 - Create skillset (key phrase extraction)
6:30 - Create indexer
7:30 - Verify in Search Explorer
9:00 - Cost summary
9:30 - Next video teaser
```

## SCREEN CHECKLIST

- [ ] Architecture diagram with AI Search highlighted
- [ ] Azure Portal pre-logged in
- [ ] Search service created (or ready to create live)
- [ ] Storage account connection string available (from video 2)
- [ ] Reference `index-files/` folder for field definitions if needed
- [ ] Search Explorer showing 10 indexed documents
- [ ] Full document view showing populated metadata + NULL product fields (dramatic reveal)
- [ ] Cost overlay graphic
