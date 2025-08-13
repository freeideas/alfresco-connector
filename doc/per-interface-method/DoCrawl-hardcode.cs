// Example hardcoded return values for Crawl2 method
// DoCrawl returns CrawlReturn

var response = new CrawlReturn
{
    error = false,
    errorMsg = "",
    moreExist = true,
    nextStartId = "next_page_token",
    nextStartDate = DateTime.Parse("2011-02-16T10:30:10.663Z"),
    items = new[]
    {
        new CrawlReturnItem
        {
            id = "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7",
            subid = "",
            type = "cm:content",
            title = "Sample Document.txt",
            path = "/Documents/Sample Document.txt",
            lastModified = DateTime.Parse("2011-02-15T21:35:26.467+00:00"),
            deleted = false
        },
        new CrawlReturnItem
        {
            id = "a38308f8-6f30-4d8a-8576-eaf6703fb9d3",
            subid = "",
            type = "lnk:link",
            title = "External Link",
            path = "/Links/External Link",
            lastModified = DateTime.Parse("2011-02-15T21:43:14.377+00:00"),
            deleted = false
        },
        new CrawlReturnItem
        {
            id = "42226a03-34a8-43b0-bb37-d86cd09353f7",
            subid = "",
            type = "fm:post",
            title = "Forum Post",
            path = "/Forum/Discussion/Forum Post",
            lastModified = DateTime.Parse("2011-02-15T22:05:46.902+00:00"),
            deleted = false
        },
        new CrawlReturnItem
        {
            id = "4b9ebe73-7b19-4aaf-b596-5e545544e2a6",
            subid = "",
            type = "dl:issue",
            title = "Issue #123",
            path = "/DataLists/Issues/Issue #123",
            lastModified = DateTime.Parse("2011-02-15T22:15:49.142+00:00"),
            deleted = false
        },
        new CrawlReturnItem
        {
            id = "8d4429e7-804f-43cf-bd81-288e561db9a8",
            subid = "",
            type = "dl:todoList",
            title = "Todo Item",
            path = "/DataLists/Todo/Todo Item",
            lastModified = DateTime.Parse("2011-02-15T22:30:39.843+00:00"),
            deleted = false
        },
        // Example of a deleted item
        new CrawlReturnItem
        {
            id = "deleted-item-001",
            subid = "",
            type = "cm:content",
            title = "Deleted Document",
            path = "/Documents/Deleted Document",
            lastModified = DateTime.Parse("2011-02-15T23:00:00.000+00:00"),
            deleted = true
        }
    }
};