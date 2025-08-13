# DataConnector IIS Uninstall Script
# Version: 2.0 - Production Ready
# Removes DataConnector from IIS (preserves files)

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

Write-Warning "========================================"
Write-Warning "DataConnector IIS Uninstall"
Write-Info "Version: 2.0 (Production)"
Write-Warning "========================================"

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
    Write-Warning "Cannot determine what to uninstall without configuration."
    exit 1
}

# Read configuration
Write-Host ""
Write-Info "Reading configuration from: $ConfigFile"
$config = Get-Content $ConfigFile | ConvertFrom-Json

# Extract configuration values
$SiteName = $config.SiteName
$AppPoolName = $config.AppPoolName
$InstallPath = $config.InstallPath

# Display what will be removed
Write-Host ""
Write-Info "Configuration to remove:"
Write-Host "  Site Name: $SiteName"
Write-Host "  App Pool: $AppPoolName"
Write-Host ""

# Import IIS modules
Write-Info "Loading IIS modules..."
Import-Module WebAdministration -ErrorAction Stop
Import-Module IISAdministration -ErrorAction Stop
Write-Success "IIS modules loaded"

Write-Host ""
Write-Warning "Starting uninstall..."

# Remove site if exists
$site = Get-Website -Name $SiteName -ErrorAction SilentlyContinue
if ($site) {
    Write-Info "Stopping site: $SiteName"
    Stop-Website -Name $SiteName -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 1
    
    Write-Info "Removing site: $SiteName"
    Remove-Website -Name $SiteName -Confirm:$false
    Write-Success "Site removed"
} else {
    Write-Warning "Site not found: $SiteName"
}

# Remove app pool if exists
$appPool = Get-ChildItem IIS:\AppPools | Where-Object { $_.Name -eq $AppPoolName }
if ($appPool) {
    Write-Info "Stopping app pool: $AppPoolName"
    Stop-WebAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 1
    
    Write-Info "Removing app pool: $AppPoolName"
    Remove-WebAppPool -Name $AppPoolName -Confirm:$false
    Write-Success "App pool removed"
} else {
    Write-Warning "App pool not found: $AppPoolName"
}

# Verify removal
Write-Host ""
Write-Info "Verifying uninstall..."

$siteStillExists = Get-Website -Name $SiteName -ErrorAction SilentlyContinue
$poolStillExists = Get-ChildItem IIS:\AppPools -ErrorAction SilentlyContinue | Where-Object { $_.Name -eq $AppPoolName }

if (!$siteStillExists -and !$poolStillExists) {
    Write-Success "Uninstall verification passed - all IIS components removed"
} else {
    Write-Warning "Some components may not have been removed:"
    if ($siteStillExists) {
        Write-Warning "  Site still exists: $SiteName"
    }
    if ($poolStillExists) {
        Write-Warning "  App pool still exists: $AppPoolName"
    }
}

Write-Host ""
Write-Success "========================================"
Write-Success "UNINSTALL COMPLETE"
Write-Success "========================================"
Write-Host ""
Write-Warning "The application files have NOT been deleted."
Write-Warning "If you want to remove them, manually delete:"

# Handle relative path for display
if ($InstallPath -eq ".") {
    $actualPath = Split-Path -Parent $MyInvocation.MyCommand.Path
    Write-Warning "  $actualPath"
} else {
    Write-Warning "  $InstallPath"
}

Write-Host ""
Write-Info "To reinstall, run: deploy-iis.ps1"
Write-Host ""