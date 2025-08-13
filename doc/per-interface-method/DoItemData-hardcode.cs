// Example hardcoded return values for ItemData2 method
// DoItemData returns ItemReturn

var response = new ItemReturn
{
    id = "a38308f8-6f30-4d8a-8576-eaf6703fb9d3",
    subid = "",
    type = "lnk:link",
    title = "W3 Schools",
    author = "Mike Jackson",
    lastModified = DateTime.Parse("2011-02-15T21:43:14.377+00:00"),
    created = DateTime.Parse("2011-02-15T21:43:14.377+00:00"),
    contentType = "text/html",
    size = 1024,
    content = System.Text.Encoding.UTF8.GetBytes("http://www.w3schools.com/"), // Simple URL as content for link type
    contentUrl = "http://100.115.192.75:8080/share/page/site/swsdp/document-details?nodeRef=workspace://SpacesStore/a38308f8-6f30-4d8a-8576-eaf6703fb9d3",
    metadata = new Hashtable
    {
        ["lnk_url"] = "http://www.w3schools.com/",
        ["cm_owner"] = "admin",
        ["lnk_description"] = "The W3 Schools web site has some good guides (with interactive examples) on how to create websites",
        ["lnk_title"] = "W3 Schools",
        ["FoldersRelativePath"] = "/Company Home/Sites/swsdp/links",
        ["PARENT_ID"] = "0e24b99c-41f0-43e1-a55e-fb9f50d73820",
        ["CM_CREATED"] = DateTime.Parse("2011-02-15T21:43:14.377+00:00")
    },
    allowedUsers = new[] { "admin", "mjackson" },
    deniedUsers = Array.Empty<string>(),
    allowedGroups = new[] { "EVERYONE", "site_swsdp_SiteContributor" },
    deniedGroups = Array.Empty<string>(),
    error = false,
    errorMsg = ""
};

// Alternative example for a regular document
var documentExample = new ItemReturn
{
    id = "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7",
    subid = "",
    type = "cm:content",
    title = "Sample Document",
    author = "Administrator",
    lastModified = DateTime.Parse("2011-02-15T21:35:26.467+00:00"),
    created = DateTime.Parse("2011-02-15T21:35:26.467+00:00"),
    contentType = "text/plain",
    size = 2048,
    content = System.Text.Encoding.UTF8.GetBytes("This is the content of the sample document."),
    contentUrl = "http://100.115.192.75:8080/alfresco/d/d/workspace/SpacesStore/5fa74ad3-9b5b-461b-9df5-de407f1f4fe7/sample.txt",
    metadata = new Hashtable
    {
        ["cm_name"] = "sample.txt",
        ["cm_description"] = "A sample text document",
        ["cm_owner"] = "admin",
        ["FoldersRelativePath"] = "/Company Home/Documents",
        ["PARENT_ID"] = "document-library-root"
    },
    allowedUsers = new[] { "admin" },
    deniedUsers = Array.Empty<string>(),
    allowedGroups = new[] { "EVERYONE" },
    deniedGroups = Array.Empty<string>(),
    error = false,
    errorMsg = ""
};