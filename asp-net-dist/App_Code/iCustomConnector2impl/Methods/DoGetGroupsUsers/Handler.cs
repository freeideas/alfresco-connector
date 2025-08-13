using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetGroupsUsers
{
    public static class Handler
    {
        public static StringArrayReturn DoGetGroupsUsers(ConnectionInfo conn, Hashtable customparams, string groupId)
        {
            try
            {
                return Hardcode.GetGroupsUsers(conn, customparams, groupId);
            }
            catch (Exception ex)
            {
                return new StringArrayReturn
                {
                    error = true,
                    errorMsg = $"Error in DoGetGroupsUsers: {ex.Message}",
                    values = new string[0]
                };
            }
        }


        private static bool DoGetGroupsUsers_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroupsUsers(conn, customparams, "GROUP_ALFRESCO_SEARCH_ADMINISTRATORS");
            
            if (result.error) return false;
            if (!string.IsNullOrEmpty(result.errorMsg)) return false;
            if (result.values == null) return false;
            if (result.values.Length != 1) return false;
            
            return true;
        }


        private static bool DoGetGroupsUsers_Returns_Expected_Users_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroupsUsers(conn, customparams, "GROUP_ALFRESCO_SEARCH_ADMINISTRATORS");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 1) return false;
            if (result.values[0] != "admin") return false;
            
            return true;
        }


        private static bool DoGetGroupsUsers_Multiple_Users_Group_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroupsUsers(conn, customparams, "GROUP_USERS");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 5) return false;
            if (Array.IndexOf(result.values, "user1") == -1) return false;
            if (Array.IndexOf(result.values, "user5") == -1) return false;
            
            return true;
        }


        private static bool DoGetGroupsUsers_Empty_Group_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroupsUsers(conn, customparams, "GROUP_EMPTY");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 0) return false;
            
            return true;
        }


        private static bool DoGetGroupsUsers_Unknown_Group_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroupsUsers(conn, customparams, "GROUP_UNKNOWN");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length == 2) return true;
            
            return false;
        }


        private static bool DoGetGroupsUsers_Case_Insensitive_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroupsUsers(conn, customparams, "group_test");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 3) return false;
            if (Array.IndexOf(result.values, "testuser1") == -1) return false;
            if (Array.IndexOf(result.values, "testuser2") == -1) return false;
            if (Array.IndexOf(result.values, "testuser3") == -1) return false;
            
            return true;
        }


        private static bool DoGetGroupsUsers_With_Null_Connection_TEST_()
        {
            var result = DoGetGroupsUsers(null, new Hashtable(), "GROUP_ALFRESCO_SEARCH_ADMINISTRATORS");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 1) return false;
            if (result.values[0] != "admin") return false;
            
            return true;
        }


        private static bool DoGetGroupsUsers_With_Custom_Params_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            customparams["SelectedDataStores"] = "swsdp";
            var result = DoGetGroupsUsers(conn, customparams, "GROUP_ADMINISTRATORS");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 3) return false;
            if (Array.IndexOf(result.values, "admin") == -1) return false;
            if (Array.IndexOf(result.values, "sysadmin") == -1) return false;
            if (Array.IndexOf(result.values, "operator") == -1) return false;
            
            return true;
        }


        private static bool DoGetGroupsUsers_Developers_Group_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroupsUsers(conn, customparams, "GROUP_DEVELOPERS");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 4) return false;
            if (Array.IndexOf(result.values, "dev1") == -1) return false;
            if (Array.IndexOf(result.values, "dev2") == -1) return false;
            if (Array.IndexOf(result.values, "dev3") == -1) return false;
            if (Array.IndexOf(result.values, "leaddev") == -1) return false;
            
            return true;
        }


        private static bool DoGetGroupsUsers_Null_GroupId_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroupsUsers(conn, customparams, null);
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 0) return false;
            
            return true;
        }


        private static bool DoGetGroupsUsers_Empty_GroupId_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroupsUsers(conn, customparams, "");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 0) return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}