using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetAvailableDatastores
{
    public static class Handler
    {
        public static DataStoreInfoReturn DoGetAvailableDatastores(ConnectionInfo conn, Hashtable customparams, string filter1, string filter2, string filter3, string templateService, bool loadTemplates)
        {
            try
            {
                return Hardcode.GetAvailableDatastores(conn, customparams, filter1, filter2, filter3, templateService, loadTemplates);
            }
            catch (Exception ex)
            {
                return new DataStoreInfoReturn
                {
                    error = true,
                    errorMsg = $"Error in DoGetAvailableDatastores: {ex.Message}",
                    datastores = Array.Empty<DataStoreInfo>()
                };
            }
        }


        private static bool DoGetAvailableDatastores_TEST_()
        {
            var conn = new ConnectionInfo();
            var customparams = new Hashtable();
            
            var result = DoGetAvailableDatastores(conn, customparams, null, null, null, "", false);
            
            if (result.error) return false;
            if (result.datastores == null || result.datastores.Length == 0) return false;
            
            // Verify first datastore matches the sample from JSON
            var firstDs = result.datastores[0];
            if (firstDs.id != "swsdp") return false;
            if (firstDs.name != "Sample: Web Site Design Project") return false;
            if (firstDs.desc != "") return false;
            if (firstDs.path != "") return false;
            
            // Verify we have expected number of datastores
            if (result.datastores.Length != 4) return false;
            
            // Verify specific datastores exist
            bool foundAlfresco = false;
            bool foundShared = false;
            bool foundTemplate = false;
            
            foreach (var ds in result.datastores)
            {
                if (ds.id == "alfresco_repo")
                {
                    foundAlfresco = true;
                    if (ds.name != "Alfresco Repository") return false;
                    if (ds.path != "/alfresco") return false;
                }
                else if (ds.id == "shared_docs")
                {
                    foundShared = true;
                    if (ds.name != "Shared Documents") return false;
                    if (ds.path != "/shared") return false;
                }
                else if (ds.id == "template_01")
                {
                    foundTemplate = true;
                    if (ds.name != "Template: Project Workspace") return false;
                    if (ds.path != "/templates/project") return false;
                }
            }
            
            if (!foundAlfresco || !foundShared || !foundTemplate) return false;
            
            return true;
        }


        private static bool DoGetAvailableDatastores_With_Null_Params_TEST_()
        {
            var result = DoGetAvailableDatastores(null, null, null, null, null, null, false);
            
            // Should still return valid data even with null parameters in hardcode mode
            if (result.error) return false;
            if (result.datastores == null || result.datastores.Length == 0) return false;
            
            return true;
        }


        private static bool DoGetAvailableDatastores_With_Filters_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            
            var result = DoGetAvailableDatastores(conn, customparams, "filter1", "filter2", "filter3", "templateService", true);
            
            // In hardcode phase, filters don't affect output
            if (result.error) return false;
            if (result.datastores == null || result.datastores.Length != 4) return false;
            
            return true;
        }


        private static bool DoGetAvailableDatastores_Error_Handling_TEST_()
        {
            // Test that exceptions are properly caught and converted to error responses
            try
            {
                // This should not throw an exception, but if it did, it should be handled
                var result = DoGetAvailableDatastores(new ConnectionInfo(), new Hashtable(), "", "", "", "", false);
                
                // Hardcode implementation should not fail
                if (result.error) return false;
                
                return true;
            }
            catch
            {
                // Should not reach here in hardcode mode
                return false;
            }
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}