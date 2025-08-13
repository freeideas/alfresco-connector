#!/home/ace/bin/uvrun
# /// script
# requires-python = ">=3.8"
# dependencies = []
# ///

"""
Generate implementation prompts for ExternalApi.cs files.
Creates a prompts.txt file with instructions for implementing each method's external API portion.
"""

import os
from pathlib import Path

def get_methods_with_external_api():
    """Get list of methods that need ExternalApi.cs implementation based on .md files in external-api dir."""
    external_api_dir = Path(__file__).parent.parent / "doc" / "external-api"
    
    # Methods that should be excluded (metadata or helper files)
    exclude_files = {"README.md", "external-api.md", "test-connection.json"}
    
    methods = []
    if external_api_dir.exists():
        for item in external_api_dir.iterdir():
            if item.is_file() and item.suffix == ".md" and item.name not in exclude_files:
                # Remove .md extension to get method name
                method_name = item.stem
                methods.append(method_name)
    
    # Order methods from easiest to hardest based on typical complexity
    # Simple methods first (mostly static or single API call)
    easy_methods = ["Describe", "GetServers", "GetWebServices"]
    # Medium complexity (simple lists with pagination)
    medium_methods = ["GetUsers2", "GetGroups2", "GetGroupsUsers2", "GetGroupsGroups2", 
                      "GetAvailableDatastores2", "GetDatastoreTypes2"]
    # Complex methods (multiple calls, complex logic, or heavy data transformation)
    hard_methods = ["RealtimeSecurityCheck", "ItemData2", "GetChanges", "Crawl2"]
    
    # Reorder based on difficulty
    ordered_methods = []
    for m in easy_methods:
        if m in methods:
            ordered_methods.append(m)
    for m in medium_methods:
        if m in methods:
            ordered_methods.append(m)
    for m in hard_methods:
        if m in methods:
            ordered_methods.append(m)
    # Add any methods not in our lists
    for m in methods:
        if m not in ordered_methods:
            ordered_methods.append(m)
    
    # Report what we found
    print(f"Found {len(ordered_methods)} methods with external API documentation")
    
    return ordered_methods

def check_implementation_files(method_name):
    """Check which implementation files exist for a given method."""
    method_dir = Path(__file__).parent.parent / "csproj" / "Methods" / method_name
    
    files = {
        "Handler": method_dir / "Handler.cs",
        "Parser": method_dir / "Parser.cs",
        "Hardcode": method_dir / "Hardcode.cs",
        "Serializer": method_dir / "Serializer.cs",
        "ExternalApi": method_dir / "ExternalApi.cs"
    }
    
    existing = {}
    for key, path in files.items():
        existing[key] = path.exists()
    
    return existing

def generate_prompt(method_name):
    """Generate implementation prompt for a single method's ExternalApi.cs."""
    impl_files = check_implementation_files(method_name)
    
    # Determine implementation status
    has_mock = impl_files["Parser"] and impl_files["Hardcode"] and impl_files["Serializer"]
    has_external = impl_files["ExternalApi"]
    
    prompt = f"""
================================================================================
{method_name}: External API Integration
================================================================================

STATUS:"""
    
    if has_external:
        prompt += f" ExternalApi.cs EXISTS"
    elif has_mock:
        prompt += f" Ready for ExternalApi implementation"
    else:
        prompt += f" Mock implementation incomplete - implement Parser/Hardcode/Serializer first"
    
    prompt += f"""

TASKS:
1. CREATE: @csproj/Methods/{method_name}/ExternalApi.cs
   - See @csproj/Methods/Describe/ExternalApi.cs for REFERENCE IMPLEMENTATION
   - Use shared utilities: ConfigurationLoader and HttpClientService
   - Include ILogger parameter for proper logging
   - Return type must match Hardcode.GetResponse() return type
   - Document difficulties, if any, in @doc/external-api/{method_name}.md and to ./report-extsvc_{method_name}.md

2. MODIFY: @csproj/Methods/{method_name}/Handler.cs
   - Replace: Hardcode.GetResponse(request)
   - With: config.GetValue<bool>("MockMode") ? Hardcode.GetResponse(request) : ExternalApi.Execute(request, config, logger).GetAwaiter().GetResult()
   - Note: Pass the logger parameter to ExternalApi.Execute()

3. CONFIGURE: @csproj/appsettings.json (AFTER TESTS PASS)
   - Set "MockMode": false to enable external API by default
   - This ensures the connector uses real API calls in production

KEY RESOURCES:
- Background information: @README.md (READ THIS FIRST for project overview!)
- REFERENCE IMPLEMENTATION: @csproj/Methods/Describe/ExternalApi.cs (STUDY THIS SECOND!)
- Shared utilities: @csproj/Services/ConfigurationLoader.cs and @csproj/Services/HttpClientService.cs
- API mapping: @doc/external-api/{method_name}.md (READ THIS THIRD!)
- Expected response: @csproj/Methods/{method_name}/Hardcode.cs
- Types to use: @csproj/Models/ICustomConnectorInterfaces.cs

IMPLEMENTATION PATTERN (from Describe example):
```csharp
public static async Task<ResponseType> Execute(object request, IConfiguration config, ILogger? logger = null)
{{
    logger?.LogInformation("{method_name}.ExternalApi: Starting execution");
    
    // Load connection configuration
    var connection = await ConfigurationLoader.LoadConnectionAsync(config, logger);
    var baseUrl = ConfigurationLoader.GetBaseUrl(connection);
    
    // Get authenticated HTTP client
    var client = HttpClientService.GetAuthenticatedClient(connection, logger);
    
    // Make API calls using HttpClientService.GetAsync/PostAsync for automatic logging
    var response = await HttpClientService.GetAsync(client, "api/endpoint", logger);
    
    // Process response...
    logger?.LogInformation("{method_name}.ExternalApi: Completed successfully");
    
    return result;
}}
```

REMEMBER:
- Zero, one or many API calls may be needed to fulfill one request
- Always log API calls and responses using the provided ILogger
- Use HttpClientService.GetAsync/PostAsync for automatic request/response logging

TEST:
- dotnet run -- _TEST_ {method_name}
- Set MockMode=false in appsettings.json and run integration tests
- LEAVE MockMode=false after tests pass (external API should be default)

DOCUMENT:
- If you hit significant issues, add "## Implementation Notes" to @doc/external-api/{method_name}.md
- Create @report-extsvc_{method_name}.md at the root of this project, with implementation summary; include recommendations, if any, for documentation or architecture improvements that would make the implementation you just did, eaisier in the future.
"""
    
    return prompt

