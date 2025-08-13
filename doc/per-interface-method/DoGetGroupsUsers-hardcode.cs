// Hardcoded implementation for iCustomConnector2.DoGetGroupsUsers() method
// Returns: StringArrayReturn
// Interface method: public abstract StringArrayReturn DoGetGroupsUsers(ConnectionInfo conn, Hashtable customparams, string groupId);

var response = new StringArrayReturn
{
    error = false,
    errorMsg = ""
};

// Return mock users based on the groupId parameter
var usersList = new List<string>();

switch (groupId?.ToUpper())
{
    case "GROUP_ALFRESCO_SEARCH_ADMINISTRATORS":
        // From the JSON transcript example
        usersList.Add("admin");
        break;
        
    case "GROUP_TEST":
        usersList.Add("testuser1");
        usersList.Add("testuser2");
        usersList.Add("testuser3");
        break;
        
    case "GROUP_ADMINISTRATORS":
        usersList.Add("admin");
        usersList.Add("sysadmin");
        usersList.Add("operator");
        break;
        
    case "GROUP_USERS":
        usersList.Add("user1");
        usersList.Add("user2");
        usersList.Add("user3");
        usersList.Add("user4");
        usersList.Add("user5");
        break;
        
    case "GROUP_DEVELOPERS":
        usersList.Add("dev1");
        usersList.Add("dev2");
        usersList.Add("dev3");
        usersList.Add("leaddev");
        break;
        
    case "GROUP_EMPTY":
        // Empty group - no users
        break;
        
    default:
        // For unknown groups, return a couple of generic users
        if (!string.IsNullOrEmpty(groupId))
        {
            usersList.Add($"user_{groupId.ToLower().Replace("group_", "")}");
            usersList.Add($"member_{groupId.ToLower().Replace("group_", "")}");
        }
        break;
}

response.values = usersList.ToArray();

return response;