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

namespace Alfresco_Connector
{
    [WebService(Namespace = "http://tempuri.org/")]
    public class DataConnector : iCustomConnector2
    {
        private static readonly ILogger _logger = LogManager.GetLogger(typeof(DataConnector));
        private static readonly MemoryCache _cache = new MemoryCache("ConnectorCache");
        private const string siteFolders_CacheKey = "siteFolders_{connectionId}";

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

        public override DiscoveryInfo DoDescribe()
        {
            return new DiscoveryInfo()
            {
                ConnectorTitle = string.Format("Alfresco Connector - {0}", GetConnectorVersion()),

                Login = true,
                LoginPrompt = "Alfresco Content Service login",
                LoginDesc = "Alfresco Content Service admin credentials",

                ConnParam = true,
                ConnParamPrompt = "Alfresco API",
                ConnParamDesc = "Type the url of the Alfresco Content Service. Example: http://127.0.0.1:8080",
                ConnParamRequired = true,
                GroupLoad = true,
                UserLoad = true,
                GroupUsersLoad = true,
                GroupGroupsLoad = true,
                MailBoxes = true,
                SupportsChangeOnlyUpdate = true,
                Types = true,
                TypesFilter = true,
                TypesDesc = "Alfresco supported content types",
                TypesPrompt = "Select types to crawl"

            };
        }

        public override DataStoreInfoReturn DoGetAvailableDatastores(ConnectionInfo conn, Hashtable customparams, string filter1, string filter2, string filter3, string templateService, bool loadTemplates)
        {
            var dataStoreInfoReturn = new DataStoreInfoReturn();
            var client = GetCachedClient(conn, true);
            dataStoreInfoReturn.datastores = new AlfrescoTranslator().TranslateAlfrescoSites(client.GetSites()).ToArray();
            return dataStoreInfoReturn;
        }

        public override DataStoreTypeReturn DoGetDatastoreTypes(ConnectionInfo conn, Hashtable customparams, DataStoreInfo datastore)
        {
            _logger.Info("Entering DoGetDatastoreTypes");
            try
            {
                var ret = new DataStoreTypeReturn();
                var datastoreTypes = new List<DataStoreType>();
                var alfrescoProvider = GetProvider(conn, true);
                var translator = new AlfrescoTranslator();

                Stopwatch sw = Stopwatch.StartNew();
                var types = alfrescoProvider.GetTypes();
                sw.Stop();
                _logger.Debug($"GetTypes done. It took {sw.ElapsedMilliseconds}ms");

                sw.Restart();
                var aspects = alfrescoProvider.GetAspects();
                sw.Stop();
                _logger.Debug($"GetAspects done. It took {sw.ElapsedMilliseconds}ms");

                datastoreTypes = translator.ToDatastoreType(types,aspects);
                

               
                ret.datastoretypes = datastoreTypes.ToArray();

                _logger.Info($"DoGetDatastoreTypes jobs done. Returning {datastoreTypes.Count} types");

                return ret;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("DoGetDatastoreTypes failed: {0}", ex));
                throw;
            }
        }

       
   
        public override CrawlReturn DoCrawl(ConnectionInfo conn, Hashtable customparams, DataStoreInfo datastore, string foldersubId, DataStoreTypeFilter typefilters, string customFilter, Hashtable allowedExtensions, DateTime lastUpdate, bool isIncremental, int maxReturns, int maxFileSize, int crawlID, int contentID, string crawler, int page)
        {
            _logger.Info($"Entering DoCrawl for repository. isIncremental: {isIncremental}. MaxReturns: {maxReturns} page: {page}. LastModified {lastUpdate}");

            try
            {

                var ret = new CrawlReturn();

                var cri = new List<CrawlReturnItem>();

                var alfrescoProvider = GetProvider(conn, true);
                var translator = new AlfrescoTranslator();


                var alfrescoItems = alfrescoProvider.GetItems(datastore.id, lastUpdate, maxReturns, typefilters);
                ret = translator.ToCrawlReturn(alfrescoItems);

                if (isIncremental)
                {
                    _logger.Debug(string.Format("Incremental crawl done, found {0} items", ret.items.Length));
                    ret.moreExist = false;
                }
               

                _logger.Info($"Connection: {conn.connectionId} Enumeration Count: {ret.items.Length}.");
                _logger.Debug($"Crawl done for page {page}. Datastore id: {datastore.id}. Items returned on page: {ret.items.Count()}.LastModified {ret.lastUpdateDate}. MoreExist {ret.moreExist}. IsIncremental {isIncremental}"); 
               
                return ret;
            }
            catch (Exception ex)
            {
                
                _logger.Error(string.Format("DoCrawl failed: {0}", ex));
                throw;
            }
        }

        public override CrawlReturn DoGetChanges(ConnectionInfo conn, Hashtable customparams, string foldersubid, DataStoreInfo datastore, DataStoreTypeFilter typefilters, string customFilter, DateTime lastUpdate, int maxReturns, int crawlID, int contentID, string crawler)
        {
            CrawlReturn cr = new CrawlReturn { moreExist = false };
            _logger.Info($"Checking for deleted nodes");

            var  items = new List<CrawlReturnItem>();
            var alfrescoProvider = GetProvider(conn, true);

            try
            {
                var deletedNodes = alfrescoProvider.GetDeletedNodes();

                foreach(var delete in deletedNodes)
                {
                    var path = delete.Path.Name;

                    if (delete.ArchivedAt >= lastUpdate)
                    {
                        var cri = new CrawlReturnItem { id = delete.Id, datastoretypeid = delete.NodeType, lastUpdate = delete.ArchivedAt, deleted = true };
                        items.Add(cri);
                    }
                }

                cr.items = items.ToArray();
                cr.lastUpdateDate = DateTime.UtcNow;
            }
            catch(Exception ex)
            {
                _logger.Error(string.Format("DoGetChanges failed: {0}", ex));
                throw;
            }

            return cr;
           
        }

