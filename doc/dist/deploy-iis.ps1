# DataConnector IIS Deployment Script
# Version: 5.0 - Production Ready
# Uses lessons learned from deployment testing

param(
    [string]$ConfigFile = ".\deploy-config.json"
)

function Write-Success {
    param([string]$Message)
    Write-Host $Message -ForegroundColor Green
}

function Write-Error {
    param([string]$Message)
    Write-Host $Message -ForegroundColor Red
}

function Write-Info {
    param([string]$Message)
    Write-Host $Message -ForegroundColor Cyan
}

function Write-Warning {
    param([string]$Message)
    Write-Host $Message -ForegroundColor Yellow
}

Write-Success "========================================"
Write-Success "DataConnector IIS Deployment"
Write-Warning "Version: 5.0 (Production)"
Write-Success "========================================"

# Check if running as Administrator
$currentPrincipal = [Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()
$isAdmin = $currentPrincipal.IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")

if (-NOT $isAdmin) {
    Write-Error "ERROR: This script requires Administrator privileges!"
    Write-Warning "Please run PowerShell as Administrator and try again."
    exit 1
}

# Check if config file exists
if (!(Test-Path $ConfigFile)) {
    Write-Error "ERROR: Configuration file not found: $ConfigFile"
    Write-Warning "Please ensure deploy-config.json exists in the current directory."
    exit 1
}

# Read configuration
Write-Host ""
Write-Info "Reading configuration from: $ConfigFile"
$config = Get-Content $ConfigFile | ConvertFrom-Json

$ServicePort = $config.ServicePort
$SiteName = $config.SiteName
$AppPoolName = $config.AppPoolName
$PhysicalPath = $config.InstallPath

# Handle relative path - if InstallPath is ".", use current directory
if ($PhysicalPath -eq ".") {
    $PhysicalPath = Split-Path -Parent $MyInvocation.MyCommand.Path
    Write-Info "Using current directory: $PhysicalPath"
}

# Display configuration
Write-Host ""
Write-Info "Configuration:"
Write-Host "  Service Port: $ServicePort"
Write-Host "  Site Name: $SiteName"
Write-Host "  App Pool: $AppPoolName"
Write-Host "  Install Path: $PhysicalPath"
Write-Host ""

# Import IIS modules
Write-Info "Loading IIS modules..."
Import-Module WebAdministration -ErrorAction Stop
Import-Module IISAdministration -ErrorAction Stop
Write-Success "IIS modules loaded"

# Check if path exists
if (!(Test-Path $PhysicalPath)) {
    Write-Error "ERROR: Install path does not exist: $PhysicalPath"
    Write-Warning "Please ensure the application files are present."
    exit 1
}

Write-Host ""
Write-Success "Starting deployment..."

# Remove existing site if exists
$existingSite = Get-Website -Name $SiteName -ErrorAction SilentlyContinue
if ($existingSite) {
    Write-Warning "Removing existing site: $SiteName"
    Stop-Website -Name $SiteName -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 1
    Remove-Website -Name $SiteName -Confirm:$false
}

# Remove existing app pool if exists
$existingPool = Get-ChildItem IIS:\AppPools -ErrorAction SilentlyContinue | Where-Object { $_.Name -eq $AppPoolName }
if ($existingPool) {
    Write-Warning "Removing existing app pool: $AppPoolName"
    Stop-WebAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 1
    Remove-WebAppPool -Name $AppPoolName -Confirm:$false
}

# Create Application Pool for .NET Core
Write-Info "Creating app pool: $AppPoolName"
New-WebAppPool -Name $AppPoolName
Set-ItemProperty -Path "IIS:\AppPools\$AppPoolName" -Name managedRuntimeVersion -Value ""
Set-ItemProperty -Path "IIS:\AppPools\$AppPoolName" -Name enable32BitAppOnWin64 -Value $false
Write-Success "App pool created (No Managed Code)"

# Create the site
Write-Info "Creating site: $SiteName on port $ServicePort"
New-WebSite -Name $SiteName -Port $ServicePort -PhysicalPath $PhysicalPath -ApplicationPool $AppPoolName
Write-Success "Site created"

# Set permissions
Write-Info "Setting permissions..."
$aclCommands = @(
    "icacls `"$PhysicalPath`" /grant `"IIS_IUSRS:(OI)(CI)RX`" /T /Q",
    "icacls `"$PhysicalPath`" /grant `"IUSR:(OI)(CI)RX`" /T /Q"
)

foreach ($cmd in $aclCommands) {
    Invoke-Expression $cmd | Out-Null
}

# Create logs directory if it doesn't exist
$logsPath = Join-Path $PhysicalPath "logs"
if (!(Test-Path $logsPath)) {
    New-Item -ItemType Directory -Path $logsPath | Out-Null
}
Invoke-Expression "icacls `"$logsPath`" /grant `"IIS_IUSRS:(OI)(CI)F`" /T /Q" | Out-Null
Write-Success "Permissions configured"

# Check appsettings.json exists and show MockMode status
$appSettingsPath = Join-Path $PhysicalPath "appsettings.json"
if (Test-Path $appSettingsPath) {
    Write-Info "Checking appsettings.json..."
    $appSettings = Get-Content $appSettingsPath | ConvertFrom-Json
    $mockMode = $appSettings.MockMode
    Write-Success "MockMode is configured as: $mockMode"
}

# Start the app pool and site
Write-Info "Starting services..."
Start-WebAppPool -Name $AppPoolName
Start-WebSite -Name $SiteName
Write-Success "Services started"

# Verify deployment
Write-Host ""
Write-Info "Verifying deployment..."
$site = Get-Website -Name $SiteName -ErrorAction SilentlyContinue
$pool = Get-ChildItem IIS:\AppPools -ErrorAction SilentlyContinue | Where-Object { $_.Name -eq $AppPoolName }

if ($site -and $pool) {
    if ($site.State -eq "Started" -and $pool.State -eq "Started") {
        Write-Success "Deployment verified - site and app pool are running"
    } else {
        Write-Warning "Site or app pool may not be running correctly"
        Write-Host "  Site State: $($site.State)"
        Write-Host "  Pool State: $($pool.State)"
    }
} else {
    Write-Warning "Could not verify deployment"
}

Write-Host ""
Write-Success "========================================"
Write-Success "DEPLOYMENT SUCCESSFUL!"
Write-Success "========================================"
Write-Host ""
Write-Info "Service URLs:"
$computerName = $env:COMPUTERNAME
Write-Warning "  http://${computerName}:${ServicePort}/DataConnector.asmx"
Write-Warning "  http://localhost:${ServicePort}/DataConnector.asmx"
Write-Host ""
Write-Success "Deployment completed successfully!"
Write-Host ""
Write-Info "To check the status later, run: .\check-iis-status.ps1"