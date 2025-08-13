using System;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Alfresco.iCustomConnector2;

namespace Alfresco
{
    /// <summary>
    /// Alfresco Data Connector Web Service
    /// Standalone ASMX deployment for IIS
    /// </summary>
    [WebService(Namespace = "http://www.alfresco.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class DataConnector : WebService, iCustomConnector2
    {
        private readonly AiGeneratedConnector _impl;
        
        public DataConnector()
        {
            _impl = new AiGeneratedConnector();
        }
        
        #region iCustomConnector2 Implementation
        
        [WebMethod]
        public CustomDataOut DoDescribe(CustomConnection conn, CustomConnectorRequest Request, string[] FileTypes)
        {
            return _impl.DoDescribe(conn, Request, FileTypes);
        }
        
        [WebMethod]
        public CustomDataOut DoGetAvailableDatastores(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetAvailableDatastores(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoGetDatastoreTypes(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetDatastoreTypes(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoCrawl(CustomConnection conn, CustomConnectorRequest Request, string ItemId, bool Recurse, string[] FileTypes)
        {
            return _impl.DoCrawl(conn, Request, ItemId, Recurse, FileTypes);
        }
        
        [WebMethod]
        public CustomDataOut DoGetChanges(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetChanges(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoItemData(CustomConnection conn, CustomConnectorRequest Request, string ItemId, string ItemType)
        {
            return _impl.DoItemData(conn, Request, ItemId, ItemType);
        }
        
        [WebMethod]
        public CustomDataOut DoGetGroups(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetGroups(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoGetGroupsGroups(CustomConnection conn, CustomConnectorRequest Request, string GroupId)
        {
            return _impl.DoGetGroupsGroups(conn, Request, GroupId);
        }
        
        [WebMethod]
        public CustomDataOut DoGetGroupsUsers(CustomConnection conn, CustomConnectorRequest Request, string GroupId)
        {
            return _impl.DoGetGroupsUsers(conn, Request, GroupId);
        }
        
        [WebMethod]
        public CustomDataOut DoGetUsers(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetUsers(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoGetServers(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetServers(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoGetWebServices(CustomConnection conn, CustomConnectorRequest Request, string ServerId)
        {
            return _impl.DoGetWebServices(conn, Request, ServerId);
        }
        
        [WebMethod]
        public CustomDataOut DoRealtimeSecurityCheck(CustomConnection conn, CustomConnectorRequest Request, string ItemId, string ItemType, string UserOrGroupName, bool IsUser)
        {
            return _impl.DoRealtimeSecurityCheck(conn, Request, ItemId, ItemType, UserOrGroupName, IsUser);
        }
        
        #endregion
        
        #region Version Information
        
        [WebMethod]
        public string GetConnectorVersion()
        {
            return "1.0.0";
        }
        
        [WebMethod]
        public string GetInterfaceVersion()
        {
            return "2.0";
        }
        
        #endregion
    }
}
