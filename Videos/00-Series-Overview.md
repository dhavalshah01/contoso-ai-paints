# Contoso PaintCo AI Search POC — YouTube Series Overview

## Series Title
**"Build an AI-Powered Product Search Agent with Azure AI — From Zero to RAG in 6 Videos"**

## Series Description (YouTube)
Learn how to build a complete Retrieval-Augmented Generation (RAG) agent using Azure AI services. We take 10 paint product PDF data sheets, upload them to Azure, extract structured data with GPT-4.1, index everything in Azure AI Search, and connect it to a chat agent — all in under 60 minutes of video.

**Technologies:** Azure Functions (.NET 8), Azure Blob Storage, Azure OpenAI (GPT-4.1), Azure AI Search, Azure AI Foundry

---

## Video Breakdown

| # | Title | Duration | Primary Screen |
|---|-------|----------|----------------|
| 1 | Project Setup & Azure Functions | ~10 min | VS Code + Terminal |
| 2 | Azure Blob Storage & PDF Upload | ~10 min | Azure Portal |
| 3 | Azure OpenAI — Deploy GPT-4.1 | ~10 min | Azure Portal + AI Studio |
| 4 | Azure AI Search — Index & Skillset | ~10 min | Azure Portal + REST API |
| 5 | Custom AI Extraction Function | ~10 min | VS Code + Azure Portal |
| 6 | RAG Agent in AI Foundry & Live Demo | ~10 min | AI Foundry Chat Playground |

---

## Tips to Keep Videos Under 10 Minutes and Engaging

### Pre-Recording Prep (Critical)
- **Pre-create all Azure resources** before recording. Show the creation screens via quick screenshots or sped-up footage, then switch to the already-created resource for configuration.
- **Pre-paste all JSON/code** into files. Don't type code live — open the file and walk through it.
- **Have all browser tabs pre-opened** (Azure Portal, AI Foundry, etc.) to avoid load times.
- **Pre-upload files** for later videos so you're not waiting on upload progress bars.
- **Use a second monitor** — one for recording, one for reference notes.

### During Recording
- **Cut dead air ruthlessly.** If something takes >5 seconds to load, say "I'll skip ahead" and cut in editing.
- **Use jump cuts** between portal navigation steps. Don't record every click.
- **Zoom into the relevant part of the screen** — don't show the full desktop when only a small panel matters.
- **Use picture-in-picture** (your webcam in corner) only during intro/outro, not during portal demos.
- **Narrate what you're ABOUT to do, then do it.** Don't narrate after the fact.
- **Use on-screen annotations** (arrows, highlights, zoom) to draw attention to specific fields.

### Pacing
- **Hook in first 15 seconds:** Start each video with the end result (e.g., Video 6 shows the chat agent answer first, then rewinds).
- **No filler intros.** Skip "Hey guys, welcome back to another video." Just say: "In this video, we set up Azure OpenAI and deploy GPT-4.1. Let's go."
- **Speed up repetitive actions** (portal navigation, waiting for deployments) at 2-4x speed with background music.
- **Use chapter markers** in YouTube for each major section.
- **End with a 5-second teaser** of what's next: "Next up: we create the AI Search index."

### Visual Production
- **Use VS Code dark theme** (consistent with the developer audience).
- **Font size 16+** in all editors and terminals so text is readable on mobile.
- **1920x1080** recording resolution minimum.
- **Clean desktop** — hide taskbar notifications, close Slack/Teams.
- **Pre-record the architecture diagram** as an image overlay. Don't draw it live.

---

## Thumbnail Ideas
1. Azure logo + magnifying glass + paint can icon → "AI Searches Your Data"
2. Split screen: PDF on left → structured JSON on right → "GPT-4.1 Extracts This"
3. Chat bubble with a paint question + answer → "Your Data, Your Agent"

---

## End Cards / CTAs
- Each video links to the next in the series.
- Pin a comment with the full playlist link + GitHub repo link.
- Final video (Video 6) links to the GitHub repo with all code and step files.

---

## YouTube Titles & Descriptions

🔗 **GitHub Repo:** https://github.com/dhavalshah01/contoso-ai-paints

---

