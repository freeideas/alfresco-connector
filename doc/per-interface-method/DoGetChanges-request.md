# DoGetChanges Method - SOAP Request Processing through iCustomConnector2

## iCustomConnector2 Interface Method
```csharp
public abstract CrawlReturn DoGetChanges(
    ConnectionInfo conn,
    Hashtable customparams,
    string foldersubid,
    DataStoreInfo datastore,
    DataStoreTypeFilter typefilters,
    string customFilter,
    DateTime lastUpdate,
    int maxReturns,
    int crawlID,
    int contentID,
    string crawler
);
```
Location: `doc/copy_src/ICustomConnectorInterfaces.cs:12`

## SOAP Request to Interface Translation

### Incoming SOAP Request
The SOAP server receives a `GetChanges` action request.

### Interface Method Call
The SOAP server translates the `GetChanges` SOAP request to the `DoGetChanges` method call on the iCustomConnector2 implementation.

## Parameter Mapping

### 1. ConnectionInfo conn
**Source**: `<conn>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `ConnectionInfo`

### 2. Hashtable customparams
**Source**: `<customparams>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `Hashtable`

### 3. string foldersubid
**Source**: `<foldersubid>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `string`

### 4. DataStoreInfo datastore
**Source**: `<datastore>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `DataStoreInfo`

### 5. DataStoreTypeFilter typefilters
**Source**: `<typefilters>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `DataStoreTypeFilter`

### 6. string customFilter
**Source**: `<customFilter>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `string`

### 7. DateTime lastUpdate
**Source**: `<lastUpdate>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `DateTime`

### 8. int maxReturns
**Source**: `<maxReturns>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `int`

### 9. int crawlID
**Source**: `<crawlID>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `int`

### 10. int contentID
**Source**: `<contentID>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `int`

### 11. string crawler
**Source**: `<crawler>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `string`

## SOAP Server Processing Flow

1. **Receive SOAP Request**: Server receives HTTP POST with `GetChanges` SOAP envelope
2. **Parse SOAP XML**: Extracts parameter values from XML elements
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Create Objects**: Instantiates required objects from SOAP data
5. **Invoke Method**: Calls `DoGetChanges()` with parameters
6. **Return Response**: Serializes the `CrawlReturn` result to SOAP response

## Return Type: CrawlReturn

The method returns a `CrawlReturn` object defined in the interface.

## Implementation Notes

1. **SOAP Action Mapping**: The SOAP action `GetChanges` maps to this interface method
2. **Parameter Extraction**: SOAP server extracts parameters from the XML request body
3. **Type Conversion**: SOAP types are converted to .NET types as needed
4. **Error Handling**: Exceptions are converted to SOAP faults
