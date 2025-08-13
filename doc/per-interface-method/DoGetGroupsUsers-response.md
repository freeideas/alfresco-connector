# DoGetGroupsUsers Method - iCustomConnector2 Response to SOAP Translation

## iCustomConnector2 Return Type
```csharp
public abstract StringArrayReturn DoGetGroupsUsers(...);
```
Returns: `StringArrayReturn` object

## SOAP Response Translation

The SOAP server receives the `StringArrayReturn` object from the iCustomConnector2 implementation and translates it to a SOAP response.

### Interface Return to SOAP Response Flow

1. **DLL Returns Object**: The iCustomConnector2 implementation returns a `StringArrayReturn` instance
2. **Server Wraps Response**: SOAP server wraps the object in a `GetGroupsUsers2Response` element
3. **Serialize to XML**: Object properties are serialized to XML elements
4. **Add SOAP Envelope**: Response is wrapped in standard SOAP envelope
5. **Return HTTP Response**: Complete SOAP XML is returned with HTTP 200 OK

## StringArrayReturn to SOAP Mapping

The `StringArrayReturn` object properties are mapped to SOAP XML elements as follows:

### StringArrayReturn Properties (from ICustomConnectorInterfaces.cs)
- `string[] values` - Array of user IDs/names
- `bool error` - Error flag (serialized as lowercase: "true"/"false")
- `string errorMsg` - Error message (empty string if no error)

## Example SOAP Response Structure

### Complete Response (from actual transcript)
```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" 
               xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <soap:Body>
    <GetGroupsUsers2Response xmlns="http://tempuri.org/">
      <GetGroupsUsers2Result>
        <error>false</error>
        <errorMsg />
        <traceInfo />  <!-- Always empty in practice -->
        <values>
          <string>admin</string>
        </values>
      </GetGroupsUsers2Result>
    </GetGroupsUsers2Response>
  </soap:Body>
</soap:Envelope>
```

### Response Element Details
- `<error>`: Boolean as lowercase string ("true"/"false")
- `<errorMsg>`: Error description or empty element
- `<traceInfo>`: Always present but empty (legacy field)
- `<values>`: Container for user array
  - `<string>`: Each user ID as a separate element
  - Empty `<values />` element if no users

## Implementation Notes

1. **Direct Serialization**: The SOAP server uses standard XML serialization
2. **Error Handling**: Method exceptions are converted to SOAP faults with error details
3. **Null Handling**: Null values are handled according to SOAP/XML standards
4. **Array Serialization**: Arrays are serialized as repeated `<string>` elements within `<values>`
5. **Namespace Requirement**: Response includes `xmlns="http://tempuri.org/"` namespace
6. **Additional Response Field**: The response includes a `<traceInfo />` element that is always empty but must be present for compatibility
7. **Empty Arrays**: When no users are found, return empty `<values />` element, not `<values></values>`
