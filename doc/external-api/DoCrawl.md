# DoCrawl - External API Mapping

## Overview
Maps the SOAP `Crawl2` request to Alfresco REST API calls for browsing repository content.

## Critical Concept: Site ID vs Node ID
**IMPORTANT**: The SOAP `datastore.id` field contains a **site ID** (e.g., "swsdp"), NOT a node ID. Alfresco sites have a documentLibrary container with its own node ID that must be retrieved first.

## External API Endpoints

### 1. Get Site's Document Library (when datastore.id is provided)
- **Path**: `/api/-default-/public/alfresco/versions/1/sites/{siteId}/containers/documentLibrary`
- **Method**: GET
- **Purpose**: Get the node ID of the site's document library
- **Returns**: Node ID in `entry.id`

### 2. Browse Content
- **Path**: `/api/-default-/public/alfresco/versions/1/nodes/{nodeId}/children`
- **Method**: GET
- **Purpose**: List contents of a folder
- **Query Parameters**:
  - `skipCount` - Number of items to skip (page * maxReturns)
  - `maxItems` - Maximum items to return
  - `include` - Include additional data (e.g., properties, permissions)

## Request Flow

```
1. If datastore.id provided (e.g., "swsdp"):
   → GET /sites/{datastore.id}/containers/documentLibrary
   → Extract node ID from response
   
2. Browse using the node ID:
   → GET /nodes/{nodeId}/children
```

## Request Mapping

| SOAP Field | External API Usage | Notes |
|------------|-------------------|-------|
| `datastore.id` | Site ID for step 1 | Site identifier, NOT a node ID |
| `foldersubId` | Node ID if provided | Specific folder within site |
| `maxReturns` | `maxItems` query param | Pagination limit |
| `page` | Calculate `skipCount` | page * maxReturns |
| `conn.account/password` | Basic Auth | Authentication |
| `conn.customparam` | Base URL | Server location |

## Response Mapping

| External API | SOAP Response | Notes |
|--------------|---------------|-------|
| `entry.id` | `<id>` | Node identifier |
| `entry.name` | `<subid>` | File/folder name |
| `entry.nodeType` | `<datastoretypeid>` | Content type |
| `entry.modifiedAt` | `<lastUpdate>` | Last modification date |
| `entry.isFolder` | `<isFolder>` | Folder flag |
| `pagination.hasMoreItems` | `<moreExist>` | More results available |
| Derive from name/mime | `<extension>` | File extension |

### Fixed Response Values
- `<encodeid>true</encodeid>`
- `<securityChanged>true</securityChanged>`
- `<fileChanged>true</fileChanged>`
- `<metadataChanged>true</metadataChanged>`
- `<deleted>false</deleted>`

## Error Handling

| Scenario | SOAP Response |
|----------|--------------|
| Site not found | `error=true`, `errorMsg="Site not found: {id}"` |
| Folder not found | `error=true`, `errorMsg="Folder not found"` |
| Auth failed | `error=true`, `errorMsg="Authentication failed"` |

## Example Implementation

```csharp
// Step 1: Convert site ID to documentLibrary node ID
if (!string.IsNullOrEmpty(datastore.id))
{
    var siteDocLibUri = $"api/.../sites/{datastore.id}/containers/documentLibrary";
    var response = await client.GetAsync(siteDocLibUri);
    var nodeId = response["entry"]["id"];
    
    // Step 2: Browse using the actual node ID
    var browseUri = $"api/.../nodes/{nodeId}/children?maxItems={maxReturns}";
    // ... continue with browsing
}
```

## Common Pitfall
❌ **WRONG**: Using `datastore.id` directly as a node ID  
✅ **RIGHT**: Using `datastore.id` to fetch the documentLibrary node ID first