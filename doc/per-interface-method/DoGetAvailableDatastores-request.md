# GetAvailableDatastores2 - SOAP Request Processing

## SOAP Method Signature (from WSDL)
```xml
<s:element name="GetAvailableDatastores2">
  <s:complexType>
    <s:sequence>
      <s:element minOccurs="0" maxOccurs="1" name="conn" type="tns:ConnectionInfo" />
      <s:element minOccurs="0" maxOccurs="1" name="customparams" type="tns:ArrayOfCustomPair" />
      <s:element minOccurs="0" maxOccurs="1" name="filter1" type="s:string" />
      <s:element minOccurs="0" maxOccurs="1" name="filter2" type="s:string" />
      <s:element minOccurs="0" maxOccurs="1" name="filter3" type="s:string" />
      <s:element minOccurs="0" maxOccurs="1" name="templateService" type="s:string" />
      <s:element minOccurs="1" maxOccurs="1" name="loadTemplates" type="s:boolean" />
      <s:element minOccurs="1" maxOccurs="1" name="tl" type="tns:TraceLevel" />
    </s:sequence>
  </s:complexType>
</s:element>
```

## Actual SOAP Request Example
```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetAvailableDatastores2 xmlns="http://tempuri.org/">
      <conn>
        <account>admin</account>
        <password>admin</password>
        <scrubAllData>true</scrubAllData>
        <connectionId>3</connectionId>
        <sourceSite>connectors</sourceSite>
        <customparam>http://100.115.192.75:8080/</customparam>
        <customparam2 />
        <customboolparam>false</customboolparam>
        <!-- Additional ConnectionInfo fields may appear here -->
      </conn>
      <customparams />  <!-- Usually empty, can contain CustomPair elements -->
      <filter1 />       <!-- Optional, often not present -->
      <filter2 />       <!-- Optional, often not present -->
      <filter3 />       <!-- Optional, often not present -->
      <templateService />
      <loadTemplates>false</loadTemplates>
      <tl>Warning</tl>  <!-- TraceLevel: Error|Warning|Trace -->
    </GetAvailableDatastores2>
  </soap:Body>
</soap:Envelope>
```

## Parameter Mapping

### 1. ConnectionInfo conn
**Source**: `<conn>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `ConnectionInfo`

### 2. ArrayOfCustomPair customparams
**Source**: `<customparams>` element in SOAP  
**Mapping**: Extracts from SOAP as ArrayOfCustomPair (can be empty)  
**Interface Type**: `ArrayOfCustomPair` (or converted to `Hashtable` for legacy interface)

### 3. string filter1
**Source**: `<filter1>` element in SOAP (optional, not present in actual transcript)  
**Mapping**: Extracts from SOAP if present, otherwise null  
**Interface Type**: `string`

### 4. string filter2
**Source**: `<filter2>` element in SOAP (optional, not present in actual transcript)  
**Mapping**: Extracts from SOAP if present, otherwise null  
**Interface Type**: `string`

### 5. string filter3
**Source**: `<filter3>` element in SOAP (optional, not present in actual transcript)  
**Mapping**: Extracts from SOAP if present, otherwise null  
**Interface Type**: `string`

### 6. string templateService
**Source**: `<templateService>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `string`

### 7. bool loadTemplates
**Source**: `<loadTemplates>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `bool`

### 8. TraceLevel tl
**Source**: `<tl>` element in SOAP  
**Mapping**: Extracts from SOAP as enum (Error/Warning/Trace)  
**Interface Type**: `TraceLevel` enum

## Parser Implementation Guide

```csharp
public class ParsedRequest
{
    public ConnectionInfo ConnectionInfo { get; set; }
    public Hashtable CustomParams { get; set; }  // Usually empty
    public string? Filter1 { get; set; }         // Often null
    public string? Filter2 { get; set; }         // Often null
    public string? Filter3 { get; set; }         // Often null
    public string? TemplateService { get; set; } // Often empty string
    public bool LoadTemplates { get; set; }      // Required
    public string TraceLevel { get; set; }       // Required: "Error"|"Warning"|"Trace"
}
```

### Key Parsing Notes:
1. **Namespace**: All elements use `xmlns="http://tempuri.org/"`
2. **ConnectionInfo**: See `/csproj/Models/ICustomConnectorInterfaces.cs` for full structure
3. **CustomParams**: Can be empty element `<customparams />` or contain `<CustomPair>` children
4. **Filters**: Optional parameters, often not present in actual requests
5. **TraceLevel**: Enum value as string, defaults to "Warning" if not specified

## Common Pitfalls to Avoid

1. **Missing TraceLevel**: The `tl` parameter is REQUIRED but wasn't in original interface docs
2. **Empty vs Null**: Empty XML elements (`<filter1 />`) should map to null or empty string
3. **CustomParams Structure**: Can be complex with nested CustomPair elements
4. **ConnectionInfo Complexity**: Has many optional fields including custom parameters and boolean flags
