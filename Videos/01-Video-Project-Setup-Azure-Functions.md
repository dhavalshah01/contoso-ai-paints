# Video 1: Project Setup & Azure Functions (.NET 8)

**Duration Target:** 10 minutes
**Primary Screen:** VS Code + Terminal
**Goal:** Viewer understands the end-to-end architecture and has the ProductExtractionFunction project created.

---

## TRANSCRIPT + SCREEN DIRECTIONS

---

### [0:00–0:30] HOOK — Show the End Result First

**SCREEN:** Quick montage (pre-recorded clips):
1. (2 sec) Azure AI Foundry Chat — user types "Which exterior paint has the lowest VOC?" → answer appears
2. (2 sec) Search Explorer — 10 documents with 37 populated fields
3. (2 sec) VS Code — ExtractProductData function code scrolling

**NARRATION:**
> "By the end of this 6-part series, you'll have a fully working AI agent that answers natural language questions about product data — pulled straight from PDF files. We'll use Azure Functions, Azure OpenAI with GPT-4.1, Azure AI Search, and Azure AI Foundry. No ML expertise needed. Let's start with the project setup."

---

### [0:30–2:30] Architecture Overview

**SCREEN:** Show the architecture diagram as a full-screen image (use the Mermaid diagram from Step 01, pre-rendered as PNG).

**NARRATION:**
> "Here's the complete architecture. I want to walk you through the data flow — it's simpler than it looks."
>
> (Point/highlight each block as you mention it)
>
> "We start with 10 PDF product data sheets — these are paint technical data sheets from our fictional company, Contoso PaintCo. We upload them to Azure Blob Storage."
>
> "Azure AI Search picks them up with an indexer. The indexer runs a skillset — a pipeline of AI skills. Skill one extracts key phrases using a built-in AI skill. Skill two is our custom Azure Function that sends the raw PDF text to GPT-4.1 and gets back 37 structured fields — SKU, color, VOC value, dry times, warranty, everything."
>
> "All of this lands in a search index with 52 fields total. Then we connect that index to Azure AI Foundry's Chat Playground as a grounding data source. The result: a RAG agent — Retrieval-Augmented Generation — that answers questions using real product data, not hallucinated information."

**SCREEN:** Show the 6-video roadmap as a quick overlay/slide:

> "Here's how the series breaks down. Video 1 — that's this one — project setup. Video 2, Blob Storage. Video 3, Azure OpenAI. Video 4, Azure AI Search index and skillset. Video 5, the custom GPT-4.1 extraction function. Video 6, wiring it all into a chat agent. Each video is under 10 minutes."

**TIP:** Point at each component. Use animated arrows if possible. Spend max 2 minutes here.

---

### [2:30–3:00] Prerequisites Checklist (Quick)

**SCREEN:** VS Code terminal, run these commands:

```
dotnet --version
func --version
```

**NARRATION:**
> "Before we start, make sure you have .NET 8 SDK and Azure Functions Core Tools v4 installed. I'll flash the download links on screen. If you see version numbers here, you're good to go."

**ON-SCREEN TEXT OVERLAY:** Links to dotnet.microsoft.com/download and npm install command for func tools.

**TIP:** Don't walk through the installation. Just show the version check. Link in description.

---

### [3:00–5:30] Create the Product Extraction Function Project

**SCREEN:** Terminal in VS Code

**NARRATION:**
> "The repo also includes a basic HTTP trigger project for health checks — you can ignore it. The function that matters is ProductExtractionFunction. This is the one that calls GPT-4.1 to extract structured product data from PDF text."

**SCREEN:** Terminal — run commands:

```
mkdir ProductExtractionFunction
cd ProductExtractionFunction
func init --worker-runtime dotnet-isolated --target-framework net8.0
func new --name ExtractProductData --template "HTTP trigger" --authlevel "function"
```

**NARRATION:**
> "We create a separate directory, initialize a new isolated .NET 8 function project, and add an HTTP trigger with function-level auth. Function-level auth means callers need a key — important since Azure AI Search will call this function during indexing."

**SCREEN:** Show folder structure of ProductExtractionFunction.

> "Standard isolated function project. Let me set the port so it doesn't conflict during local development."

