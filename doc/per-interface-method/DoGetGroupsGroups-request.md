# DoGetGroupsGroups Method - SOAP Request Processing through iCustomConnector2

## iCustomConnector2 Interface Method
```csharp
public abstract StringArrayReturn DoGetGroupsGroups(
    ConnectionInfo conn,
    Hashtable customparams,
    string groupId
);
```
Location: `doc/copy_src/ICustomConnectorInterfaces.cs:18`

## SOAP Request to Interface Translation

### Incoming SOAP Request
The SOAP server receives a `GetGroupsGroups2` action request.

### Interface Method Call
The SOAP server translates the `GetGroupsGroups2` SOAP request to the `DoGetGroupsGroups` method call on the iCustomConnector2 implementation.

## Parameter Mapping

### 1. ConnectionInfo conn
**Source**: `<conn>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `ConnectionInfo`

### 2. Hashtable customparams
**Source**: `<customparams>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `Hashtable`

### 3. string groupId
**Source**: `<groupId>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `string`

## SOAP Server Processing Flow

1. **Receive SOAP Request**: Server receives HTTP POST with `GetGroupsGroups2` SOAP envelope
2. **Parse SOAP XML**: Extracts parameter values from XML elements
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Create Objects**: Instantiates required objects from SOAP data
5. **Invoke Method**: Calls `DoGetGroupsGroups()` with parameters
6. **Return Response**: Serializes the `StringArrayReturn` result to SOAP response

## Return Type: StringArrayReturn

The method returns a `StringArrayReturn` object defined in the interface.

## Implementation Notes

1. **SOAP Action Mapping**: The SOAP action `GetGroupsGroups2` maps to this interface method
2. **Parameter Extraction**: SOAP server extracts parameters from the XML request body
3. **Type Conversion**: SOAP types are converted to .NET types as needed
4. **Error Handling**: Exceptions are converted to SOAP faults
