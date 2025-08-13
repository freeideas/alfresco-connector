# DoGetGroupsGroups - External API Mapping

## Overview
Maps the SOAP `GetGroupsGroups2` request to external API calls for retrieving nested group membership (groups within groups).

## External API Endpoints

### Group Members
- **Path**: `/api/-default-/public/alfresco/versions/1/groups/{groupId}/members`
- **Method**: GET
- **Purpose**: List members of a specific group

### Query Parameters
- `skipCount` - Pagination offset
- `maxItems` - Maximum results
- `where` - Filter (e.g., memberType=GROUP)

## Request Mapping

### From SOAP to External API

| SOAP Field | External API | Notes |
|------------|--------------|-------|
| `groupIds[]` | `{groupId}` in path | Process each group |
| `conn.account` | Basic Auth username | Authentication |
| `conn.password` | Basic Auth password | Authentication |

## Response Mapping

### From External API to SOAP

| External API | SOAP Response | Notes |
|--------------|---------------|-------|
| Parent Group ID | `<parentGroupId>` | Container group |
| `entry.id` | `<childGroupId>` | Nested group identifier |
| `entry.displayName` | `<childGroupName>` | Nested group name |
| `entry.memberType` | Filter to GROUP only | Exclude users |

### Response Structure
```xml
<GroupNestingReturn>
  <error>false</error>
  <errorMsg />
  <nestings>
    <GroupNesting>
      <parentGroupId>GROUP_ALFRESCO_ADMINISTRATORS</parentGroupId>
      <childGroupId>GROUP_SITE_ADMINISTRATORS</childGroupId>
      <childGroupName>Site Administrators</childGroupName>
    </GroupNesting>
    <GroupNesting>
      <parentGroupId>GROUP_ALFRESCO_ADMINISTRATORS</parentGroupId>
      <childGroupId>GROUP_SYSTEM_ARCHITECTS</childGroupId>
      <childGroupName>System Architects</childGroupName>
    </GroupNesting>
  </nestings>
</GroupNestingReturn>
```

## Error Handling

| External API Error | SOAP Response |
|-------------------|---------------|
| 401 Unauthorized | `error=true`, `errorMsg="Authentication failed"` |
| 404 Not Found | Skip group, continue with others | Group doesn't exist |
| 500 Server Error | `error=true`, `errorMsg="External API error"` |

## Example External API Call

```bash
curl -u admin:admin \
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/groups/GROUP_ALFRESCO_ADMINISTRATORS/members?where=(memberType='GROUP')"
```

## Implementation Notes

1. Must call the API once for each parent group ID
2. Filter results to only include groups (memberType=GROUP)
3. Groups can contain both users and other groups
4. Nested groups inherit permissions from parent groups
5. Avoid infinite recursion when groups reference each other
6. The GROUP_ prefix is typically part of the group ID