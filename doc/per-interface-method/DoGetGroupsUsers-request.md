# DoGetGroupsUsers Method - SOAP Request Processing through iCustomConnector2

## iCustomConnector2 Interface Method
```csharp
public abstract StringArrayReturn DoGetGroupsUsers(
    ConnectionInfo conn,
    Hashtable customparams,
    string groupId
);
```
Location: `doc/copy_src/ICustomConnectorInterfaces.cs:17`

## SOAP Request to Interface Translation

### Incoming SOAP Request
The SOAP server receives a `GetGroupsUsers2` action request.

### Interface Method Call
The SOAP server translates the `GetGroupsUsers2` SOAP request to the `DoGetGroupsUsers` method call on the iCustomConnector2 implementation.

## Parameter Mapping

### 1. ConnectionInfo conn
**Source**: `<conn>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `ConnectionInfo`
**Key Fields Used**:
- `account`, `password`, `connectionId`, `sourceSite`
- `customparam`, `customparam2`, `customparam3`, `customparam4`
- `customboolparam`, `customboolparam2`, `customboolparam3`, `customboolparam4`
- `scrubAllData`

**Note**: The `<conn>` element also contains redundant nested collections (`BoolParameter`, `StringParameter`, `ChoiceParameter`) that duplicate the direct field values. These nested collections can be safely ignored during parsing.

### 2. Hashtable customparams
**Source**: `<customparams>` element in SOAP  
**Mapping**: Extract `<CustomPair>` elements and create key-value pairs  
**Interface Type**: `Hashtable`
**Structure**:
```xml
<customparams>
  <CustomPair>
    <key>SelectedDataStores</key>
    <value>swsdp</value>
  </CustomPair>
</customparams>
```

### 3. string groupId
**Source**: `<groupId>` element in SOAP  
**Mapping**: Direct string extraction  
**Interface Type**: `string`
**Example**: `GROUP_ALFRESCO_SEARCH_ADMINISTRATORS`

### 4. TraceLevel (Additional Parameter)
**Source**: `<tl>` element in SOAP  
**Mapping**: Direct string extraction (not passed to interface method)  
**Purpose**: Controls logging/trace verbosity  
**Example Values**: `Warning`, `Info`, `Debug`

## SOAP Server Processing Flow

1. **Receive SOAP Request**: Server receives HTTP POST with `GetGroupsUsers2` SOAP envelope
2. **Parse SOAP XML**: Extracts parameter values from XML elements
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Create Objects**: Instantiates required objects from SOAP data
5. **Invoke Method**: Calls `DoGetGroupsUsers()` with parameters
6. **Return Response**: Serializes the `StringArrayReturn` result to SOAP response

## Return Type: StringArrayReturn

The method returns a `StringArrayReturn` object defined in the interface.

## Implementation Notes

1. **SOAP Action Mapping**: The SOAP action `GetGroupsUsers2` maps to this interface method
2. **Parameter Extraction**: SOAP server extracts parameters from the XML request body
3. **Type Conversion**: SOAP types are converted to .NET types as needed
4. **Error Handling**: Exceptions are converted to SOAP faults

## Example SOAP Request (from actual transcript)

```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetGroupsUsers2 xmlns="http://tempuri.org/">
      <conn>
        <account>admin</account>
        <password>admin</password>
        <scrubAllData>true</scrubAllData>
        <connectionId>3</connectionId>
        <sourceSite>connectors</sourceSite>
        <customparam>http://100.115.192.75:8080/</customparam>
        <!-- Additional fields and nested collections omitted for brevity -->
      </conn>
      <customparams>
        <CustomPair>
          <key>SelectedDataStores</key>
          <value>swsdp</value>
        </CustomPair>
      </customparams>
      <groupId>GROUP_ALFRESCO_SEARCH_ADMINISTRATORS</groupId>
      <tl>Warning</tl>
    </GetGroupsUsers2>
  </soap:Body>
</soap:Envelope>
```