        public override ItemReturn DoItemData(ConnectionInfo conn, Hashtable customparams, string id, string subid, string foldersubid, DataStoreInfo datastore, DataStoreTypeFilter typefilters, int maxFileSize, Hashtable allowedExtensions, DateTime lastUpdate, bool isIncremental, int crawlID, int contentID, string crawler, bool getmetadata, bool getsecurity, bool getfile)
        {

            _logger.Debug(string.Format("DoItemData called for id: {0}", id));

            try
            {
                var ir = new ItemReturn();

                var alfrescoProvider = GetProvider(conn);
                var translator = new AlfrescoTranslator();


                var alfrescoItem = alfrescoProvider.GetItemDetails(id);

                if (!Utils.IsIncludedByFilter(alfrescoItem, typefilters))
                {
                    _logger.Info($"Item id {id} is not included in the type filter. Will be marked as deleted");
                    return new ItemReturn() { notFound = true };
                }

                ir = translator.ToItemReturn(alfrescoItem);


                ir.url = $"{conn.customparam}/share/page/site/{datastore.id}/document-details?nodeRef=workspace://SpacesStore/{id}";

                _logger.Info($"Finished fetching data+content for item {id}");
               
                return ir;

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("http error NotFound"))
                {
                    _logger.Info($"Item id {id} was not found in source system. Will mark as deleted");
                    _logger.Debug(ex.Message.ToString());
                    return new ItemReturn() { notFound = true };
                }
                else
                    _logger.Error(string.Format("DoItemDataFailed: {0}", ex));
                throw;
            }
        }

    

        public override SystemIdentityInfoReturn DoGetGroups(ConnectionInfo conn, Hashtable customparams, string additionalColumns)
        {
            _logger.Debug("Entering GetGroups call");
            try
            {
                var translator = new AlfrescoTranslator();


                var ret = new SystemIdentityInfoReturn();
                var identities = new List<SystemIdentityInfo>();
                var alfrescoProvider = GetProvider(conn);

                var allGroups = alfrescoProvider.GetGroups();
                ret = translator.GroupToSystemIdentityInfo(allGroups);

                _logger.Info($"DoGetGroups done. Returning {ret.items.Length} users.");

                return ret;


            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("DoGetGroups failed. {0}", ex));
                throw;
            }
        }

        public override StringArrayReturn DoGetGroupsGroups(ConnectionInfo conn, Hashtable customparams, string groupId)
        {
            _logger.Debug(string.Format("Entering GroupGroups call. GroupId: {0}", groupId));
            
            try
            {
                var ret = new StringArrayReturn();
                var members = new List<string>();

                var alfrescoProvider = GetProvider(conn);
                var groupGroups = alfrescoProvider.GetGroupGroups(groupId);

                foreach (string subgroup in groupGroups)
                {
                    if (!string.IsNullOrEmpty(subgroup))
                        members.Add(subgroup);
                }

                ret.values = members.ToArray();

                _logger.Info($"GetGroupGroups done. Returning {members.Count} subgroups for group {groupId}");
                return ret;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("DoGetGroupsGroups failed. {0}", ex));
                throw;
            }


        }
        public override StringArrayReturn DoGetGroupsUsers(ConnectionInfo conn, Hashtable customparams, string groupId)
        {
            _logger.Debug(string.Format("Entering GroupUsers call. GroupId: {0}", groupId));
            
            try
            {
                var ret = new StringArrayReturn();
                var members = new List<string>();
                var alfrescoProvider = GetProvider(conn);
                var groupUsers = alfrescoProvider.GetGroupUsers(groupId);

                foreach (string user in groupUsers)
                {
                    if (!string.IsNullOrEmpty(user))
                        members.Add(user);
                }

                ret.values = members.ToArray();

                _logger.Info($"GetGroupUsers done. Returning {members.Count} users for group {groupId}");
                return ret;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("DoGetGroupsGroups failed. {0}", ex));
                throw;
            }
        }
        public override SystemIdentityInfoReturn DoGetUsers(ConnectionInfo conn, Hashtable customparams, string additionalColumns)
        {
            _logger.Info("Entering GetUsers call");

            try
            {
                var ret = new SystemIdentityInfoReturn();
                var alfrescoProvider = GetProvider(conn, true);
                var translator = new AlfrescoTranslator();

                var allUsers = alfrescoProvider.GetUsers();

                ret = translator.UserToSystemIdentityInfo(allUsers);

                _logger.Info($"DoGetUsers done. Returning {ret.items.Length} users.");

                return ret;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("DoGetUsers failed. {0}", ex));
                throw;
            }
        }


        #region notImplemented
        public override StringArrayReturn DoGetServers(ConnectionInfo conn, Hashtable customparams)
        {
            throw new NotImplementedException();
        }

        public override WebServiceInfo DoGetWebServices()
        {
            throw new NotImplementedException();
        }

        public override SecurityItemReturn DoRealtimeSecurityCheck(ConnectionInfo conn, Hashtable customparams, SecurityItem[] items, string adusername, string[] userids)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
