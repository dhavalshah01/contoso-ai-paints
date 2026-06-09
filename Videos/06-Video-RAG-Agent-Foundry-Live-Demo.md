# Video 6: RAG Agent in Azure AI Foundry — Live Demo & Finale

**Duration Target:** 10 minutes
**Primary Screen:** Azure AI Foundry Chat Playground
**Goal:** Viewer connects the search index as a grounding data source, configures a system prompt, tests real-world prompts across 6 categories, and sees the completed RAG agent in action.

---

## TRANSCRIPT + SCREEN DIRECTIONS

---

### [0:00–0:30] Hook — The Agent Answers a Real Question

**SCREEN:** Azure AI Foundry Chat Playground — pre-recorded clip.

User types: *"I need a low-VOC paint for my kid's bedroom. What do you recommend?"*

Agent responds with the Kids' Room EcoSafe paint, cites the SKU, VOC value, and safety handling info.

**NARRATION:**
> "This is a chat agent that answers product questions using real data — not hallucinated. It pulls from our search index with 37 extracted fields across 10 products. In this final video, we wire it all together and put it through its paces. Let's build the agent."

---

### [0:30–2:00] Configure RBAC Permissions (Quick — Essential Setup)

**SCREEN:** Azure Portal — Access Control (IAM) pages.

**NARRATION:**
> "Before the Foundry Playground can talk to our search index, we need four role assignments. I'll do these fast — they're all in the Azure Portal under Access Control IAM."

**SCREEN:** Show each step as a quick montage — 10 seconds per role. Speed up portal navigation at 2x.

> "First: on the OpenAI resource, assign YOUR user account the 'Cognitive Services OpenAI User' role. This lets you use GPT-4.1 in the playground."

> "Second: on the Search service, assign your user 'Search Index Data Reader'. This lets the playground read index data."

> "Third: on the OpenAI resource, go to Identity, turn on System Assigned Managed Identity. This gives the OpenAI resource its own identity."

> "Fourth: on the Search service, assign that managed identity TWO roles — 'Search Index Data Reader' and 'Search Service Contributor'. This lets the OpenAI resource access the search index on your behalf."

**SCREEN:** Quick summary slide:

```
Your User:
  → Cognitive Services OpenAI User (on OpenAI resource)
  → Search Index Data Reader (on Search service)

OpenAI Managed Identity:
  → Search Index Data Reader (on Search service)
  → Search Service Contributor (on Search service)
```

> "Also: on the Search service under Settings → Keys, change API Access Control from 'API Key' to 'Both'. This enables RBAC authentication alongside API keys."

> "Wait 2-5 minutes for propagation. I've already done this — let's move on."

**TIP:** This is pure setup — speed it up aggressively. Pre-record the portal clicks and play at 2x with voiceover. If pre-done, show the summary slide and say "I've already configured these."

---

### [2:00–3:30] Connect Search Index in Foundry Chat Playground

