# DoGetDatastoreTypes Method - Complete Implementation Guide

## Overview
The DoGetDatastoreTypes method retrieves available content types for a specific datastore. Content types define the schema and metadata structure for documents and folders within the repository.

## Method Signature
```csharp
public abstract DataStoreTypeReturn DoGetDatastoreTypes(
    ConnectionInfo conn,
    Hashtable customparams,
    DataStoreInfo datastore
);
```
**Location**: `doc/copy_src/ICustomConnectorInterfaces.cs:10`
**Returns**: `DataStoreTypeReturn` object containing array of available content types

## Request Parameters

### Required Parameters
- **conn** (`ConnectionInfo`) - Connection information including credentials and server details
- **customparams** (`Hashtable`) - Custom parameters collection (often empty)
- **datastore** (`DataStoreInfo`) - Datastore to query for types (may be null for global types)

### ConnectionInfo Properties
- `account` - Username for authentication
- `password` - Password for authentication
- `customparam` - Base URL for the external API server

### DataStoreInfo Properties (if specified)
- `id` - Datastore identifier (site ID)
- `name` - Datastore display name
- Additional datastore metadata

## Request Structure

### SOAP Request
```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetDatastoreTypes2 xmlns="http://tempuri.org/">
      <conn>
        <account>admin</account>
        <password>admin</password>
        <customparam>http://100.115.192.75:8080/</customparam>
      </conn>
      <customparams />
      <datastore>
        <id>swsdp</id>
        <name>Sample: Web Site Design Project</name>
      </datastore>
    </GetDatastoreTypes2>
  </soap:Body>
</soap:Envelope>
```

### Processing Flow
1. **Receive SOAP Request**: Server receives HTTP POST with `GetDatastoreTypes2` envelope
2. **Parse Parameters**: Extracts connection info and datastore information from XML
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Create Objects**: Instantiates ConnectionInfo and DataStoreInfo objects from SOAP data
5. **Invoke Method**: Calls `DoGetDatastoreTypes()` with parameters
6. **Return Response**: Serializes the `DataStoreTypeReturn` result to SOAP response

## Response Structure

### Return Type: DataStoreTypeReturn
Contains an array of `DataStoreType` objects, each representing an available content type:

#### DataStoreType Properties
- `id` - Unique type identifier (e.g., "cm:content", "cm:folder")
- `name` - Display name for the type
- `description` - Description of the content type
- `parentType` - Parent type identifier (inheritance)
- `properties` - Array of property definitions for metadata fields

#### Property Definition
Each property contains:
- `name` - Property identifier (e.g., "cm:title")
- `type` - Data type (string, date, boolean, etc.)
- `required` - Whether the property is required
- `description` - Property description

### SOAP Response
```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetDatastoreTypes2Response xmlns="http://tempuri.org/">
      <GetDatastoreTypes2Result>
        <error>false</error>
        <errorMsg />
        <traceInfo />
        <types>
          <DataStoreType>
            <id>cm:content</id>
            <name>Content</name>
            <description>General content type for files and documents</description>
            <parentType>cm:cmobject</parentType>
            <properties>
              <Property>
                <name>cm:name</name>
                <type>string</type>
                <required>true</required>
                <description>File or folder name</description>
              </Property>
              <Property>
                <name>cm:title</name>
                <type>string</type>
                <required>false</required>
                <description>Display title</description>
              </Property>
              <Property>
                <name>cm:description</name>
                <type>string</type>
                <required>false</required>
                <description>Content description</description>
              </Property>
            </properties>
          </DataStoreType>
          <DataStoreType>
            <id>cm:folder</id>
            <name>Folder</name>
            <description>Container for organizing content</description>
            <parentType>cm:cmobject</parentType>
            <properties>
              <Property>
                <name>cm:name</name>
                <type>string</type>
                <required>true</required>
                <description>Folder name</description>
              </Property>
            </properties>
          </DataStoreType>
        </types>
      </GetDatastoreTypes2Result>
    </GetDatastoreTypes2Response>
  </soap:Body>
</soap:Envelope>
```

## Mock Response Example

### Typical Implementation Response
```csharp
public override DataStoreTypeReturn DoGetDatastoreTypes(
    ConnectionInfo conn, Hashtable customparams, DataStoreInfo datastore)
{
    try
    {
        var result = new DataStoreTypeReturn();
        var types = new List<DataStoreType>();
        
        // Content type for files
        types.Add(new DataStoreType
        {
            id = "cm:content",
            name = "Content",
            description = "General content type for files and documents",
            parentType = "cm:cmobject",
            properties = new PropertyDefinition[]
            {
                new PropertyDefinition
                {
                    name = "cm:name",
                    type = "string",
                    required = true,
                    description = "File or folder name"
                },
                new PropertyDefinition
                {
                    name = "cm:title",
                    type = "string",
                    required = false,
                    description = "Display title"
                },
                new PropertyDefinition
                {
                    name = "cm:description",
                    type = "string",
                    required = false,
                    description = "Content description"
                },
                new PropertyDefinition
                {
                    name = "cm:created",
                    type = "datetime",
                    required = false,
                    description = "Creation date"
                }
            }
        });
        
        // Folder type
        types.Add(new DataStoreType
        {
            id = "cm:folder",
            name = "Folder",
            description = "Container for organizing content",
            parentType = "cm:cmobject",
            properties = new PropertyDefinition[]
            {
                new PropertyDefinition
                {
                    name = "cm:name",
                    type = "string",
                    required = true,
                    description = "Folder name"
                },
                new PropertyDefinition
                {
                    name = "cm:title",
                    type = "string",
                    required = false,
                    description = "Display title"
                }
            }
        });

        result.types = types.ToArray();
        result.error = false;
        result.errorMsg = "";
        
        return result;
    }
    catch (Exception ex)
    {
        return new DataStoreTypeReturn
        {
            error = true,
            errorMsg = $"Failed to retrieve datastore types: {ex.Message}",
            types = new DataStoreType[0]
        };
    }
}
```

