# DoGetGroupsUsers - External API Mapping

## Overview
Maps the SOAP `GetGroupsUsers2` request to external API calls for retrieving group membership (users in groups).

## External API Endpoints

### Group Members
- **Path**: `/api/-default-/public/alfresco/versions/1/groups/{groupId}/members`
- **Method**: GET
- **Purpose**: List members of a specific group

### Query Parameters
- `skipCount` - Pagination offset
- `maxItems` - Maximum results
- `where` - Filter (e.g., memberType=PERSON)

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
| Group ID | `<groupId>` | Group identifier |
| `entry.id` | `<userId>` | User identifier |
| `entry.displayName` | `<userName>` | User display name |
| `entry.memberType` | Filter to PERSON only | Exclude subgroups |

### Response Structure
```xml
<GroupMembershipReturn>
  <error>false</error>
  <errorMsg />
  <memberships>
    <GroupMembership>
      <groupId>GROUP_ALFRESCO_ADMINISTRATORS</groupId>
      <userId>admin</userId>
      <userName>Administrator</userName>
    </GroupMembership>
    <GroupMembership>
      <groupId>GROUP_ALFRESCO_ADMINISTRATORS</groupId>
      <userId>alice</userId>
      <userName>Alice Smith</userName>
    </GroupMembership>
    <GroupMembership>
      <groupId>GROUP_SITE_COLLABORATORS</groupId>
      <userId>bob</userId>
      <userName>Bob Jones</userName>
    </GroupMembership>
  </memberships>
</GroupMembershipReturn>
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
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/groups/GROUP_ALFRESCO_ADMINISTRATORS/members?where=(memberType='PERSON')"
```

## Implementation Notes

1. Must call the API once for each group ID provided
2. Filter results to only include users (memberType=PERSON)
3. Groups can contain both users and other groups
4. Large groups may require pagination
5. The GROUP_ prefix is typically part of the group ID
6. Batch multiple group queries for efficiency