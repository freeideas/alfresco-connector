// Hardcoded implementation for iCustomConnector2.DoGetServers() method
// Returns: StringArrayReturn
// Interface method: public abstract StringArrayReturn DoGetServers(ConnectionInfo conn, Hashtable customparams);

var response = new StringArrayReturn
{
    error = false,
    errorMsg = "",
    values = new[]
    {
        "alfresco-prod.example.com",
        "alfresco-dev.example.com", 
        "alfresco-test.example.com",
        "content-server-01.local",
        "content-server-02.local",
        "sharepoint-bridge.example.com"
    }
};

// If the connection has a specific server in customparam, include it
if (!string.IsNullOrEmpty(conn.customparam))
{
    // Parse URL from customparam and extract server name
    if (System.Uri.TryCreate(conn.customparam, System.UriKind.Absolute, out var uri))
    {
        var serverList = new System.Collections.Generic.List<string>(response.values);
        if (!serverList.Contains(uri.Host))
        {
            serverList.Insert(0, uri.Host);
            response.values = serverList.ToArray();
        }
    }
}

return response;