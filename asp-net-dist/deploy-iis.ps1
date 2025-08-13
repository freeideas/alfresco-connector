# Alfresco Connector ASMX IIS Deployment Script
# Creates a new IIS site on port 1582 for ASMX service

param([string]$ConfigFile = ".\deploy-config.json")

# Color functions
function Write-Success { Write-Host $args[0] -ForegroundColor Green }
function Write-Error { Write-Host $args[0] -ForegroundColor Red }
function Write-Info { Write-Host $args[0] -ForegroundColor Cyan }
function Write-Warning { Write-Host $args[0] -ForegroundColor Yellow }

Write-Success "========================================"
Write-Success "Alfresco Connector ASMX Deployment"
Write-Success "========================================"

# Check Administrator
if (!([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]"Administrator")) {
    Write-Error "ERROR: Run as Administrator!"
    exit 1
}

# Read config
$config = Get-Content $ConfigFile | ConvertFrom-Json
$ServicePort = $config.ServicePort
$SiteName = $config.SiteName
$AppPoolName = $config.AppPoolName
$PhysicalPath = if ($config.InstallPath -eq ".") { $PSScriptRoot } else { $config.InstallPath }

Write-Info "Port: $ServicePort, Site: $SiteName, Path: $PhysicalPath"

# Import IIS
Import-Module WebAdministration -ErrorAction Stop

# Remove existing
if (Get-Website -Name $SiteName -ErrorAction SilentlyContinue) {
    Stop-Website -Name $SiteName -ErrorAction SilentlyContinue
    Remove-Website -Name $SiteName -Confirm:$false
}
if (Get-WebAppPool -Name $AppPoolName -ErrorAction SilentlyContinue) {
    Stop-WebAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
    Remove-WebAppPool -Name $AppPoolName -Confirm:$false
}

# Create App Pool (.NET Framework)
New-WebAppPool -Name $AppPoolName
Set-ItemProperty -Path "IIS:\AppPools\$AppPoolName" -Name managedRuntimeVersion -Value "v4.0"

# Create Site
New-WebSite -Name $SiteName -Port $ServicePort -PhysicalPath $PhysicalPath -ApplicationPool $AppPoolName

# Set permissions
icacls "$PhysicalPath" /grant "IIS_IUSRS:(OI)(CI)RX" /T /Q

# Start services
Start-WebAppPool -Name $AppPoolName
Start-WebSite -Name $SiteName

Write-Success "DEPLOYMENT SUCCESSFUL!"
Write-Warning "Service URL: http://localhost:$ServicePort/DataConnector.asmx"
