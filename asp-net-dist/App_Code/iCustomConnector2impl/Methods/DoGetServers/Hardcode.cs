using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetServers
{
    public static class Hardcode
    {
        public static StringArrayReturn GetServers(ConnectionInfo conn, Hashtable customparams)
        {
            var response = new StringArrayReturn
            {
                error = false,
                errorMsg = "",
                values = new[]
                {
                    "alfresco-prod.example.com",
                    "alfresco-dev.example.com", 
                    "alfresco-test.example.com",
                    "content-server-01.local",
                    "content-server-02.local",
                    "sharepoint-bridge.example.com"
                }
            };

            if (!string.IsNullOrEmpty(conn.customparam))
            {
                if (System.Uri.TryCreate(conn.customparam, System.UriKind.Absolute, out var uri))
                {
                    var serverList = new List<string>(response.values);
                    if (!serverList.Contains(uri.Host))
                    {
                        serverList.Insert(0, uri.Host);
                        response.values = serverList.ToArray();
                    }
                }
            }

            return response;
        }


        private static bool GetServers_TEST_()
        {
            var conn = new ConnectionInfo();
            var customparams = new Hashtable();
            
            var result = GetServers(conn, customparams);
            
            if (result.error) return false;
            if (string.IsNullOrEmpty(result.errorMsg) == false) return false;
            if (result.values == null) return false;
            if (result.values.Length != 6) return false;
            if (result.values[0] != "alfresco-prod.example.com") return false;
            if (result.values[1] != "alfresco-dev.example.com") return false;
            if (result.values[2] != "alfresco-test.example.com") return false;
            if (result.values[3] != "content-server-01.local") return false;
            if (result.values[4] != "content-server-02.local") return false;
            if (result.values[5] != "sharepoint-bridge.example.com") return false;
            
            return true;
        }


        private static bool GetServers_With_Custom_Param_TEST_()
        {
            var conn = new ConnectionInfo
            {
                customparam = "http://100.115.192.75:8080/"
            };
            var customparams = new Hashtable();
            
            var result = GetServers(conn, customparams);
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 7) return false;
            if (result.values[0] != "100.115.192.75") return false;
            if (!result.values.Contains("alfresco-prod.example.com")) return false;
            
            return true;
        }


        private static bool GetServers_Duplicate_Server_TEST_()
        {
            var conn = new ConnectionInfo
            {
                customparam = "http://alfresco-prod.example.com/"
            };
            var customparams = new Hashtable();
            
            var result = GetServers(conn, customparams);
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 6) return false;
            if (result.values.Where(x => x == "alfresco-prod.example.com").Count() != 1) return false;
            
            return true;
        }


        private static bool GetServers_Invalid_URL_TEST_()
        {
            var conn = new ConnectionInfo
            {
                customparam = "not-a-valid-url"
            };
            var customparams = new Hashtable();
            
            var result = GetServers(conn, customparams);
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 6) return false;
            if (result.values[0] != "alfresco-prod.example.com") return false;
            
            return true;
        }


        private static bool GetServers_HTTPS_URL_TEST_()
        {
            var conn = new ConnectionInfo
            {
                customparam = "https://secure-alfresco.example.com:8443/"
            };
            var customparams = new Hashtable();
            
            var result = GetServers(conn, customparams);
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 7) return false;
            if (result.values[0] != "secure-alfresco.example.com") return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Hardcode)); }
    }
}