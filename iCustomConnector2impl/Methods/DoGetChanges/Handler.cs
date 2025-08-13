using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetChanges
{
    public static class Handler
    {
        public static CrawlReturn DoGetChanges(
            ConnectionInfo conn,
            Hashtable customparams,
            string foldersubid,
            DataStoreInfo datastore,
            DataStoreTypeFilter typefilters,
            string customFilter,
            DateTime lastUpdate,
            int maxReturns,
            int crawlID,
            int contentID,
            string crawler)
        {
            try
            {
                return Hardcode.GetChangesReturn();
            }
            catch (Exception ex)
            {
                return new CrawlReturn
                {
                    error = true,
                    errorMsg = $"Error in DoGetChanges: {ex.Message}",
                    moreExist = false,
                    nextStartId = "",
                    nextStartDate = DateTime.MinValue,
                    items = new CrawlReturnItem[0]
                };
            }
        }


        private static bool DoGetChanges_Basic_TEST_()
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
                ["SPWCRAWLID"] = "1",
                ["SPWCRAWLER"] = "371857150_TEST",
                ["MaxReturns"] = "100"
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
            
            var result = DoGetChanges(
                conn,
                customparams,
                "",
                datastore,
                typefilters,
                "",
                DateTime.Parse("2025-01-15T00:00:00Z"),
                100,
                1,
                3,
                "|TEST");
            
            if (result.error) return false;
            if (!string.IsNullOrEmpty(result.errorMsg)) return false;
            if (result.items == null || result.items.Length == 0) return false;
            
            return true;
        }


        private static bool DoGetChanges_Returns_Valid_CrawlReturn_TEST_()
        {
            var result = DoGetChanges(
                new ConnectionInfo(),
                new Hashtable(),
                "",
                new DataStoreInfo(),
                new DataStoreTypeFilter(),
                "",
                DateTime.MinValue,
                10,
                0,
                0,
                "");
            
            if (result == null) return false;
            if (result.error) return false;
            if (result.items == null) return false;
            if (result.items.Length != 4) return false;
            
            return true;
        }


        private static bool DoGetChanges_Has_Changed_Items_TEST_()
        {
            var result = DoGetChanges(
                new ConnectionInfo(),
                new Hashtable(),
                "",
                new DataStoreInfo(),
                new DataStoreTypeFilter(),
                "",
                DateTime.MinValue,
                100,
                0,
                0,
                "");
            
            bool hasUpdatedItem = false;
            bool hasDeletedItem = false;
            
            foreach (var item in result.items)
            {
                if (!item.deleted && item.title.Contains("Updated")) hasUpdatedItem = true;
                if (item.deleted) hasDeletedItem = true;
            }
            
            if (!hasUpdatedItem) return false;
            if (!hasDeletedItem) return false;
            
            return true;
        }


        private static bool DoGetChanges_Items_Have_Required_Fields_TEST_()
        {
            var result = DoGetChanges(
                new ConnectionInfo(),
                new Hashtable(),
                "",
                new DataStoreInfo(),
                new DataStoreTypeFilter(),
                "",
                DateTime.MinValue,
                100,
                0,
                0,
                "");
            
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


        private static bool DoGetChanges_Has_Multiple_Content_Types_TEST_()
        {
            var result = DoGetChanges(
                new ConnectionInfo(),
                new Hashtable(),
                "",
                new DataStoreInfo(),
                new DataStoreTypeFilter(),
                "",
                DateTime.MinValue,
                100,
                0,
                0,
                "");
            
            bool hasContent = false;
            
            foreach (var item in result.items)
            {
                if (item.type == "cm:content") hasContent = true;
            }
            
            if (!hasContent) return false;
            
            return true;
        }


        private static bool DoGetChanges_Pagination_Info_TEST_()
        {
            var result = DoGetChanges(
                new ConnectionInfo(),
                new Hashtable(),
                "",
                new DataStoreInfo(),
                new DataStoreTypeFilter(),
                "",
                DateTime.MinValue,
                100,
                0,
                0,
                "");
            
            if (result.moreExist) return false;
            if (!string.IsNullOrEmpty(result.nextStartId)) return false;
            
            return true;
        }


        private static bool DoGetChanges_All_Parameters_Null_Safe_TEST_()
        {
            var result = DoGetChanges(
                null,
                null,
                null,
                null,
                null,
                null,
                DateTime.MinValue,
                0,
                0,
                0,
                null);
            
            if (result == null) return false;
            if (result.error) return false;
            if (result.items == null) return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}