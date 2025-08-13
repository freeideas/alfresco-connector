// Example hardcoded return values for GetUsers2 method
// DoGetUsers returns SystemIdentityInfoReturn

var response = new SystemIdentityInfoReturn
{
    error = false,
    errorMsg = "",
    identities = new[]
    {
        new SystemIdentityInfo
        {
            id = "abeecher",
            name = "abeecher",
            displayName = "Alice Beecher",
            email = "abeecher@example.com"
        },
        new SystemIdentityInfo
        {
            id = "admin",
            name = "admin",
            displayName = "Administrator",
            email = "admin@alfresco.com"
        },
        new SystemIdentityInfo
        {
            id = "guest",
            name = "guest",
            displayName = "Guest",
            email = ""
        },
        new SystemIdentityInfo
        {
            id = "mjackson",
            name = "mjackson",
            displayName = "Mike Jackson",
            email = "mjackson@example.com"
        }
    }
};