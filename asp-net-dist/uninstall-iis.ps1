# Alfresco Connector ASMX IIS Uninstall Script

param([string]$ConfigFile = ".\deploy-config.json")

function Write-Success { Write-Host $args[0] -ForegroundColor Green }
function Write-Error { Write-Host $args[0] -ForegroundColor Red }
function Write-Info { Write-Host $args[0] -ForegroundColor Cyan }
function Write-Warning { Write-Host $args[0] -ForegroundColor Yellow }

Write-Warning "========================================"
Write-Warning "Alfresco Connector ASMX Uninstall"
Write-Warning "========================================"

# Check Administrator
if (!([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]"Administrator")) {
    Write-Error "ERROR: Run as Administrator!"
    exit 1
}

# Read config
$config = Get-Content $ConfigFile | ConvertFrom-Json
$SiteName = $config.SiteName
$AppPoolName = $config.AppPoolName

Import-Module WebAdministration -ErrorAction Stop

# Remove site
if (Get-Website -Name $SiteName -ErrorAction SilentlyContinue) {
    Stop-Website -Name $SiteName -ErrorAction SilentlyContinue
    Remove-Website -Name $SiteName -Confirm:$false
    Write-Success "Site removed: $SiteName"
}

# Remove app pool
if (Get-WebAppPool -Name $AppPoolName -ErrorAction SilentlyContinue) {
    Stop-WebAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
    Remove-WebAppPool -Name $AppPoolName -Confirm:$false
    Write-Success "App pool removed: $AppPoolName"
}

Write-Success "UNINSTALL COMPLETE"
Write-Warning "Files have NOT been deleted"
