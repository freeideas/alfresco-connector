# GetGroups2 Method - SOAP Response Structure

## SOAP Response Envelope
```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" 
               xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <soap:Body>
    <GetGroups2Response xmlns="http://tempuri.org/">
      <GetGroups2Result>
        <error>false</error>
        <errorMsg />
        <traceInfo />
        <items>
          <!-- Array of SystemIdentityInfo elements -->
        </items>
      </GetGroups2Result>
    </GetGroups2Response>
  </soap:Body>
</soap:Envelope>
```

## GetGroups2Result Structure

### Root Fields
- `<error>` - Boolean indicating if an error occurred (lowercase: "true" or "false")
- `<errorMsg>` - Error message text (empty string if no error)
- `<traceInfo>` - Trace/debug information (populated based on `<tl>` request parameter)
- `<items>` - Array container for group information

### SystemIdentityInfo Elements (within `<items>`)

**IMPORTANT**: The actual SOAP response uses different field names than the C# SystemIdentityInfo class!

#### Actual SOAP Fields (from JSON transcript):
```xml
<SystemIdentityInfo>
  <uniqueid>GROUP_ALFRESCO_ADMINISTRATORS</uniqueid>
  <name>ALFRESCO_ADMINISTRATORS</name>
  <active>true</active>
</SystemIdentityInfo>
```

#### Field Mappings:
| C# Property (SystemIdentityInfo) | SOAP Element Name | Description |
|----------------------------------|-------------------|-------------|
| `id` | `<uniqueid>` | Unique identifier for the group (prefixed with "GROUP_") |
| `name` | `<name>` | Display name of the group |
| N/A | `<active>` | Whether the group is active (always "true" for groups) |
| `displayName` | Not included | Not serialized in response |
| `email` | Not included | Not serialized in response |

## Example Response with Data
```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" 
               xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <soap:Body>
    <GetGroups2Response xmlns="http://tempuri.org/">
      <GetGroups2Result>
        <error>false</error>
        <errorMsg />
        <traceInfo />
        <items>
          <SystemIdentityInfo>
            <uniqueid>GROUP_ALFRESCO_ADMINISTRATORS</uniqueid>
            <name>ALFRESCO_ADMINISTRATORS</name>
            <active>true</active>
          </SystemIdentityInfo>
          <SystemIdentityInfo>
            <uniqueid>GROUP_site_swsdp</uniqueid>
            <name>site_swsdp</name>
            <active>true</active>
          </SystemIdentityInfo>
          <!-- Additional SystemIdentityInfo elements -->
        </items>
      </GetGroups2Result>
    </GetGroups2Response>
  </soap:Body>
</soap:Envelope>
```

## Implementation Notes

1. **Field Name Mismatch**: The SOAP response uses `<uniqueid>` instead of `<id>` for the group identifier
2. **Active Field**: The `<active>` field is included in the SOAP response but doesn't exist in the C# SystemIdentityInfo class - must be hardcoded in serializer
3. **Group ID Prefix**: Group identifiers typically start with "GROUP_" prefix
4. **Empty Elements**: Use self-closing tags or empty element syntax for null/empty values
5. **Boolean Format**: Boolean values are lowercase strings ("true"/"false")
6. **Namespace**: The GetGroups2Response element must include `xmlns="http://tempuri.org/"`

## Error Response Example
```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetGroups2Response xmlns="http://tempuri.org/">
      <GetGroups2Result>
        <error>true</error>
        <errorMsg>Connection failed: Invalid credentials</errorMsg>
        <traceInfo />
        <items />
      </GetGroups2Result>
    </GetGroups2Response>
  </soap:Body>
</soap:Envelope>
```