### Video 1: Project Setup & Azure Functions — Build an AI Product Search Agent (Part 1/6)

**Description:**
In this first video, we set up the project from scratch and create the ProductExtractionFunction — an Azure Function (.NET 8 isolated) that will call GPT-4.1 to extract 37 structured fields from raw PDF text. We walk through the architecture, prerequisites, project scaffolding, and a full code walkthrough of the extraction function including the prompt design and data model.

What you'll learn:
- End-to-end RAG architecture overview
- Creating an isolated .NET 8 Azure Functions project
- HTTP trigger with function-level auth
- Walkthrough: GPT-4.1 prompt for structured data extraction
- 37-field strongly typed data model

Prerequisites: .NET 8 SDK, Azure Functions Core Tools v4

🔗 GitHub Repo: https://github.com/dhavalshah01/contoso-ai-paints

**Tags:** azure ai search, azure functions, rag tutorial, retrieval augmented generation, azure openai, gpt-4.1, dotnet 8, azure functions .net 8, ai product search, pdf data extraction, structured data extraction, custom skillset azure, azure ai foundry, ai agent tutorial, azure tutorial, prompt engineering, document intelligence, serverless ai, azure functions tutorial, cloud ai pipeline

**Category:** Education
**Type:** How-to

**Problems:**
0:30 How does a RAG pipeline work end-to-end with Azure AI services?
2:30 What prerequisites do I need for Azure Functions with .NET 8?
3:00 How do I create an Azure Functions project with .NET 8 isolated worker?
5:30 How does GPT-4.1 extract structured data from PDF text?
5:30 How do I design a prompt for structured JSON extraction from documents?
8:00 How do I verify an Azure Functions project builds correctly?

---

### Video 2: Azure Blob Storage & PDF Upload — Build an AI Product Search Agent (Part 2/6)

**Description:**
We create an Azure Blob Storage account and upload 10 paint product PDF data sheets — the raw data source for everything in this series. These unstructured PDFs will later be indexed by Azure AI Search and transformed into structured data by GPT-4.1.

What you'll learn:
- Creating an Azure Storage Account (LRS, Standard)
- Creating a private blob container
- Uploading PDF files via the Azure Portal
- Copying the connection string for Azure AI Search (used in Video 4)
- Quick mention of Azure Storage Explorer

🔗 GitHub Repo: https://github.com/dhavalshah01/contoso-ai-paints

**Tags:** azure blob storage, azure storage account, upload pdf azure, blob container, azure portal tutorial, azure storage explorer, azure ai search data source, pdf upload cloud, azure storage setup, rag tutorial, ai product search, azure tutorial, cloud storage, azure for beginners, blob storage tutorial, document storage azure, azure data pipeline

**Category:** Education
**Type:** How-to

**Problems:**
1:30 How do I create an Azure Storage Account for AI Search?
3:30 How do I create a private blob container in Azure?
5:30 How do I upload PDF files to Azure Blob Storage?
7:30 How do I get the storage connection string for Azure AI Search?
8:30 What is Azure Storage Explorer and when should I use it?

---

### Video 3: Azure OpenAI — Deploy GPT-4.1 — Build an AI Product Search Agent (Part 3/6)

**Description:**
We provision Azure OpenAI and deploy the GPT-4.1 model — the brain that extracts structured product data from PDF text and powers the final chat agent. We also deploy text-embedding-3-large for future vector search, grab the API endpoint and key, and do a quick sanity test in the Chat Playground.

What you'll learn:
- Creating an Azure OpenAI resource
- Why GPT-4.1 for structured data extraction
- Deploying GPT-4.1 (Global Standard)
- Deploying text-embedding-3-large (optional)
- Getting the endpoint URL and API key
- Quick API test in Azure AI Foundry Chat Playground
- Cost breakdown (~$0.15 for 10 documents)

🔗 GitHub Repo: https://github.com/dhavalshah01/contoso-ai-paints

**Tags:** azure openai, gpt-4.1, deploy gpt model, azure openai tutorial, azure ai foundry, text-embedding-3-large, openai api key, azure openai pricing, azure openai cost, gpt-4.1 deployment, azure ai studio, openai structured extraction, rag tutorial, ai product search, prompt engineering, azure openai setup, azure cognitive services, llm deployment azure

