# DoDescribe Method - Complete Implementation Guide

## Overview
The DoDescribe method discovers connector capabilities and configuration options. It's typically called once during connector initialization to understand what features are available and how to configure the connection.

## Method Signature
```csharp
public abstract DiscoveryInfo DoDescribe();
```
**Location**: `doc/copy_src/ICustomConnectorInterfaces.cs:8`
**Returns**: `DiscoveryInfo` object containing connector capabilities

## Request Parameters
**No parameters** - This method takes no parameters and returns static/dynamic configuration information.

## Request Structure

### SOAP Request
```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <Describe xmlns="http://tempuri.org/" />
  </soap:Body>
</soap:Envelope>
```

### Processing Flow
1. **Receive SOAP Request**: Server receives HTTP POST with SOAP envelope
2. **Parse SOAP Action**: Identifies `Describe` action from SOAPAction header or body
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Invoke Method**: Calls `DoDescribe()` on the loaded instance
5. **Return Response**: Serializes the `DiscoveryInfo` result to SOAP response

## Response Structure

### Return Type: DiscoveryInfo
The method returns a `DiscoveryInfo` object (defined in `ICustomConnectorInterfaces.cs:25-92`) with the following key properties:

#### Core Configuration
- `ConnectorTitle` - Display name of the connector
- `Login` - Whether login is required
- `LoginPrompt` - UI prompt for login field  
- `Password` - Whether password is required
- `PasswordPrompt` - UI prompt for password field

#### Connection Parameters
- `ConnParam` - Enable first string parameter
- `ConnParamPrompt` - Prompt text for parameter
- `ConnParamDesc` - Parameter description
- `ConnParamRequired` - Whether parameter is required
- `ConnParamDefault` - Default value
- Similar patterns for ConnParam2, ConnParam3

#### Boolean Parameters
- `ConnBoolParam` - Enable first boolean parameter
- `ConnBoolParamPrompt` - Prompt text
- `ConnBoolParamDesc` - Description
- `ConnBoolParamDefault` - Default value
- Similar patterns for ConnBoolParam2, ConnBoolParam3, ConnBoolParam4

#### Feature Capabilities
- `DataStores` - Support for datastores
- `Types` - Support for content types
- `UserLoad` - Support user loading
- `GroupLoad` - Support group loading
- `GroupUsersLoad` - Load group members
- `GroupGroupsLoad` - Load nested groups
- `RealTimeSecurity` - Real-time security checking
- `SupportsIncrementalCrawling` - Incremental update support
- `SupportsChangeOnlyUpdate` - Change-only updates
- `SupportsSecurityOnlyUpdate` - Security-only updates

### SOAP Response
```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <DescribeResponse xmlns="http://tempuri.org/">
      <DescribeResult>
        <ConnectorTitle>External API Connector - 1.0.0</ConnectorTitle>
        <Login>true</Login>
        <LoginPrompt>External API login</LoginPrompt>
        <LoginDesc>External API admin credentials</LoginDesc>
        <Password>false</Password>
        <PasswordPrompt>Password</PasswordPrompt>
        <ConnParam>true</ConnParam>
        <ConnParamPrompt>API Base URL</ConnParamPrompt>
        <ConnParamDesc>Enter the base URL for the external API</ConnParamDesc>
        <ConnParamRequired>true</ConnParamRequired>
        <DataStores>false</DataStores>
        <Types>true</Types>
        <UserLoad>true</UserLoad>
        <GroupLoad>true</GroupLoad>
        <GroupUsersLoad>true</GroupUsersLoad>
        <GroupGroupsLoad>true</GroupGroupsLoad>
        <RealTimeSecurity>false</RealTimeSecurity>
        <SupportsIncrementalCrawling>true</SupportsIncrementalCrawling>
        <!-- Additional properties... -->
      </DescribeResult>
    </DescribeResponse>
  </soap:Body>
</soap:Envelope>
```

## Mock Response Example

### Typical Implementation Response
```csharp
public override DiscoveryInfo DoDescribe()
{
    return new DiscoveryInfo
    {
        ConnectorTitle = "External API Connector - 1.0.0",
        Login = true,
        LoginPrompt = "External API login",
        LoginDesc = "External API admin credentials",
        Password = false,
        ConnParam = true,
        ConnParamPrompt = "API Base URL",
        ConnParamDesc = "Enter the base URL for the external API",
        ConnParamRequired = true,
        ConnParamDefault = "",
        DataStores = false,
        Types = true,
        TypesPrompt = "Select content types",
        TypesDesc = "Choose which content types to crawl",
        UserLoad = true,
        GroupLoad = true,
        GroupUsersLoad = true,
        GroupGroupsLoad = true,
        RealTimeSecurity = false,
        SupportsIncrementalCrawling = true,
        SupportsChangeOnlyUpdate = true,
        SupportsSecurityOnlyUpdate = false,
        UseDeltaForIncremental = false,
        CheckForChangeBasedOnIDOnly = false,
        UseCustomPagingFix = false,
        MultipleWebServiceOption = false
    };
}
```

## External API Mapping

### Discovery Endpoints
- **Path**: `/api/-default-/public/alfresco/versions/1/discovery`
- **Method**: GET
- **Purpose**: Get server capabilities and version information

### Authentication Mapping
| SOAP Field | External API | Notes |
|------------|--------------|-------|
| N/A (no auth for describe) | Basic Auth (optional) | May be used to test connectivity |

### Response Mapping
| External API Field | SOAP Response | Notes |
|-------------------|---------------|-------|
| `repository.version` | `ConnectorTitle` | Include version in title |
| Static configuration | Most fields | Predetermined connector capabilities |

### Example External API Call
```bash
curl -u admin:admin \
  "http://100.115.192.75:8080/alfresco/api/-default-/public/alfresco/versions/1/discovery"
```

## Error Handling
| Scenario | Response |
|----------|----------|
| Normal operation | Return filled DiscoveryInfo object |
| External API unavailable | Return static configuration (most properties are static anyway) |
| Authentication test fails | Still return DiscoveryInfo but note in logs |

## Implementation Notes

1. **Static Configuration**: Most values are predetermined based on the connector's capabilities
2. **No Authentication Required**: This method can be called before authentication to discover connection requirements
3. **Version Information**: Can optionally retrieve version from external API discovery endpoint
4. **UI Configuration**: The prompts and descriptions configure the user interface in the crawling system
5. **Feature Flags**: Boolean properties tell the crawler what operations are supported
6. **Parameter Definition**: Connection parameters define what users need to provide for authentication and configuration
7. **Backward Compatibility**: Ensure all required properties are populated to avoid serialization issues

## Important Notes

- Initialize all string properties to empty strings to avoid null reference exceptions
- Boolean properties default to false unless explicitly set to true
- This method is called early in the connector lifecycle before authentication
- The response helps configure the user interface and determines available crawling features
- Most implementations return largely static configuration with optional dynamic version information