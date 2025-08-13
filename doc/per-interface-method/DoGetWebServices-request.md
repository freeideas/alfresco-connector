# DoGetWebServices Method - SOAP Request Processing through iCustomConnector2

## iCustomConnector2 Interface Method
```csharp
public abstract WebServiceInfo DoGetWebServices();
```
Location: `doc/copy_src/ICustomConnectorInterfaces.cs:20`

## SOAP Request to Interface Translation

### Incoming SOAP Request
The SOAP server receives a `GetWebServices` action request.

### Interface Method Call
The SOAP server translates the `GetWebServices` SOAP request to the `DoGetWebServices` method call on the iCustomConnector2 implementation.

## Parameter Mapping

**No parameters** - This method takes no input parameters.

## SOAP Server Processing Flow

1. **Receive SOAP Request**: Server receives HTTP POST with `GetWebServices` SOAP envelope
2. **Parse SOAP XML**: Extracts parameter values from XML elements
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Create Objects**: Instantiates required objects from SOAP data
5. **Invoke Method**: Calls `DoGetWebServices()` with parameters
6. **Return Response**: Serializes the `WebServiceInfo` result to SOAP response

## Return Type: WebServiceInfo

The method returns a `WebServiceInfo` object defined in the interface.

## Implementation Notes

1. **SOAP Action Mapping**: The SOAP action `GetWebServices` maps to this interface method
2. **Parameter Extraction**: SOAP server extracts parameters from the XML request body
3. **Type Conversion**: SOAP types are converted to .NET types as needed
4. **Error Handling**: Exceptions are converted to SOAP faults
