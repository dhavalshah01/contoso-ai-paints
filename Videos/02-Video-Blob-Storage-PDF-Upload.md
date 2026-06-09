# Video 2: Azure Blob Storage & PDF Upload

**Duration Target:** 10 minutes
**Primary Screen:** Azure Portal
**Goal:** Viewer has a storage account with 10 product PDFs uploaded, ready for indexing.

---

## TRANSCRIPT + SCREEN DIRECTIONS

---

### [0:00–0:20] Recap & Goal

**SCREEN:** Architecture diagram — Blob Storage block highlighted.

**NARRATION:**
> "In video 1, we set up  Azure Function project. Now we need the data. In this video, we create an Azure Blob Storage account and upload 10 paint product PDF data sheets. These PDFs are the raw source — the AI Search indexer will read them in video 4. Let's go."

---

### [0:20–1:30] Quick Look at the Source PDFs

**SCREEN:** Windows File Explorer — `data/files/` folder showing 10 PDF files.

**NARRATION:**
> "Here are our 10 product data sheets. Each one is a 1-2 page technical data sheet for a fictional Contoso PaintCo product — exterior satin paint, interior ultra-matte, primer, enamel, wood stain, floor coating, metal DTM paint, ceiling flat, kids room ecoSafe, and masonry heavy-body."

**SCREEN:** Double-click one PDF (e.g., Product 01) to open in a PDF viewer — show it for 5 seconds.

> "Each PDF has product identity, technical specs, application instructions, safety info, and warranty. This is the unstructured data that GPT-4.1 will turn into 37 structured fields later in the series."

**TIP:** Open only ONE PDF. Show it for 5 seconds max. Don't read the content — just point out the sections visually.

---

### [1:30–3:30] Create the Storage Account

**SCREEN:** Azure Portal — portal.azure.com (pre-logged in)

**NARRATION:**
> "Head to the Azure Portal. Click 'Create a resource', search for 'Storage account', and click Create."

**SCREEN:** Azure Portal → Create a Resource → Storage Account → Create

**ACTIONS (narrate as you click):**
> "Subscription — pick yours. Resource group — I'll use rg-contoso-paints. If you don't have one, click 'Create new'. Storage account name: contosopaintdata — must be globally unique, all lowercase, no dashes. Region: East US 2. Performance: Standard. Redundancy: LRS — locally redundant storage is fine for a POC."

**SCREEN:** Show the filled-out Basics tab.

> "Skip the other tabs for now — defaults are fine. Click 'Review + create'."

**SCREEN:** Validation passes → click Create.

> "Takes about 30 seconds. While it deploys, let me explain why we chose LRS. For a POC, we don't need geo-redundancy. In production, you'd pick GRS or ZRS for durability."

**TIP:** If pre-created, say: "I've already created this, let me navigate to it." Skip the waiting screen. Cut the deployment wait in editing.

---

### [3:30–5:30] Create the Documents Container

**SCREEN:** Azure Portal → Storage account → Containers.

**NARRATION:**
> "Once the storage account is created, go to Data storage → Containers on the left menu. Click '+ Container'."

**SCREEN:** Show the "+ Container" dialog.

> "Name it 'documents'. Set the access level to Private — we don't want these PDFs publicly accessible. Click Create."

**SCREEN:** The 'documents' container appears in the list. Click into it.

> "Container created. It's empty. Let's upload our PDFs."

---

### [5:30–7:30] Upload PDF Files

**SCREEN:** Inside the `documents` container → click "Upload" button.

**NARRATION:**
> "Click Upload. Then Browse for files. Navigate to your data/files folder and select all 10 product PDFs. You can Ctrl+A to select all."

**SCREEN:** File picker dialog → select all PDFs → click Open.

> "I've selected all 10. You can see them listed in the upload panel."

**SCREEN:** Upload panel showing 10 files queued.

> "Expand 'Advanced' briefly — blob type is 'Block blob', access tier is 'Hot'. These are fine. Click Upload."

**SCREEN:** Upload progress — all 10 files uploading. (Speed this up to 2x in editing if it takes more than 5 seconds.)

> "Done! All 10 files uploaded successfully."

**SCREEN:** Container view now showing all 10 PDF files with their sizes.

> "Let me verify — 10 files, all roughly 50-200 KB each. Click on any file to see its properties."

**SCREEN:** Click on one file → show the Properties panel (blob URL, content type, size, last modified).

> "Here's the blob URL — this is what Azure AI Search will use to find the file. Content type is application/pdf. Everything looks good."

---

### [7:30–8:30] Get the Connection String (Needed Later)

**SCREEN:** Navigate to Storage Account → Security + networking → Access keys.

**NARRATION:**
> "One more thing before we move on. We need the storage connection string for Azure AI Search to connect to this account. Go to Security + networking → Access keys."

**SCREEN:** Access Keys page → click "Show" on key1.

> "Click Show, then copy the Connection string for Key 1. Save this somewhere secure — a notepad file, a password manager, whatever. You'll need it in video 4 when we create the search data source."

**ON-SCREEN TEXT OVERLAY:** "Save this connection string! You'll need it in Video 4."

**TIP:** Don't show the actual key for more than 2 seconds. Blur it in post-production or rotate the key after recording.

---

### [8:30–9:00] Alternative: Azure Storage Explorer (Quick Mention)

**SCREEN:** Quick screenshot of Azure Storage Explorer (don't open it live).

**NARRATION:**
> "If you prefer a desktop app over the portal, Azure Storage Explorer does the same thing. Download it from the link in the description. You can drag and drop files, it has a nice tree view, and it works offline with cached data. But the portal method we just used works perfectly for our 10 files."

**TIP:** Don't demo Storage Explorer live — it's a tangent. Show a static screenshot for 5 seconds.

---

### [9:00–9:30] Verify Upload Summary  

**SCREEN:** Container view with all 10 files.

**NARRATION:**
> "Quick recap. We have:"
>
> (Use on-screen counter/bullets)
>
> "Storage account: contosopaintdata. Container: documents. 10 PDF product data sheets uploaded. Connection string saved for later. That's it."

---

### [9:30–10:00] Next Video Teaser

**SCREEN:** Architecture diagram — Azure OpenAI block highlighted.

**NARRATION:**
> "In the next video, we create Azure OpenAI and deploy GPT-4.1 — the model that will extract structured product data from these PDFs. This is where the AI magic starts. See you in video 3."

**SCREEN:** End card.

---

## CHAPTER MARKERS

```
0:00 - Recap & goal
0:20 - Source PDF overview
1:30 - Create storage account
3:30 - Create documents container
5:30 - Upload 10 PDF files
7:30 - Copy connection string
8:30 - Azure Storage Explorer mention
9:00 - Upload verification
9:30 - Next video teaser
```

## SCREEN CHECKLIST

- [ ] Architecture diagram with Blob Storage highlighted
- [ ] Azure Portal pre-logged in
- [ ] data/files folder with 10 PDFs ready
- [ ] One PDF opened in viewer for quick preview
- [ ] Storage account creation form filled out
- [ ] Container view showing all uploaded files
- [ ] Access Keys page (blur actual keys in post)
- [ ] Azure Storage Explorer screenshot (static image)
