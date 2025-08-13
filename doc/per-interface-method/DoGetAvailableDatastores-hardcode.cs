// Example hardcoded return values for GetAvailableDatastores2 method
// DoGetAvailableDatastores returns DataStoreInfoReturn

var response = new DataStoreInfoReturn
{
    error = false,
    errorMsg = "",
    datastores = new[]
    {
        new DataStoreInfo
        {
            id = "swsdp",
            name = "Sample: Web Site Design Project",
            desc = "",
            path = ""
        },
        new DataStoreInfo
        {
            id = "alfresco_repo",
            name = "Alfresco Repository",
            desc = "Main document repository",
            path = "/alfresco"
        },
        new DataStoreInfo
        {
            id = "shared_docs",
            name = "Shared Documents",
            desc = "Company-wide shared documents",
            path = "/shared"
        }
    }
};

// Example with template-based datastore
var templateDatastore = new DataStoreInfo
{
    id = "template_01",
    name = "Template: Project Workspace",
    desc = "Standard project workspace template",
    path = "/templates/project"
};