# Check IIS Status Script
# Version: 1.0
# Verifies DataConnector deployment status

param(
    [string]$SiteName = "DataConnectorService",
    [string]$AppPoolName = "DataConnectorAppPool"
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
Write-Warning "IIS Status Check"
Write-Warning "========================================"
Write-Host ""

# Import IIS modules
Import-Module WebAdministration -ErrorAction SilentlyContinue
Import-Module IISAdministration -ErrorAction SilentlyContinue

# Check if site exists
$site = Get-Website -Name $SiteName -ErrorAction SilentlyContinue

if ($site) {
    Write-Success "Site Found: $SiteName"
    Write-Info "  State: $($site.State)"
    Write-Info "  Physical Path: $($site.PhysicalPath)"
    Write-Info "  ID: $($site.ID)"
    
    # Get bindings
    $site.Bindings.Collection | ForEach-Object {
        $bindingInfo = $_.bindingInformation
        Write-Info "  Binding: $bindingInfo"
        # Extract port from binding (format is *:port:hostname)
        if ($bindingInfo -match ':(\d+):') {
            $port = $matches[1]
            Write-Info "  Port: $port"
        }
    }
    
    # Check app pool
    $appPool = Get-ChildItem IIS:\AppPools -ErrorAction SilentlyContinue | Where-Object { $_.Name -eq $AppPoolName }
    if ($appPool) {
        Write-Host ""
        Write-Success "App Pool Found: $AppPoolName"
        Write-Info "  State: $($appPool.State)"
        Write-Info "  .NET CLR Version: $($appPool.managedRuntimeVersion)"
        Write-Info "  Pipeline Mode: $($appPool.managedPipelineMode)"
        Write-Info "  Enable 32-bit: $($appPool.enable32BitAppOnWin64)"
    } else {
        Write-Warning "App Pool NOT found: $AppPoolName"
    }
    
    # Check if application files exist
    Write-Host ""
    Write-Info "Checking application files..."
    $requiredFiles = @(
        "DataConnector.dll",
        "appsettings.json",
        "web.config"
    )
    
    $allFilesPresent = $true
    foreach ($file in $requiredFiles) {
        $filePath = Join-Path $site.PhysicalPath $file
        if (Test-Path $filePath) {
            Write-Success "  Found: $file"
        } else {
            Write-Error "  Missing: $file"
            $allFilesPresent = $false
        }
    }
    
    # Check MockMode setting
    $appSettingsPath = Join-Path $site.PhysicalPath "appsettings.json"
    if (Test-Path $appSettingsPath) {
        $appSettings = Get-Content $appSettingsPath | ConvertFrom-Json
        Write-Host ""
        Write-Info "Application Settings:"
        Write-Info "  MockMode: $($appSettings.MockMode)"
    }
    
    # Summary
    Write-Host ""
    Write-Success "========================================"
    if ($site.State -eq "Started" -and $appPool.State -eq "Started" -and $allFilesPresent) {
        Write-Success "SERVICE IS OPERATIONAL"
        Write-Success "========================================"
        Write-Host ""
        Write-Info "You can access the service at:"
        if ($port) {
            $computerName = $env:COMPUTERNAME
            Write-Warning "  http://localhost:$port/DataConnector.asmx"
            Write-Warning "  http://${computerName}:$port/DataConnector.asmx"
        }
    } else {
        Write-Warning "SERVICE MAY HAVE ISSUES"
        Write-Success "========================================"
        if ($site.State -ne "Started") {
            Write-Warning "  Site is not started"
        }
        if ($appPool.State -ne "Started") {
            Write-Warning "  App pool is not started"
        }
        if (!$allFilesPresent) {
            Write-Warning "  Some required files are missing"
        }
    }
} else {
    Write-Error "Site NOT found: $SiteName"
    Write-Host ""
    Write-Info "To deploy the service, run:"
    Write-Warning "  powershell -ExecutionPolicy Bypass -File deploy-iis.ps1"
}

Write-Host ""