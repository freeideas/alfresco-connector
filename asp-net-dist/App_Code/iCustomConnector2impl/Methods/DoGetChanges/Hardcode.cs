using System;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetChanges
{
    public static class Hardcode
    {
        public static CrawlReturn GetChangesReturn()
        {
            return new CrawlReturn
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
                        deleted = true
                    }
                }
            };
        }


        private static bool GetChangesReturn_TEST_()
        {
            var result = GetChangesReturn();
            
            if (result.error) return false;
            if (!string.IsNullOrEmpty(result.errorMsg)) return false;
            if (result.moreExist) return false;
            if (!string.IsNullOrEmpty(result.nextStartId)) return false;
            if (result.items == null || result.items.Length == 0) return false;
            
            return true;
        }


        private static bool GetChangesReturn_Items_Count_TEST_()
        {
            var result = GetChangesReturn();
            
            if (result.items.Length != 4) return false;
            
            return true;
        }


        private static bool GetChangesReturn_First_Item_TEST_()
        {
            var result = GetChangesReturn();
            
            var firstItem = result.items[0];
            if (firstItem.id != "DOC001") return false;
            if (firstItem.type != "cm:content") return false;
            if (firstItem.title != "Updated Project Charter") return false;
            if (firstItem.path != "/projects/swsdp/documents/charter.docx") return false;
            if (firstItem.deleted) return false;
            
            return true;
        }


        private static bool GetChangesReturn_Deleted_Item_TEST_()
        {
            var result = GetChangesReturn();
            
            var deletedItem = result.items[3];
            if (deletedItem.id != "DOC003") return false;
            if (deletedItem.type != "cm:content") return false;
            if (deletedItem.title != "Old Requirements Document") return false;
            if (!deletedItem.deleted) return false;
            
            return true;
        }


        private static bool GetChangesReturn_Has_Updated_Items_TEST_()
        {
            var result = GetChangesReturn();
            
            bool hasUpdatedItem = false;
            
            foreach (var item in result.items)
            {
                if (item.title.Contains("Updated") || item.title.Contains("Revised") || item.title.Contains("New"))
                {
                    hasUpdatedItem = true;
                    break;
                }
            }
            
            if (!hasUpdatedItem) return false;
            
            return true;
        }


        private static bool GetChangesReturn_All_Content_Type_TEST_()
        {
            var result = GetChangesReturn();
            
            bool allContentType = true;
            
            foreach (var item in result.items)
            {
                if (item.type != "cm:content")
                {
                    allContentType = false;
                    break;
                }
            }
            
            if (!allContentType) return false;
            
            return true;
        }


        private static bool GetChangesReturn_Has_Mixed_Deleted_Status_TEST_()
        {
            var result = GetChangesReturn();
            
            bool hasDeleted = false;
            bool hasNotDeleted = false;
            
            foreach (var item in result.items)
            {
                if (item.deleted) hasDeleted = true;
                if (!item.deleted) hasNotDeleted = true;
            }
            
            if (!hasDeleted) return false;
            if (!hasNotDeleted) return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Hardcode)); }
    }
}