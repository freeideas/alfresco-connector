// Example hardcoded return values for Describe method

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