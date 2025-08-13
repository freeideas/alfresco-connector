# DoRealtimeSecurityCheck Method - SOAP Request Processing through iCustomConnector2

## iCustomConnector2 Interface Method
```csharp
public abstract SecurityItemReturn DoRealtimeSecurityCheck(
    ConnectionInfo conn,
    Hashtable customparams,
    SecurityItem[] items,
    string adusername,
    string[] userids
);
```
**Return Type**: `SecurityItemReturn` (from DataConnector.Models)  
**Purpose**: Performs real-time security validation to check if a user has access to specific items

## SOAP Request to Interface Translation

### Incoming SOAP Request Structure (from actual client)
```xml
<RealtimeSecurityCheck xmlns="http://tempuri.org/">
  <conn>...</conn>
  <customparams>...</customparams>
  <items>...</items>
  <adusername>...</adusername>
  <userids>...</userids>
</RealtimeSecurityCheck>
```

## Parameter Mapping with Examples

### 1. ConnectionInfo conn
**Source**: `<conn>` element in SOAP  
**Interface Type**: `ConnectionInfo` (use from `/csproj/Models/ICustomConnectorInterfaces.cs`)  
**Example from actual request**:
```xml
<conn>
  <account>admin</account>
  <password>admin</password>
  <scrubAllData>true</scrubAllData>
  <connectionId>3</connectionId>
  <sourceSite>connectors</sourceSite>
  <customparam>http://100.115.192.75:8080/</customparam>
  <customparam2 />
  <customboolparam>false</customboolparam>
  <customboolparam2>false</customboolparam2>
  <customboolparam3>false</customboolparam3>
  <customboolparam4>false</customboolparam4>
  <customparam3 />
  <customparam4 />
</conn>
```

### 2. Hashtable customparams
**Source**: `<customparams>` element in SOAP  
**Interface Type**: `System.Collections.Hashtable`  
**Structure**: Contains `<CustomPair>` elements with `<key>` and `<value>` sub-elements  
**Example from actual request**:
```xml
<customparams>
  <CustomPair>
    <key>SPWCONTENTID</key>
    <value>3</value>
  </CustomPair>
</customparams>
```
**Parsing**: Extract each CustomPair and add to Hashtable as key-value pairs

### 3. SecurityItem[] items
**Source**: `<items>` element in SOAP  
**Interface Type**: `SecurityItem[]` (array from DataConnector.Models)  
**Example from actual request**:
```xml
<items>
  <SecurityItem>
    <id>5fa74ad3-9b5b-461b-9df5-de407f1f4fe7</id>
    <subid />
  </SecurityItem>
  <SecurityItem>
    <id>a38308f8-6f30-4d8a-8576-eaf6703fb9d3</id>
    <subid />
  </SecurityItem>
  <SecurityItem>
    <id>602b72e5-e365-4eee-b68d-b3dd26270ee3</id>
    <subid />
  </SecurityItem>
</items>
```

### 4. string adusername
**Source**: `<adusername>` element in SOAP  
**Interface Type**: `string`  
**Example**: `<adusername>testuser</adusername>`

### 5. string[] userids
**Source**: `<userids>` element in SOAP  
**Interface Type**: `string[]` (array of strings)  
**Structure**: Contains multiple `<string>` elements  
**Example from actual request**:
```xml
<userids>
  <string>user1</string>
  <string>user2</string>
</userids>
```

## SOAP Server Processing Flow

1. **Receive SOAP Request**: Server receives HTTP POST with `RealtimeSecurityCheck` SOAP envelope
2. **Parse SOAP XML**: Extracts parameter values from XML elements
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Create Objects**: Instantiates required objects from SOAP data
5. **Invoke Method**: Calls `DoRealtimeSecurityCheck()` with parameters
6. **Return Response**: Serializes the `SecurityItemReturn` result to SOAP response

## Return Type: SecurityItemReturn

The method returns a `SecurityItemReturn` object defined in the interface.

## Implementation Notes

1. **SOAP Action Mapping**: The SOAP action `RealtimeSecurityCheck` maps to this interface method
2. **Parameter Extraction**: SOAP server extracts parameters from the XML request body
3. **Type Conversion**: SOAP types are converted to .NET types as needed
4. **Error Handling**: Exceptions are converted to SOAP faults
