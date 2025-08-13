# DoGetGroups - External API Mapping

## Overview
Maps the SOAP `GetGroups2` request to external API calls for retrieving group information.

## External API Endpoints

### Primary Endpoint
- **Path**: `/api/-default-/public/alfresco/versions/1/groups`
- **Method**: GET
- **Purpose**: List all groups in the system

### Query Parameters
- `skipCount` - Number of items to skip
- `maxItems` - Maximum items to return
- `orderBy` - Sort field

## Request Mapping

### From SOAP to External API

| SOAP Field | External API | Notes |
|------------|--------------|-------|
| `conn.account` | Basic Auth username | Authentication |
| `conn.password` | Basic Auth password | Authentication |
| `conn.customparam` | Base URL | Server location |
| `customparams` | Not used | No group filtering in basic call |

## Response Mapping

### From External API to SOAP

| External API | SOAP Response | Notes |
|--------------|---------------|-------|
| `entry.id` | `<uniqueid>` | Group identifier |
| `entry.displayName` | `<name>` | Group display name |
| Always true | `<active>` | Groups always active |
| Empty | `<info>` | No additional info for groups |

### Response Structure
```xml
<SystemIdentityInfo>
  <uniqueid>GROUP_ALFRESCO_ADMINISTRATORS</uniqueid>
  <name>ALFRESCO_ADMINISTRATORS</name>
  <active>true</active>
  <info />
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
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/groups?maxItems=100"
```

## Implementation Notes

1. Groups use the prefix "GROUP_" in their IDs
2. System groups cannot be modified
3. Groups don't have email addresses like users
4. The info collection is typically empty for groups
5. All groups are considered active (no disabled state)