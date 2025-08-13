using System;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoDescribe
{
    public static class Hardcode
    {
        public static DiscoveryInfo GetDiscoveryInfo()
        {
            return new DiscoveryInfo
            {
                ConnectorTitle = "Alfresco Connector - 1.1.0.1-19c25df",
                Login = true,
                LoginPrompt = "Alfresco Content Service login",
                LoginDesc = "Alfresco Content Service admin credentials",
                Password = false,
                PasswordPrompt = "Password",
                
                ConnParam = true,
                ConnParamPrompt = "Alfresco API",
                ConnParamDesc = "Type the url of the Alfresco Content Service. Example: http://127.0.0.1:8080",
                ConnParamDefault = "",
                ConnParamRequired = true,
                
                ConnParam2 = false,
                ConnParamPrompt2 = "Parameter",
                ConnParamDesc2 = "Enter a parameter",
                ConnParamDefault2 = "",
                ConnParamRequired2 = false,
                
                ConnParam3 = false,
                ConnParamPrompt3 = "Parameter",
                ConnParamDesc3 = "Enter a parameter",
                ConnParamDefault3 = "",
                ConnParamRequired3 = false,
                
                ConnBoolParam = false,
                ConnBoolParamPrompt = "Bool Parameter",
                ConnBoolParamDesc = "Check for this to be true",
                ConnBoolParamDefault = false,
                
                ConnBoolParam2 = false,
                ConnBoolParamPrompt2 = "Bool Parameter",
                ConnBoolParamDesc2 = "Check for this to be true",
                ConnBoolParamDefault2 = false,
                
                ConnBoolParam3 = false,
                ConnBoolParamPrompt3 = "Bool Parameter",
                ConnBoolParamDesc3 = "Check for this to be true",
                ConnBoolParamDefault3 = false,
                
                ConnBoolParam4 = false,
                ConnBoolParamPrompt4 = "Bool Parameter",
                ConnBoolParamDesc4 = "Check for this to be true",
                ConnBoolParamDefault4 = false,
                
                DataStores = false,
                DataStoresPrompt = "Datastores",
                DataStoresDesc = "Pick a datastore",
                DataStoresMultiSelect = false,
                
                Types = true,
                TypesFilter = true,
                TypesPrompt = "Select types to crawl",
                TypesDesc = "Alfresco supported content types",
                
                UserLoad = true,
                GroupLoad = true,
                GroupUsersLoad = true,
                GroupGroupsLoad = true,
                
                AllUsersGroup = false,
                RealTimeSecurity = false,
                
                CheckForChangeBasedOnIDOnly = false,
                SupportsSecurityOnlyUpdate = false,
                SupportsChangeOnlyUpdate = true,
                
                UseDeltaForIncremental = false,
                UseCustomPagingFix = false,
                
                MailBoxes = true,
                
                ContentFilter = false,
                ContentFilterDesc = "",
                
                AuthorEditable = false,
                
                ImpersonateUser = false,
                MultiLoginDelimiter = ",",
                MultiLoginEnabled = false,
                
                SubLicenseCode = "",
                MultipleWebServiceOption = false
            };
        }


        private static bool GetDiscoveryInfo_TEST_()
        {
            var result = GetDiscoveryInfo();
            
            if (result.ConnectorTitle != "Alfresco Connector - 1.1.0.1-19c25df") return false;
            if (!result.Login) return false;
            if (result.Password) return false;
            if (!result.ConnParam) return false;
            if (!result.ConnParamRequired) return false;
            if (!result.Types) return false;
            if (!result.TypesFilter) return false;
            if (!result.UserLoad) return false;
            if (!result.GroupLoad) return false;
            if (!result.GroupUsersLoad) return false;
            if (!result.GroupGroupsLoad) return false;
            if (!result.SupportsChangeOnlyUpdate) return false;
            if (!result.MailBoxes) return false;
            
            return true;
        }


        private static bool GetDiscoveryInfo_String_Values_TEST_()
        {
            var result = GetDiscoveryInfo();
            
            if (result.LoginPrompt != "Alfresco Content Service login") return false;
            if (result.LoginDesc != "Alfresco Content Service admin credentials") return false;
            if (result.ConnParamPrompt != "Alfresco API") return false;
            if (!result.ConnParamDesc.Contains("http://127.0.0.1:8080")) return false;
            if (result.TypesPrompt != "Select types to crawl") return false;
            if (result.TypesDesc != "Alfresco supported content types") return false;
            if (result.MultiLoginDelimiter != ",") return false;
            if (result.SubLicenseCode != "") return false;
            if (result.ContentFilterDesc != "") return false;
            if (result.ConnParamDefault != "") return false;
            if (result.ConnParamDefault2 != "") return false;
            if (result.ConnParamDefault3 != "") return false;
            
            return true;
        }


        private static bool GetDiscoveryInfo_Boolean_False_Values_TEST_()
        {
            var result = GetDiscoveryInfo();
            
            if (result.Password) return false;
            if (result.ConnParam2) return false;
            if (result.ConnParam3) return false;
            if (result.ConnParamRequired2) return false;
            if (result.ConnParamRequired3) return false;
            if (result.ConnBoolParam) return false;
            if (result.ConnBoolParam2) return false;
            if (result.ConnBoolParam3) return false;
            if (result.ConnBoolParam4) return false;
            if (result.ConnBoolParamDefault) return false;
            if (result.ConnBoolParamDefault2) return false;
            if (result.ConnBoolParamDefault3) return false;
            if (result.ConnBoolParamDefault4) return false;
            if (result.DataStores) return false;
            if (result.DataStoresMultiSelect) return false;
            if (result.AllUsersGroup) return false;
            if (result.RealTimeSecurity) return false;
            if (result.CheckForChangeBasedOnIDOnly) return false;
            if (result.SupportsSecurityOnlyUpdate) return false;
            if (result.UseDeltaForIncremental) return false;
            if (result.UseCustomPagingFix) return false;
            if (result.ContentFilter) return false;
            if (result.AuthorEditable) return false;
            if (result.ImpersonateUser) return false;
            if (result.MultiLoginEnabled) return false;
            if (result.MultipleWebServiceOption) return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Hardcode)); }
    }
}