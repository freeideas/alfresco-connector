# DoGetUsers - External API Mapping

## Overview
Maps the SOAP `GetUsers2` request to external API calls for retrieving user information.

## External API Endpoints

### Primary Endpoint
- **Path**: `/api/-default-/public/alfresco/versions/1/people`
- **Method**: GET
- **Purpose**: List all users in the system

### Query Parameters
- `skipCount` - Number of items to skip
- `maxItems` - Maximum items to return (default: 100)
- `orderBy` - Sort field (e.g., firstName, lastName)

## Request Mapping

### From SOAP to External API

| SOAP Field | External API | Notes |
|------------|--------------|-------|
| `conn.account` | Basic Auth username | Authentication |
| `conn.password` | Basic Auth password | Authentication |
| `conn.customparam` | Base URL | Server location |
| `customparams["SelectedDataStores"]` | Filter context | Optional filtering |

### Authentication
- Use Basic Authentication header
- Format: `Authorization: Basic base64(username:password)`

## Response Mapping

### From External API to SOAP

| External API | SOAP Response | Notes |
|--------------|---------------|-------|
| `entry.id` | `<uniqueid>` | User identifier |
| `entry.firstName + entry.lastName` | `<name>` | Full name |
| `entry.email` | `<info>` CustomPair with key="Email" | Email in info collection |
| `entry.firstName` | `<info>` CustomPair with key="FirstName" | First name in info |
| `entry.lastName` | `<info>` CustomPair with key="LastName" | Last name in info |
| `entry.enabled` | `<active>` | Account status |

### Response Structure
```xml
<SystemIdentityInfo>
  <uniqueid>admin</uniqueid>
  <name>Administrator</name>
  <active>true</active>
  <info>
    <CustomPair>
      <key>Email</key>
      <value>admin@example.com</value>
    </CustomPair>
    <CustomPair>
      <key>FirstName</key>
      <value>Admin</value>
    </CustomPair>
    <CustomPair>
      <key>LastName</key>
      <value>User</value>
    </CustomPair>
  </info>
</SystemIdentityInfo>
```

## Error Handling

| External API Error | SOAP Response |
|-------------------|---------------|
| 401 Unauthorized | `error=true`, `errorMsg="Authentication failed"` |
| 403 Forbidden | `error=true`, `errorMsg="Access denied"` |
| 500 Server Error | `error=true`, `errorMsg="External API error"` |

## Example External API Call

```bash
curl -u admin:admin \
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/people?maxItems=100"
```

## Implementation Notes

1. The external API returns paginated results
2. Combine firstName and lastName for SOAP name field
3. The `enabled` field maps to `active` in SOAP
4. Additional user properties may be available via `include=properties`
5. For large user bases, implement pagination handling