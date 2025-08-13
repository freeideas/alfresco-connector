# GetAvailableDatastores2 - SOAP Response Structure

## Response Type: DataStoreInfoReturn
From `/csproj/Models/ICustomConnectorInterfaces.cs`:
```csharp
public class DataStoreInfoReturn
{
    public DataStoreInfo[] datastores { get; set; }
    public bool error { get; set; }
    public string errorMsg { get; set; }
}

public class DataStoreInfo
{
    public string id { get; set; }
    public string name { get; set; }
    public string desc { get; set; }
    public string path { get; set; }
    public string owner { get; set; }
    public string server { get; set; }
    public bool MBFlagValue { get; set; }
}
```

## Actual SOAP Response Example

```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" 
               xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <soap:Body>
    <GetAvailableDatastores2Response xmlns="http://tempuri.org/">
      <GetAvailableDatastores2Result>
        <error>false</error>
        <errorMsg />
        <traceInfo />  <!-- Note: Not in DataStoreInfoReturn model but appears in response -->
        <datastores>
          <DataStoreInfo>
            <name>Sample: Web Site Design Project</name>
            <desc />
            <owner />
            <id>swsdp</id>
            <server />
            <templateid />      <!-- Note: Extra fields not in model -->
            <templatedesc />    <!-- Note: Extra fields not in model -->
            <defaultviewname /> <!-- Note: Extra fields not in model -->
            <defaultviewid />   <!-- Note: Extra fields not in model -->
            <alert />           <!-- Note: Extra fields not in model -->
            <MBFlagValue>true</MBFlagValue>
          </DataStoreInfo>
        </datastores>
      </GetAvailableDatastores2Result>
    </GetAvailableDatastores2Response>
  </soap:Body>
</soap:Envelope>
```

## Serializer Implementation Guide

### Critical Notes for Serialization:

1. **Extra Fields Required**: The actual SOAP response includes fields NOT in the DataStoreInfo model:
   - `traceInfo` (after errorMsg)
   - `templateid`, `templatedesc`, `defaultviewname`, `defaultviewid`, `alert` (in each DataStoreInfo)
   - These must be added as empty elements to match legacy client expectations

2. **Field Order Matters**: DataStoreInfo fields must appear in this exact order:
   ```xml
   <name>...</name>
   <desc>...</desc>
   <owner>...</owner>
   <id>...</id>
   <server>...</server>
   <templateid />
   <templatedesc />
   <defaultviewname />
   <defaultviewid />
   <alert />
   <MBFlagValue>...</MBFlagValue>
   ```

3. **Namespace Declaration**: Must include all three namespaces in the envelope:
   ```xml
   xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"
   xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
   xmlns:xsd="http://www.w3.org/2001/XMLSchema"
   ```

4. **Boolean Serialization**: Booleans serialize as lowercase: `true`/`false`

5. **Empty Arrays**: If no datastores, still include empty `<datastores />` element

## Error Response Example

```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetAvailableDatastores2Response xmlns="http://tempuri.org/">
      <GetAvailableDatastores2Result>
        <error>true</error>
        <errorMsg>Connection failed: Invalid credentials</errorMsg>
        <traceInfo />
        <datastores />
      </GetAvailableDatastores2Result>
    </GetAvailableDatastores2Response>
  </soap:Body>
</soap:Envelope>
```
