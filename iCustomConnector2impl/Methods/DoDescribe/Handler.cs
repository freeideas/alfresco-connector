using System;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoDescribe
{
    public static class Handler
    {
        public static DiscoveryInfo DoDescribe()
        {
            try
            {
                return Hardcode.GetDiscoveryInfo();
            }
            catch (Exception ex)
            {
                // NOTE: DiscoveryInfo doesn't have error fields, so we return a minimal valid object
                return new DiscoveryInfo
                {
                    ConnectorTitle = $"Error in DoDescribe: {ex.Message}",
                    Login = false,
                    Password = false
                };
            }
        }


        private static bool DoDescribe_TEST_()
        {
            var result = DoDescribe();
            
            if (string.IsNullOrEmpty(result.ConnectorTitle)) return false;
            if (!result.ConnectorTitle.Contains("Alfresco Connector")) return false;
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


        private static bool DoDescribe_Configuration_TEST_()
        {
            var result = DoDescribe();
            
            // Test specific configuration values from the JSON example
            if (result.ConnectorTitle != "Alfresco Connector - 1.1.0.1-19c25df") return false;
            if (result.LoginPrompt != "Alfresco Content Service login") return false;
            if (result.LoginDesc != "Alfresco Content Service admin credentials") return false;
            if (result.ConnParamPrompt != "Alfresco API") return false;
            if (!result.ConnParamDesc.Contains("http://127.0.0.1:8080")) return false;
            if (result.TypesPrompt != "Select types to crawl") return false;
            if (result.TypesDesc != "Alfresco supported content types") return false;
            if (result.MultiLoginDelimiter != ",") return false;
            
            return true;
        }


        private static bool DoDescribe_Boolean_Defaults_TEST_()
        {
            var result = DoDescribe();
            
            // Test boolean properties that should be false
            if (result.Password) return false;
            if (result.ConnParam2) return false;
            if (result.ConnParam3) return false;
            if (result.ConnBoolParam) return false;
            if (result.ConnBoolParam2) return false;
            if (result.ConnBoolParam3) return false;
            if (result.ConnBoolParam4) return false;
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


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}