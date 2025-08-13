# DoGetChanges - External API Mapping

## Overview
Maps the SOAP `GetChanges` request to external API calls for retrieving incremental changes (security/permissions).

## External API Endpoints

### Audit Log Endpoint
- **Path**: `/api/-default-/public/alfresco/versions/1/audit-applications/{auditAppId}/audit-entries`
- **Method**: GET
- **Purpose**: Get audit entries for permission changes

### Activities Endpoint (Alternative)
- **Path**: `/api/-default-/public/alfresco/versions/1/activities`
- **Method**: GET
- **Purpose**: Get recent activities

### Query Parameters
- `where` - Filter clause (e.g., `(createdAt > '2024-01-01T00:00:00.000Z')`)
- `skipCount` - Pagination offset
- `maxItems` - Maximum results

## Request Mapping

### From SOAP to External API

| SOAP Field | External API | Notes |
|------------|--------------|-------|
| `lastUpdate` | `where` clause with date | Filter by modification date |
| `datastore.id` | Site ID for filtering | Site context (may need conversion to node ID) |
| `maxReturns` | `maxItems` | Result limit |
| `conn.account` | Basic Auth username | Authentication |
| `conn.password` | Basic Auth password | Authentication |

## Response Mapping

### From External API to SOAP

| External API | SOAP Response | Notes |
|--------------|---------------|-------|
| `entry.id` | `<itemId>` | Node/item identifier |
| `entry.createdAt` | `<changeDate>` | When change occurred |
| `entry.createdByUser.id` | `<changedBy>` | User who made change |
| Permission data | `<permissions>` | ACL entries |
| `pagination.hasMoreItems` | `<moreExist>` | More changes available |

### Response Structure
```xml
<SecurityChangesReturn>
  <error>false</error>
  <errorMsg />
  <changes>
    <SecurityChange>
      <itemId>12345678-1234-1234-1234-123456789012</itemId>
      <changeDate>2024-01-15T10:30:00Z</changeDate>
      <changedBy>admin</changedBy>
      <changeType>PERMISSION_MODIFIED</changeType>
      <permissions>
        <SecurityItem>
          <id>admin</id>
          <type>USER</type>
          <permission>WRITE</permission>
          <allow>true</allow>
        </SecurityItem>
      </permissions>
    </SecurityChange>
  </changes>
  <moreExist>true</moreExist>
  <nextStartDate>2024-01-15T11:00:00Z</nextStartDate>
</SecurityChangesReturn>
```

## Error Handling

| External API Error | SOAP Response |
|-------------------|---------------|
| 401 Unauthorized | `error=true`, `errorMsg="Authentication failed"` |
| 404 Not Found | `error=true`, `errorMsg="Audit app not found"` |
| 500 Server Error | `error=true`, `errorMsg="External API error"` |

## Example External API Call

```bash
curl -u admin:admin \
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/audit-applications/alfresco-access/audit-entries?where=(createdAt>'2024-01-01T00:00:00.000Z')&maxItems=100"
```

## Implementation Notes

1. Permission changes may need to be tracked via audit logs
2. Not all external APIs support incremental permission tracking
3. May need to combine multiple API calls to get complete change data
4. The changeType can be: PERMISSION_ADDED, PERMISSION_REMOVED, PERMISSION_MODIFIED
5. Track the last change date for next incremental request
6. Consider using activities API if audit is not available
7. **NOTE**: If filtering by `datastore.id`, remember it's a site ID that may need conversion to node IDs for querying