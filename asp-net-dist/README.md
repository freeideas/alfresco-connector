# Alfresco Data Connector - IIS Deployment Package

## Overview

This is a standalone ASP.NET Web Service (ASMX) implementation of the Alfresco iCustomConnector2 interface. 
It deploys as a **separate IIS site on port 1582**, independent from other web applications.

## Requirements

- Windows Server with IIS installed
- .NET Framework 4.7.2 or higher
- IIS configured with ASP.NET support
- Administrator privileges for deployment

## Quick Deployment (Automated)

### PowerShell Scripts Included

This package includes automated deployment scripts:

1. **deploy-iis.ps1** - Installs the service as a new IIS site on port 1582
2. **check-iis-status.ps1** - Verifies the deployment and service health
3. **uninstall-iis.ps1** - Removes the service from IIS (preserves files)
4. **deploy-config.json** - Configuration file (port 1582, site name, etc.)

### Automated Deployment Steps

1. **Extract all files** to your desired location (e.g., `C:\AlfrescoConnector`)

2. **Open PowerShell as Administrator**

3. **Navigate to the extracted directory:**
   ```powershell
   cd C:\AlfrescoConnector
   ```

4. **Run the deployment script:**
   ```powershell
   .\deploy-iis.ps1
   ```

5. **Verify the deployment:**
   ```powershell
   .\check-iis-status.ps1
   ```

The service will be available at: **http://[server]:1582/DataConnector.asmx**

## Manual Deployment Steps

If you prefer manual configuration:

### 1. Prepare IIS

1. Open IIS Manager
2. Ensure ASP.NET 4.7.2 is registered with IIS:
   ```powershell
   # Run as Administrator
   %windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe -i
   ```

### 2. Create New Site (Not Under Default Web Site)

1. In IIS Manager, right-click on "Sites"
2. Select "Add Website..."
3. Configure:
   - Site name: `AlfrescoConnectorASMX`
   - Physical path: Browse to where you extracted these files
   - Port: `1582`
   - Application pool: Create new or select one running .NET Framework 4.7.2

### 3. Configure Application Pool

1. Select "Application Pools" in IIS Manager
2. Find or create pool named `AlfrescoConnectorPool`
3. Basic Settings:
   - .NET CLR Version: `.NET CLR Version v4.0.30319`
   - Managed Pipeline Mode: `Integrated`
4. Advanced Settings:
   - Enable 32-bit Applications: `False`
   - Process Identity: `ApplicationPoolIdentity`

### 4. Set Permissions

Grant IIS permissions to the application directory:
```powershell
icacls "C:\AlfrescoConnector" /grant "IIS_IUSRS:(OI)(CI)RX" /T
icacls "C:\AlfrescoConnector\App_Data" /grant "IIS_IUSRS:(OI)(CI)M" /T
```

### 5. Test the Service

1. Navigate to: `http://localhost:1582/DataConnector.asmx`
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
