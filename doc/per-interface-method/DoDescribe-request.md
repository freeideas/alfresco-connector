# DoDescribe Method - SOAP Request Processing through iCustomConnector2

## iCustomConnector2 Interface Method
```csharp
public abstract DiscoveryInfo DoDescribe();
```
Location: `doc/copy_src/ICustomConnectorInterfaces.cs:8`

## SOAP Request to Interface Translation

### Incoming SOAP Request
```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <Describe xmlns="http://tempuri.org/" />
  </soap:Body>
</soap:Envelope>
```

### Interface Method Call
The SOAP server receives the `Describe` action and translates it to:
```csharp
DiscoveryInfo result = customConnector.DoDescribe();
```

## Parameter Mapping

**No parameters** - The SOAP request contains an empty `<Describe />` element, and the interface method takes no parameters.

## SOAP Server Processing Flow

1. **Receive SOAP Request**: Server receives HTTP POST with SOAP envelope
2. **Parse SOAP Action**: Identifies `Describe` action from SOAPAction header or body
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Invoke Method**: Calls `DoDescribe()` on the loaded instance
5. **Return Response**: Serializes the `DiscoveryInfo` result to SOAP response

## Return Type: DiscoveryInfo

The method returns a `DiscoveryInfo` object (defined in `ICustomConnectorInterfaces.cs:25-92`) containing connector capabilities and configuration options.

## Implementation Notes

1. **No Authentication Required**: This method is typically called before authentication to discover connector capabilities
2. **Static Response**: Often returns static configuration data about the connector
3. **No External API Call**: Usually doesn't require external API interaction
4. **Namespace Requirement**: The SOAP element must include `xmlns="http://tempuri.org/"`