**SCREEN:** Open `ProductExtractionFunction/Properties/launchSettings.json` — highlight port 7057.

> "Port 7057 for this one, so both functions can run simultaneously during local development."

**SCREEN:** Terminal — run:

```
dotnet add package Newtonsoft.Json
```

> "We need Newtonsoft.Json for JSON serialization — it's what the Azure AI Search custom skill format expects."

---

### [5:30–8:00] Walkthrough: The Extraction Function Code

**SCREEN:** Open `ParseProductJson.cs` — scroll slowly through the file.

**NARRATION:**
> "I won't write this code live — that would take 20 minutes. Instead, let me walk you through the four key parts. The full source code is in the GitHub repo linked in the description."

**(Part 1 — Scroll to the function signature)**

> "Part one: the entry point. The function accepts POST requests in the Azure AI Search custom skill format — a JSON body with a 'values' array. Each value has a recordId and a data object containing the raw PDF text as a string. This format is defined by Azure AI Search for custom skills — we must match it exactly."

**(Part 2 — Scroll to CallOpenAI method, highlight the prompt)**

> "Part two: the OpenAI call. For each record, we send the PDF text to GPT-4.1 via the Azure OpenAI REST API. The key is the prompt — look at this."
>
> (Zoom into the user prompt string)
>
> "It says: extract product data from this text and return a JSON object with these 37 fields. Then it lists every field with its type and a description — viscosityMin as a number, vocUnit as a string like 'g/L', dryTimeTouchMinutes in minutes. We set temperature to 0.0 for deterministic output — we want the same extraction every time."

**(Part 3 — Scroll to JSON parsing / code fence stripping)**

> "Part three: response parsing. GPT sometimes wraps JSON in markdown code fences, so we strip those. Then we parse the raw JSON string into a C# object."

**(Part 4 — Scroll to ExtractedProductFields class)**

> "Part four: the data model. 37 strongly typed fields — strings for text, doubles for decimals like VOC and solids percentage, ints for whole numbers like dry time minutes and warranty years. Each field maps to a column in the search index. When the indexer calls this function, it gets back structured data ready to index."

**TIP:** Use code folding to collapse method bodies and expand one at a time. Highlight the prompt text and the field list — those are the most interesting parts.

---

### [8:00–9:00] Build Verification

**SCREEN:** Terminal in VS Code — inside `ProductExtractionFunction/` folder.

**NARRATION:**
> "Let's verify it builds. Navigate to the ProductExtractionFunction folder and run dotnet build."

**SCREEN:** Run `dotnet build` — show green "Build succeeded" output.

> "Build succeeded — zero errors, zero warnings. We won't run it yet because it needs Azure OpenAI credentials. We set that up in video 3."

> "Quick note on something you'll see in the code — the API key and endpoint are hardcoded as constants at the top of the file. For a POC, that's fine. In production, you'd use Azure Key Vault or app settings. Don't commit API keys to a public repo."

---

### [9:00–10:00] Wrap-Up & Next Video Teaser

**SCREEN:** Architecture diagram again — highlight Blob Storage block.

**NARRATION:**
> "That's it for project setup. We have the ProductExtractionFunction ready — it takes raw PDF text, sends it to GPT-4.1, and returns 37 structured fields. In the next video, we upload our 10 product PDF files to Azure Blob Storage — the data source for everything else. See you there."

**SCREEN:** End card with "Video 2: Azure Blob Storage & PDF Upload" title + subscribe button.

---

## CHAPTER MARKERS (for YouTube)

```
0:00 - End result preview
0:30 - Architecture & series roadmap
2:30 - Prerequisites check
3:00 - Create extraction function project
5:30 - Code walkthrough: extraction flow
8:00 - Build verification
9:00 - Next video teaser
```

## SCREEN CHECKLIST

- [ ] Architecture diagram (pre-rendered PNG)
- [ ] 6-video series roadmap slide/overlay
- [ ] VS Code with dark theme, font size 16+
- [ ] Terminal with clear history
- [ ] ProductExtractionFunction folder in VS Code Explorer
- [ ] ParseProductJson.cs open and code-folded for 4-part walkthrough
- [ ] Zoom-ready on the GPT prompt text and ExtractedProductFields class
