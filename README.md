# iCustomConnector2 Implementation Project

## Project Goal

**This project delivers SOURCE CODE (not compiled components) for integration into a .NET Framework 4.7.2 legacy project.**

### Deliverables:
1. **Modified `dist/DataConnector.asmx.cs`** - Drop-in replacement for the original file that delegates all `iCustomConnector2` methods to our implementation
2. **Supporting source code in `iCustomConnector2impl/`** - Complete implementation that compiles in .NET Framework 4.7.2 environments

### Development Environment:
- Developed and tested on Linux using .NET 8.0
- Source code is compatible with C# 7.3 and .NET Framework 4.7.2
- No external NuGet dependencies - uses only BCL types

## Overview

We deliver a complete SOURCE CODE implementation of the `iCustomConnector2` interface:
1. A modified `DataConnector.asmx.cs` that delegates ALL method calls to our implementation (no more NotImplementedException!)
2. A structured tree of `.cs` files implementing the delegated methods that will compile in your .NET Framework 4.7.2 project

**IMPORTANT**: We implement ALL methods from the `iCustomConnector2` interface to ensure full compatibility with legacy code. This includes:

**Core Methods** (currently delegated in dist/DataConnector.asmx.cs):
- DoDescribe
- DoGetAvailableDatastores  
- DoGetDatastoreTypes
- DoCrawl
- DoGetChanges
- DoItemData
- DoGetGroups
- DoGetGroupsGroups
- DoGetGroupsUsers
- DoGetUsers

**Additional Methods** (for complete interface implementation):
- DoGetServers
- DoGetWebServices
- DoRealtimeSecurityCheck

Note: GetConnectorVersion and GetInterfaceVersion are protected/internal methods, not abstract interface methods.

## Project Structure

```
/iCustomConnector2impl/      # SOURCE CODE to copy to your .NET Framework 4.7.2 project
  iCustomConnector2impl.csproj  # Multi-targets for testing (not needed in legacy project)
  AiGeneratedConnector.cs    # Main class implementing iCustomConnector2 interface
  
  /Methods/                  # Each method in its own directory
    /DoDescribe/
      Handler.cs             # Main orchestrator with method implementation
      Hardcode.cs            # Hardcoded responses
      External.cs            # External API implementation
    /DoGetAvailableDatastores/
      Handler.cs
      Hardcode.cs
      External.cs
    /DoCrawl/
      Handler.cs
      Hardcode.cs
      External.cs
    /DoGetDatastoreTypes/
    /DoGetChanges/
    /DoItemData/
    /DoGetUsers/
    /DoGetGroups/
    /DoGetGroupsUsers/
    /DoGetGroupsGroups/
    /DoGetServers/
    /DoGetWebServices/
    /DoRealtimeSecurityCheck/
    
  /Models/
    ICustomConnectorInterfaces.cs  # Copy for compilation
    
  /Services/
    LibTest.cs               # Unit testing framework
    ExternalApiClient.cs     # Shared REST client (if needed)
    Configuration.cs         # Runtime configuration

/scripts/
  dist.py                    # Builds the distribution package
  unit_test.py              # Runs unit tests

/doc/
  DataConnector.asmx.cs     # Original file for reference
  /copy_src/
    ICustomConnectorInterfaces.cs  # Interface definitions
    LibTest.cs              # Test framework
  /external-api/            # API documentation and examples
  /per-interface-method/    # SOAP examples

/dist/                      # Distribution package (git-tracked)
  README.md                 # Integration instructions for legacy developers
  DataConnector.asmx.cs     # Modified with new method bodies
  /iCustomConnector2impl/   # Implementation files to add to legacy project
    AiGeneratedConnector.cs
    /Methods/...
    /Models/...
    /Services/...
```

## Implementation Strategy

### Method Organization

Each `iCustomConnector2` interface method gets its own directory with a consistent structure:
- `Handler.cs` - Main method implementation that orchestrates the logic and includes unit tests
- `Hardcode.cs` - Returns hardcoded/mocked responses for testing and development
- `External.cs` - Integration with external APIs when needed

**📁 Implementation Resources Already Provided:**

