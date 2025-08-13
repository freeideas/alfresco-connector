using System;
using System.Collections;
using System.Linq;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoRealtimeSecurityCheck
{
    public static class Hardcode
    {
        public static SecurityItemReturn DoRealtimeSecurityCheck(ConnectionInfo conn, Hashtable customparams, SecurityItem[] items, string adusername, string[] userids)
        {
            var response = new SecurityItemReturn();

            // Create mock security check results for the requested items
            response.results = items.Select((item, index) =>
            {
                // Mock logic: alternate between allowed and denied for demonstration
                // In reality, this would check actual security permissions
                var allowed = index % 2 == 0;
                
                // Special case: if the item ID starts with "5fa", allow it
                if (item.id.StartsWith("5fa"))
                {
                    allowed = true;
                }
                
                // Special case: if the item ID starts with "602", deny it
                if (item.id.StartsWith("602"))
                {
                    allowed = false;
                }
                
                return new SecurityItemResult
                {
                    id = item.id,
                    subid = item.subid,
                    allowed = allowed
                };
            }).ToArray();

            // Set success status
            response.error = false;
            response.errorMsg = "";

            return response;
        }


        private static bool DoRealtimeSecurityCheck_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var items = new SecurityItem[]
            {
                new SecurityItem { id = "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7", subid = "" },
                new SecurityItem { id = "a38308f8-6f30-4d8a-8576-eaf6703fb9d3", subid = "" },
                new SecurityItem { id = "602b72e5-e365-4eee-b68d-b3dd26270ee3", subid = "" }
            };
            var adusername = "testuser";
            var userids = new string[] { "user1", "user2" };

            var result = DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);

            if (result == null) return false;
            if (result.error) return false;
            if (!string.IsNullOrEmpty(result.errorMsg)) return false;
            if (result.results == null) return false;
            if (result.results.Length != 3) return false;

            return true;
        }


        private static bool DoRealtimeSecurityCheck_Security_Logic_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var items = new SecurityItem[]
            {
                new SecurityItem { id = "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7", subid = "" },
                new SecurityItem { id = "a38308f8-6f30-4d8a-8576-eaf6703fb9d3", subid = "" },
                new SecurityItem { id = "602b72e5-e365-4eee-b68d-b3dd26270ee3", subid = "" }
            };
            var adusername = "testuser";
            var userids = new string[] { "user1", "user2" };

            var result = DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);

            // Verify specific security logic based on hardcode rules
            if (!result.results[0].allowed) return false; // 5fa* should be allowed
            if (result.results[1].allowed) return false; // a38* should be denied (index 1, odd)
            if (result.results[2].allowed) return false; // 602* should be denied

            // Verify IDs are preserved
            if (result.results[0].id != "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7") return false;
            if (result.results[1].id != "a38308f8-6f30-4d8a-8576-eaf6703fb9d3") return false;
            if (result.results[2].id != "602b72e5-e365-4eee-b68d-b3dd26270ee3") return false;

            return true;
        }


        private static bool DoRealtimeSecurityCheck_Empty_Items_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var items = new SecurityItem[0];
            var adusername = "testuser";
            var userids = new string[] { "user1" };

            var result = DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);

            if (result == null) return false;
            if (result.error) return false;
            if (result.results == null) return false;
            if (result.results.Length != 0) return false;

            return true;
        }


        private static bool DoRealtimeSecurityCheck_SubId_Preservation_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var items = new SecurityItem[]
            {
                new SecurityItem { id = "test123", subid = "sub456" }
            };
            var adusername = "testuser";
            var userids = new string[] { "user1" };

            var result = DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);

            if (result == null) return false;
            if (result.results == null) return false;
            if (result.results.Length != 1) return false;
            if (result.results[0].id != "test123") return false;
            if (result.results[0].subid != "sub456") return false;

            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Hardcode)); }
    }
}