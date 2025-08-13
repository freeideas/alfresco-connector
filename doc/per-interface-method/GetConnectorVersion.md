# GetConnectorVersion Method: iCustomConnector2 Interface Implementation Guide

## IMPORTANT NOTE

**This method has no .json example of call from legacy SOAP clients.** While a SOAP endpoint is defined in the service interface for completeness, there is no captured traffic showing a legacy SOAP client invoking this method. The SOAP endpoint exists but has not been observed in use.

## Overview

The `GetConnectorVersion` method in the `iCustomConnector2` interface returns the version number of the connector assembly. Unlike other interface methods, this is a protected method with a default implementation that provides version information for diagnostic and compatibility purposes.

## Method Signature

```csharp
protected string GetConnectorVersion()
```

## Implementation Strategy

### 1. Default Implementation

The base `iCustomConnector2` class provides a default implementation:

```csharp
protected string GetConnectorVersion() 
{ 
    return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0.0"; 
}
```

### 2. No External API Call Required

This method operates entirely within the .NET runtime and does not require any external API calls to any external service.

### 3. Return Value Structure

The method returns a version string in standard .NET format:
- Format: `Major.Minor.Build.Revision`
- Example: `1.0.0.0` or `2.3.1.5`
- Default: `0.0.0.0` if version cannot be determined

### 4. Property Mapping

| Assembly Property | Return Value Component | Example |
|-------------------|------------------------|---------|
| `Version.Major` | Major version | 1 |
| `Version.Minor` | Minor version | 0 |
| `Version.Build` | Build number | 0 |
| `Version.Revision` | Revision number | 0 |

### 5. Access Level Considerations

#### Protected Access
- Only accessible within the class and derived classes
- Not part of the public interface exposed to external clients
- Cannot be called directly by connector consumers

#### Usage Scenarios
1. **Internal Logging**: Log version during connector initialization
2. **Derived Classes**: Custom connectors can access version for enhanced functionality
3. **Debugging**: Include version in error messages or diagnostic output
4. **Compatibility Checks**: Verify minimum version requirements in derived implementations

### 6. Implementation Options

#### Option 1: Use Default Implementation
Most implementations should use the default implementation as-is:
```csharp
// No override needed - base implementation is sufficient
```

#### Option 2: Custom Override (if needed)
Override only if custom version logic is required:
```csharp
protected override string GetConnectorVersion()
{
    // Custom version logic, e.g., include build date
    var version = base.GetConnectorVersion();
    var buildDate = GetBuildDate();
    return $"{version} ({buildDate})";
}
```

### 7. Version Number Management

The version is typically set in the project file:
```xml
<Project>
  <PropertyGroup>
    <AssemblyVersion>1.0.0.0</AssemblyVersion>
    <FileVersion>1.0.0.0</FileVersion>
    <Version>1.0.0</Version>
  </PropertyGroup>
</Project>
```

## Additional Considerations

### Testing
- No separate _TEST_ method required (protected method with default implementation)
- Version can be verified through assembly inspection tools
- Include version in DoDescribe() output for visibility

### Error Handling
- The null-coalescing operator ensures "0.0.0.0" is returned if version is null
- No exceptions are thrown by the default implementation

### Performance
- Uses reflection but only once per call
- Minimal performance impact
- Consider caching if called frequently in derived classes

## SOAP Implementation Note

While the SOAP server includes a GetConnectorVersion endpoint for architectural completeness, **no .json example exists of legacy SOAP clients calling this endpoint**. If implementing the SOAP server:

1. The endpoint can return a NotImplementedException or a dummy response
2. No request/response translation logic is required
3. No test data exists because no captured traffic shows this method being invoked
4. Resources should be focused on methods with confirmed usage

## Summary

The GetConnectorVersion implementation:
1. Provides assembly version information without external dependencies
2. Uses .NET reflection to read version from assembly metadata
3. Returns a standard version string format
4. Serves as a utility for diagnostics and compatibility checking
5. Has a complete default implementation that rarely needs overriding
6. **Has no captured examples of legacy SOAP clients calling it despite having a SOAP endpoint**

This method enables version tracking and debugging capabilities while maintaining the simplicity of not requiring any external API calls or complex logic.