**Category:** Education
**Type:** How-to

**Problems:**
0:20 Why should I use GPT-4.1 for structured data extraction?
1:30 How do I create an Azure OpenAI resource?
3:30 How do I get the Azure OpenAI endpoint and API key?
5:00 How do I deploy GPT-4.1 in Azure AI Foundry?
7:30 How do I deploy text-embedding-3-large for vector search?
8:30 How do I test Azure OpenAI in the Chat Playground?
9:00 How much does Azure OpenAI GPT-4.1 cost for document extraction?

---

### Video 4: Azure AI Search — Index, Skillset & Indexer — Build an AI Product Search Agent (Part 4/6)

**Description:**
We create the Azure AI Search service and define a 52-field index, blob data source, key phrase extraction skillset, and indexer — all from the Azure Portal using pre-built JSON definitions. By the end, our 10 PDFs are indexed with metadata and AI-extracted key phrases. Product fields are still NULL — that's where Video 5 comes in.

What you'll learn:
- Creating an Azure AI Search service (Basic tier)
- Defining a 52-field index schema via JSON
- Connecting a blob data source to the search service
- Key Phrase Extraction skillset (built-in cognitive skill)
- Configuring field mappings and the indexer
- Running the indexer and verifying results in Search Explorer

🔗 GitHub Repo: https://github.com/dhavalshah01/contoso-ai-paints

**Tags:** azure ai search, search index, skillset, indexer, key phrase extraction, cognitive skills, azure search tutorial, search index schema, field mappings, blob indexer, azure ai search portal, document indexing, azure search skillset, ai enrichment pipeline, rag tutorial, ai product search, azure portal, search explorer, azure cognitive search

**Category:** Education
**Type:** How-to

**Problems:**
0:20 How do I create an Azure AI Search service?
2:30 How do I create a 52-field search index using JSON?
4:30 How do I connect Azure Blob Storage as a data source for AI Search?
5:30 How do I set up a Key Phrase Extraction skillset in Azure AI Search?
6:30 How do I create an indexer with field mappings in Azure AI Search?
6:30 How do I connect a data source, skillset, and index with an indexer?

---

### Video 5: Custom AI Extraction — Deploy the GPT-4.1 Function — Build an AI Product Search Agent (Part 5/6)

**Description:**
This is the core of the project. We deploy the Azure Function to Azure, test it with a sample payload, then update the skillset with a Custom WebApiSkill that calls GPT-4.1 for structured extraction. We add 37 output field mappings to the indexer, re-run it, and watch every product field go from NULL to fully populated — SKU, color, VOC, dry times, warranty, and more.

What you'll learn:
- Deploying an Azure Function with `func azure functionapp publish`
- Testing the custom skill with a sample payload
- Adding a WebApiSkill to the skillset (GPT-4.1 extraction)
- Configuring 37 output field mappings in the indexer
- Re-running the indexer and verifying all fields populated
- Before/after: NULL fields → structured product data

🔗 GitHub Repo: https://github.com/dhavalshah01/contoso-ai-paints

**Tags:** custom web api skill, azure ai search custom skill, gpt-4.1 extraction, azure functions deploy, func azure functionapp publish, pdf to json, structured data extraction, output field mappings, azure function openai, ai data extraction, prompt engineering, rag tutorial, ai product search, document intelligence, azure functions .net 8, custom skillset, webapiskill, pdf parsing ai, gpt structured output

**Category:** Education
**Type:** How-to

**Problems:**
0:30 How does a Custom WebApiSkill work in Azure AI Search?
1:30 How do I deploy an Azure Function using func azure functionapp publish?
3:00 How do I test an Azure AI Search custom skill with a sample payload?
4:30 How do I add a WebApiSkill to an Azure AI Search skillset?
6:00 How do I configure output field mappings for 37 extracted fields?
7:30 How do I re-run an Azure AI Search indexer after updating the skillset?

---

### Video 6: RAG Agent in Azure AI Foundry — Live Demo & Finale — Build an AI Product Search Agent (Part 6/6)

