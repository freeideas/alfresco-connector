using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetUsers
{
    public static class Handler
    {
        public static SystemIdentityInfoReturn DoGetUsers(ConnectionInfo conn, Hashtable customparams, string additionalColumns)
        {
            try
            {
                return Hardcode.GetUsers(conn, customparams, additionalColumns);
            }
            catch (Exception ex)
            {
                return new SystemIdentityInfoReturn
                {
                    error = true,
                    errorMsg = $"Error in DoGetUsers: {ex.Message}",
                    identities = new SystemIdentityInfo[0]
                };
            }
        }


        private static bool DoGetUsers_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetUsers(conn, customparams, "");
            
            if (result.error) return false;
            if (!string.IsNullOrEmpty(result.errorMsg)) return false;
            if (result.identities == null) return false;
            if (result.identities.Length != 4) return false;
            
            return true;
        }


        private static bool DoGetUsers_Returns_Expected_Users_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetUsers(conn, customparams, "");
            
            var userIds = new string[result.identities.Length];
            for (int i = 0; i < result.identities.Length; i++)
                userIds[i] = result.identities[i].id;
            
            if (Array.IndexOf(userIds, "abeecher") == -1) return false;
            if (Array.IndexOf(userIds, "admin") == -1) return false;
            if (Array.IndexOf(userIds, "guest") == -1) return false;
            if (Array.IndexOf(userIds, "mjackson") == -1) return false;
            
            return true;
        }


        private static bool DoGetUsers_User_Properties_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetUsers(conn, customparams, "");
            
            foreach (var user in result.identities)
            {
                if (string.IsNullOrEmpty(user.id)) return false;
                if (string.IsNullOrEmpty(user.name)) return false;
                if (string.IsNullOrEmpty(user.displayName)) return false;
                // NOTE: email can be empty for guest user
            }
            
            return true;
        }


        private static bool DoGetUsers_With_Null_Connection_TEST_()
        {
            var result = DoGetUsers(null, new Hashtable(), "");
            
            // NOTE: Should still return hardcoded data even with null connection in hardcode phase
            if (result.error) return false;
            if (result.identities == null) return false;
            if (result.identities.Length != 4) return false;
            
            return true;
        }


        private static bool DoGetUsers_With_Custom_Params_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            customparams["SelectedDataStores"] = "swsdp";
            var result = DoGetUsers(conn, customparams, "Email,FirstName,LastName");
            
            if (result.error) return false;
            if (result.identities == null) return false;
            if (result.identities.Length != 4) return false;
            
            return true;
        }


        private static bool DoGetUsers_Alice_Details_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetUsers(conn, customparams, "");
            
            var alice = Array.Find(result.identities, u => u.id == "abeecher");
            if (alice == null) return false;
            if (alice.name != "abeecher") return false;
            if (alice.displayName != "Alice Beecher") return false;
            if (alice.email != "abeecher@example.com") return false;
            
            return true;
        }


        private static bool DoGetUsers_Admin_Details_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetUsers(conn, customparams, "");
            
            var admin = Array.Find(result.identities, u => u.id == "admin");
            if (admin == null) return false;
            if (admin.name != "admin") return false;
            if (admin.displayName != "Administrator") return false;
            if (admin.email != "admin@alfresco.com") return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}