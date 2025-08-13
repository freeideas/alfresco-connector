using System;
using System.Collections;

namespace SPWorks.Search.Service.Interfaces
{
    public abstract class iCustomConnector2
    {
        public abstract DiscoveryInfo DoDescribe();
        public abstract DataStoreInfoReturn DoGetAvailableDatastores(ConnectionInfo conn, Hashtable customparams, string filter1, string filter2, string filter3, string templateService, bool loadTemplates);
        public abstract DataStoreTypeReturn DoGetDatastoreTypes(ConnectionInfo conn, Hashtable customparams, DataStoreInfo datastore);
        public abstract CrawlReturn DoCrawl(ConnectionInfo conn, Hashtable customparams, DataStoreInfo datastore, string foldersubId, DataStoreTypeFilter typefilters, string customFilter, Hashtable allowedExtensions, DateTime lastUpdate, bool isIncremental, int maxReturns, int maxFileSize, int crawlID, int contentID, string crawler, int page);
        public abstract CrawlReturn DoGetChanges(ConnectionInfo conn, Hashtable customparams, string foldersubid, DataStoreInfo datastore, DataStoreTypeFilter typefilters, string customFilter, DateTime lastUpdate, int maxReturns, int crawlID, int contentID, string crawler);
        public abstract ItemReturn DoItemData(ConnectionInfo conn, Hashtable customparams, string id, string subid, string foldersubid, DataStoreInfo datastore, DataStoreTypeFilter typefilters, int maxFileSize, Hashtable allowedExtensions, DateTime lastUpdate, bool isIncremental, int crawlID, int contentID, string crawler, bool getmetadata, bool getsecurity, bool getfile);
        public abstract SecurityItemReturn DoRealtimeSecurityCheck(ConnectionInfo conn, Hashtable customparams, SecurityItem[] items, string adusername, string[] userids);
        public abstract SystemIdentityInfoReturn DoGetUsers(ConnectionInfo conn, Hashtable customparams, string additionalColumns);
        public abstract SystemIdentityInfoReturn DoGetGroups(ConnectionInfo conn, Hashtable customparams, string additionalColumns);
        public abstract StringArrayReturn DoGetGroupsUsers(ConnectionInfo conn, Hashtable customparams, string groupId);
        public abstract StringArrayReturn DoGetGroupsGroups(ConnectionInfo conn, Hashtable customparams, string groupId);
        public abstract StringArrayReturn DoGetServers(ConnectionInfo conn, Hashtable customparams);
        public abstract WebServiceInfo DoGetWebServices();
        protected string GetConnectorVersion() { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0.0"; }
    }


    public class DiscoveryInfo
    {
        public string ConnectorTitle { get; set; } = "";
        public string SubLicenseCode { get; set; } = "";
        public bool Login { get; set; }
        public string LoginPrompt { get; set; } = "";
        public string LoginDesc { get; set; } = "";
        public bool Password { get; set; }
        public string PasswordPrompt { get; set; } = "";
        public bool MultiLoginEnabled { get; set; }
        public string MultiLoginDelimiter { get; set; } = "";
        public bool ImpersonateUser { get; set; }
        public bool AuthorEditable { get; set; }
        public bool RealTimeSecurity { get; set; }
        public bool AllUsersGroup { get; set; }
        public bool Types { get; set; }
        public bool TypesFilter { get; set; }
        public string TypesPrompt { get; set; } = "";
        public string TypesDesc { get; set; } = "";
        public bool GroupLoad { get; set; }
        public bool UserLoad { get; set; }
        public bool GroupUsersLoad { get; set; }
        public bool GroupGroupsLoad { get; set; }
        public bool CheckForChangeBasedOnIDOnly { get; set; }
        public bool SupportsChangeOnlyUpdate { get; set; }
        public bool SupportsSecurityOnlyUpdate { get; set; }
        public bool ContentFilter { get; set; }
        public string ContentFilterDesc { get; set; } = "";
        public bool ConnParam { get; set; }
        public string ConnParamPrompt { get; set; } = "";
        public string ConnParamDesc { get; set; } = "";
        public bool ConnParamRequired { get; set; }
        public string ConnParamDefault { get; set; } = "";
        public bool ConnParam2 { get; set; }
        public string ConnParamPrompt2 { get; set; } = "";
        public string ConnParamDesc2 { get; set; } = "";
        public bool ConnParamRequired2 { get; set; }
        public string ConnParamDefault2 { get; set; } = "";
        public bool ConnParam3 { get; set; }
        public string ConnParamPrompt3 { get; set; } = "";
        public string ConnParamDesc3 { get; set; } = "";
        public bool ConnParamRequired3 { get; set; }
        public string ConnParamDefault3 { get; set; } = "";
        public bool ConnBoolParam { get; set; }
        public string ConnBoolParamPrompt { get; set; } = "";
        public string ConnBoolParamDesc { get; set; } = "";
        public bool ConnBoolParamDefault { get; set; }
        public bool ConnBoolParam2 { get; set; }
        public string ConnBoolParamPrompt2 { get; set; } = "";
        public string ConnBoolParamDesc2 { get; set; } = "";
        public bool ConnBoolParamDefault2 { get; set; }
        public bool ConnBoolParam3 { get; set; }
        public string ConnBoolParamPrompt3 { get; set; } = "";
        public string ConnBoolParamDesc3 { get; set; } = "";
        public bool ConnBoolParamDefault3 { get; set; }
        public bool ConnBoolParam4 { get; set; }
        public string ConnBoolParamPrompt4 { get; set; } = "";
        public string ConnBoolParamDesc4 { get; set; } = "";
        public bool ConnBoolParamDefault4 { get; set; }
        public bool DataStores { get; set; }
        public bool DataStoresMultiSelect { get; set; }
        public string DataStoresPrompt { get; set; } = "";
        public string DataStoresDesc { get; set; } = "";
        public bool UseDeltaForIncremental { get; set; }
        public bool UseCustomPagingFix { get; set; }
        public bool MailBoxes { get; set; }
        public bool MultipleWebServiceOption { get; set; }
    }


