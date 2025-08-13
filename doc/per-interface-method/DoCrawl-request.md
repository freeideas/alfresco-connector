# DoCrawl Method - SOAP Request Processing through iCustomConnector2

## iCustomConnector2 Interface Method
```csharp
public abstract CrawlReturn DoCrawl(
    ConnectionInfo conn, 
    Hashtable customparams, 
    DataStoreInfo datastore, 
    string foldersubId, 
    DataStoreTypeFilter typefilters, 
    string customFilter, 
    Hashtable allowedExtensions, 
    DateTime lastUpdate, 
    bool isIncremental, 
    int maxReturns, 
    int maxFileSize, 
    int crawlID, 
    int contentID, 
    string crawler, 
    int page
);
```
Location: `doc/copy_src/ICustomConnectorInterfaces.cs:11`

## SOAP Request to Interface Translation

### Incoming SOAP Request
The SOAP server receives a `Crawl2` action request containing crawl parameters.

### Interface Method Call
The SOAP server translates the `Crawl2` SOAP request to the `DoCrawl` method call on the iCustomConnector2 implementation.

## Parameter Mapping

The SOAP server extracts parameters from the SOAP XML and maps them to the interface method parameters:

### 1. ConnectionInfo conn
**Source**: `<conn>` element in SOAP  
**Mapping**: Creates ConnectionInfo object from SOAP connection data  
**Interface Type**: `ConnectionInfo` (defined at `ICustomConnectorInterfaces.cs:76-91`)
**Fields from actual SOAP**:
- `account`: User account name
- `password`: User password  
- `scrubAllData`: Boolean flag (typically true)
- `connectionId`: Integer connection identifier
- `sourceSite`: Source site identifier (e.g., "connectors")
- `customparam`: Custom parameter (often a URL)
- `customparam2`, `customparam3`, `customparam4`: Additional custom parameters (often empty)
- `customboolparam`, `customboolparam2`, `customboolparam3`, `customboolparam4`: Boolean custom parameters
**Note**: The SOAP also contains BoolParameter, StringParameter, and ChoiceParameter collections which are redundant with the direct fields

### 2. Hashtable customparams
**Source**: `<customparams>` collection of `<CustomPair>` elements  
**Mapping**: Converts key-value pairs to Hashtable  
**Interface Type**: `System.Collections.Hashtable`
**Common keys from actual SOAP**:
- `SPWCONTENTID`: Content ID value (e.g., "3")
- `SPWCRAWLID`: Crawl ID value (e.g., "0")
- `SPWCRAWLER`: Crawler identifier (e.g., "371857150_TEST")
- `MaxReturns`: Maximum returns override (e.g., "10")

### 3. DataStoreInfo datastore
**Source**: `<datastore>` element  
**Mapping**: Creates DataStoreInfo from SOAP datastore data  
**Interface Type**: `DataStoreInfo` (defined at `ICustomConnectorInterfaces.cs:93-102`)
**Fields from actual SOAP**:
- `name`: DataStore name (e.g., "Sample: Web Site Design Project")
- `desc`: Description (often empty)
- `owner`: Owner (often empty)
- `id`: Unique identifier (e.g., "swsdp")
- `server`: Server (often empty)
- `MBFlagValue`: Boolean flag (e.g., true)

### 4. string foldersubId
**Source**: `<foldersubId>` element  
**Mapping**: Direct string mapping (empty string if element is empty)  
**Interface Type**: `string`

### 5. DataStoreTypeFilter typefilters
**Source**: `<typefilters>` element  
**Mapping**: Creates filter from type array  
**Interface Type**: `DataStoreTypeFilter` (defined at `ICustomConnectorInterfaces.cs:135-140`)
**Fields from actual SOAP**:
- `filterAction`: Action type (e.g., "IncludeAll")
- `dataStoreTypes`: List<string> containing type filters (often contains single empty string element)

### 6. string customFilter
**Source**: Not present in typical SOAP requests  
**Mapping**: Empty string default  
**Interface Type**: `string`

### 7. Hashtable allowedExtensions
**Source**: `<allowedExtensions>` element  
**Mapping**: Converts to Hashtable (often empty)  
**Interface Type**: `System.Collections.Hashtable`

### 8. DateTime lastUpdate
**Source**: `<lastUpdate>` element  
**Mapping**: Parses ISO 8601 datetime string  
**Interface Type**: `System.DateTime`

### 9. bool isIncremental
**Source**: `<isIncremental>` element  
**Mapping**: Direct boolean conversion  
**Interface Type**: `bool`

### 10. int maxReturns
**Source**: `<maxReturns>` element  
**Mapping**: Direct integer conversion  
**Interface Type**: `int`

### 11. int maxFileSize
**Source**: `<maxFileSize>` element  
**Mapping**: Direct integer conversion (bytes)  
**Interface Type**: `int`

### 12. int crawlID
**Source**: `<crawlID>` element  
**Mapping**: Direct integer conversion  
**Interface Type**: `int`

### 13. int contentID
**Source**: `<contentID>` element  
**Mapping**: Direct integer conversion  
**Interface Type**: `int`

### 14. string crawler
**Source**: `<crawler>` element  
**Mapping**: Direct string mapping  
**Interface Type**: `string`

### 15. int page
**Source**: `<page>` element  
**Mapping**: Direct integer conversion (for pagination)  
**Interface Type**: `int`

## SOAP Server Processing Flow

1. **Receive SOAP Request**: Server receives HTTP POST with `Crawl2` SOAP envelope
2. **Parse SOAP XML**: Extracts all parameter values from XML elements
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Create Objects**: Instantiates ConnectionInfo, DataStoreInfo, and other required objects
5. **Invoke Method**: Calls `DoCrawl()` with all 15 parameters
6. **Return Response**: Serializes the `CrawlReturn` result to SOAP response

## Return Type: CrawlReturn

The method returns a `CrawlReturn` object (defined at `ICustomConnectorInterfaces.cs:159-167`) containing:
- Array of `CrawlReturnItem` objects (items found)
- Error status and message
- Pagination information (moreExist, nextStartId, nextStartDate)

## Implementation Notes

1. **Complex Parameter Mapping**: This method has the most parameters of any iCustomConnector2 method
2. **Pagination Support**: The `page` parameter and return values support paginated results
3. **Incremental Crawling**: The `isIncremental` and `lastUpdate` parameters enable delta crawling
4. **Type Filtering**: The `typefilters` parameter allows selective crawling by content type
5. **SOAP Elements Not Mapped**: 
   - `<tl>`: Trace level (e.g., "Warning") - not passed to interface method
   - `<customchoiceval>`, `<customchoiceval2>`, `<customchoiceval3>`, `<customchoiceval4>`: Choice values in conn element (typically empty)
   - `<BoolParameter>`, `<StringParameter>`, `<ChoiceParameter>`: Collections in conn that duplicate the direct field values