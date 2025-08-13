# DoGetServers - External API Mapping

## Overview
Maps the SOAP `GetServers2` request to external API calls for retrieving server endpoint information.

## External API Endpoints

### Server Discovery
- **Path**: `/api/-default-/public/alfresco/versions/1/discovery`
- **Method**: GET
- **Purpose**: Get server information and endpoints

## Request Mapping

### From SOAP to External API

| SOAP Field | External API | Notes |
|------------|--------------|-------|
| `conn.account` | Basic Auth username | Authentication |
| `conn.password` | Basic Auth password | Authentication |
| `conn.customparam` | Use as base URL | Server location |

## Response Mapping

### From External API to SOAP

The response is typically constructed from configuration and discovery information:

| Source | SOAP Response | Notes |
|--------|---------------|-------|
| Config | `<id>` | Server identifier |
| Config | `<name>` | Server display name |
| `conn.customparam` | `<url>` | Server base URL |
| Discovery API | `<version>` | Server version |
| Static | `<type>` | "REST API" or similar |

### Response Structure
```xml
<ServerInfoReturn>
  <error>false</error>
  <errorMsg />
  <servers>
    <ServerInfo>
      <id>primary</id>
      <name>Primary External API Server</name>
      <url>http://100.115.192.75:8080/alfresco</url>
      <version>7.0.0</version>
      <type>REST API</type>
      <description>Main content repository</description>
      <active>true</active>
    </ServerInfo>
  </servers>
</ServerInfoReturn>
```

## Error Handling

| External API Error | SOAP Response |
|-------------------|---------------|
| 401 Unauthorized | `error=true`, `errorMsg="Authentication failed"` |
| Connection failed | `error=true`, `errorMsg="Server unreachable"` |
| 500 Server Error | `error=true`, `errorMsg="External API error"` |

## Example External API Call

```bash
curl -u admin:admin \
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/discovery"
```

## Implementation Notes

1. This method typically returns configuration rather than dynamic data
2. May return multiple servers if configured for federation
3. The URL should be the base API endpoint
4. Version information helps with compatibility checks
5. The active flag indicates if the server is currently available
6. This is often called during connector initialization