using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetGroups
{
    public static class Hardcode
    {
        public static SystemIdentityInfoReturn GetGroups(ConnectionInfo conn, Hashtable customparams, string additionalColumns)
        {
            var groups = new[]
            {
                new SystemIdentityInfo
                {
                    id = "GROUP_ALFRESCO_ADMINISTRATORS",
                    name = "ALFRESCO_ADMINISTRATORS",
                    displayName = "ALFRESCO_ADMINISTRATORS",
                    email = ""
                },
                new SystemIdentityInfo
                {
                    id = "GROUP_ALFRESCO_MODEL_ADMINISTRATORS",
                    name = "ALFRESCO_MODEL_ADMINISTRATORS",
                    displayName = "ALFRESCO_MODEL_ADMINISTRATORS",
                    email = ""
                },
                new SystemIdentityInfo
                {
                    id = "GROUP_ALFRESCO_SEARCH_ADMINISTRATORS",
                    name = "ALFRESCO_SEARCH_ADMINISTRATORS",
                    displayName = "ALFRESCO_SEARCH_ADMINISTRATORS",
                    email = ""
                },
                new SystemIdentityInfo
                {
                    id = "GROUP_ALFRESCO_SYSTEM_ADMINISTRATORS",
                    name = "ALFRESCO_SYSTEM_ADMINISTRATORS",
                    displayName = "ALFRESCO_SYSTEM_ADMINISTRATORS",
                    email = ""
                },
                new SystemIdentityInfo
                {
                    id = "GROUP_EMAIL_CONTRIBUTORS",
                    name = "EMAIL_CONTRIBUTORS",
                    displayName = "EMAIL_CONTRIBUTORS",
                    email = ""
                },
                new SystemIdentityInfo
                {
                    id = "GROUP_SITE_ADMINISTRATORS",
                    name = "SITE_ADMINISTRATORS",
                    displayName = "SITE_ADMINISTRATORS",
                    email = ""
                },
                new SystemIdentityInfo
                {
                    id = "GROUP_site_swsdp",
                    name = "site_swsdp",
                    displayName = "site_swsdp",
                    email = ""
                },
                new SystemIdentityInfo
                {
                    id = "GROUP_site_swsdp_SiteCollaborator",
                    name = "site_swsdp_SiteCollaborator",
                    displayName = "site_swsdp_SiteCollaborator",
                    email = ""
                },
                new SystemIdentityInfo
                {
                    id = "GROUP_site_swsdp_SiteConsumer",
                    name = "site_swsdp_SiteConsumer",
                    displayName = "site_swsdp_SiteConsumer",
                    email = ""
                },
                new SystemIdentityInfo
                {
                    id = "GROUP_site_swsdp_SiteContributor",
                    name = "site_swsdp_SiteContributor",
                    displayName = "site_swsdp_SiteContributor",
                    email = ""
                },
                new SystemIdentityInfo
                {
                    id = "GROUP_site_swsdp_SiteManager",
                    name = "site_swsdp_SiteManager",
                    displayName = "site_swsdp_SiteManager",
                    email = ""
                }
            };

            return new SystemIdentityInfoReturn
            {
                identities = groups,
                error = false,
                errorMsg = ""
            };
        }


        private static bool GetGroups_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroups(conn, customparams, "");
            
            if (result.error) return false;
            if (!string.IsNullOrEmpty(result.errorMsg)) return false;
            if (result.identities == null) return false;
            if (result.identities.Length != 11) return false;
            
            return true;
        }


        private static bool GetGroups_Alfresco_Administrators_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroups(conn, customparams, "");
            
            var admin = Array.Find(result.identities, g => g.id == "GROUP_ALFRESCO_ADMINISTRATORS");
            if (admin == null) return false;
            if (admin.name != "ALFRESCO_ADMINISTRATORS") return false;
            if (admin.displayName != "ALFRESCO_ADMINISTRATORS") return false;
            if (admin.email != "") return false;
            
            return true;
        }


        private static bool GetGroups_Site_Swsdp_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroups(conn, customparams, "");
            
            var site = Array.Find(result.identities, g => g.id == "GROUP_site_swsdp");
            if (site == null) return false;
            if (site.name != "site_swsdp") return false;
            if (site.displayName != "site_swsdp") return false;
            if (site.email != "") return false;
            
            return true;
        }


        private static bool GetGroups_Site_Manager_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroups(conn, customparams, "");
            
            var manager = Array.Find(result.identities, g => g.id == "GROUP_site_swsdp_SiteManager");
            if (manager == null) return false;
            if (manager.name != "site_swsdp_SiteManager") return false;
            if (manager.displayName != "site_swsdp_SiteManager") return false;
            if (manager.email != "") return false;
            
            return true;
        }


        private static bool GetGroups_Email_Contributors_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroups(conn, customparams, "");
            
            var emailContrib = Array.Find(result.identities, g => g.id == "GROUP_EMAIL_CONTRIBUTORS");
            if (emailContrib == null) return false;
            if (emailContrib.name != "EMAIL_CONTRIBUTORS") return false;
            if (emailContrib.displayName != "EMAIL_CONTRIBUTORS") return false;
            if (emailContrib.email != "") return false;
            
            return true;
        }


        private static bool GetGroups_All_Have_Group_Prefix_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetGroups(conn, customparams, "");
            
            foreach (var group in result.identities)
            {
                if (!group.id.StartsWith("GROUP_")) return false;
            }
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Hardcode)); }
    }
}