    public class ConnectionInfo
    {
        public string account { get; set; } = "";
        public string password { get; set; } = "";
        public string customparam { get; set; } = "";
        public string customparam2 { get; set; } = "";
        public string customparam3 { get; set; } = "";
        public bool customboolparam { get; set; }
        public bool customboolparam2 { get; set; }
        public bool customboolparam3 { get; set; }
        public bool customboolparam4 { get; set; }
    }


    public class DataStoreInfo
    {
        public string id { get; set; } = "";
        public string name { get; set; } = "";
        public string desc { get; set; } = "";
        public string path { get; set; } = "";
    }


    public class DataStoreInfoReturn
    {
        public DataStoreInfo[] datastores { get; set; } = Array.Empty<DataStoreInfo>();
        public bool error { get; set; }
        public string errorMsg { get; set; } = "";
    }


    public class DataStoreType
    {
        public string id { get; set; } = "";
        public string name { get; set; } = "";
        public TypeData[] typedata { get; set; } = Array.Empty<TypeData>();
    }


    public class TypeData
    {
        public string name { get; set; } = "";
        public string type { get; set; } = "";
        public string displayname { get; set; } = "";
        public bool searchable { get; set; }
        public bool facetable { get; set; }
        public bool sortable { get; set; }
    }


    public class DataStoreTypeReturn
    {
        public DataStoreType[] datastoretypes { get; set; } = Array.Empty<DataStoreType>();
        public bool error { get; set; }
        public string errorMsg { get; set; } = "";
    }


    public class DataStoreTypeFilter
    {
        public string[] types { get; set; } = Array.Empty<string>();
    }


    public class CrawlReturn
    {
        public CrawlReturnItem[] items { get; set; } = Array.Empty<CrawlReturnItem>();
        public bool error { get; set; }
        public string errorMsg { get; set; } = "";
        public bool moreExist { get; set; }
        public string nextStartId { get; set; } = "";
        public DateTime nextStartDate { get; set; }
    }


    public class CrawlReturnItem
    {
        public string id { get; set; } = "";
        public string subid { get; set; } = "";
        public string type { get; set; } = "";
        public string title { get; set; } = "";
        public string path { get; set; } = "";
        public DateTime lastModified { get; set; }
        public bool deleted { get; set; }
    }


    public class ItemReturn
    {
        public string id { get; set; } = "";
        public string subid { get; set; } = "";
        public string type { get; set; } = "";
        public string title { get; set; } = "";
        public string author { get; set; } = "";
        public DateTime lastModified { get; set; }
        public DateTime created { get; set; }
        public string contentType { get; set; } = "";
        public long size { get; set; }
        public byte[]? content { get; set; }
        public string contentUrl { get; set; } = "";
        public Hashtable? metadata { get; set; }
        public string[] allowedUsers { get; set; } = Array.Empty<string>();
        public string[] deniedUsers { get; set; } = Array.Empty<string>();
        public string[] allowedGroups { get; set; } = Array.Empty<string>();
        public string[] deniedGroups { get; set; } = Array.Empty<string>();
        public bool error { get; set; }
        public string errorMsg { get; set; } = "";
    }


    public class SecurityItem
    {
        public string id { get; set; } = "";
        public string subid { get; set; } = "";
    }


    public class SecurityItemReturn
    {
        public SecurityItemResult[] results { get; set; } = Array.Empty<SecurityItemResult>();
        public bool error { get; set; }
        public string errorMsg { get; set; } = "";
    }


    public class SecurityItemResult
    {
        public string id { get; set; } = "";
        public string subid { get; set; } = "";
        public bool allowed { get; set; }
    }


    public class SystemIdentityInfo
    {
        public string id { get; set; } = "";
        public string name { get; set; } = "";
        public string displayName { get; set; } = "";
        public string email { get; set; } = "";
    }


    public class SystemIdentityInfoReturn
    {
        public SystemIdentityInfo[] identities { get; set; } = Array.Empty<SystemIdentityInfo>();
        public bool error { get; set; }
        public string errorMsg { get; set; } = "";
    }


    public class StringArrayReturn
    {
        public string[] values { get; set; } = Array.Empty<string>();
        public bool error { get; set; }
        public string errorMsg { get; set; } = "";
    }


    public class WebServiceInfo
    {
        public CustomPair[] services { get; set; } = Array.Empty<CustomPair>();
    }


    public class CustomPair
    {
        public string key { get; set; } = "";
        public string value { get; set; } = "";
    }
}