**Description:**
The grand finale! We connect our search index to Azure AI Foundry's Chat Playground, configure RBAC permissions, set a system prompt, and test the RAG agent with real-world prompts — customer recommendations, coverage calculations, technical specs, kids' room safety, VOC compliance, product comparisons, and edge cases. The agent answers using real product data, not hallucinations.

What you'll learn:
- Configuring RBAC roles for OpenAI + Search integration
- Connecting Azure AI Search as a grounding data source in Foundry
- Writing an effective system prompt for a product expert agent
- Live demo: 7 prompts across 6 categories
- Grounding boundaries: the agent says "I don't know" when data isn't available
- Complete end-to-end RAG pipeline in action

🔗 GitHub Repo: https://github.com/dhavalshah01/contoso-ai-paints

**Tags:** rag agent, azure ai foundry, chat playground, retrieval augmented generation, grounded ai, azure openai chat, system prompt, rbac azure, ai agent demo, product search agent, gpt-4.1 rag, azure ai search grounding, ai chat agent, no hallucination ai, azure ai foundry tutorial, ai product assistant, live demo ai, azure rbac, rag pipeline, ai agent tutorial

**Category:** Education
**Type:** How-to

**Problems:**
0:30 How do I configure RBAC permissions for Azure OpenAI and AI Search?
2:00 How do I connect a search index as a data source in Azure AI Foundry?
3:30 How do I write a system prompt for a product expert RAG agent?
4:00 How does a RAG agent recommend products based on customer needs?
4:45 How can a RAG agent calculate paint coverage from structured data?
5:15 How do I get technical specs like viscosity and film thickness from a chat agent?
5:45 How does a RAG agent handle safety questions for kids' room paint?
6:15 How can I query VOC compliance across products with a chat agent?
6:45 How do I get a RAG agent to compare products in a table format?
7:15 How does a grounded RAG agent handle questions about data it doesn't have?

---

## Social Media Posts

### Video 1: Project Setup & Azure Functions

#### LinkedIn

I just launched a 6-part YouTube series: Build an AI-Powered Product Search Agent with Azure AI

The problem: Paint manufacturers have hundreds of product PDF data sheets with critical data (SKUs, VOC values, dry times, coverage) buried in unstructured text. Manual extraction doesn't scale.

The solution: An end-to-end RAG pipeline using Azure Functions, Azure OpenAI (GPT-4.1), Azure AI Search, and Azure AI Foundry — all in under 60 minutes of video.

In Part 1, we set up the project and build the core Azure Function (.NET 8) that calls GPT-4.1 to extract 37 structured fields from raw PDF text. I walk through the full architecture, prompt design, and data model.

What you'll learn:
- RAG architecture from scratch
- Azure Functions with .NET 8 isolated worker
- GPT-4.1 prompt engineering for structured extraction
- 37-field data model design

🎥 Watch: https://www.youtube.com/watch?v=Cok8n3AzucA
💻 GitHub: https://github.com/dhavalshah01/contoso-ai-paints

#AzureAI #RAG #AzureOpenAI #GPT4 #AzureFunctions #DotNet #AI #CloudArchitecture

#### Reddit

**Title:** I built an AI-powered product search agent with Azure AI — 6-part video series (Part 1: Project Setup & Azure Functions)

**Subreddits:** r/azure, r/dotnet, r/artificial

I created a 6-part YouTube series showing how to build a complete RAG (Retrieval-Augmented Generation) pipeline using Azure services.

The use case: 10 paint product PDF data sheets → Azure Blob Storage → Azure AI Search with a custom skillset → GPT-4.1 extracts 37 structured fields → searchable index → chat agent in Azure AI Foundry.

Part 1 covers the project setup and the core Azure Function (.NET 8 isolated) that calls GPT-4.1 for structured data extraction. Full code walkthrough of the prompt design and 37-field data model.

🎥 Video: https://www.youtube.com/watch?v=Cok8n3AzucA
💻 Full source code: https://github.com/dhavalshah01/contoso-ai-paints

Tech stack: Azure Functions (.NET 8), Azure OpenAI (GPT-4.1), Azure AI Search, Azure Blob Storage, Azure AI Foundry

