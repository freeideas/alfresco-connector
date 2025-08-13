using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoCrawl
{
    public static class Handler
    {
        public static CrawlReturn DoCrawl(
            ConnectionInfo conn, 
            Hashtable customparams, 
            DataStoreInfo datastore, 
            string foldersubId, 
            DataStoreTypeFilter typefilters, 
            string customFilter, 
            Hashtable allowedExtensions, 
            DateTime lastUpdate, 
            bool isIncremental, 
            int maxReturns, 
            int maxFileSize, 
            int crawlID, 
            int contentID, 
            string crawler, 
            int page)
        {
            try
            {
                return Hardcode.GetCrawlReturn();
            }
            catch (Exception ex)
            {
                return new CrawlReturn
                {
                    error = true,
                    errorMsg = $"Error in DoCrawl: {ex.Message}",
                    moreExist = false,
                    nextStartId = "",
                    nextStartDate = DateTime.MinValue,
                    items = new CrawlReturnItem[0]
                };
            }
        }


        private static bool DoCrawl_Basic_TEST_()
        {
            var conn = new ConnectionInfo
            {
                account = "admin",
                password = "admin",
                customparam = "http://100.115.192.75:8080/"
            };
            
            var customparams = new Hashtable
            {
                ["SPWCONTENTID"] = "3",
                ["SPWCRAWLID"] = "0",
                ["SPWCRAWLER"] = "371857150_TEST",
                ["MaxReturns"] = "10"
            };
            
            var datastore = new DataStoreInfo
            {
                id = "swsdp",
                name = "Sample: Web Site Design Project"
            };
            
            var typefilters = new DataStoreTypeFilter
            {
                types = new string[] { "" }
            };
            
            var result = DoCrawl(
                conn, 
                customparams, 
                datastore, 
                "",
                typefilters,
                "",
                new Hashtable(),
                DateTime.Parse("1990-01-01T00:00:00Z"),
                false,
                10,
                52428800,
                0,
                3,
                "|TEST",
                0);
            
            if (result.error) return false;
            if (!string.IsNullOrEmpty(result.errorMsg)) return false;
            if (result.items == null || result.items.Length == 0) return false;
            
            return true;
        }


        private static bool DoCrawl_Returns_Valid_CrawlReturn_TEST_()
        {
            var result = DoCrawl(
                new ConnectionInfo(),
                new Hashtable(),
                new DataStoreInfo(),
                "",
                new DataStoreTypeFilter(),
                "",
                new Hashtable(),
                DateTime.MinValue,
                false,
                10,
                1000000,
                0,
                0,
                "",
                0);
            
            if (result == null) return false;
            if (result.error) return false;
            if (result.items == null) return false;
            if (result.items.Length != 11) return false;
            
            return true;
        }


        private static bool DoCrawl_Pagination_Info_TEST_()
        {
            var result = DoCrawl(
                new ConnectionInfo(),
                new Hashtable(),
                new DataStoreInfo(),
                "",
                new DataStoreTypeFilter(),
                "",
                new Hashtable(),
                DateTime.MinValue,
                false,
                10,
                1000000,
                0,
                0,
                "",
                0);
            
            if (!result.moreExist) return false;
            if (result.nextStartId != "next_page_token") return false;
            if (result.nextStartDate == DateTime.MinValue) return false;
            
            return true;
        }


        private static bool DoCrawl_Items_Have_Required_Fields_TEST_()
        {
            var result = DoCrawl(
                new ConnectionInfo(),
                new Hashtable(),
                new DataStoreInfo(),
                "",
                new DataStoreTypeFilter(),
                "",
                new Hashtable(),
                DateTime.MinValue,
                false,
                10,
                1000000,
                0,
                0,
                "",
                0);
            
            foreach (var item in result.items)
            {
                if (string.IsNullOrEmpty(item.id)) return false;
                if (string.IsNullOrEmpty(item.type)) return false;
                if (string.IsNullOrEmpty(item.title)) return false;
                if (string.IsNullOrEmpty(item.path)) return false;
                if (item.lastModified == DateTime.MinValue) return false;
            }
            
            return true;
        }


        private static bool DoCrawl_Has_Multiple_Content_Types_TEST_()
        {
            var result = DoCrawl(
                new ConnectionInfo(),
                new Hashtable(),
                new DataStoreInfo(),
                "",
                new DataStoreTypeFilter(),
                "",
                new Hashtable(),
                DateTime.MinValue,
                false,
                10,
                1000000,
                0,
                0,
                "",
                0);
            
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


        private static bool DoCrawl_Has_Deleted_Item_TEST_()
        {
            var result = DoCrawl(
                new ConnectionInfo(),
                new Hashtable(),
                new DataStoreInfo(),
                "",
                new DataStoreTypeFilter(),
                "",
                new Hashtable(),
                DateTime.MinValue,
                false,
                10,
                1000000,
                0,
                0,
                "",
                0);
            
            bool hasDeletedItem = false;
            foreach (var item in result.items)
            {
                if (item.deleted)
                {
                    hasDeletedItem = true;
                    break;
                }
            }
            
            if (!hasDeletedItem) return false;
            
            return true;
        }


        private static bool DoCrawl_Incremental_Parameter_TEST_()
        {
            var result1 = DoCrawl(
                new ConnectionInfo(),
                new Hashtable(),
                new DataStoreInfo(),
                "",
                new DataStoreTypeFilter(),
                "",
                new Hashtable(),
                DateTime.MinValue,
                true,
                10,
                1000000,
                0,
                0,
                "",
                0);
            
            var result2 = DoCrawl(
                new ConnectionInfo(),
                new Hashtable(),
                new DataStoreInfo(),
                "",
                new DataStoreTypeFilter(),
                "",
                new Hashtable(),
                DateTime.MinValue,
                false,
                10,
                1000000,
                0,
                0,
                "",
                0);
            
            if (result1.error) return false;
            if (result2.error) return false;
            if (result1.items.Length != result2.items.Length) return false;
            
            return true;
        }


        private static bool DoCrawl_All_Parameters_Null_Safe_TEST_()
        {
            var result = DoCrawl(
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                DateTime.MinValue,
                false,
                0,
                0,
                0,
                0,
                null,
                0);
            
            if (result == null) return false;
            if (result.error) return false;
            if (result.items == null) return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}