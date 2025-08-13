using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetAvailableDatastores
{
    public static class Hardcode
    {
        public static DataStoreInfoReturn GetAvailableDatastores(ConnectionInfo conn, Hashtable customparams, string filter1, string filter2, string filter3, string templateService, bool loadTemplates)
        {
            return new DataStoreInfoReturn
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
                    },
                    new DataStoreInfo
                    {
                        id = "template_01",
                        name = "Template: Project Workspace",
                        desc = "Standard project workspace template",
                        path = "/templates/project"
                    }
                }
            };
        }


        private static bool GetAvailableDatastores_TEST_()
        {
            var conn = new ConnectionInfo();
            var customparams = new Hashtable();
            var result = GetAvailableDatastores(conn, customparams, null, null, null, "", false);
            
            if (result.error) return false;
            if (result.datastores.Length != 4) return false;
            if (result.datastores[0].id != "swsdp") return false;
            if (result.datastores[0].name != "Sample: Web Site Design Project") return false;
            if (result.datastores[1].id != "alfresco_repo") return false;
            if (result.datastores[2].id != "shared_docs") return false;
            if (result.datastores[3].id != "template_01") return false;
            
            return true;
        }


        private static bool GetAvailableDatastores_With_Filters_TEST_()
        {
            var conn = new ConnectionInfo();
            var customparams = new Hashtable();
            var result = GetAvailableDatastores(conn, customparams, "filter1", "filter2", "filter3", "templateService", true);
            
            // In hardcode mode, filters don't affect the result
            if (result.error) return false;
            if (result.datastores.Length != 4) return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Hardcode)); }
    }
}