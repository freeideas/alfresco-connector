# GetGroups2 Method - SOAP Request Structure

## SOAP Request Elements

### Request Envelope Structure
```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" 
               xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <soap:Body>
    <GetGroups2 xmlns="http://tempuri.org/">
      <conn>...</conn>
      <customparams>...</customparams>
      <tl>...</tl>
    </GetGroups2>
  </soap:Body>
</soap:Envelope>
```

## Parameter Details

### 1. ConnectionInfo (`<conn>`)
**Required**: Yes  
**Contains**: Authentication and connection parameters

#### Core Fields (commonly used):
- `<account>` - User account name (string)
- `<password>` - User password (string)
- `<connectionId>` - Connection identifier (int)
- `<sourceSite>` - Source site identifier (string)
- `<scrubAllData>` - Whether to scrub all data (bool)
- `<customparam>` - Custom parameter 1, often a URL (string)
- `<customparam2>` - Custom parameter 2 (string, may be empty)
- `<customparam3>` - Custom parameter 3 (string, may be empty)
- `<customparam4>` - Custom parameter 4 (string, may be empty)
- `<customboolparam>` - Custom boolean parameter 1 (bool)
- `<customboolparam2>` - Custom boolean parameter 2 (bool)
- `<customboolparam3>` - Custom boolean parameter 3 (bool)
- `<customboolparam4>` - Custom boolean parameter 4 (bool)

#### Additional Fields (in full request but often ignored):
- `<customchoiceval>` through `<customchoiceval4>` - Choice parameters (usually empty)
- `<BoolParameter>` - Collection of BoolPair elements (duplicate of customboolparam fields)
- `<StringParameter>` - Collection of StringPair elements (duplicate of customparam fields)
- `<ChoiceParameter>` - Collection of ChoicePair elements (duplicate of customchoiceval fields)

### 2. Custom Parameters (`<customparams>`)
**Required**: No (can be empty or omitted)  
**Contains**: Collection of key-value pairs for custom configuration

Structure:
```xml
<customparams>
  <CustomPair>
    <key>SelectedDataStores</key>
    <value>swsdp</value>
  </CustomPair>
  <!-- Additional CustomPair elements as needed -->
</customparams>
```

Common keys:
- `SelectedDataStores` - Specifies which data store(s) to query

### 3. Trace Level (`<tl>`)
**Required**: No  
**Values**: `Warning`, `Verbose`, or empty  
**Purpose**: Controls the verbosity of trace information in the response

### 4. Additional Columns (`<additionalColumns>`)
**Required**: No  
**Note**: Not present in the actual SOAP transcript but may be included in some requests  
**Purpose**: Specifies additional data columns to return

## SOAP Server Processing Flow

1. **Receive SOAP Request**: Server receives HTTP POST with `GetGroups2` SOAP envelope
2. **Parse SOAP XML**: Extracts parameter values from XML elements
3. **Load DLL**: Dynamically loads the iCustomConnector2 implementation
4. **Create Objects**: Instantiates required objects from SOAP data
5. **Invoke Method**: Calls `DoGetGroups()` with parameters
6. **Return Response**: Serializes the `SystemIdentityInfoReturn` result to SOAP response

## Return Type: SystemIdentityInfoReturn

The method returns a `SystemIdentityInfoReturn` object defined in the interface.

## Implementation Notes

1. **SOAP Action Mapping**: The SOAP action `GetGroups2` maps to this interface method
2. **Parameter Extraction**: SOAP server extracts parameters from the XML request body
3. **Type Conversion**: SOAP types are converted to .NET types as needed
4. **Error Handling**: Exceptions are converted to SOAP faults