**SCREEN:** Navigate to [ai.azure.com](https://ai.azure.com) → Sign in.

**NARRATION:**
> "Open Azure AI Foundry at ai.azure.com. Go to Playgrounds → Chat."

**SCREEN:** Chat Playground — select gpt-4.1 deployment.

> "Make sure GPT-4.1 is selected as the deployment."

**SCREEN:** Click "Add your data" or "+ Add a data source".

> "Click 'Add your data'. Data source type: Azure AI Search. Subscription: yours. Search service: contoso-paint-search. Index: contoso-paints-index."

**SCREEN:** Step through the data source wizard:

> "Search type: Keyword — our index doesn't have vector embeddings yet. Content data field: content. Title: productName. File name: metadata_storage_name."

> "Click Next, review, Save and close."

**SCREEN:** The data source appears as connected.

> "Our search index is now connected as a grounding data source. Every chat message will first search the index, then GPT-4.1 generates an answer from the search results."

---

### [3:30–4:00] Add the System Prompt

**SCREEN:** Setup pane → System message box.

**NARRATION:**
> "One more thing — the system prompt. This tells the agent its role and rules."

**SCREEN:** Paste the system message:
```
You are a Contoso PaintCo product expert assistant. You help customers, contractors, store associates, and compliance officers find accurate paint product information.

Rules:
- ONLY answer questions using the product data provided in the search results.
- If the search results do not contain enough information, say "I don't have that information in the current product catalog."
- Always cite the product name and SKU when referencing a specific product.
- When comparing products, use a structured table format.
- For safety questions, always include the full safety/handling text.
- Respond in a professional, helpful tone.
```

> "Apply changes. The agent is ready."

---

### [4:00–8:30] Live Demo — 6 Prompt Categories

**NARRATION:**
> "Let's test this with real-world scenarios. I have prompts across six categories — customer, contractor, compliance, comparison, warranty, and edge cases."

---

#### [4:00–4:45] Prompt 1 — Customer: Product Recommendation

**SCREEN:** Type in chat:
```
I need to paint the exterior of my house. I want a satin finish with low VOC. What do you recommend?
```

**NARRATION (after response appears):**
> "The agent found the Premium Exterior Satin Paint — SKU CTSO-PAINT-EXT-SAT-1G-DB. VOC of 45 grams per liter. It cited the product name, SKU, and pulled the exact VOC value from our index. Not hallucinated — this is real data."

**TIP:** Let the response load. Read the key parts aloud. Don't read every word — highlight the citations and data accuracy. 30 seconds per prompt-response pair.

---

#### [4:45–5:15] Prompt 2 — Customer: Coverage Calculation

**SCREEN:** Type:
```
I have 1,500 square feet of interior walls to paint. How many gallons of your interior ultra-matte paint do I need?
```

> "It used coverageMin and coverageMax from the index to calculate gallons needed. And it recommended the number of coats from the recommendedCoats field. Math from structured data, not guessing."

---

#### [5:15–5:45] Prompt 3 — Contractor: Technical Specs

**SCREEN:** Type:
```
Give me the full technical data for your garage floor coating — viscosity, solids content, film thickness, and resin type.
```

> "All the technical fields — viscosity 85-95 KU, solids by volume, recommended dry and wet film thickness in mils, epoxy-modified acrylic resin. This is spec-sheet accuracy from a chat interface."

---

#### [5:45–6:15] Prompt 4 — Kids' Room Safety

**SCREEN:** Type:
```
I have a 3-year-old and want to paint their bedroom. Do you have a safe, low-VOC option?
```

> "It found the Kids' Room EcoSafe paint, cited the low VOC, and pulled the full safety handling information. Exactly what a worried parent needs."

---

#### [6:15–6:45] Prompt 5 — Compliance: VOC Regulation

**SCREEN:** Type:
```
We're in California and need to comply with SCAQMD Rule 1113. List all products with VOC below 50 g/L.
```

> "It filtered by VOC value and listed qualifying products with their exact measurements. Regulatory compliance from a chat prompt."

---

#### [6:45–7:15] Prompt 6 — Comparison Table

**SCREEN:** Type:
```
Compare the exterior satin paint and the exterior wood stain side by side. Include VOC, coverage, dry times, warranty, resin type, and cleanup. Use a table.
```

> "Beautiful comparison table — two products, six attributes, all from index data. This alone saves a contractor 15 minutes of flipping through data sheets."

**SCREEN:** Zoom into the table. Let it fill the screen for 5 seconds.

---

#### [7:15–7:45] Prompt 7 — Edge Case: Data Not in Index

**SCREEN:** Type:
```
What is the price of your exterior satin paint?
```

> "And here's the important one — the agent says 'I don't have pricing information in the current product catalog.' It didn't make up a price. It respected the grounding boundary. This is what the system prompt's rules enforce."

---

#### [7:45–8:30] Prompt 8 — Multi-Product Scenario

**SCREEN:** Type:
```
I own a commercial building with a concrete parking garage floor and metal stairway railings. Which products do I need and how many coats for each?
```

> "Two products recommended — the floor coating and the DTM metal paint. Each with specific coat counts. The agent combined data from multiple documents to answer a complex real-world question."

**TIP:** 8 prompts in 4.5 minutes = ~35 seconds each. Don't pause long between prompts. This section should feel fast and impressive — rapid-fire Q&A showing the agent's range.

---

### [8:30–9:00] Tune the Agent Settings

**SCREEN:** Setup pane — show Strictness and Retrieved documents sliders.

**NARRATION:**
> "Quick note: two settings you can tune. Strictness controls how tightly the agent stays grounded in search results — 1 is loose, 5 is strict. We're at 3, which is a good default. Retrieved documents controls how many search results GPT sees — default is 3, increase to 5 if answers seem incomplete."

> "For production, you'd create a persistent Foundry Agent with these settings locked in, plus an API endpoint for integration into web apps or Teams bots."

---

### [9:00–9:30] Series Recap — What We Built

**SCREEN:** Architecture diagram — all blocks highlighted.

**NARRATION:**
> "Let's zoom out. In 6 videos, we built:"
>
> "10 PDF data sheets uploaded to Blob Storage."
>
> "An Azure Function that calls GPT-4.1 to extract 37 structured fields per document."
>
> "An Azure AI Search index with 52 fields, a skillset with key phrase extraction and custom AI extraction, and an indexer that runs automatically."
>
> "A RAG agent in Azure AI Foundry that answers natural language product questions grounded in real indexed data."
>
> "Total Azure cost: about 75 dollars a month for search, plus pennies for OpenAI usage. No ML expertise required."

**ON-SCREEN:**
```
Blob Storage   → 10 PDFs
Azure Function → GPT-4.1 extraction (37 fields)
AI Search      → 52-field index + skillset + indexer
AI Foundry     → RAG chat agent

Monthly cost: ~$75 (Search) + ~$0.15 (OpenAI)
```

---

### [9:30–10:00] Next Steps & Outro

**SCREEN:** Bullet list slide.

**NARRATION:**
> "Where to go from here: add vector search with the embedding model we deployed in video 3. Build a persistent Foundry Agent with an API endpoint. Integrate into a customer-facing web app. Or add more data sources — pricing, inventory, competitor specs."
>
> "All the code, JSON configs, and step files are in the GitHub repo linked in the description. If this series helped, hit subscribe. Thanks for watching."

**SCREEN:** End card with playlist link, GitHub repo link, subscribe button.

---

## CHAPTER MARKERS

```
0:00 - Agent answers a real question (hook)
0:30 - Configure RBAC permissions
2:00 - Connect search index in Foundry
3:30 - Add system prompt
4:00 - Live demo: customer recommendations
4:45 - Live demo: coverage calculation
5:15 - Live demo: technical specs
5:45 - Live demo: kids' room safety
6:15 - Live demo: VOC compliance
6:45 - Live demo: product comparison table
7:15 - Live demo: edge case (no pricing data)
7:45 - Live demo: multi-product scenario
8:30 - Tune agent settings
9:00 - Series recap
9:30 - Next steps & outro
```

## SCREEN CHECKLIST

- [ ] Pre-recorded hook clip (agent answering a question)
- [ ] Azure Portal — IAM pages for role assignments (pre-done or speed-recorded)
- [ ] RBAC summary slide
- [ ] Azure AI Foundry — Chat Playground open
- [ ] GPT-4.1 deployment selected
- [ ] Data source connected (contoso-paints-index)
- [ ] System message pasted and applied
- [ ] 8 test prompts pre-typed in a notepad for quick copy-paste
- [ ] Architecture diagram — final version with all blocks highlighted
- [ ] Cost summary graphic
- [ ] Next steps bullet slide
- [ ] End card with playlist + GitHub links

---

## PRODUCTION TIPS SPECIFIC TO THIS VIDEO

1. **Pre-type all 8 prompts** in a notepad file. Copy-paste them into the chat — don't type live. Typing takes 10-15 seconds each; pasting takes 2.
2. **Pre-warm the playground** — send a throwaway prompt before recording to ensure the connection is live and responses are fast.
3. **If a response is slow** (>5 seconds), say "Let me skip ahead" and cut in editing. GPT-4.1 responses are typically 3-8 seconds.
4. **Zoom into the response text** after each prompt. Mobile viewers can't read small text in a full-screen portal view.
5. **The comparison table prompt (Prompt 6)** is the most visually impressive — make sure it renders fully on screen. Consider a 5-second zoom-hold.
6. **The edge case prompt (Prompt 7)** is the most important for credibility — it proves the agent doesn't hallucinate. Emphasize this.