Happy to answer questions about the architecture or implementation!

#### Twitter/X

🚀 New series: Build an AI Product Search Agent with Azure AI — from zero to RAG in 6 videos

Part 1: Project setup + Azure Function that calls GPT-4.1 to extract 37 fields from PDF text

🎥 https://www.youtube.com/watch?v=Cok8n3AzucA
💻 https://github.com/dhavalshah01/contoso-ai-paints

#Azure #RAG #GPT4 #DotNet #AI

---

### Video 2: Azure Blob Storage & PDF Upload

#### LinkedIn

Part 2/6: Azure Blob Storage & PDF Upload — AI Product Search Agent Series

Every AI pipeline starts with data. In this video, we create an Azure Storage account and upload 10 paint product PDF data sheets — the raw source that the entire RAG pipeline will transform into structured, searchable product data.

What you'll learn:
- Storage account setup (LRS, Standard)
- Private blob container creation
- Bulk PDF upload via Azure Portal
- Saving the connection string for Azure AI Search

Simple but foundational — get the data layer right, and everything downstream flows smoothly.

🎥 Watch: https://youtu.be/ZVIzQQEr2RY
💻 GitHub: https://github.com/dhavalshah01/contoso-ai-paints
◀️ Part 1: https://www.youtube.com/watch?v=Cok8n3AzucA

#AzureAI #BlobStorage #RAG #AzureOpenAI #CloudArchitecture

#### Reddit

**Title:** AI Product Search Agent with Azure AI — Part 2: Azure Blob Storage & PDF Upload

**Subreddits:** r/azure

Continuing my 6-part series on building a RAG pipeline with Azure AI.

Part 2 is straightforward but essential — setting up Azure Blob Storage and uploading 10 product PDF data sheets. These are the unstructured documents that GPT-4.1 will later extract 37 structured fields from.

🎥 Video: https://youtu.be/ZVIzQQEr2RY
💻 Source code: https://github.com/dhavalshah01/contoso-ai-paints
Part 1 (Project Setup): https://www.youtube.com/watch?v=Cok8n3AzucA

#### Twitter/X

Part 2/6: Azure Blob Storage & PDF Upload

Upload 10 product PDFs → the raw data that GPT-4.1 will transform into structured search data

🎥 https://youtu.be/ZVIzQQEr2RY
💻 https://github.com/dhavalshah01/contoso-ai-paints

#Azure #BlobStorage #RAG #AI

---

### Video 3: Azure OpenAI — Deploy GPT-4.1

#### LinkedIn

Part 3/6: Deploy GPT-4.1 on Azure OpenAI — AI Product Search Agent Series

The brain of the pipeline. In this video, we provision Azure OpenAI and deploy GPT-4.1 — the model that does two critical jobs:

1. Extracts 37 structured fields from raw PDF text during indexing
2. Powers the RAG chat agent that answers natural language questions

We also deploy text-embedding-3-large for future vector search, and I break down the cost: ~$0.15 total for extracting all 10 documents. Enterprise AI doesn't have to be expensive.

🎥 Watch: https://youtu.be/HoDNXV5arjk
💻 GitHub: https://github.com/dhavalshah01/contoso-ai-paints
◀️ Part 1: https://www.youtube.com/watch?v=Cok8n3AzucA

#AzureOpenAI #GPT4 #RAG #AI #AzureAI #PromptEngineering

#### Reddit

**Title:** AI Product Search Agent — Part 3: Deploy GPT-4.1 on Azure OpenAI (~$0.15 to extract 10 documents)

**Subreddits:** r/azure, r/artificial, r/MachineLearning

Part 3 of my 6-part Azure AI RAG series. We provision Azure OpenAI, deploy GPT-4.1, and deploy text-embedding-3-large for future vector search.

Key takeaway: the total cost for GPT-4.1 to extract 37 structured fields from 10 PDF documents is roughly $0.15. Each chat query costs $0.01-0.05. No fixed monthly fee on Standard S0.

🎥 Video: https://youtu.be/HoDNXV5arjk
💻 Source code: https://github.com/dhavalshah01/contoso-ai-paints
Part 1: https://www.youtube.com/watch?v=Cok8n3AzucA