## External API Mapping

### Primary Endpoint
- **Path**: `/api/-default-/public/alfresco/versions/1/types`
- **Method**: GET
- **Purpose**: List available content type definitions

**IMPORTANT Implementation Note**: When using HttpClient with a BaseAddress, the request URI must start with a leading slash (`/`). For example:
```csharp
// ✅ CORRECT - includes leading slash
var requestUri = "/alfresco/api/-default-/public/alfresco/versions/1/types?include=properties&maxItems=100";

// ❌ WRONG - missing leading slash will cause 404 Not Found
var requestUri = "alfresco/api/-default-/public/alfresco/versions/1/types?include=properties&maxItems=100";
```

### Query Parameters
- `skipCount` - Number of items to skip (pagination)
- `maxItems` - Maximum items to return
- `include` - Include additional data (`properties` to get property definitions)

### Authentication Mapping
| SOAP Field | External API | Notes |
|------------|--------------|-------|
| `conn.account` | Basic Auth username | User credentials |
| `conn.password` | Basic Auth password | Password |
| `conn.customparam` | Base URL | Server endpoint |

### Response Mapping
| External API Field | SOAP Response | Notes |
|-------------------|---------------|-------|
| `entry.id` | `DataStoreType.id` | Type identifier (e.g., cm:content) |
| `entry.title` | `DataStoreType.name` | Display name |
| `entry.description` | `DataStoreType.description` | Type description |
| `entry.parent.id` | `DataStoreType.parentType` | Parent type in hierarchy |
| `entry.properties` | `DataStoreType.properties` | Metadata property definitions |

**CRITICAL Data Type Mapping**: The SOAP client only accepts these MetaData.dataType values:
- `String`
- `Integer` 
- `Double`
- `Date`
- `Bool`

⚠️ **DO NOT** use `Long` or `Float` - these will cause XML deserialization errors!

Map external API data types as follows:
```csharp
// External API → SOAP dataType mapping
"d:text"     → "String"
"d:mltext"   → "String"
"d:content"  → "String"
"d:int"      → "Integer"
"d:long"     → "Double"    // NOT "Long" - will cause error!
"d:float"    → "Double"    // NOT "Float" - will cause error!
"d:double"   → "Double"
"d:date"     → "Date"
"d:datetime" → "Date"
"d:boolean"  → "Bool"
"d:noderef"  → "String"
"d:category" → "String"
"d:any"      → "String"
default      → "String"
```

Failure to map these correctly will result in errors like:
```
Instance validation error: 'Long' is not a valid value for MetaDataTypeEnum
```

### Example External API Call
```bash
curl -u admin:admin \
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/types?include=properties&maxItems=100"
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
          "id": "cm:content",
          "title": "Content",
          "description": "General content type",
          "parent": {
            "id": "cm:cmobject"
          },
          "properties": [
            {
              "id": "cm:name",
              "title": "Name",
              "dataType": "d:text",
              "mandatory": true
            },
            {
              "id": "cm:title",
              "title": "Title",
              "dataType": "d:text",
              "mandatory": false
            }
          ]
        }
      },
      {
        "entry": {
          "id": "cm:folder",
          "title": "Folder",
          "description": "Folder type",
          "parent": {
            "id": "cm:cmobject"
          },
          "properties": [
            {
              "id": "cm:name",
              "title": "Name",
              "dataType": "d:text",
              "mandatory": true
            }
          ]
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
| 403 Forbidden | `error=true`, `errorMsg="Access denied to content types"` |
| 404 Not Found | `error=true`, `errorMsg="Content types endpoint not found"` |
| 500 Server Error | `error=true`, `errorMsg="External API error: [details]"` |

## Implementation Notes

1. **Global vs Datastore-specific Types**: Content types are typically global to the system, though some implementations may filter by datastore
2. **Type Hierarchy**: Content types have inheritance relationships (parentType field)
3. **Property Metadata**: Each type defines what metadata properties are available and whether they're required
4. **Common Types**: Most systems will have at least `cm:content` (files) and `cm:folder` (containers)
5. **Custom Types**: Organizations may define custom content types with specific metadata requirements
6. **Property Types**: Map external API property types to SOAP-compatible types (string, datetime, boolean, etc.)
7. **Filtering**: The datastore parameter can be used to filter types relevant to a specific repository
8. **Caching**: Content type information changes rarely, so caching is recommended for performance

## Important Notes

- Content types define the metadata schema available during crawling
- Type information is used by crawling systems to understand what properties to extract
- The type hierarchy allows for inheritance of properties from parent types
- Property requirements help validate metadata completeness during indexing
- Custom types should include all relevant properties for search and filtering
- Type information is typically static but may vary between different external systems