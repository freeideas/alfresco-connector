using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetServers
{
    public static class Handler
    {
        public static StringArrayReturn DoGetServers(ConnectionInfo conn, Hashtable customparams)
        {
            try
            {
                return Hardcode.GetServers(conn, customparams);
            }
            catch (Exception ex)
            {
                return new StringArrayReturn
                {
                    error = true,
                    errorMsg = $"Error in DoGetServers: {ex.Message}",
                    values = Array.Empty<string>()
                };
            }
        }


        private static bool DoGetServers_TEST_()
        {
            var conn = new ConnectionInfo
            {
                account = "admin",
                password = "admin",
                customparam = "http://100.115.192.75:8080/"
            };
            var customparams = new Hashtable();

            var result = DoGetServers(conn, customparams);
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length == 0) return false;
            if (!System.Linq.Enumerable.Contains(result.values, "alfresco-prod.example.com")) return false;
            if (!System.Linq.Enumerable.Contains(result.values, "100.115.192.75")) return false;
            
            return true;
        }


        private static bool DoGetServers_Empty_Connection_TEST_()
        {
            var conn = new ConnectionInfo();
            var customparams = new Hashtable();

            var result = DoGetServers(conn, customparams);
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length == 0) return false;
            if (!System.Linq.Enumerable.Contains(result.values, "alfresco-prod.example.com")) return false;
            if (!System.Linq.Enumerable.Contains(result.values, "alfresco-dev.example.com")) return false;
            if (!System.Linq.Enumerable.Contains(result.values, "alfresco-test.example.com")) return false;
            
            return true;
        }


        private static bool DoGetServers_Custom_Server_TEST_()
        {
            var conn = new ConnectionInfo
            {
                customparam = "https://custom-server.example.com:8443/"
            };
            var customparams = new Hashtable();

            var result = DoGetServers(conn, customparams);
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length == 0) return false;
            if (!System.Linq.Enumerable.Contains(result.values, "custom-server.example.com")) return false;
            if (result.values[0] != "custom-server.example.com") return false;
            
            return true;
        }


        private static bool DoGetServers_Invalid_URL_TEST_()
        {
            var conn = new ConnectionInfo
            {
                customparam = "not-a-valid-url"
            };
            var customparams = new Hashtable();

            var result = DoGetServers(conn, customparams);
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length == 0) return false;
            if (!System.Linq.Enumerable.Contains(result.values, "alfresco-prod.example.com")) return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}