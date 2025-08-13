using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoRealtimeSecurityCheck
{
    public static class Handler
    {
        public static SecurityItemReturn DoRealtimeSecurityCheck(ConnectionInfo conn, Hashtable customparams, SecurityItem[] items, string adusername, string[] userids)
        {
            try
            {
                return Hardcode.DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);
            }
            catch (Exception ex)
            {
                return new SecurityItemReturn
                {
                    results = new SecurityItemResult[0],
                    error = true,
                    errorMsg = $"Error in DoRealtimeSecurityCheck: {ex.Message}"
                };
            }
        }


        private static bool DoRealtimeSecurityCheck_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var items = new SecurityItem[]
            {
                new SecurityItem { id = "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7", subid = "" },
                new SecurityItem { id = "a38308f8-6f30-4d8a-8576-eaf6703fb9d3", subid = "" }
            };
            var adusername = "testuser";
            var userids = new string[] { "user1", "user2" };

            var result = DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);

            if (result == null) return false;
            if (result.error) return false;
            if (result.results == null) return false;
            if (result.results.Length != 2) return false;

            return true;
        }


        private static bool DoRealtimeSecurityCheck_Error_Handling_TEST_()
        {
            // Test with null items to trigger exception handling
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            SecurityItem[] items = null;
            var adusername = "testuser";
            var userids = new string[] { "user1" };

            var result = DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);

            if (result == null) return false;
            if (!result.error) return false;
            if (string.IsNullOrEmpty(result.errorMsg)) return false;
            if (!result.errorMsg.Contains("Error in DoRealtimeSecurityCheck")) return false;
            if (result.results == null) return false;
            if (result.results.Length != 0) return false;

            return true;
        }


        private static bool DoRealtimeSecurityCheck_Delegation_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var items = new SecurityItem[]
            {
                new SecurityItem { id = "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7", subid = "" },
                new SecurityItem { id = "602b72e5-e365-4eee-b68d-b3dd26270ee3", subid = "" }
            };
            var adusername = "testuser";
            var userids = new string[] { "user1", "user2" };

            var result = DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);

            // Verify that the hardcode logic is working through the handler
            if (result == null) return false;
            if (result.error) return false;
            if (result.results.Length != 2) return false;
            if (!result.results[0].allowed) return false; // 5fa* should be allowed
            if (result.results[1].allowed) return false; // 602* should be denied

            return true;
        }


        private static bool DoRealtimeSecurityCheck_Multiple_Items_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var items = new SecurityItem[]
            {
                new SecurityItem { id = "item1", subid = "sub1" },
                new SecurityItem { id = "item2", subid = "sub2" },
                new SecurityItem { id = "item3", subid = "sub3" },
                new SecurityItem { id = "item4", subid = "sub4" }
            };
            var adusername = "testuser";
            var userids = new string[] { "user1", "user2", "user3" };

            var result = DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);

            if (result == null) return false;
            if (result.error) return false;
            if (result.results.Length != 4) return false;
            
            // Verify alternating pattern (even indices allowed)
            if (!result.results[0].allowed) return false; // index 0, even
            if (result.results[1].allowed) return false; // index 1, odd  
            if (!result.results[2].allowed) return false; // index 2, even
            if (result.results[3].allowed) return false; // index 3, odd

            // Verify IDs are preserved
            if (result.results[0].id != "item1") return false;
            if (result.results[1].id != "item2") return false;
            if (result.results[2].id != "item3") return false;
            if (result.results[3].id != "item4") return false;

            return true;
        }


        private static bool DoRealtimeSecurityCheck_CustomParams_TEST_()
        {
            var conn = new ConnectionInfo 
            { 
                account = "admin", 
                password = "admin",
                customparam = "http://localhost:8080"
            };
            var customparams = new Hashtable();
            customparams["SPWCONTENTID"] = "3";
            customparams["TEST_PARAM"] = "test_value";
            
            var items = new SecurityItem[]
            {
                new SecurityItem { id = "test_item", subid = "" }
            };
            var adusername = "testuser";
            var userids = new string[] { "user1", "user2" };

            var result = DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);

            if (result == null) return false;
            if (result.error) return false;
            if (result.results.Length != 1) return false;
            if (result.results[0].id != "test_item") return false;

            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}