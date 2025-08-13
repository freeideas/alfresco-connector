# DoCrawl Method - iCustomConnector2 Response to SOAP Translation

## iCustomConnector2 Return Type
```csharp
public abstract CrawlReturn DoCrawl(...);
```
Returns: `CrawlReturn` object

## SOAP Response Translation

The SOAP server receives the `CrawlReturn` object from the iCustomConnector2 implementation and translates it to a SOAP response.

### Interface Return to SOAP Response Flow

1. **DLL Returns Object**: The iCustomConnector2 implementation returns a `CrawlReturn` instance
2. **Server Wraps Response**: SOAP server wraps the object in a `Crawl2Response` element
3. **Serialize to XML**: Object properties are serialized to XML elements
4. **Add SOAP Envelope**: Response is wrapped in standard SOAP envelope
5. **Return HTTP Response**: Complete SOAP XML is returned with HTTP 200 OK

## CrawlReturn to SOAP Mapping

The `CrawlReturn` object (defined at `ICustomConnectorInterfaces.cs:142-150`) contains:
- `items`: Array of CrawlReturnItem objects
- `error`: Boolean error flag  
- `errorMsg`: Error message string
- `moreExist`: Boolean indicating more results available
- `nextStartId`: ID for next page (typically unused, empty string)
- `nextStartDate`: DateTime for next page

**IMPORTANT**: The actual SOAP response contains additional fields NOT in the base CrawlReturn type:
- `traceInfo`: Trace information (typically empty)
- `lastUpdateDate`: Last update timestamp (e.g., "2011-02-16T10:30:10.663Z")
- `currentSystemTime`: Current system time (e.g., "2025-07-24T16:38:53.8421821Z")
- `nextSyncToken`: Sync token (typically empty)
- `folderDeleted`: Boolean flag (typically false)

## CrawlReturnItem Structure

The base `CrawlReturnItem` (defined at `ICustomConnectorInterfaces.cs:152-161`) contains:
- `id`: Unique identifier (GUID)
- `subid`: Sub-identifier (typically empty)
- `type`: Content type 
- `title`: Item title
- `path`: Item path
- `lastModified`: DateTime
- `deleted`: Boolean deletion flag

**IMPORTANT**: The actual SOAP response contains additional fields NOT in the base type:
- `encodeid`: Boolean flag (typically true)
- `extension`: File extension (e.g., "txt")
- `datastoretypeid`: Type identifier (e.g., "cm:content", "lnk:link", "fm:post", "dl:issue")
- `lastUpdate`: Update timestamp (same as lastModified)
- `foldersubid`: Folder sub-ID (typically empty)
- `isFolder`: Boolean folder flag (typically false)
- `securityChanged`: Boolean security change flag (typically true)
- `fileChanged`: Boolean file change flag (typically true)
- `metadataChanged`: Boolean metadata change flag (typically true)

## Actual SOAP Response Structure

```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" 
               xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <soap:Body>
    <Crawl2Response xmlns="http://tempuri.org/">
      <Crawl2Result>
        <error>false</error>
        <errorMsg />
        <traceInfo />
        <moreExist>true</moreExist>
        <lastUpdateDate>2011-02-16T10:30:10.663Z</lastUpdateDate>
        <items>
          <CrawlReturnItem>
            <encodeid>true</encodeid>
            <extension>txt</extension>
            <id>5fa74ad3-9b5b-461b-9df5-de407f1f4fe7</id>
            <subid />
            <datastoretypeid>cm:content</datastoretypeid>
            <lastUpdate>2011-02-15T21:35:26.467+00:00</lastUpdate>
            <foldersubid />
            <isFolder>false</isFolder>
            <securityChanged>true</securityChanged>
            <fileChanged>true</fileChanged>
            <metadataChanged>true</metadataChanged>
            <deleted>false</deleted>
          </CrawlReturnItem>
          <!-- More CrawlReturnItem elements... -->
        </items>
        <currentSystemTime>2025-07-24T16:38:53.8421821Z</currentSystemTime>
        <nextSyncToken />
        <folderDeleted>false</folderDeleted>
      </Crawl2Result>
    </Crawl2Response>
  </soap:Body>
</soap:Envelope>
```

## Implementation Notes

1. **Extended Types Required**: Due to missing fields in base types, implementations MUST create extended versions:
   - `ExtendedCrawlReturn` extending `CrawlReturn` with additional fields
   - `ExtendedCrawlReturnItem` extending `CrawlReturnItem` with additional fields
2. **Direct Serialization**: The SOAP server uses standard XML serialization
3. **Error Handling**: Method exceptions are converted to SOAP faults with error details
4. **Null Handling**: Null values are handled according to SOAP/XML standards
5. **Array Serialization**: Arrays are serialized as repeated XML elements
6. **Namespace Requirement**: Response includes `xmlns="http://tempuri.org/"` namespace
7. **Field Name Mapping**: Note that `type` in base class maps to `datastoretypeid` in SOAP XML
