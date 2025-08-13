# DoRealtimeSecurityCheck Method - iCustomConnector2 Response to SOAP Translation

## iCustomConnector2 Return Type
```csharp
public abstract SecurityItemReturn DoRealtimeSecurityCheck(...);
```
Returns: `SecurityItemReturn` object (from DataConnector.Models)

## SecurityItemReturn Type Definition
```csharp
public class SecurityItemReturn
{
    public SecurityItemResult[] results { get; set; } = Array.Empty<SecurityItemResult>();
    public bool error { get; set; }
    public string errorMsg { get; set; } = "";
}

public class SecurityItemResult
{
    public string id { get; set; } = "";
    public string subid { get; set; } = "";
    public bool allowed { get; set; }
}
```

## SOAP Response Structure

### Complete SOAP Response Example (Success Case)
```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" 
               xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <soap:Body>
    <RealtimeSecurityCheckResponse xmlns="http://tempuri.org/">
      <RealtimeSecurityCheckResult>
        <error>false</error>
        <errorMsg></errorMsg>
        <traceInfo />
        <items>
          <SecurityItemResult>
            <id>5fa74ad3-9b5b-461b-9df5-de407f1f4fe7</id>
            <subid></subid>
            <allowed>true</allowed>
          </SecurityItemResult>
          <SecurityItemResult>
            <id>a38308f8-6f30-4d8a-8576-eaf6703fb9d3</id>
            <subid></subid>
            <allowed>false</allowed>
          </SecurityItemResult>
        </items>
      </RealtimeSecurityCheckResult>
    </RealtimeSecurityCheckResponse>
  </soap:Body>
</soap:Envelope>
```

### Error Response Example (from actual legacy system)
```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" 
               xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <soap:Body>
    <RealtimeSecurityCheckResponse xmlns="http://tempuri.org/">
      <RealtimeSecurityCheckResult>
        <error>true</error>
        <errorMsg>DoRealtimeSecurityCheck is not implemented in this connector. This method would normally perform real-time security validation for the specified items and user.</errorMsg>
        <traceInfo />
        <items />
      </RealtimeSecurityCheckResult>
    </RealtimeSecurityCheckResponse>
  </soap:Body>
</soap:Envelope>
```

## Field Mapping Details

### SecurityItemReturn → RealtimeSecurityCheckResult
- `error` (bool) → `<error>` element (lowercase string: "true" or "false")
- `errorMsg` (string) → `<errorMsg>` element
- Not mapped directly → `<traceInfo />` (always empty element)
- `results` (SecurityItemResult[]) → `<items>` element containing multiple `<SecurityItemResult>` elements

### SecurityItemResult → SecurityItemResult XML
Each item in the `results` array becomes:
```xml
<SecurityItemResult>
  <id>{item.id}</id>
  <subid>{item.subid}</subid>
  <allowed>{item.allowed as lowercase string}</allowed>
</SecurityItemResult>
```

## XML Serialization Requirements

### Critical Settings for Serializer.cs
```csharp
var settings = new XmlWriterSettings 
{ 
    Indent = false, 
    OmitXmlDeclaration = true,
    ConformanceLevel = ConformanceLevel.Fragment  // REQUIRED for multiple root elements
};
```

## Implementation Notes

1. **XML Writer Configuration**: Must use `ConformanceLevel.Fragment` to allow multiple root-level elements within the result
2. **Boolean Serialization**: Booleans must be serialized as lowercase strings ("true"/"false")
3. **Empty Arrays**: When `results` is empty or null, serialize as empty `<items />` element
4. **Namespace Requirement**: Response must include `xmlns="http://tempuri.org/"` on the Response element
5. **Element Names**: The response array is named `<items>` not `<results>` in the XML
6. **Error Handling**: When returning an error, set `error=true`, provide meaningful `errorMsg`, and return empty `results` array
