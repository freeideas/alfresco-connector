# Alfresco External API Integration

This directory contains the Alfresco-specific REST API mappings for the SOAP-to-REST bridge.

## Test Environment

- **Server**: `http://100.115.192.75:8080/alfresco`
- **Credentials**: admin/admin
- **Base API Path**: `/alfresco/api/-default-/public/alfresco/versions/1`
- **Purpose**: Live testing and development (feel free to create/modify/delete content)

## API Documentation

- **Swagger UI**: [api-explorer.alfresco.com](https://api-explorer.alfresco.com/api-explorer/?urls.primary)
- Interactive API exploration and testing

## Authentication

Basic Auth header: `Authorization: Basic base64(username:password)`

## Base Configuration

- **Content Type**: `application/json`
- **Base URL Pattern**: `/alfresco/api/-default-/public/alfresco/versions/1`

## Core Endpoints

### Repository Operations
- `/nodes/{nodeId}` - Node CRUD operations
- `/nodes/{nodeId}/children` - List folder contents
- `/nodes/{nodeId}/content` - Download/upload content
- `/nodes/{nodeId}?include=permissions,properties` - Get node with metadata

### Sites and Containers
- `/sites` - List all sites
- `/sites/{siteId}` - Get site details
- `/sites/{siteId}/containers` - List site containers
- `/sites/{siteId}/containers/documentLibrary` - Get document library

### Search
- `/queries/nodes` - Simple search
- `/search/versions/1/search` - Advanced search API

### Users and Groups
- `/people` - User management
- `/people/{personId}` - Individual user details
- `/groups` - Group management
- `/groups/{groupId}/members` - Group membership

## Method Mappings

Each SOAP method maps to specific Alfresco REST endpoints. See individual method files in this directory for detailed mappings:

- `DoDescribe.md` - Capabilities discovery
- `DoGetAvailableDatastores.md` - Site enumeration
- `DoGetDatastoreTypes.md` - Content type discovery
- `DoCrawl.md` - Content retrieval
- `DoGetChanges.md` - Incremental updates
- `DoItemData.md` - Full item details
- `DoRealtimeSecurityCheck.md` - Permission validation
- `DoGetUsers.md` - User listing
- `DoGetGroups.md` - Group listing
- `DoGetGroupsUsers.md` - Group membership
- `DoGetGroupsGroups.md` - Nested groups
- `DoGetServers.md` - Server discovery
- `DoGetWebServices.md` - Service endpoints

## Common Patterns

### Pagination
Most list endpoints support:
- `skipCount` - Number of items to skip (default: 0)
- `maxItems` - Maximum items to return (default: 100, max: 1000)

Example:
```bash
?skipCount=0&maxItems=50
```

### Include Parameters
Optimize responses by including specific data:
- `include=properties` - Include all properties
- `include=permissions` - Include permission data
- `include=path` - Include full path information

Example:
```bash
?include=properties,permissions,path
```

### Error Format
Standard error format:
```json
{
  "error": {
    "errorKey": "framework.exception.EntityNotFound",
    "statusCode": 404,
    "briefSummary": "Node not found",
    "stackTrace": "..."
  }
}
```

## Example Operations

### Test Connection
```bash
curl -u admin:admin \
  http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/people/-me-
```

### Browse Repository
```bash
# Get root folder
curl -u admin:admin \
  http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/nodes/-root-

# List children
curl -u admin:admin \
  http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/nodes/{nodeId}/children
```

### Search Content
```bash
curl -u admin:admin -X POST \
  http://100.115.192.75:8080/alfresco/api/-default-/public/search/versions/1/search \
  -H "Content-Type: application/json" \
  -d '{
    "query": {
      "query": "TYPE:\"cm:content\" AND content.mimetype:\"application/pdf\""
    },
    "paging": {"maxItems": 10}
  }'
```

### Download Content
```bash
curl -u admin:admin \
  http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/nodes/{nodeId}/content \
  -o downloaded-file.pdf
```