#### Twitter/X

Part 3/6: Deploy GPT-4.1 on Azure OpenAI

Cost to extract 37 fields from 10 PDFs? ~$0.15
Chat queries? ~$0.01 each

Enterprise AI on a budget 💰

🎥 https://youtu.be/HoDNXV5arjk
💻 https://github.com/dhavalshah01/contoso-ai-paints

#AzureOpenAI #GPT4 #RAG #AI

---

### Video 4: Azure AI Search — Index, Skillset & Indexer

#### LinkedIn

Part 4/6: Azure AI Search — 52-Field Index, Skillset & Indexer

This is where the pipeline takes shape. We create the Azure AI Search service and define:

- A 52-field search index (product attributes, metadata, key phrases)
- A blob data source pointing to our PDFs
- A key phrase extraction skillset (built-in cognitive skill)
- An indexer that ties it all together

By the end, 10 PDFs are indexed with metadata and AI-extracted key phrases. Product fields are still NULL — that's the cliffhanger for Video 5, where GPT-4.1 fills them in.

All created from the Azure Portal using pre-built JSON definitions. No REST client or Postman needed.

🎥 Watch: https://youtu.be/9_94-0T_fh8
💻 GitHub: https://github.com/dhavalshah01/contoso-ai-paints
◀️ Part 1: https://www.youtube.com/watch?v=Cok8n3AzucA

#AzureAISearch #Indexer #Skillset #RAG #AzureAI #CloudArchitecture

#### Reddit

**Title:** AI Product Search Agent — Part 4: Azure AI Search with 52-field index, skillset & indexer (all from the portal)

**Subreddits:** r/azure, r/dotnet

Part 4 of my Azure AI RAG series. We create Azure AI Search and define a 52-field index, blob data source, key phrase extraction skillset, and indexer — all from the Azure Portal using pre-built JSON files.

The interesting bit: after this video, all product-specific fields (SKU, VOC, coverage, etc.) are still NULL. The index has the schema but only metadata and key phrases are populated. Video 5 is where GPT-4.1 fills in all 37 product fields.

🎥 Video: https://youtu.be/9_94-0T_fh8
💻 All JSON definitions in the repo: https://github.com/dhavalshah01/contoso-ai-paints
Part 1: https://www.youtube.com/watch?v=Cok8n3AzucA

#### Twitter/X

Part 4/6: Azure AI Search — 52-field index, skillset & indexer

10 PDFs indexed with key phrases. But 37 product fields are still NULL...

Cliffhanger for Part 5 🎬

🎥 https://youtu.be/9_94-0T_fh8
💻 https://github.com/dhavalshah01/contoso-ai-paints

#AzureAISearch #RAG #Azure #AI

---

### Video 5: Custom AI Extraction — Deploy the GPT-4.1 Function

#### LinkedIn

Part 5/6: The Core — GPT-4.1 Extracts 37 Fields from PDFs Automatically

This is the most satisfying video in the series.

We deploy the Azure Function, add a Custom WebApiSkill to the Azure AI Search skillset, configure 37 output field mappings, and re-run the indexer.

The result: every product field goes from NULL to fully populated. SKU, color, VOC, dry times, coverage, warranty, safety handling — all extracted by GPT-4.1 from raw PDF text. Zero manual data entry.

Before: sku: null, vocValue: null, color: null
After: sku: "CTSO-PAINT-EXT-SAT-1G-DB", vocValue: 45, color: "Deep Blue"

One skill. 37 fields. 10 documents. Fully automated.

🎥 Watch: https://youtu.be/aFAwXZY8CMU
💻 GitHub: https://github.com/dhavalshah01/contoso-ai-paints
◀️ Part 1: https://www.youtube.com/watch?v=Cok8n3AzucA

#AzureAI #GPT4 #AzureFunctions #DataExtraction #RAG #PromptEngineering #AI

#### Reddit

**Title:** From NULL to fully populated — GPT-4.1 extracts 37 fields from PDFs via Azure AI Search custom skill (Part 5/6)

**Subreddits:** r/azure, r/dotnet, r/artificial, r/MachineLearning

