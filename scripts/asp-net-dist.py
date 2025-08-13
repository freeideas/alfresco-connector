#!/home/ace/bin/uvrun
# /// script
# requires-python = ">=3.8"
# dependencies = []
# ///

"""
Build standalone ASP.NET ASMX deployment package for IIS
Creates a complete, self-contained web service application
"""

import os
import shutil
from pathlib import Path
from datetime import datetime
import zipfile

def main():
    # Define paths
    script_dir = Path(__file__).parent
    project_root = script_dir.parent
    source_dir = project_root / "iCustomConnector2impl"
    doc_dir = project_root / "doc"
    dist_dir = project_root / "asp-net-dist"
    
    print("Building ASP.NET standalone deployment package...")
    
    # Clean dist directory but preserve PowerShell scripts
    ps_scripts_backup = {}
    ps_files = ["deploy-iis.ps1", "uninstall-iis.ps1", "check-iis-status.ps1", "deploy-config.json"]
    
    if dist_dir.exists():
        # Backup PowerShell scripts if they exist
        for script in ps_files:
            script_path = dist_dir / script
            if script_path.exists():
                ps_scripts_backup[script] = script_path.read_text()
        shutil.rmtree(dist_dir)
    
    dist_dir.mkdir(parents=True)
    
    # Restore PowerShell scripts
    for script, content in ps_scripts_backup.items():
        (dist_dir / script).write_text(content)
        print(f"Restored: {script}")
    
    print(f"Created directory: {dist_dir}")
    
    # Create the ASMX directive file (1 line)
    asmx_file = dist_dir / "DataConnector.asmx"
    asmx_file.write_text('<%@ WebService Language="C#" CodeBehind="DataConnector.asmx.cs" Class="Alfresco.DataConnector" %>\n')
    print(f"Created: DataConnector.asmx")
    
    # Create DataConnector.asmx.cs with proper standalone structure
    create_standalone_webservice(dist_dir, doc_dir)
    print(f"Created: DataConnector.asmx.cs")
    
    # Create App_Code directory and copy implementation
    app_code_dir = dist_dir / "App_Code"
    app_code_dir.mkdir()
    
    # Copy implementation files to App_Code
    impl_dest = app_code_dir / "iCustomConnector2impl"
    shutil.copytree(source_dir, impl_dest)
    
    # Remove unnecessary files from App_Code
    for pattern in ["*.csproj", "Program.cs", "bin", "obj", ".vs"]:
        for file in impl_dest.rglob(pattern):
            if file.is_file():
                file.unlink()
            elif file.is_dir():
                shutil.rmtree(file)
    
    print(f"Copied implementation to App_Code/")
    
    # Create web.config
    create_web_config(dist_dir)
    print(f"Created: web.config")
    
    # Create README with deployment instructions
    create_readme(dist_dir)
    print(f"Created: README.md")
    
    # Copy PowerShell deployment scripts if they exist
    ps_scripts = ["deploy-iis.ps1", "uninstall-iis.ps1", "check-iis-status.ps1", "deploy-config.json"]
    for script in ps_scripts:
        script_path = dist_dir / script
        if script_path.exists():
            print(f"Including: {script}")
    
    # Create a zip file for easy deployment
    timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
    zip_name = f"AlfrescoConnector_IIS_{timestamp}.zip"
    zip_path = dist_dir.parent / zip_name
    
    with zipfile.ZipFile(zip_path, 'w', zipfile.ZIP_DEFLATED) as zipf:
        for file in dist_dir.rglob('*'):
            if file.is_file():
                arcname = file.relative_to(dist_dir)
                zipf.write(file, arcname)
    
    print(f"\nCreated deployment package: {zip_path}")
    print(f"Package size: {zip_path.stat().st_size / 1024:.1f} KB")
    
    # Move zip into dist directory
    shutil.move(str(zip_path), str(dist_dir / zip_name))
    
    print(f"\n✅ ASP.NET deployment package ready in: {dist_dir}")
    print(f"   Deployment zip: {dist_dir / zip_name}")

