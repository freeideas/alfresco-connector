# DoGetAvailableDatastores Method - Complete Implementation Guide

## Overview
The DoGetAvailableDatastores method retrieves a list of available data repositories or sites that can be crawled. Each datastore represents a separate content repository such as an Alfresco site or document library.

## Method Signature
```csharp
public abstract DataStoreInfoReturn DoGetAvailableDatastores(
    ConnectionInfo conn,
    Hashtable customparams,
    string filter1,
    string filter2,
    string filter3,
    string templateService,
    bool loadTemplates
);
```
**Location**: `doc/copy_src/ICustomConnectorInterfaces.cs:9`
**Returns**: `DataStoreInfoReturn` object containing array of available datastores

## Request Parameters

### Required Parameters
- **conn** (`ConnectionInfo`) - Connection information including credentials and server details
- **customparams** (`Hashtable`) - Custom parameters collection (often empty)
- **filter1** (`string`) - First filter parameter (implementation specific)
- **filter2** (`string`) - Second filter parameter (implementation specific) 
- **filter3** (`string`) - Third filter parameter (implementation specific)
- **templateService** (`string`) - Template service identifier (often empty)
- **loadTemplates** (`bool`) - Whether to load template information

### ConnectionInfo Properties
- `account` - Username for authentication
- `password` - Password for authentication
- `customparam` - Base URL for the external API server
- Additional connection parameters as configured

## Request Structure

### SOAP Request
```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetAvailableDatastores2 xmlns="http://tempuri.org/">
      <conn>
        <account>admin</account>
        <password>admin</password>
        <customparam>http://100.115.192.75:8080/</customparam>
      </conn>
      <customparams />
      <filter1></filter1>
      <filter2></filter2>
      <filter3></filter3>
      <templateService></templateService>
      <loadTemplates>false</loadTemplates>
    </GetAvailableDatastores2>
  </soap:Body>
</soap:Envelope>
```

### Processing Flow
1. **Receive SOAP Request**: Server receives HTTP POST with `GetAvailableDatastores2` envelope
2. **Parse Parameters**: Extracts connection info and filter parameters from XML
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Create Objects**: Instantiates ConnectionInfo and parameter objects from SOAP data
5. **Invoke Method**: Calls `DoGetAvailableDatastores()` with all parameters
6. **Return Response**: Serializes the `DataStoreInfoReturn` result to SOAP response

## Response Structure

### Return Type: DataStoreInfoReturn
Contains an array of `DataStoreInfo` objects, each representing an available datastore:

#### DataStoreInfo Properties
- `id` - Unique identifier for the datastore (e.g., site ID)
- `name` - Display name/title of the datastore
- `desc` - Description of the datastore
- `path` - Path or GUID for the datastore location
- `owner` - Owner username (often "admin")
- `server` - Server identifier or URL
- `MBFlagValue` - Legacy mailbox flag (typically false or 0)

### SOAP Response
```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetAvailableDatastores2Response xmlns="http://tempuri.org/">
      <GetAvailableDatastores2Result>
        <error>false</error>
        <errorMsg />
        <traceInfo />
        <datastores>
          <DataStoreInfo>
            <id>swsdp</id>
            <name>Sample: Web Site Design Project</name>
            <desc>Sample Alfresco site for collaboration</desc>
            <path>/sites/swsdp</path>
            <owner>admin</owner>
            <server>http://100.115.192.75:8080</server>
            <MBFlagValue>false</MBFlagValue>
          </DataStoreInfo>
          <DataStoreInfo>
            <id>test-site</id>
            <name>Test Site</name>
            <desc>Testing and development site</desc>
            <path>/sites/test-site</path>
            <owner>admin</owner>
            <server>http://100.115.192.75:8080</server>
            <MBFlagValue>false</MBFlagValue>
          </DataStoreInfo>
        </datastores>
      </GetAvailableDatastores2Result>
    </GetAvailableDatastores2Response>
  </soap:Body>
</soap:Envelope>
```

## Mock Response Example

