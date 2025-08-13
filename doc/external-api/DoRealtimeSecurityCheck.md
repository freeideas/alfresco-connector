# DoRealtimeSecurityCheck - External API Mapping

## Overview
Maps the SOAP `RealtimeSecurityCheck` request to external API calls for real-time permission validation.

## External API Endpoints

### Permission Check
- **Path**: `/api/-default-/public/alfresco/versions/1/nodes/{nodeId}?include=permissions`
- **Method**: GET
- **Purpose**: Get current permissions for a node

### Alternative: Direct Permission API
- **Path**: `/api/-default-/public/alfresco/versions/1/nodes/{nodeId}/permissions`
- **Method**: GET
- **Purpose**: Get detailed permission information

## Request Mapping

### From SOAP to External API

| SOAP Field | External API | Notes |
|------------|--------------|-------|
| `itemIds[]` | Multiple `{nodeId}` calls | Check each item |
| `userIds[]` | Filter permissions by user | Check specific users |
| `conn.account` | Basic Auth username | Authentication |
| `conn.password` | Basic Auth password | Authentication |

## Response Mapping

### From External API to SOAP

| External API | SOAP Response | Notes |
|--------------|---------------|-------|
| `permissions.inherited` | Evaluate access | Check inheritance |
| `permissions.locallySet` | Direct permissions | Explicit permissions |
| `allowableOperations[]` | Map to allow/deny | Convert to boolean |
| Error response | `<hasAccess>false</hasAccess>` | No access on error |

### Response Structure
```xml
<SecurityCheckReturn>
  <error>false</error>
  <errorMsg />
  <results>
    <SecurityCheckResult>
      <itemId>12345678-1234-1234-1234-123456789012</itemId>
      <userId>admin</userId>
      <hasAccess>true</hasAccess>
      <permissions>
        <Permission>READ</Permission>
        <Permission>WRITE</Permission>
      </permissions>
    </SecurityCheckResult>
    <SecurityCheckResult>
      <itemId>87654321-4321-4321-4321-210987654321</itemId>
      <userId>user1</userId>
      <hasAccess>false</hasAccess>
      <permissions />
    </SecurityCheckResult>
  </results>
</SecurityCheckReturn>
```

## Permission Mapping

| External API Permission | SOAP Permission | Notes |
|------------------------|-----------------|-------|
| `Consumer` | READ | View content |
| `Contributor` | WRITE | Edit content |
| `Collaborator` | WRITE | Edit and delete |
| `Coordinator` | FULL_CONTROL | Full access |
| `Editor` | WRITE | Edit permissions |

## Error Handling

| External API Error | SOAP Response |
|-------------------|---------------|
| 401 Unauthorized | `error=true`, `errorMsg="Authentication failed"` |
| 404 Not Found | `hasAccess=false` for that item | Item doesn't exist |
| 403 Forbidden | `hasAccess=false` | No permission |
| 500 Server Error | `error=true`, `errorMsg="External API error"` |

## Example External API Call

```bash
curl -u admin:admin \
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/nodes/12345678-1234-1234-1234-123456789012?include=permissions,allowableOperations"
```

## Implementation Notes

1. Must check permissions for each item/user combination
2. May require multiple API calls (one per node)
3. Cache results for performance if checking many items
4. The allowableOperations array indicates what the current user can do
5. For checking other users' permissions, may need admin privileges
6. Consider batch processing for multiple items
7. Real-time checks should be fast - implement timeouts