def create_standalone_webservice(dist_dir, doc_dir):
    """Create the standalone DataConnector.asmx.cs file"""
    
    # Read the original file for reference
    original_file = doc_dir / "DataConnector.asmx.cs"
    original_content = original_file.read_text()
    
    # Extract the class definition and modify for standalone deployment
    webservice_code = '''using System;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Alfresco.iCustomConnector2;

namespace Alfresco
{
    /// <summary>
    /// Alfresco Data Connector Web Service
    /// Standalone ASMX deployment for IIS
    /// </summary>
    [WebService(Namespace = "http://www.alfresco.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class DataConnector : WebService, iCustomConnector2
    {
        private readonly AiGeneratedConnector _impl;
        
        public DataConnector()
        {
            _impl = new AiGeneratedConnector();
        }
        
        #region iCustomConnector2 Implementation
        
        [WebMethod]
        public CustomDataOut DoDescribe(CustomConnection conn, CustomConnectorRequest Request, string[] FileTypes)
        {
            return _impl.DoDescribe(conn, Request, FileTypes);
        }
        
        [WebMethod]
        public CustomDataOut DoGetAvailableDatastores(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetAvailableDatastores(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoGetDatastoreTypes(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetDatastoreTypes(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoCrawl(CustomConnection conn, CustomConnectorRequest Request, string ItemId, bool Recurse, string[] FileTypes)
        {
            return _impl.DoCrawl(conn, Request, ItemId, Recurse, FileTypes);
        }
        
        [WebMethod]
        public CustomDataOut DoGetChanges(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetChanges(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoItemData(CustomConnection conn, CustomConnectorRequest Request, string ItemId, string ItemType)
        {
            return _impl.DoItemData(conn, Request, ItemId, ItemType);
        }
        
        [WebMethod]
        public CustomDataOut DoGetGroups(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetGroups(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoGetGroupsGroups(CustomConnection conn, CustomConnectorRequest Request, string GroupId)
        {
            return _impl.DoGetGroupsGroups(conn, Request, GroupId);
        }
        
        [WebMethod]
        public CustomDataOut DoGetGroupsUsers(CustomConnection conn, CustomConnectorRequest Request, string GroupId)
        {
            return _impl.DoGetGroupsUsers(conn, Request, GroupId);
        }
        
        [WebMethod]
        public CustomDataOut DoGetUsers(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetUsers(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoGetServers(CustomConnection conn, CustomConnectorRequest Request)
        {
            return _impl.DoGetServers(conn, Request);
        }
        
        [WebMethod]
        public CustomDataOut DoGetWebServices(CustomConnection conn, CustomConnectorRequest Request, string ServerId)
        {
            return _impl.DoGetWebServices(conn, Request, ServerId);
        }
        
        [WebMethod]
        public CustomDataOut DoRealtimeSecurityCheck(CustomConnection conn, CustomConnectorRequest Request, string ItemId, string ItemType, string UserOrGroupName, bool IsUser)
        {
            return _impl.DoRealtimeSecurityCheck(conn, Request, ItemId, ItemType, UserOrGroupName, IsUser);
        }
        
        #endregion
        
        #region Version Information
        
        [WebMethod]
        public string GetConnectorVersion()
        {
            return "1.0.0";
        }
        
        [WebMethod]
        public string GetInterfaceVersion()
        {
            return "2.0";
        }
        
        #endregion
    }
}
'''
    
    (dist_dir / "DataConnector.asmx.cs").write_text(webservice_code)

def create_web_config(dist_dir):
    """Create web.config for IIS deployment"""
    
    web_config = '''<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.web>
    <!-- Compilation settings for .NET Framework 4.7.2 -->
    <compilation debug="true" targetFramework="4.7.2">
      <assemblies>
        <add assembly="System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"/>
        <add assembly="System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"/>
      </assemblies>
    </compilation>
    
    <!-- HTTP Runtime settings -->
    <httpRuntime targetFramework="4.7.2" maxRequestLength="51200" executionTimeout="300"/>
    
    <!-- Web Services settings -->
    <webServices>
      <protocols>
        <add name="HttpGet"/>
        <add name="HttpPost"/>
        <add name="HttpSoap"/>
        <add name="HttpSoap12"/>
      </protocols>
    </webServices>
    
    <!-- Custom errors (optional - set to Off for debugging) -->
    <customErrors mode="RemoteOnly"/>
    
    <!-- Authentication (adjust as needed) -->
    <authentication mode="Windows"/>
    
    <!-- Authorization (adjust as needed) -->
    <authorization>
      <allow users="*"/>
    </authorization>
  </system.web>
  
  <system.webServer>
    <!-- IIS settings -->
    <defaultDocument>
      <files>
        <clear/>
        <add value="DataConnector.asmx"/>
      </files>
    </defaultDocument>
    
    <!-- CORS settings (adjust as needed) -->
    <httpProtocol>
      <customHeaders>
        <add name="Access-Control-Allow-Origin" value="*"/>
        <add name="Access-Control-Allow-Methods" value="GET, POST"/>
        <add name="Access-Control-Allow-Headers" value="Content-Type, SOAPAction"/>
      </customHeaders>
    </httpProtocol>
    
    <!-- Handler mappings for ASMX -->
    <handlers>
      <remove name="WebServiceHandlerFactory-Integrated"/>
      <add name="WebServiceHandlerFactory-Integrated" 
           path="*.asmx" 
           verb="*" 
           type="System.Web.Services.Protocols.WebServiceHandlerFactory, System.Web.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" 
           resourceType="Unspecified" 
           requireAccess="Script" 
           preCondition="integratedMode"/>
    </handlers>
  </system.webServer>
  
  <appSettings>
    <!-- Configuration settings -->
    <add key="ConnectorMode" value="Hardcoded"/> <!-- Options: Hardcoded, External -->
    <add key="ExternalApiBaseUrl" value="https://api.example.com/"/>
    <add key="EnableLogging" value="true"/>
    <add key="LogPath" value="~/App_Data/Logs/"/>
  </appSettings>
</configuration>
'''
    
    (dist_dir / "web.config").write_text(web_config)

def create_readme(dist_dir):
    """Create README with IIS deployment instructions"""
    
    readme = r'''# Alfresco Data Connector - IIS Deployment Package

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
'''
    
    (dist_dir / "README.md").write_text(readme)

if __name__ == "__main__":
    main()