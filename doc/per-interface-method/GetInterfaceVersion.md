# GetInterfaceVersion - Special Error Response

## Overview

The legacy SOAP client calls `GetInterfaceVersion` which is **NOT** part of the iCustomConnector2 interface. The legacy server returns a specific SOAP fault that the client **depends on** to function correctly. This must be implemented exactly as shown.

## 1. SOAP Request

```xml
POST /DataConnector.asmx HTTP/1.1
SOAPAction: "http://tempuri.org/GetInterfaceVersion"
Content-Type: text/xml; charset=utf-8
Host: localhost:1582
Content-Length: 333
Expect: 100-continue
Connection: Keep-Alive

<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
               xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Body>
        <GetInterfaceVersion xmlns="http://tempuri.org/" />
    </soap:Body>
</soap:Envelope>
```

## 2. Required SOAP Fault Response

**IMPORTANT**: This exact error response is required. The client depends on receiving this specific fault.

```xml
HTTP/1.1 500 Internal Server Error
Cache-Control: private
Content-Type: text/xml; charset=utf-8
Server: Microsoft-IIS/10.0
X-AspNet-Version: 4.0.30319
X-Powered-By: ASP.NET
Date: Thu, 24 Jul 2025 16:38:51 GMT
Content-Length: 808

<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" 
               xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
               xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <soap:Body>
        <soap:Fault>
            <faultcode>soap:Client</faultcode>
            <faultstring>System.Web.Services.Protocols.SoapException: Server did not recognize the value of HTTP Header SOAPAction: http://tempuri.org/GetInterfaceVersion.
   at System.Web.Services.Protocols.Soap11ServerProtocolHelper.RouteRequest()
   at System.Web.Services.Protocols.SoapServerProtocol.Initialize()
   at System.Web.Services.Protocols.ServerProtocolFactory.Create(Type type, HttpContext context, HttpRequest request, HttpResponse response, Boolean&amp; abortProcessing)</faultstring>
            <detail />
        </soap:Fault>
    </soap:Body>
</soap:Envelope>
```

## 3. Implementation Requirements

### Key Points

1. **This is NOT an iCustomConnector2 method** - It does not exist in the interface
2. **The client DEPENDS on this error** - Without this specific fault response, the client will malfunction
3. **Must return HTTP 500** - Not 200 with an error flag
4. **Must be a SOAP Fault** - Not a regular response with error=true

### Implementation Strategy

In your SOAP server implementation:

1. **Do NOT implement GetInterfaceVersion as a regular method**
2. **Let the SOAP framework handle it as an unrecognized action**
3. **Configure your server to return this exact fault for unrecognized SOAPActions**

### Example with SoapCore (ASP.NET Core)

SoapCore will automatically generate a similar fault for unrecognized operations. However, you may need to:

1. Ensure the fault message matches exactly
2. Configure the error handling to match the legacy format
3. Test that the stack trace elements are included

```csharp
// In your startup configuration, ensure unhandled SOAPActions 
// return the appropriate fault response
services.AddSoapCore();

// The framework should automatically handle unrecognized operations
// No need to implement GetInterfaceVersion
```

### Alternative: Manual Implementation

If your framework doesn't automatically generate the correct fault, you can manually handle it:

```csharp
[WebMethod]
public void GetInterfaceVersion()
{
    // This method should never be called if the framework is configured correctly
    // If it is called, throw a SoapException to generate the fault
    throw new SoapException(
        "Server did not recognize the value of HTTP Header SOAPAction: http://tempuri.org/GetInterfaceVersion.",
        SoapException.ClientFaultCode
    );
}
```

## 4. Testing

To verify correct implementation:

1. Send the exact SOAP request shown above
2. Verify you receive:
   - HTTP 500 status code
   - SOAP Fault (not a regular response)
   - Fault code: `soap:Client`
   - Fault string containing the exact error message
   - Empty `<detail />` element

## 5. General Rule for Unimplemented Methods

This GetInterfaceVersion response establishes a pattern: **Any SOAP operation that is not part of the iCustomConnector2 interface should return this same type of SOAP fault**.

The fault string should follow the pattern:
```
System.Web.Services.Protocols.SoapException: Server did not recognize the value of HTTP Header SOAPAction: http://tempuri.org/[MethodName].
```

This ensures backward compatibility with the legacy client's error handling.