def main():
    """Generate prompts for all methods and write to file."""
    output_file = Path(__file__).parent / "prompts.txt"
    
    print("Generating ExternalApi implementation prompts...")
    
    methods = get_methods_with_external_api()
    
    if not methods:
        print("No methods with external API documentation found!")
        return
    
    print(f"Found {len(methods)} methods to process")
    
    with open(output_file, "w") as f:
        f.write("EXTERNAL API IMPLEMENTATION PROMPTS\n")
        f.write("=" * 80 + "\n")
        f.write("\nIMPORTANT: Read @README.md first for implementation patterns and guidelines.\n")
        f.write("\nMethods are ordered from EASIEST to HARDEST.\n")
        f.write("Start with the easy ones to build confidence and establish patterns.\n")
        f.write("=" * 80 + "\n\n")
        
        f.write("METHODS REQUIRING EXTERNAL API:\n")
        for i, method in enumerate(methods, 1):
            impl_files = check_implementation_files(method)
            if impl_files["ExternalApi"]:
                status = "✓"
            elif impl_files["Parser"] and impl_files["Hardcode"] and impl_files["Serializer"]:
                status = "⚠"  # Mock exists, needs ExternalApi
            else:
                status = "○"  # Not ready for ExternalApi yet
            f.write(f"{i:2}. {status} {method}\n")
        
        f.write("\nLEGEND:\n")
        f.write("  ✓ = ExternalApi.cs exists\n")
        f.write("  ⚠ = Ready for ExternalApi implementation (mock complete)\n")
        f.write("  ○ = Not ready (needs mock implementation first)\n")
        
        f.write("\n" + "=" * 80 + "\n")
        
        for method in methods:
            prompt = generate_prompt(method)
            f.write(prompt)
            f.write("\n")
        
        f.write("\n" + "=" * 80 + "\n")
        f.write("END OF PROMPTS\n")
        f.write("=" * 80 + "\n")
    
    print(f"✓ Prompts written to: {output_file}")
    print(f"  Total methods: {len(methods)}")
    
    # Count implementation status
    ready_count = 0
    external_exists = 0
    not_ready = 0
    
    for method in methods:
        impl_files = check_implementation_files(method)
        if impl_files["ExternalApi"]:
            external_exists += 1
        elif impl_files["Parser"] and impl_files["Hardcode"] and impl_files["Serializer"]:
            ready_count += 1
        else:
            not_ready += 1
    
    print(f"  ExternalApi.cs exists: {external_exists}")
    print(f"  Ready for ExternalApi: {ready_count}")
    print(f"  Not ready (needs mock): {not_ready}")
    
    if ready_count > 0:
        print(f"\nMethods ready for ExternalApi implementation:")
        for method in methods:
            impl_files = check_implementation_files(method)
            if not impl_files["ExternalApi"] and impl_files["Parser"] and impl_files["Hardcode"] and impl_files["Serializer"]:
                print(f"  - {method}")

if __name__ == "__main__":
    main()