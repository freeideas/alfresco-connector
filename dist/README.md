# Integration Instructions for Legacy Developers

## Overview
This package contains a refactored implementation of the Connector that separates business logic from the ASMX infrastructure.

## Contents
- `DataConnector.asmx.cs` - Modified version of your existing file
- `iCustomConnector2impl/` - New implementation files to add to your project

## Integration Steps

### 1. Backup Your Current Files
```
cp DataConnector.asmx.cs DataConnector.asmx.cs.backup
```

### 2. Add Implementation Files
1. Copy the entire `iCustomConnector2impl/` folder to your project root
2. In Visual Studio:
   - Right-click your project → Add → Existing Item
   - Navigate to `iCustomConnector2impl/`
   - Select all `.cs` files (include all subdirectories)
   - **DO NOT** include the `.csproj` file

### 3. Replace DataConnector.asmx.cs
Replace your existing `DataConnector.asmx.cs` with the provided version.

### 4. Build and Test
1. Build your solution - it should compile without errors
2. All your existing SOAP clients will continue to work
3. The behavior is controlled by switches in each method's Handler.cs file

## Configuration

Each method has a hardcoded switch in its Handler.cs file:
```csharp
bool useHardcodedData = true;  // Set to false for external API
```

You can configure each method independently:
- `iCustomConnector2impl/Methods/DoDescribe/Handler.cs`
- `iCustomConnector2impl/Methods/DoCrawl/Handler.cs`
- etc.

## What Changed

### In DataConnector.asmx.cs
- Added: `using AiGeneratedConnector.Implementation;`
- Added: `private static readonly AiGeneratedConnector _impl = new AiGeneratedConnector();`
- Modified: All `override` method bodies now call `_impl.MethodName(...)`
- Unchanged: Helper methods, fields, WebService attributes, WSDL contract

### New Files
- `iCustomConnector2impl/` - Complete implementation without legacy dependencies

## Compatibility
- **Requires .NET Framework 4.6.1 or higher** (for .NET Standard 2.0 support)
- For older .NET Framework versions (4.5 and earlier):
  - The C# code itself is compatible
  - You'll need to add the files directly to your project (no .csproj file)
  - The code uses basic BCL types that exist in all .NET Framework versions
- No additional NuGet packages required
- All existing legacy dependencies remain in DataConnector.asmx.cs
