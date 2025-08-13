# Check IIS Status Script for ASMX Service

param([string]$ConfigFile = ".\deploy-config.json")

function Write-Success { Write-Host $args[0] -ForegroundColor Green }
function Write-Error { Write-Host $args[0] -ForegroundColor Red }
function Write-Info { Write-Host $args[0] -ForegroundColor Cyan }
function Write-Warning { Write-Host $args[0] -ForegroundColor Yellow }

Write-Warning "========================================"
Write-Warning "ASMX Service Status Check"
Write-Warning "========================================"

# Read config
if (Test-Path $ConfigFile) {
    $config = Get-Content $ConfigFile | ConvertFrom-Json
    $SiteName = $config.SiteName
    $AppPoolName = $config.AppPoolName
    $ServicePort = $config.ServicePort
} else {
    $SiteName = "AlfrescoConnectorASMX"
    $AppPoolName = "AlfrescoConnectorPool"
    $ServicePort = 1582
}

Import-Module WebAdministration -ErrorAction SilentlyContinue

# Check site
$site = Get-Website -Name $SiteName -ErrorAction SilentlyContinue
if ($site) {
    Write-Success "Site Found: $SiteName"
    Write-Info "  State: $($site.State)"
    Write-Info "  Path: $($site.PhysicalPath)"
    
    # Check app pool
    $appPool = Get-WebAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
    if ($appPool) {
        Write-Success "App Pool Found: $AppPoolName"
        Write-Info "  State: $($appPool.State)"
    }
    
    if ($site.State -eq "Started" -and $appPool.State -eq "Started") {
        Write-Success "SERVICE IS OPERATIONAL"
        Write-Warning "URL: http://localhost:$ServicePort/DataConnector.asmx"
    } else {
        Write-Warning "SERVICE MAY HAVE ISSUES"
    }
} else {
    Write-Error "Site NOT found: $SiteName"
    Write-Info "Run: .\deploy-iis.ps1"
}
