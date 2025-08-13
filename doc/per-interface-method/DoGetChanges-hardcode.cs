// Example hardcoded return values for GetChanges method
// DoGetChanges returns CrawlReturn

var response = new CrawlReturn
{
    error = false,
    errorMsg = "",
    moreExist = false,
    nextStartId = "",
    nextStartDate = DateTime.Now,
    items = new[]
    {
        new CrawlReturnItem
        {
            id = "DOC001",
            subid = "",
            type = "cm:content",
            title = "Updated Project Charter",
            path = "/projects/swsdp/documents/charter.docx",
            lastModified = DateTime.Now.AddDays(-1),
            deleted = false
        },
        new CrawlReturnItem
        {
            id = "DOC002",
            subid = "",
            type = "cm:content",
            title = "Revised Design Specifications",
            path = "/projects/swsdp/documents/design-specs.pdf",
            lastModified = DateTime.Now.AddDays(-2),
            deleted = false
        },
        new CrawlReturnItem
        {
            id = "IMG001",
            subid = "",
            type = "cm:content",
            title = "New Logo Design",
            path = "/projects/swsdp/images/logo-v2.png",
            lastModified = DateTime.Now.AddDays(-3),
            deleted = false
        },
        new CrawlReturnItem
        {
            id = "DOC003",
            subid = "",
            type = "cm:content",
            title = "Old Requirements Document",
            path = "/projects/swsdp/documents/old-requirements.doc",
            lastModified = DateTime.Now.AddDays(-1),
            deleted = true  // Deleted item
        }
    }
};

// Note: In actual implementation, you might check the lastUpdate parameter:
// if (lastUpdate < new DateTime(2025, 1, 1))
// {
//     response.error = true;
//     response.errorMsg = "DoGetChanges requires a recent lastUpdate date.";
//     response.items = Array.Empty<CrawlReturnItem>();
// }