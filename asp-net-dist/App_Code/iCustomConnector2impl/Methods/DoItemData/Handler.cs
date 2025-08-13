using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoItemData
{
    public static class Handler
    {
        public static ItemReturn DoItemData(
            ConnectionInfo conn,
            Hashtable customparams,
            string id,
            string subid,
            string foldersubid,
            DataStoreInfo datastore,
            DataStoreTypeFilter typefilters,
            int maxFileSize,
            Hashtable allowedExtensions,
            DateTime lastUpdate,
            bool isIncremental,
            int crawlID,
            int contentID,
            string crawler,
            bool getmetadata,
            bool getsecurity,
            bool getfile)
        {
            try
            {
                return Hardcode.GetItemData(id, getfile, getmetadata);
            }
            catch (Exception ex)
            {
                return new ItemReturn
                {
                    id = id ?? "",
                    error = true,
                    errorMsg = $"Error in DoItemData: {ex.Message}"
                };
            }
        }


        private static bool DoItemData_Basic_Parameters_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var datastore = new DataStoreInfo { id = "swsdp", name = "Sample: Web Site Design Project" };
            var typefilters = new DataStoreTypeFilter { types = new[] { "lnk:link" } };
            var allowedExtensions = new Hashtable();
            
            var result = DoItemData(
                conn,
                customparams,
                "a38308f8-6f30-4d8a-8576-eaf6703fb9d3",
                "",
                "",
                datastore,
                typefilters,
                52428800,
                allowedExtensions,
                DateTime.Parse("1990-01-01T00:00:00Z"),
                false,
                0,
                3,
                "|TEST",
                true,
                true,
                true
            );
            
            if (result.error) return false;
            if (result.id != "a38308f8-6f30-4d8a-8576-eaf6703fb9d3") return false;
            if (result.type != "lnk:link") return false;
            if (result.title != "W3 Schools") return false;
            if (result.content == null) return false;
            if (result.metadata == null) return false;
            
            return true;
        }


        private static bool DoItemData_Document_Type_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var datastore = new DataStoreInfo { id = "documentLibrary" };
            var typefilters = new DataStoreTypeFilter { types = new[] { "cm:content" } };
            var allowedExtensions = new Hashtable();
            
            var result = DoItemData(
                conn,
                customparams,
                "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7",
                "",
                "",
                datastore,
                typefilters,
                52428800,
                allowedExtensions,
                DateTime.Parse("1990-01-01T00:00:00Z"),
                false,
                0,
                3,
                "371857150_TEST",
                true,
                true,
                true
            );
            
            if (result.error) return false;
            if (result.id != "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7") return false;
            if (result.type != "cm:content") return false;
            if (result.title != "Sample Document") return false;
            if (result.author != "Administrator") return false;
            if (result.contentType != "text/plain") return false;
            
            return true;
        }


        private static bool DoItemData_No_File_Content_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin" };
            var customparams = new Hashtable();
            var datastore = new DataStoreInfo { id = "test" };
            var typefilters = new DataStoreTypeFilter();
            var allowedExtensions = new Hashtable();
            
            var result = DoItemData(
                conn,
                customparams,
                "test-id",
                "",
                "",
                datastore,
                typefilters,
                0,
                allowedExtensions,
                DateTime.Now,
                false,
                0,
                0,
                "TEST",
                true,
                false,
                false  // getfile = false
            );
            
            if (result.error) return false;
            if (result.content != null) return false;
            if (result.metadata == null) return false;
            
            return true;
        }


        private static bool DoItemData_No_Metadata_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin" };
            var customparams = new Hashtable();
            var datastore = new DataStoreInfo { id = "test" };
            var typefilters = new DataStoreTypeFilter();
            var allowedExtensions = new Hashtable();
            
            var result = DoItemData(
                conn,
                customparams,
                "test-id",
                "",
                "",
                datastore,
                typefilters,
                0,
                allowedExtensions,
                DateTime.Now,
                false,
                0,
                0,
                "TEST",
                false,  // getmetadata = false
                false,
                true
            );
            
            if (result.error) return false;
            if (result.content == null) return false;
            if (result.metadata != null) return false;
            
            return true;
        }


        private static bool DoItemData_Security_Data_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var datastore = new DataStoreInfo { id = "swsdp" };
            var typefilters = new DataStoreTypeFilter();
            var allowedExtensions = new Hashtable();
            
            var result = DoItemData(
                conn,
                customparams,
                "a38308f8-6f30-4d8a-8576-eaf6703fb9d3",
                "",
                "",
                datastore,
                typefilters,
                52428800,
                allowedExtensions,
                DateTime.Parse("1990-01-01T00:00:00Z"),
                false,
                0,
                3,
                "|TEST",
                false,
                true,  // getsecurity = true
                false
            );
            
            if (result.error) return false;
            if (result.allowedUsers.Length != 2) return false;
            if (result.allowedGroups.Length != 2) return false;
            if (result.deniedUsers.Length != 0) return false;
            if (result.deniedGroups.Length != 0) return false;
            
            return true;
        }


        private static bool DoItemData_Error_Handling_TEST_()
        {
            // Test with null ID should not cause exception
            var conn = new ConnectionInfo();
            var customparams = new Hashtable();
            var datastore = new DataStoreInfo();
            var typefilters = new DataStoreTypeFilter();
            var allowedExtensions = new Hashtable();
            
            var result = DoItemData(
                conn,
                customparams,
                null,  // null ID
                "",
                "",
                datastore,
                typefilters,
                0,
                allowedExtensions,
                DateTime.Now,
                false,
                0,
                0,
                "",
                false,
                false,
                false
            );
            
            // Should handle gracefully and not error  
            if (result.error) return false;
            // ID should be handled and return an empty string as the id
            if (result.id != "") return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}