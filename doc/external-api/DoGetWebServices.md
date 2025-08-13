# DoGetWebServices - External API Mapping

## Overview
Maps the SOAP `GetWebServices` request to return configured web service endpoints.

## External API Endpoints

This method typically doesn't call external APIs but returns configuration about available SOAP web services.

## Request Mapping

### From SOAP to External API

| SOAP Field | External API | Notes |
|------------|--------------|-------|
| `conn.account` | Not used | For authentication check only |
| `conn.password` | Not used | For authentication check only |

## Response Mapping

This returns static configuration about the SOAP web service itself:

### Response Structure
```xml
<WebServiceInfoReturn>
  <error>false</error>
  <errorMsg />
  <services>
    <WebServiceInfo>
      <name>DataConnector</name>
      <endpoint>http://localhost:1582/DataConnector.asmx</endpoint>
      <wsdl>http://localhost:1582/DataConnector.asmx?wsdl</wsdl>
      <description>SOAP to REST API Bridge Service</description>
      <version>1.0.0</version>
      <methods>
        <Method>
          <name>Crawl2</name>
          <description>Browse repository content</description>
        </Method>
        <Method>
          <name>GetUsers2</name>
          <description>Retrieve user list</description>
        </Method>
        <Method>
          <name>GetGroups2</name>
          <description>Retrieve group list</description>
        </Method>
        <Method>
          <name>ItemData2</name>
          <description>Download file content</description>
        </Method>
        <Method>
          <name>CheckSecurity2</name>
          <description>Verify permissions</description>
        </Method>
        <Method>
          <name>GetChanges</name>
          <description>Get incremental changes</description>
        </Method>
        <Method>
          <name>Describe</name>
          <description>Get connector capabilities</description>
        </Method>
        <Method>
          <name>GetAvailableDatastores2</name>
          <description>List available repositories</description>
        </Method>
        <Method>
          <name>GetDatastoreTypes2</name>
          <description>List content types</description>
        </Method>
        <Method>
          <name>GetGroupsUsers2</name>
          <description>Get group memberships</description>
        </Method>
        <Method>
          <name>GetGroupsGroups2</name>
          <description>Get nested groups</description>
        </Method>
        <Method>
          <name>GetServers2</name>
          <description>Get server endpoints</description>
        </Method>
      </methods>
    </WebServiceInfo>
  </services>
</WebServiceInfoReturn>
```

## Error Handling

| Error Condition | SOAP Response |
|----------------|---------------|
| Invalid credentials | `error=true`, `errorMsg="Authentication failed"` |
| Internal error | `error=true`, `errorMsg="Configuration error"` |

## Implementation Notes

1. This is a metadata method about the SOAP service itself
2. Returns information about available SOAP methods
3. Useful for service discovery and documentation
4. The endpoint and WSDL URLs should match actual deployment
5. Version information helps clients verify compatibility
6. No external API call is typically needed