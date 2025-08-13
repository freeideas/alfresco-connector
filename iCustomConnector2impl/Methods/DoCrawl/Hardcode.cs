using System;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoCrawl
{
    public static class Hardcode
    {
        public static CrawlReturn GetCrawlReturn()
        {
            return new CrawlReturn
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
                        id = "602b72e5-e365-4eee-b68d-b3dd26270ee3",
                        subid = "",
                        type = "lnk:link",
                        title = "Another Link",
                        path = "/Links/Another Link",
                        lastModified = DateTime.Parse("2011-02-15T21:44:04.01+00:00"),
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
                        id = "308ad851-b4ab-4f41-bbd0-c83398d2afe4",
                        subid = "",
                        type = "fm:post",
                        title = "Another Forum Post",
                        path = "/Forum/Discussion/Another Forum Post",
                        lastModified = DateTime.Parse("2011-02-15T22:06:21.034+00:00"),
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
                        id = "a53c7a85-12d0-4eb1-8e03-f030e0778da3",
                        subid = "",
                        type = "dl:issue",
                        title = "Issue #456",
                        path = "/DataLists/Issues/Issue #456",
                        lastModified = DateTime.Parse("2011-02-15T22:19:20.437+00:00"),
                        deleted = false
                    },
                    new CrawlReturnItem
                    {
                        id = "e57195d3-aeda-432d-bfc4-0a556b2d8ab9",
                        subid = "",
                        type = "dl:issue",
                        title = "Issue #789",
                        path = "/DataLists/Issues/Issue #789",
                        lastModified = DateTime.Parse("2011-02-15T22:23:00.75+00:00"),
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
                    new CrawlReturnItem
                    {
                        id = "86796712-4dc6-4b8d-973f-a943ef7f23ed",
                        subid = "",
                        type = "fm:post",
                        title = "Latest Forum Post",
                        path = "/Forum/Discussion/Latest Forum Post",
                        lastModified = DateTime.Parse("2011-02-16T10:30:10.663+00:00"),
                        deleted = false
                    },
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
        }


        private static bool GetCrawlReturn_TEST_()
        {
            var result = GetCrawlReturn();
            
            if (result.error) return false;
            if (!string.IsNullOrEmpty(result.errorMsg)) return false;
            if (!result.moreExist) return false;
            if (result.nextStartId != "next_page_token") return false;
            if (result.items == null || result.items.Length == 0) return false;
            
            return true;
        }


        private static bool GetCrawlReturn_Items_Count_TEST_()
        {
            var result = GetCrawlReturn();
            
            if (result.items.Length != 11) return false;
            
            return true;
        }


        private static bool GetCrawlReturn_First_Item_TEST_()
        {
            var result = GetCrawlReturn();
            
            var firstItem = result.items[0];
            if (firstItem.id != "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7") return false;
            if (firstItem.type != "cm:content") return false;
            if (firstItem.title != "Sample Document.txt") return false;
            if (firstItem.path != "/Documents/Sample Document.txt") return false;
            if (firstItem.deleted) return false;
            
            return true;
        }


        private static bool GetCrawlReturn_Deleted_Item_TEST_()
        {
            var result = GetCrawlReturn();
            
            var deletedItem = result.items[10];
            if (deletedItem.id != "deleted-item-001") return false;
            if (deletedItem.type != "cm:content") return false;
            if (deletedItem.title != "Deleted Document") return false;
            if (!deletedItem.deleted) return false;
            
            return true;
        }


        private static bool GetCrawlReturn_Different_Types_TEST_()
        {
            var result = GetCrawlReturn();
            
            bool hasContent = false;
            bool hasLink = false;
            bool hasPost = false;
            bool hasIssue = false;
            bool hasTodo = false;
            
            foreach (var item in result.items)
            {
                if (item.type == "cm:content") hasContent = true;
                if (item.type == "lnk:link") hasLink = true;
                if (item.type == "fm:post") hasPost = true;
                if (item.type == "dl:issue") hasIssue = true;
                if (item.type == "dl:todoList") hasTodo = true;
            }
            
            if (!hasContent) return false;
            if (!hasLink) return false;
            if (!hasPost) return false;
            if (!hasIssue) return false;
            if (!hasTodo) return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Hardcode)); }
    }
}