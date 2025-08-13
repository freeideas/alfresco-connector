# DoItemData Method - SOAP Request Processing through iCustomConnector2

## iCustomConnector2 Interface Method
```csharp
public abstract ItemReturn DoItemData(
    ConnectionInfo conn,
    Hashtable customparams,
    string id,
    string subid,
    string foldersubid,
    DataStoreInfo datastore,
    DataStoreTypeFilter typefilters,
    int maxFileSize,
    Hashtable allowedExtensions,
    DateTime lastUpdate,
    bool isIncremental,
    int crawlID,
    int contentID,
    string crawler,
    bool getmetadata,
    bool getsecurity,
    bool getfile
);
```
Location: `doc/copy_src/ICustomConnectorInterfaces.cs:13`

## SOAP Request to Interface Translation

### Incoming SOAP Request
The SOAP server receives a `ItemData2` action request.

### Interface Method Call
The SOAP server translates the `ItemData2` SOAP request to the `DoItemData` method call on the iCustomConnector2 implementation.

## Parameter Mapping

### 1. ConnectionInfo conn
**Source**: `<conn>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `ConnectionInfo`

### 2. Hashtable customparams
**Source**: `<customparams>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `Hashtable`

### 3. string id
**Source**: `<id>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `string`

### 4. string subid
**Source**: `<subid>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `string`

### 5. string foldersubid
**Source**: `<foldersubid>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `string`

### 6. DataStoreInfo datastore
**Source**: `<datastore>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `DataStoreInfo`

### 7. DataStoreTypeFilter typefilters
**Source**: `<typefilters>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `DataStoreTypeFilter`

### 8. int maxFileSize
**Source**: `<maxFileSize>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `int`

### 9. Hashtable allowedExtensions
**Source**: `<allowedExtensions>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `Hashtable`

### 10. DateTime lastUpdate
**Source**: `<lastUpdate>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `DateTime`

### 11. bool isIncremental
**Source**: `<isIncremental>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `bool`

### 12. int crawlID
**Source**: `<crawlID>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `int`

### 13. int contentID
**Source**: `<contentID>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `int`

### 14. string crawler
**Source**: `<crawler>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `string`

### 15. bool getmetadata
**Source**: `<getmetadata>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `bool`

### 16. bool getsecurity
**Source**: `<getsecurity>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `bool`

### 17. bool getfile
**Source**: `<getfile>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `bool`

## SOAP Server Processing Flow

1. **Receive SOAP Request**: Server receives HTTP POST with `ItemData2` SOAP envelope
2. **Parse SOAP XML**: Extracts parameter values from XML elements
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Create Objects**: Instantiates required objects from SOAP data
5. **Invoke Method**: Calls `DoItemData()` with parameters
6. **Return Response**: Serializes the `ItemReturn` result to SOAP response

## Return Type: ItemReturn

The method returns a `ItemReturn` object defined in the interface.

## Implementation Notes

1. **SOAP Action Mapping**: The SOAP action `ItemData2` maps to this interface method
2. **Parameter Extraction**: SOAP server extracts parameters from the XML request body
3. **Type Conversion**: SOAP types are converted to .NET types as needed
4. **Error Handling**: Exceptions are converted to SOAP faults
