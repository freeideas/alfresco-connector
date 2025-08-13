using Alfresco_Connector.Provider;
using Alfresco_Connector.Translator;
using Alfresco_Connector.Types;
using BAInsight.Logging;
using SPWorks.Search.Service.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Services;
using AiGeneratedConnector; // ADDED: Our implementation namespace

namespace Alfresco_Connector
{
    [WebService(Namespace = "http://tempuri.org/")]
    public class DataConnector : iCustomConnector2
    {
        private static readonly ILogger _logger = LogManager.GetLogger(typeof(DataConnector));
        private static readonly MemoryCache _cache = new MemoryCache("ConnectorCache");
        private const string siteFolders_CacheKey = "siteFolders_{connectionId}";
        
        // ADDED: Single instance of our implementation
        private static readonly Connector _impl = new Connector();

        public virtual AlfrescoProvider GetProvider(ConnectionInfo conn, bool forceLogin = false)
        {
            var client = GetCachedClient(conn, forceLogin);
            return new AlfrescoProvider(client);
        }

        public AlfrescoClient GetCachedClient(ConnectionInfo conn,bool forceLogin = false)
        {
            string key = "AlfrescoClient_" + conn.connectionId;
            var existsInCache = _cache.Get(key);

            if (existsInCache == null || forceLogin)
            {
                if (existsInCache != null) _cache.Remove(key);
                _logger.Debug(string.Format("Client not found in cache. Creating new one. Key: {0}", key));
                var client = new AlfrescoClient(conn.account, conn.password, conn.customparam,conn.connectionId.ToString());
                var item = new System.Runtime.Caching.CacheItem(key, client);
                var policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(30) };
                _cache.Add(item, policy);
            }
            else
            {
                _logger.Debug(string.Format("Client found in cache. Key: {0}", key));
            }

            return (AlfrescoClient)_cache[key];
        }

        // MODIFIED: All override methods now delegate to _impl
        public override DiscoveryInfo DoDescribe()
        {
            return _impl.DoDescribe();
        }

        public override DataStoreInfoReturn DoGetAvailableDatastores(ConnectionInfo conn, Hashtable customparams, string filter1, string filter2, string filter3, string templateService, bool loadTemplates)
        {
            return _impl.DoGetAvailableDatastores(conn, customparams, filter1, filter2, filter3, templateService, loadTemplates);
        }

        public override DataStoreTypeReturn DoGetDatastoreTypes(ConnectionInfo conn, Hashtable customparams, DataStoreInfo datastore)
        {
            return _impl.DoGetDatastoreTypes(conn, customparams, datastore);
        }

        public override CrawlReturn DoCrawl(ConnectionInfo conn, Hashtable customparams, DataStoreInfo datastore, string foldersubId, DataStoreTypeFilter typefilters, string customFilter, Hashtable allowedExtensions, DateTime lastUpdate, bool isIncremental, int maxReturns, int maxFileSize, int crawlID, int contentID, string crawler, int page)
        {
            return _impl.DoCrawl(conn, customparams, datastore, foldersubId, typefilters, customFilter, allowedExtensions, lastUpdate, isIncremental, maxReturns, maxFileSize, crawlID, contentID, crawler, page);
        }

        public override CrawlReturn DoGetChanges(ConnectionInfo conn, Hashtable customparams, string foldersubid, DataStoreInfo datastore, DataStoreTypeFilter typefilters, string customFilter, DateTime lastUpdate, int maxReturns, int crawlID, int contentID, string crawler)
        {
            return _impl.DoGetChanges(conn, customparams, foldersubid, datastore, typefilters, customFilter, lastUpdate, maxReturns, crawlID, contentID, crawler);
        }

        public override ItemReturn DoItemData(ConnectionInfo conn, Hashtable customparams, string id, string subid, string foldersubid, DataStoreInfo datastore, DataStoreTypeFilter typefilters, int maxFileSize, Hashtable allowedExtensions, DateTime lastUpdate, bool isIncremental, int crawlID, int contentID, string crawler, bool getmetadata, bool getsecurity, bool getfile)
        {
            return _impl.DoItemData(conn, customparams, id, subid, foldersubid, datastore, typefilters, maxFileSize, allowedExtensions, lastUpdate, isIncremental, crawlID, contentID, crawler, getmetadata, getsecurity, getfile);
        }

        public override SystemIdentityInfoReturn DoGetGroups(ConnectionInfo conn, Hashtable customparams, string additionalColumns)
        {
            return _impl.DoGetGroups(conn, customparams, additionalColumns);
        }

        public override StringArrayReturn DoGetGroupsGroups(ConnectionInfo conn, Hashtable customparams, string groupId)
        {
            return _impl.DoGetGroupsGroups(conn, customparams, groupId);
        }

        public override StringArrayReturn DoGetGroupsUsers(ConnectionInfo conn, Hashtable customparams, string groupId)
        {
            return _impl.DoGetGroupsUsers(conn, customparams, groupId);
        }

        public override SystemIdentityInfoReturn DoGetUsers(ConnectionInfo conn, Hashtable customparams, string additionalColumns)
        {
            return _impl.DoGetUsers(conn, customparams, additionalColumns);
        }

        // MODIFIED: All methods now properly delegate to _impl
        public override StringArrayReturn DoGetServers(ConnectionInfo conn, Hashtable customparams)
        {
            return _impl.DoGetServers(conn, customparams);
        }

        public override WebServiceInfo DoGetWebServices()
        {
            return _impl.DoGetWebServices();
        }

        public override SecurityItemReturn DoRealtimeSecurityCheck(ConnectionInfo conn, Hashtable customparams, SecurityItem[] items, string adusername, string[] userids)
        {
            return _impl.DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);
        }
    }
}