### Typical Implementation Response
```csharp
public override DataStoreInfoReturn DoGetAvailableDatastores(
    ConnectionInfo conn, Hashtable customparams, string filter1, 
    string filter2, string filter3, string templateService, bool loadTemplates)
{
    try
    {
        var result = new DataStoreInfoReturn();
        var datastores = new List<DataStoreInfo>();
        
        // Add sample datastore representing an Alfresco site
        datastores.Add(new DataStoreInfo
        {
            id = "swsdp",
            name = "Sample: Web Site Design Project",
            desc = "Sample Alfresco site for web design collaboration",
            path = "/sites/swsdp",
            owner = "admin",
            server = conn.customparam,
            MBFlagValue = false
        });
        
        datastores.Add(new DataStoreInfo
        {
            id = "test-site",
            name = "Test Site",
            desc = "Site for testing and development",
            path = "/sites/test-site",
            owner = "admin", 
            server = conn.customparam,
            MBFlagValue = false
        });

        result.datastores = datastores.ToArray();
        result.error = false;
        result.errorMsg = "";
        
        return result;
    }
    catch (Exception ex)
    {
        return new DataStoreInfoReturn
        {
            error = true,
            errorMsg = $"Failed to retrieve datastores: {ex.Message}",
            datastores = new DataStoreInfo[0]
        };
    }
}
```

## External API Mapping

### Primary Endpoint
- **Path**: `/api/-default-/public/alfresco/versions/1/sites`
- **Method**: GET
- **Purpose**: List all available Alfresco sites

### Query Parameters
- `skipCount` - Number of items to skip (pagination)
- `maxItems` - Maximum items to return (default 100)
- `orderBy` - Sort field (e.g., title, description)

### Authentication Mapping
| SOAP Field | External API | Notes |
|------------|--------------|-------|
| `conn.account` | Basic Auth username | User credentials |
| `conn.password` | Basic Auth password | Password |
| `conn.customparam` | Base URL | Server endpoint |

### Response Mapping
| External API Field | SOAP Response | Notes |
|-------------------|---------------|-------|
| `entry.id` | `DataStoreInfo.id` | Site identifier |
| `entry.title` | `DataStoreInfo.name` | Display name |
| `entry.description` | `DataStoreInfo.desc` | Description |
| `entry.guid` | `DataStoreInfo.path` | Site GUID or path |
| Static "admin" | `DataStoreInfo.owner` | Owner (typically admin) |
| Server URL | `DataStoreInfo.server` | Base server URL |

### Example External API Call
```bash
curl -u admin:admin \
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/sites?maxItems=100"
```

### Example External API Response
```json
{
  "list": {
    "pagination": {
      "count": 2,
      "hasMoreItems": false,
      "totalItems": 2
    },
    "entries": [
      {
        "entry": {
          "role": "SiteManager",
          "visibility": "PUBLIC",
          "guid": "b4cff62a-664d-4d45-9302-98723eac1319",
          "description": "This is a Sample Alfresco Team site.",
          "id": "swsdp",
          "preset": "site-dashboard",
          "title": "Sample: Web Site Design Project"
        }
      }
    ]
  }
}
```

## Error Handling
| External API Error | SOAP Response |
|-------------------|---------------|
| 401 Unauthorized | `error=true`, `errorMsg="Authentication failed"` |
| 403 Forbidden | `error=true`, `errorMsg="Access denied to sites"` |
| 404 Not Found | `error=true`, `errorMsg="Sites endpoint not found"` |
| 500 Server Error | `error=true`, `errorMsg="External API error: [details]"` |
| Connection Timeout | `error=true`, `errorMsg="Connection timeout"` |

## Implementation Notes

1. **Site Mapping**: External API sites map directly to SOAP datastores
2. **Permission Filtering**: Only sites accessible to the authenticated user are returned
3. **Private Sites**: Private sites may not be visible depending on user permissions
4. **Template Support**: The `loadTemplates` parameter can be used to include template information
5. **Filtering**: The filter1-3 parameters can be used to filter sites by name, description, or type
6. **Pagination**: For large numbers of sites, implement pagination using skipCount/maxItems
7. **Caching**: Consider caching datastore lists for performance, especially for frequently called requests
8. **Error Recovery**: Handle network errors gracefully and provide meaningful error messages

## Important Notes

- Sites represent the primary organizational unit in most content management systems
- **CRITICAL**: The datastore ID returned here is a **site ID** (e.g., "swsdp"), NOT a node ID
- When using this ID in subsequent operations (like Crawl2), you must first convert it to a documentLibrary node ID
- See `Crawl2.md` for the correct pattern: fetch `/sites/{siteId}/containers/documentLibrary` to get the actual node ID
- Ensure proper authentication before making external API calls
- Handle empty responses gracefully (return empty array rather than null)
- The MBFlagValue field is legacy and typically set to false for modern implementations
- Server URL in the response should match the base URL used for API calls