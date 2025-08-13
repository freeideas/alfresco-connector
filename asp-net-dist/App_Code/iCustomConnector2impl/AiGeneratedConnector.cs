using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector
{
    /// <summary>
    /// Main implementation class that unifies all the method handlers into a single iCustomConnector2 implementation.
    /// This class serves as the central integration point, delegating each method to its respective Handler.
    /// </summary>
    public class Connector : iCustomConnector2
    {
        public override DiscoveryInfo DoDescribe()
        {
            return Methods.DoDescribe.Handler.DoDescribe();
        }

        public override DataStoreInfoReturn DoGetAvailableDatastores(ConnectionInfo conn, Hashtable customparams, string filter1, string filter2, string filter3, string templateService, bool loadTemplates)
        {
            return Methods.DoGetAvailableDatastores.Handler.DoGetAvailableDatastores(conn, customparams, filter1, filter2, filter3, templateService, loadTemplates);
        }

        public override DataStoreTypeReturn DoGetDatastoreTypes(ConnectionInfo conn, Hashtable customparams, DataStoreInfo datastore)
        {
            return Methods.DoGetDatastoreTypes.Handler.DoGetDatastoreTypes(conn, customparams, datastore);
        }

        public override CrawlReturn DoCrawl(ConnectionInfo conn, Hashtable customparams, DataStoreInfo datastore, string foldersubId, DataStoreTypeFilter typefilters, string customFilter, Hashtable allowedExtensions, DateTime lastUpdate, bool isIncremental, int maxReturns, int maxFileSize, int crawlID, int contentID, string crawler, int page)
        {
            return Methods.DoCrawl.Handler.DoCrawl(conn, customparams, datastore, foldersubId, typefilters, customFilter, allowedExtensions, lastUpdate, isIncremental, maxReturns, maxFileSize, crawlID, contentID, crawler, page);
        }

        public override CrawlReturn DoGetChanges(ConnectionInfo conn, Hashtable customparams, string foldersubid, DataStoreInfo datastore, DataStoreTypeFilter typefilters, string customFilter, DateTime lastUpdate, int maxReturns, int crawlID, int contentID, string crawler)
        {
            return Methods.DoGetChanges.Handler.DoGetChanges(conn, customparams, foldersubid, datastore, typefilters, customFilter, lastUpdate, maxReturns, crawlID, contentID, crawler);
        }

        public override ItemReturn DoItemData(ConnectionInfo conn, Hashtable customparams, string id, string subid, string foldersubid, DataStoreInfo datastore, DataStoreTypeFilter typefilters, int maxFileSize, Hashtable allowedExtensions, DateTime lastUpdate, bool isIncremental, int crawlID, int contentID, string crawler, bool getmetadata, bool getsecurity, bool getfile)
        {
            return Methods.DoItemData.Handler.DoItemData(conn, customparams, id, subid, foldersubid, datastore, typefilters, maxFileSize, allowedExtensions, lastUpdate, isIncremental, crawlID, contentID, crawler, getmetadata, getsecurity, getfile);
        }

        public override SystemIdentityInfoReturn DoGetUsers(ConnectionInfo conn, Hashtable customparams, string additionalColumns)
        {
            return Methods.DoGetUsers.Handler.DoGetUsers(conn, customparams, additionalColumns);
        }

        public override SystemIdentityInfoReturn DoGetGroups(ConnectionInfo conn, Hashtable customparams, string additionalColumns)
        {
            return Methods.DoGetGroups.Handler.DoGetGroups(conn, customparams, additionalColumns);
        }

        public override StringArrayReturn DoGetGroupsUsers(ConnectionInfo conn, Hashtable customparams, string groupId)
        {
            return Methods.DoGetGroupsUsers.Handler.DoGetGroupsUsers(conn, customparams, groupId);
        }

        public override StringArrayReturn DoGetGroupsGroups(ConnectionInfo conn, Hashtable customparams, string groupId)
        {
            return Methods.DoGetGroupsGroups.Handler.DoGetGroupsGroups(conn, customparams, groupId);
        }

        public override StringArrayReturn DoGetServers(ConnectionInfo conn, Hashtable customparams)
        {
            return Methods.DoGetServers.Handler.DoGetServers(conn, customparams);
        }

        public override WebServiceInfo DoGetWebServices()
        {
            return Methods.DoGetWebServices.Handler.DoGetWebServices();
        }

        public override SecurityItemReturn DoRealtimeSecurityCheck(ConnectionInfo conn, Hashtable customparams, SecurityItem[] items, string adusername, string[] userids)
        {
            return Methods.DoRealtimeSecurityCheck.Handler.DoRealtimeSecurityCheck(conn, customparams, items, adusername, userids);
        }
    }
}