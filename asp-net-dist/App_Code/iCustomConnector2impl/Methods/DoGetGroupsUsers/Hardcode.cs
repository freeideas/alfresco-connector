using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetGroupsUsers
{
    public static class Hardcode
    {
        public static StringArrayReturn GetGroupsUsers(ConnectionInfo conn, Hashtable customparams, string groupId)
        {
            var response = new StringArrayReturn
            {
                error = false,
                errorMsg = ""
            };

            var usersList = new List<string>();

            switch (groupId?.ToUpper())
            {
                case "GROUP_ALFRESCO_SEARCH_ADMINISTRATORS":
                    usersList.Add("admin");
                    break;
                    
                case "GROUP_TEST":
                    usersList.Add("testuser1");
                    usersList.Add("testuser2");
                    usersList.Add("testuser3");
                    break;
                    
                case "GROUP_ADMINISTRATORS":
                    usersList.Add("admin");
                    usersList.Add("sysadmin");
                    usersList.Add("operator");
                    break;
                    
                case "GROUP_USERS":
                    usersList.Add("user1");
                    usersList.Add("user2");
                    usersList.Add("user3");
                    usersList.Add("user4");
                    usersList.Add("user5");
                    break;
                    
                case "GROUP_DEVELOPERS":
                    usersList.Add("dev1");
                    usersList.Add("dev2");
                    usersList.Add("dev3");
                    usersList.Add("leaddev");
                    break;
                    
                case "GROUP_EMPTY":
                    break;
                    
                default:
                    if (!string.IsNullOrEmpty(groupId))
                    {
                        usersList.Add($"user_{groupId.ToLower().Replace("group_", "")}");
                        usersList.Add($"member_{groupId.ToLower().Replace("group_", "")}");
                    }
                    break;
            }

            response.values = usersList.ToArray();
            return response;
        }


        private static bool GetGroupsUsers_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroupsUsers(conn, customparams, "GROUP_ALFRESCO_SEARCH_ADMINISTRATORS");
            
            if (result.error) return false;
            if (!string.IsNullOrEmpty(result.errorMsg)) return false;
            if (result.values == null) return false;
            if (result.values.Length != 1) return false;
            if (result.values[0] != "admin") return false;
            
            return true;
        }


        private static bool GetGroupsUsers_Alfresco_Administrators_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroupsUsers(conn, customparams, "GROUP_ALFRESCO_SEARCH_ADMINISTRATORS");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 1) return false;
            if (result.values[0] != "admin") return false;
            
            return true;
        }


        private static bool GetGroupsUsers_Test_Group_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroupsUsers(conn, customparams, "GROUP_TEST");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 3) return false;
            if (Array.IndexOf(result.values, "testuser1") == -1) return false;
            if (Array.IndexOf(result.values, "testuser2") == -1) return false;
            if (Array.IndexOf(result.values, "testuser3") == -1) return false;
            
            return true;
        }


        private static bool GetGroupsUsers_Administrators_Group_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroupsUsers(conn, customparams, "GROUP_ADMINISTRATORS");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 3) return false;
            if (Array.IndexOf(result.values, "admin") == -1) return false;
            if (Array.IndexOf(result.values, "sysadmin") == -1) return false;
            if (Array.IndexOf(result.values, "operator") == -1) return false;
            
            return true;
        }


        private static bool GetGroupsUsers_Users_Group_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroupsUsers(conn, customparams, "GROUP_USERS");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 5) return false;
            if (Array.IndexOf(result.values, "user1") == -1) return false;
            if (Array.IndexOf(result.values, "user2") == -1) return false;
            if (Array.IndexOf(result.values, "user3") == -1) return false;
            if (Array.IndexOf(result.values, "user4") == -1) return false;
            if (Array.IndexOf(result.values, "user5") == -1) return false;
            
            return true;
        }


        private static bool GetGroupsUsers_Developers_Group_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroupsUsers(conn, customparams, "GROUP_DEVELOPERS");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 4) return false;
            if (Array.IndexOf(result.values, "dev1") == -1) return false;
            if (Array.IndexOf(result.values, "dev2") == -1) return false;
            if (Array.IndexOf(result.values, "dev3") == -1) return false;
            if (Array.IndexOf(result.values, "leaddev") == -1) return false;
            
            return true;
        }


        private static bool GetGroupsUsers_Empty_Group_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroupsUsers(conn, customparams, "GROUP_EMPTY");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 0) return false;
            
            return true;
        }


        private static bool GetGroupsUsers_Unknown_Group_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroupsUsers(conn, customparams, "GROUP_CUSTOM");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 2) return false;
            if (Array.IndexOf(result.values, "user_custom") == -1) return false;
            if (Array.IndexOf(result.values, "member_custom") == -1) return false;
            
            return true;
        }


        private static bool GetGroupsUsers_Case_Insensitive_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroupsUsers(conn, customparams, "group_alfresco_search_administrators");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 1) return false;
            if (result.values[0] != "admin") return false;
            
            return true;
        }


        private static bool GetGroupsUsers_Null_GroupId_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroupsUsers(conn, customparams, null);
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 0) return false;
            
            return true;
        }


        private static bool GetGroupsUsers_Empty_GroupId_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroupsUsers(conn, customparams, "");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 0) return false;
            
            return true;
        }


        private static bool GetGroupsUsers_With_Custom_Params_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            customparams["SelectedDataStores"] = "swsdp";
            var result = GetGroupsUsers(conn, customparams, "GROUP_ALFRESCO_SEARCH_ADMINISTRATORS");
            
            if (result.error) return false;
            if (result.values == null) return false;
            if (result.values.Length != 1) return false;
            if (result.values[0] != "admin") return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Hardcode)); }
    }
}