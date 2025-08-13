# DoGetDatastoreTypes Method - SOAP Request Processing through iCustomConnector2

## iCustomConnector2 Interface Method
```csharp
public abstract DataStoreTypeReturn DoGetDatastoreTypes(
    ConnectionInfo conn,
    Hashtable customparams,
    DataStoreInfo datastore
);
```
Location: `doc/copy_src/ICustomConnectorInterfaces.cs:10`

## SOAP Request to Interface Translation

### Incoming SOAP Request
The SOAP server receives a `GetDatastoreTypes2` action request.

### Interface Method Call
The SOAP server translates the `GetDatastoreTypes2` SOAP request to the `DoGetDatastoreTypes` method call on the iCustomConnector2 implementation.

## Parameter Mapping

### 1. ConnectionInfo conn
**Source**: `<conn>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `ConnectionInfo`

### 2. Hashtable customparams
**Source**: `<customparams>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `Hashtable`

### 3. DataStoreInfo datastore
**Source**: `<datastore>` element in SOAP  
**Mapping**: Extracts from SOAP and creates/converts to interface type  
**Interface Type**: `DataStoreInfo`

## SOAP Server Processing Flow

1. **Receive SOAP Request**: Server receives HTTP POST with `GetDatastoreTypes2` SOAP envelope
2. **Parse SOAP XML**: Extracts parameter values from XML elements
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Create Objects**: Instantiates required objects from SOAP data
5. **Invoke Method**: Calls `DoGetDatastoreTypes()` with parameters
6. **Return Response**: Serializes the `DataStoreTypeReturn` result to SOAP response

## Return Type: DataStoreTypeReturn

The method returns a `DataStoreTypeReturn` object defined in the interface.

## Implementation Notes

1. **SOAP Action Mapping**: The SOAP action `GetDatastoreTypes2` maps to this interface method
2. **Parameter Extraction**: SOAP server extracts parameters from the XML request body
3. **Type Conversion**: SOAP types are converted to .NET types as needed
4. **Error Handling**: Exceptions are converted to SOAP faults
