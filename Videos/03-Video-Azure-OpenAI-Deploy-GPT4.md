# Video 3: Azure OpenAI — Deploy GPT-4.1

**Duration Target:** 10 minutes
**Primary Screen:** Azure Portal + Azure AI Foundry
**Goal:** Viewer has Azure OpenAI provisioned with GPT-4.1 deployed and API keys saved.

---

## TRANSCRIPT + SCREEN DIRECTIONS

---

### [0:00–0:20] Recap & Goal

**SCREEN:** Architecture diagram — Azure OpenAI block highlighted.

**NARRATION:**
> "We have our function projects and PDF files in blob storage. Now we need the brain — Azure OpenAI with GPT-4.1. This model does two critical jobs: it extracts structured product data from raw PDF text during indexing, and it generates answers for our chat agent at the end. Let's set it up."

---

### [0:20–1:30] What is Azure OpenAI and Why GPT-4.1?

**SCREEN:** Slide or text overlay — keep this brief.

**NARRATION:**
> "Azure OpenAI gives you access to OpenAI models running in Azure's data centers. You get enterprise security, private networking, and data residency — your data stays in your Azure region and isn't used for model training."
>
> "We're using GPT-4.1 specifically because it's excellent at structured data extraction — it follows JSON schemas reliably, handles long documents well, and has strong instruction-following for the kind of field-by-field extraction we need."
>
> "We'll also deploy text-embedding-3-large as a model available for future vector search, but we won't use it in this series."

**TIP:** 60 seconds max on this explanation. Don't go deep into model comparisons.

---

### [1:30–3:30] Create the Azure OpenAI Resource

**SCREEN:** Azure Portal → Create a resource → search "Azure OpenAI" → Create.

**NARRATION:**
> "In the Azure Portal, Create a resource, search for Azure OpenAI, click Create."

**SCREEN:** Basics tab form.

**ACTIONS (narrate as you fill):**
> "Resource group: rg-contoso-paints — same one we used for storage. Name: contoso-paint-openai. Region: East US 2 — must match your other resources and support the models you need. Pricing tier: Standard S0 — this is pay-as-you-go."

**SCREEN:** Show filled form.

> "Network tab — All networks for this POC. In production, you'd use private endpoints. Skip Tags. Review + create."

**SCREEN:** Validation → Create. (Cut the deployment wait — jump to completion.)

> "Deployment takes about a minute. And it's done."

**SCREEN:** Click "Go to resource."

---

### [3:30–5:00] Get the Endpoint and API Key

**SCREEN:** Azure OpenAI resource → Keys and Endpoint page.

**NARRATION:**
> "First thing — grab the endpoint and API key. In the left menu under Resource Management, click 'Keys and Endpoint'."

**SCREEN:** Show endpoint URL and Key 1 (blur key value).

> "The endpoint is https://contoso-paint-openai.openai.azure.com/. Copy this. Then click 'Show' on KEY 1 and copy that too."
>
> "Save both in your resources file. You'll need them in video 5 when we configure the extraction function."

**ON-SCREEN TEXT OVERLAY:**
```
Endpoint: https://contoso-paint-openai.openai.azure.com/
API Key:  [saved securely]
```

> "Treat API keys like passwords — don't commit them to Git, don't paste them in chat or email."

**TIP:** Show the keys page for 5 seconds. Blur actual key values in post-production.

---

### [5:00–7:30] Deploy the GPT-4.1 Model

**SCREEN:** Azure OpenAI resource → Model Deployments → click "Manage Deployments" (opens Azure AI Foundry/Studio).

**NARRATION:**
> "Now we deploy the model. Click Model Deployments in the left menu, then click 'Manage Deployments' to open Azure AI Foundry."

**SCREEN:** Azure AI Foundry → Deployments page.

> "Click '+ Deploy model' → 'Deploy base model'."

**SCREEN:** Model selection dialog.

> "Search for gpt-4.1. Select it."

**SCREEN:** Deployment configuration:

> "Deployment name: gpt-4.1 — I keep the name the same as the model for clarity. Deployment type: Global Standard. Tokens per minute rate limit: 450K is the default — fine for our 10 document POC. Click Deploy."

**SCREEN:** Deployment completes. Show the deployment in the list.

> "Done. GPT-4.1 is now deployed and ready to receive API calls."

---

### [7:30–8:30] Deploy text-embedding-3-large (Optional, Quick)

**SCREEN:** Same deployments page → + Deploy model again.

**NARRATION:**
> "While we're here, let's deploy the embedding model too. Click Deploy model again, search for text-embedding-3-large, select it."

**SCREEN:** Deployment configuration.

> "Deployment name: text-embedding-3-large. Same defaults. Deploy."

> "We won't use this model in this series — it's for vector/semantic search which is a natural next step after this POC. But it's good to have it ready."

**TIP:** Speed through this at 2x. The steps are identical to GPT-4.1.

---

### [8:30–9:00] Quick API Test (Optional but Impressive)

**SCREEN:** Azure AI Foundry → Chat Playground.

**NARRATION:**
> "Let's do a quick sanity check. Go to Playgrounds → Chat. Make sure gpt-4.1 is selected as the deployment."

**SCREEN:** Type in the chat: "Say hello in 5 words or less."

> "I'll just make sure our model responds."

**SCREEN:** Response appears: "Hello! How are you today?"

> "GPT-4.1 is alive and responding. We'll put it to real work in video 5 when we send it raw PDF text and ask for structured extraction."

**TIP:** This is a 20-second test. Don't have a conversation. Just one prompt and one response.

---

### [9:00–9:30] Cost Summary

**SCREEN:** Text overlay or slide.

**NARRATION:**
> "Quick note on cost. GPT-4.1 pricing is usage-based — you pay per token. For our 10 documents, that's roughly 10-15 cents total for the extraction pass. The chat agent queries are a few cents each. Azure OpenAI resource itself has no fixed monthly fee on Standard S0."

**ON-SCREEN:**
```
GPT-4.1 Extraction (10 docs): ~$0.15
Chat queries: ~$0.01–0.05 each
Monthly fixed cost: $0.00
```

---

### [9:30–10:00] Wrap-Up & Next Video Teaser

**SCREEN:** Architecture diagram — Azure AI Search block highlighted.

**NARRATION:**
> "We now have Azure OpenAI with GPT-4.1 deployed and ready. In the next video, we create Azure AI Search — define the index schema with 52 fields, connect it to our blob storage, and set up a skillset to extract key phrases from the PDF content. That's video 4."

**SCREEN:** End card.

---

## CHAPTER MARKERS

```
0:00 - Recap & goal
0:20 - Why Azure OpenAI & GPT-4.1
1:30 - Create Azure OpenAI resource
3:30 - Get endpoint & API key
5:00 - Deploy GPT-4.1 model
7:30 - Deploy embedding model (optional)
8:30 - Quick API test
9:00 - Cost summary
9:30 - Next video teaser
```

## SCREEN CHECKLIST

- [ ] Architecture diagram with Azure OpenAI highlighted
- [ ] Azure Portal pre-logged in
- [ ] OpenAI resource creation form filled out
- [ ] Keys and Endpoint page visible (blur keys in post)
- [ ] Azure AI Foundry / AI Studio deployments page
- [ ] GPT-4.1 deployment configuration dialog
- [ ] Chat Playground with a quick test prompt
- [ ] Cost overlay graphic