1. **Hardcoded Implementations** in `/doc/per-interface-method/`:
   - `/doc/per-interface-method/DoDescribe-hardcode.cs` → copy to `/Methods/DoDescribe/Hardcode.cs`
   - `/doc/per-interface-method/DoCrawl-hardcode.cs` → copy to `/Methods/DoCrawl/Hardcode.cs`
   - `/doc/per-interface-method/DoGetUsers-hardcode.cs` → copy to `/Methods/DoGetUsers/Hardcode.cs`
   - etc.

2. **External API Specifications** in `/doc/external-api/`:
   - `/doc/external-api/DoDescribe.md` → instructions for `/Methods/DoDescribe/External.cs`
   - `/doc/external-api/DoCrawl.md` → instructions for `/Methods/DoCrawl/External.cs`
   - `/doc/external-api/DoGetUsers.md` → instructions for `/Methods/DoGetUsers/External.cs`
   - etc.
   - Each file contains detailed API endpoint mappings, request/response transformations, and example code

### Modified DataConnector.asmx.cs

The `dist/DataConnector.asmx.cs` contains a modified version that delegates all `iCustomConnector2` method calls to our `AiGeneratedConnector` implementation. This allows seamless integration of our implementation into existing systems.

## Development Workflow

### 1. Build & Test Locally

**Important Guidelines:**
- Follow the coding standards in `/doc/C-SHARP_CODE_GUIDELINES.md`
- Use the testing approach described in `/doc/C-SHARP_TESTING.md`
- **Hardcoded implementations are already provided!** Copy from `/doc/per-interface-method/[Method]-hardcode.cs` to your method's `Hardcode.cs` file
- **External API specifications are already documented!** Follow `/doc/external-api/[Method].md` when implementing your method's `External.cs` file

```bash
# Build the implementation
cd iCustomConnector2impl
dotnet build

# Run unit tests
cd ..
./scripts/unit_test.py

# Test specific method
./scripts/unit_test.py --class AiGeneratedConnector.Methods.DoDescribe
```

### 2. Generate Distribution

```bash
# Generate dist folder
./scripts/dist.py

# This creates:
# - dist/README.md (integration instructions)
# - dist/DataConnector.asmx.cs (modified)
# - dist/iCustomConnector2impl/ (implementation files)
```

### 3. Unit Testing

Each method includes test methods using LibTest in the Handler.cs file. See `/doc/C-SHARP_TESTING.md` for the testing approach.

## Integration Instructions

### Simple Integration Steps:
1. **Copy source files to your .NET Framework 4.7.2 project:**
   - Replace your existing `DataConnector.asmx.cs` with our `dist/DataConnector.asmx.cs`
   - Copy the entire `iCustomConnector2impl/` folder to your project
   - Ensure the existing Alfresco dependencies are available

2. **Build your legacy project** - The source code will compile directly in .NET Framework 4.7.2

3. **That's it!** All `iCustomConnector2` methods now have working implementations instead of NotImplementedException

### What's in the dist/ directory:
- `dist/DataConnector.asmx.cs` - Drop-in replacement that delegates to our implementation
- `dist/iCustomConnector2impl/` - Complete source code implementation (copy this entire folder)
- `dist/README.md` - Additional integration notes

## Compatibility Guarantee

### Target Framework
- **Primary target: .NET Framework 4.7.2** (as specified by project requirements)
- Source code uses only C# 7.3 features
- Multi-targets .NET 8.0 for development/testing and .NET Standard 2.0 for compatibility
- **No nullable reference types** - removed for C# 7.3 compatibility

### Zero External Dependencies
- Uses only Base Class Library (BCL) types
- No NuGet packages required
- All custom types come from your existing `ICustomConnectorInterfaces.cs`

### Development & Testing
- Developed on Linux with .NET 8.0
- Source code tested to ensure .NET Framework 4.7.2 compatibility
- Unit tests verify all implementations work correctly

## Scripts

### dist.py
Builds the distribution package by:
1. Cleaning and creating the dist/ directory
2. Copying implementation files from iCustomConnector2impl/
3. Creating modified DataConnector.asmx.cs with delegated method bodies
4. Generating integration README.md

### unit_test.py
Runs unit tests using the LibTest framework:
- Tests all methods with `_TEST_` suffix
- Can test individual classes with `--class` parameter
- Provides colored output and summary statistics
