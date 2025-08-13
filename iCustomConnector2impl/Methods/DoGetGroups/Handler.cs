using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetGroups
{
    public static class Handler
    {
        public static SystemIdentityInfoReturn DoGetGroups(ConnectionInfo conn, Hashtable customparams, string additionalColumns)
        {
            try
            {
                return Hardcode.GetGroups(conn, customparams, additionalColumns);
            }
            catch (Exception ex)
            {
                return new SystemIdentityInfoReturn
                {
                    error = true,
                    errorMsg = $"Error in DoGetGroups: {ex.Message}",
                    identities = new SystemIdentityInfo[0]
                };
            }
        }


        private static bool DoGetGroups_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroups(conn, customparams, "");
            
            if (result.error) return false;
            if (!string.IsNullOrEmpty(result.errorMsg)) return false;
            if (result.identities == null) return false;
            if (result.identities.Length != 11) return false;
            
            return true;
        }


        private static bool DoGetGroups_Returns_Expected_Groups_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroups(conn, customparams, "");
            
            var groupIds = new string[result.identities.Length];
            for (int i = 0; i < result.identities.Length; i++)
                groupIds[i] = result.identities[i].id;
            
            if (Array.IndexOf(groupIds, "GROUP_ALFRESCO_ADMINISTRATORS") == -1) return false;
            if (Array.IndexOf(groupIds, "GROUP_ALFRESCO_MODEL_ADMINISTRATORS") == -1) return false;
            if (Array.IndexOf(groupIds, "GROUP_ALFRESCO_SEARCH_ADMINISTRATORS") == -1) return false;
            if (Array.IndexOf(groupIds, "GROUP_ALFRESCO_SYSTEM_ADMINISTRATORS") == -1) return false;
            if (Array.IndexOf(groupIds, "GROUP_EMAIL_CONTRIBUTORS") == -1) return false;
            if (Array.IndexOf(groupIds, "GROUP_SITE_ADMINISTRATORS") == -1) return false;
            if (Array.IndexOf(groupIds, "GROUP_site_swsdp") == -1) return false;
            if (Array.IndexOf(groupIds, "GROUP_site_swsdp_SiteCollaborator") == -1) return false;
            if (Array.IndexOf(groupIds, "GROUP_site_swsdp_SiteConsumer") == -1) return false;
            if (Array.IndexOf(groupIds, "GROUP_site_swsdp_SiteContributor") == -1) return false;
            if (Array.IndexOf(groupIds, "GROUP_site_swsdp_SiteManager") == -1) return false;
            
            return true;
        }


        private static bool DoGetGroups_Group_Properties_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroups(conn, customparams, "");
            
            foreach (var group in result.identities)
            {
                if (string.IsNullOrEmpty(group.id)) return false;
                if (string.IsNullOrEmpty(group.name)) return false;
                if (string.IsNullOrEmpty(group.displayName)) return false;
                if (!group.id.StartsWith("GROUP_")) return false;
                // NOTE: email is expected to be empty for all groups
            }
            
            return true;
        }


        private static bool DoGetGroups_With_Null_Connection_TEST_()
        {
            var result = DoGetGroups(null, new Hashtable(), "");
            
            // NOTE: Should still return hardcoded data even with null connection in hardcode phase
            if (result.error) return false;
            if (result.identities == null) return false;
            if (result.identities.Length != 11) return false;
            
            return true;
        }


        private static bool DoGetGroups_With_Custom_Params_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            customparams["SelectedDataStores"] = "swsdp";
            var result = DoGetGroups(conn, customparams, "Active,Description");
            
            if (result.error) return false;
            if (result.identities == null) return false;
            if (result.identities.Length != 11) return false;
            
            return true;
        }


        private static bool DoGetGroups_Alfresco_Administrators_Details_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroups(conn, customparams, "");
            
            var alfrescoAdmins = Array.Find(result.identities, g => g.id == "GROUP_ALFRESCO_ADMINISTRATORS");
            if (alfrescoAdmins == null) return false;
            if (alfrescoAdmins.name != "ALFRESCO_ADMINISTRATORS") return false;
            if (alfrescoAdmins.displayName != "ALFRESCO_ADMINISTRATORS") return false;
            if (alfrescoAdmins.email != "") return false;
            
            return true;
        }


        private static bool DoGetGroups_Site_Swsdp_Details_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroups(conn, customparams, "");
            
            var siteSwsdp = Array.Find(result.identities, g => g.id == "GROUP_site_swsdp");
            if (siteSwsdp == null) return false;
            if (siteSwsdp.name != "site_swsdp") return false;
            if (siteSwsdp.displayName != "site_swsdp") return false;
            if (siteSwsdp.email != "") return false;
            
            return true;
        }


        private static bool DoGetGroups_Site_Manager_Details_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroups(conn, customparams, "");
            
            var siteManager = Array.Find(result.identities, g => g.id == "GROUP_site_swsdp_SiteManager");
            if (siteManager == null) return false;
            if (siteManager.name != "site_swsdp_SiteManager") return false;
            if (siteManager.displayName != "site_swsdp_SiteManager") return false;
            if (siteManager.email != "") return false;
            
            return true;
        }


        private static bool DoGetGroups_All_Unique_IDs_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = DoGetGroups(conn, customparams, "");
            
            for (int i = 0; i < result.identities.Length; i++)
            {
                for (int j = i + 1; j < result.identities.Length; j++)
                {
                    if (result.identities[i].id == result.identities[j].id) return false;
                }
            }
            
            return true;
        }


        private static bool DoGetGroups_Exception_Handling_TEST_()
        {
            // NOTE: This test simulates an exception scenario by using unusual parameters
            var conn = new ConnectionInfo { account = "", password = "" };
            var customparams = new Hashtable();
            var result = DoGetGroups(conn, customparams, null);
            
            // NOTE: In hardcode phase, this should still work since we ignore parameters
            if (result.error) return false;
            if (result.identities == null) return false;
            if (result.identities.Length != 11) return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}