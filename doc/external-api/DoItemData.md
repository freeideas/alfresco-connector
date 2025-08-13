# DoItemData - External API Mapping

## Overview
Maps the SOAP `ItemData2` request to external API calls for retrieving file content and metadata.

## External API Endpoints

### Content Download
- **Path**: `/api/-default-/public/alfresco/versions/1/nodes/{nodeId}/content`
- **Method**: GET
- **Purpose**: Download file content

### Metadata Retrieval
- **Path**: `/api/-default-/public/alfresco/versions/1/nodes/{nodeId}`
- **Method**: GET
- **Purpose**: Get file metadata
- **Query**: `?include=properties,permissions`

## Request Mapping

### From SOAP to External API

| SOAP Field | External API | Notes |
|------------|--------------|-------|
| `itemId` | `{nodeId}` in path | Direct node ID (UUID format) |
| `conn.account` | Basic Auth username | Authentication |
| `conn.password` | Basic Auth password | Authentication |
| `datastore.id` | Not used directly | Site context (informational only) |

## Response Mapping

### From External API to SOAP

| External API | SOAP Response | Notes |
|--------------|---------------|-------|
| File content bytes | `<content>` | Base64 encoded |
| `entry.name` | `<fileName>` | File name |
| `entry.content.mimeType` | `<mimeType>` | Content type |
| `entry.content.sizeInBytes` | `<fileSize>` | File size |
| `entry.modifiedAt` | `<lastModified>` | Modification date |
| `entry.createdAt` | `<created>` | Creation date |
| `entry.properties` | `<metadata>` | Custom properties |

### Response Structure
```xml
<ItemDataReturn>
  <error>false</error>
  <errorMsg />
  <content>BASE64_ENCODED_CONTENT</content>
  <fileName>document.pdf</fileName>
  <mimeType>application/pdf</mimeType>
  <fileSize>1024576</fileSize>
  <lastModified>2024-01-15T10:30:00Z</lastModified>
  <created>2024-01-01T09:00:00Z</created>
  <metadata>
    <CustomPair>
      <key>cm:author</key>
      <value>John Doe</value>
    </CustomPair>
  </metadata>
</ItemDataReturn>
```

## Error Handling

| External API Error | SOAP Response |
|-------------------|---------------|
| 401 Unauthorized | `error=true`, `errorMsg="Authentication failed"` |
| 404 Not Found | `error=true`, `errorMsg="File not found"` |
| 403 Forbidden | `error=true`, `errorMsg="Access denied"` |
| 500 Server Error | `error=true`, `errorMsg="External API error"` |

## Example External API Calls

### Get Metadata
```bash
curl -u admin:admin \
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/nodes/12345678-1234-1234-1234-123456789012?include=properties"
```

### Download Content
```bash
curl -u admin:admin \
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/nodes/12345678-1234-1234-1234-123456789012/content" \
  -o file.pdf
```

## Implementation Notes

1. Make two API calls: one for metadata, one for content
2. Content must be Base64 encoded for SOAP response
3. Handle large files carefully (memory considerations)
4. **IMPORTANT**: The `itemId` is a direct node ID (UUID), not a site ID
5. Unlike Crawl2, ItemData2 uses the node ID directly without conversion
6. Custom properties are returned as key-value pairs
7. Binary content types are supported (PDF, images, etc.)