This is the payoff video. We deploy the Azure Function, wire it into the Azure AI Search skillset as a Custom WebApiSkill, add 37 output field mappings, and re-run the indexer.

Before: `sku: null, vocValue: null, color: null`
After: `sku: "CTSO-PAINT-EXT-SAT-1G-DB", vocValue: 45, color: "Deep Blue"`

Every product field populated automatically from raw PDF text. The function sends each document's text to GPT-4.1 with a structured prompt, gets back JSON, and the indexer maps it into the search index.

🎥 Video: https://youtu.be/aFAwXZY8CMU
💻 Source code: https://github.com/dhavalshah01/contoso-ai-paints
Part 1: https://www.youtube.com/watch?v=Cok8n3AzucA

#### Twitter/X

Part 5/6: GPT-4.1 extracts 37 fields from PDFs — fully automated

Before: sku: null, vocValue: null
After: sku: "CTSO-PAINT-EXT-SAT-1G-DB", vocValue: 45

One custom skill. 37 fields. Zero manual entry.

🎥 https://youtu.be/aFAwXZY8CMU
💻 https://github.com/dhavalshah01/contoso-ai-paints

#Azure #GPT4 #AI #DataExtraction #RAG

---

### Video 6: RAG Agent in Azure AI Foundry — Live Demo & Finale

#### LinkedIn

Part 6/6: The Grand Finale — RAG Agent Answers Product Questions Using Real Data

We've built the pipeline. Now we use it.

In this final video, we connect our search index to Azure AI Foundry's Chat Playground and test a fully grounded RAG agent with real-world scenarios:

🎨 "I need a low-VOC paint for my kid's bedroom"
📐 "How many gallons for 1,500 sq ft of interior walls?"
⚖️ "Which products comply with SCAQMD Rule 1113?"
📊 "Compare exterior satin vs wood stain — table format"

The agent answers every question from real indexed data. And when asked about pricing (which isn't in the index), it says "I don't have that information." No hallucinations.

6 videos. Under 60 minutes total. PDFs → structured data → searchable index → chat agent.

🎥 Watch: https://youtu.be/DusO4Z-XZOQ
💻 GitHub: https://github.com/dhavalshah01/contoso-ai-paints
▶️ Full playlist starting with Part 1: https://www.youtube.com/watch?v=Cok8n3AzucA

#AzureAI #RAG #AzureAIFoundry #GPT4 #AI #CloudArchitecture #ChatAgent

#### Reddit

**Title:** RAG Agent answers product questions from PDFs — zero hallucinations, full citations (Part 6/6 finale)

**Subreddits:** r/azure, r/artificial, r/MachineLearning, r/dotnet

Final video of my 6-part Azure AI series. We connect the search index (52 fields, 10 products extracted by GPT-4.1) to Azure AI Foundry's Chat Playground as a grounding data source.

The agent handles:
- Product recommendations ("low-VOC paint for a kid's bedroom")
- Coverage calculations (uses coverageMin/Max fields + math)
- Technical specs (viscosity, solids, film thickness, resin type)
- Compliance queries ("products with VOC below 50 g/L for SCAQMD")
- Side-by-side comparisons in table format
- Edge cases — it says "I don't have that information" for pricing, which isn't in the index

The full pipeline: PDFs → Blob Storage → AI Search → GPT-4.1 extraction → 37-field index → RAG chat agent.

🎥 Video: https://youtu.be/DusO4Z-XZOQ
💻 Full source code: https://github.com/dhavalshah01/contoso-ai-paints
Full series starting at Part 1: https://www.youtube.com/watch?v=Cok8n3AzucA

#### Twitter/X

Part 6/6: RAG agent LIVE — answers product questions from real PDF data

"Low-VOC paint for my kid's bedroom?" ✅ Answered with SKU + safety data
"Price of exterior satin?" ❌ "I don't have that info" — no hallucination

6 videos. PDFs → chat agent. Done.

🎥 https://youtu.be/DusO4Z-XZOQ
💻 https://github.com/dhavalshah01/contoso-ai-paints

#Azure #RAG #AI #GPT4 #AzureAIFoundry
