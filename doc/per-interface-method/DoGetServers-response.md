# DoGetServers Method - iCustomConnector2 Response to SOAP Translation

## iCustomConnector2 Return Type
```csharp
public abstract StringArrayReturn DoGetServers(...);
```
Returns: `StringArrayReturn` object

## SOAP Response Translation

The SOAP server receives the `StringArrayReturn` object from the iCustomConnector2 implementation and translates it to a SOAP response.

### Interface Return to SOAP Response Flow

1. **DLL Returns Object**: The iCustomConnector2 implementation returns a `StringArrayReturn` instance
2. **Server Wraps Response**: SOAP server wraps the object in a `GetServersResponse` element
3. **Serialize to XML**: Object properties are serialized to XML elements
4. **Add SOAP Envelope**: Response is wrapped in standard SOAP envelope
5. **Return HTTP Response**: Complete SOAP XML is returned with HTTP 200 OK

## StringArrayReturn to SOAP Mapping

The `StringArrayReturn` object properties are mapped to SOAP XML elements. The exact field mappings depend on the return type structure defined in `ICustomConnectorInterfaces.cs`.

## Example SOAP Response Structure

```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetServersResponse xmlns="http://tempuri.org/">
      <GetServersResult>
        <!-- StringArrayReturn properties serialized here -->
      </GetServersResult>
    </GetServersResponse>
  </soap:Body>
</soap:Envelope>
```

## Implementation Notes

1. **Direct Serialization**: The SOAP server uses standard XML serialization
2. **Error Handling**: Method exceptions are converted to SOAP faults with error details
3. **Null Handling**: Null values are handled according to SOAP/XML standards
4. **Array Serialization**: Arrays are serialized as repeated XML elements
5. **Namespace Requirement**: Response includes `xmlns="http://tempuri.org/"` namespace
