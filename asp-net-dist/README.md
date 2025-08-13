# Alfresco Data Connector - IIS Deployment Package

## Overview

This is a standalone ASP.NET Web Service (ASMX) implementation of the Alfresco iCustomConnector2 interface. It can be deployed directly to IIS without modifying any existing applications.

## Requirements

- Windows Server with IIS installed
- .NET Framework 4.7.2 or higher
- IIS configured with ASP.NET support

## Quick Deployment Steps

### 1. Prepare IIS

1. Open IIS Manager
2. Ensure ASP.NET 4.7.2 is registered with IIS:
   ```powershell
   # Run as Administrator if needed
   %windir%\Microsoft.NET\Framework644.0.30319spnet_regiis.exe -i
   ```

### 2. Create Application

1. In IIS Manager, right-click on your site (e.g., "Default Web Site")
2. Select "Add Application"
3. Configure:
   - Alias: `AlfrescoConnector` (or your preferred name)
   - Physical path: Browse to where you extracted these files
   - Application pool: Select one running .NET Framework 4.7.2

### 3. Deploy Files

1. Extract all files from this package to your chosen directory
2. Ensure the IIS_IUSRS group has read permissions on the directory
3. If logging is enabled, ensure write permissions on App_Data folder

### 4. Test the Service

1. Navigate to: `http://[server]/AlfrescoConnector/DataConnector.asmx`
2. You should see the ASMX service description page
3. Click on any method to test it

## File Structure

```
/AlfrescoConnector/           # IIS Application root
  DataConnector.asmx          # Service endpoint (1 line directive)
  DataConnector.asmx.cs       # Service implementation
  web.config                  # IIS configuration
  /App_Code/                  # Source code (compiled on demand)
    /iCustomConnector2impl/   # Implementation classes
      AiGeneratedConnector.cs
      /Methods/               # Method implementations
      /Models/                # Data models
      /Services/              # Helper services
```

## Configuration

Edit `web.config` to adjust settings:

### Connection Mode
```xml
<add key="ConnectorMode" value="Hardcoded"/>
```
Options:
- `Hardcoded` - Uses built-in test data
- `External` - Connects to external API

### External API (if using External mode)
```xml
<add key="ExternalApiBaseUrl" value="https://api.example.com/"/>
```

### Logging
```xml
<add key="EnableLogging" value="true"/>
<add key="LogPath" value="~/App_Data/Logs/"/>
```

## Security Considerations

1. **Authentication**: Default uses Windows Authentication. Adjust in web.config as needed.
2. **CORS**: Currently allows all origins (*). Restrict in production.
3. **SSL**: Configure IIS to use HTTPS in production.
4. **Permissions**: Limit IIS_IUSRS permissions to minimum required.

## Troubleshooting

### Service returns 500 error
- Check Event Viewer for detailed error messages
- Ensure .NET Framework 4.7.2 is installed
- Verify App Pool is running correct .NET version

### "Could not load type" error
- Ensure all files in App_Code are present
- Check that compilation debug="true" in web.config
- Restart the Application Pool

### Methods return NotImplementedException
- Verify all files in App_Code/iCustomConnector2impl are present
- Check DataConnector.asmx.cs references correct namespace

### Performance issues
- Set compilation debug="false" in production
- Configure application pool recycling appropriately
- Consider precompiling the application

## Testing with SOAP UI

1. Create new SOAP project
2. WSDL URL: `http://[server]/AlfrescoConnector/DataConnector.asmx?wsdl`
3. Test methods with sample requests from documentation

## Support

This implementation includes:
- All iCustomConnector2 interface methods
- Hardcoded test responses for development
- Extensible architecture for external API integration

## Version Information

- Implementation Version: 1.0.0
- Interface Version: 2.0
- Target Framework: .NET Framework 4.7.2
- Deployment Type: ASP.NET Web Service (ASMX)
