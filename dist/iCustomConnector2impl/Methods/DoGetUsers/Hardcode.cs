using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetUsers
{
    public static class Hardcode
    {
        public static SystemIdentityInfoReturn GetUsers(ConnectionInfo conn, Hashtable customparams, string additionalColumns)
        {
            return new SystemIdentityInfoReturn
            {
                error = false,
                errorMsg = "",
                identities = new[]
                {
                    new SystemIdentityInfo
                    {
                        id = "abeecher",
                        name = "abeecher",
                        displayName = "Alice Beecher",
                        email = "abeecher@example.com"
                    },
                    new SystemIdentityInfo
                    {
                        id = "admin",
                        name = "admin",
                        displayName = "Administrator",
                        email = "admin@alfresco.com"
                    },
                    new SystemIdentityInfo
                    {
                        id = "guest",
                        name = "guest",
                        displayName = "Guest",
                        email = ""
                    },
                    new SystemIdentityInfo
                    {
                        id = "mjackson",
                        name = "mjackson",
                        displayName = "Mike Jackson",
                        email = "mjackson@example.com"
                    }
                }
            };
        }


        private static bool GetUsers_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetUsers(conn, customparams, "");
            
            if (result.error) return false;
            if (!string.IsNullOrEmpty(result.errorMsg)) return false;
            if (result.identities == null) return false;
            if (result.identities.Length != 4) return false;
            
            return true;
        }


        private static bool GetUsers_Alice_Beecher_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetUsers(conn, customparams, "");
            
            var alice = Array.Find(result.identities, u => u.id == "abeecher");
            if (alice == null) return false;
            if (alice.name != "abeecher") return false;
            if (alice.displayName != "Alice Beecher") return false;
            if (alice.email != "abeecher@example.com") return false;
            
            return true;
        }


        private static bool GetUsers_Administrator_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetUsers(conn, customparams, "");
            
            var admin = Array.Find(result.identities, u => u.id == "admin");
            if (admin == null) return false;
            if (admin.name != "admin") return false;
            if (admin.displayName != "Administrator") return false;
            if (admin.email != "admin@alfresco.com") return false;
            
            return true;
        }


        private static bool GetUsers_Guest_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetUsers(conn, customparams, "");
            
            var guest = Array.Find(result.identities, u => u.id == "guest");
            if (guest == null) return false;
            if (guest.name != "guest") return false;
            if (guest.displayName != "Guest") return false;
            if (guest.email != "") return false;
            
            return true;
        }


        private static bool GetUsers_Mike_Jackson_TEST_()
        {
            var conn = new ConnectionInfo { account = "admin", password = "admin" };
            var customparams = new Hashtable();
            var result = GetUsers(conn, customparams, "");
            
            var mike = Array.Find(result.identities, u => u.id == "mjackson");
            if (mike == null) return false;
            if (mike.name != "mjackson") return false;
            if (mike.displayName != "Mike Jackson") return false;
            if (mike.email != "mjackson@example.com") return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Hardcode)); }
    }
}