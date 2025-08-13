#!/home/ace/bin/uvrun
# /// script
# requires-python = ">=3.8"
# dependencies = []
# ///

"""
Generate hardcode implementation prompts for ALL methods in the iCustomConnector2 interface.
Creates prompts for implementing the hardcoded/mock version of each method.
Phase 1: Hardcode implementation only (no external services).

IMPORTANT: Generates prompts for ALL abstract methods in iCustomConnector2 interface to ensure
full compatibility with legacy code, including methods that currently throw NotImplementedException.
"""

import os
from pathlib import Path

def get_methods():
    """Get list of ALL methods from iCustomConnector2 interface, ordered by implementation difficulty."""
    # Get methods from documentation files
    doc_dir = Path(__file__).parent.parent / "doc" / "per-interface-method"
    
    # Find all methods that have .json files (authoritative source)
    methods_from_docs = set()
    if doc_dir.exists():
        for file in doc_dir.iterdir():
            if file.suffix == '.json' and file.stem != 'ItemData2_original':
                method_name = file.stem
                # Skip files that don't match interface methods
                if method_name in ['GetConnectorVersion', 'GetInterfaceVersion', 'SecuritySyncComplete', 'ShutdownNow']:
                    continue
                methods_from_docs.add(method_name)
    
    # ALL methods from iCustomConnector2 interface for complete implementation
    # Including both delegated methods and those that currently throw NotImplementedException
    all_interface_methods = {
        "DoDescribe",                 # DoDescribe -> _impl.DoDescribe()
        "DoGetAvailableDatastores",  # DoGetAvailableDatastores -> _impl.DoGetAvailableDatastores()
        "DoGetDatastoreTypes",       # DoGetDatastoreTypes -> _impl.DoGetDatastoreTypes()
        "DoCrawl",                   # DoCrawl -> _impl.DoCrawl()
        "DoGetChanges",               # DoGetChanges -> _impl.DoGetChanges()
        "DoItemData",                # DoItemData -> _impl.DoItemData()
        "DoGetGroups",               # DoGetGroups -> _impl.DoGetGroups()
        "DoGetGroupsGroups",         # DoGetGroupsGroups -> _impl.DoGetGroupsGroups()
        "DoGetGroupsUsers",          # DoGetGroupsUsers -> _impl.DoGetGroupsUsers()
        "DoGetUsers",                # DoGetUsers -> _impl.DoGetUsers()
        "DoGetServers",              # DoGetServers (currently throws NotImplementedException)
        "DoGetWebServices",          # DoGetWebServices (currently throws NotImplementedException)
        "DoRealtimeSecurityCheck",   # DoRealtimeSecurityCheck (currently throws NotImplementedException)
    }
    
    # Note: GetConnectorVersion and GetInterfaceVersion are protected/internal methods, not abstract interface methods
    
    # Order methods by difficulty (easiest to hardest)
    difficulty_order = [
        # Simple methods - return static/simple data
        "DoGetDatastoreTypes",       # Returns static list of types
        "DoGetAvailableDatastores",  # Returns simple list
        
        # Medium complexity - single entity operations
        "DoDescribe",                 # Returns connector capabilities
        "DoGetUsers",                # Returns user list with properties
        "DoGetGroups",               # Returns group list with properties
        "DoGetGroupsUsers",          # Returns group membership
        "DoGetGroupsGroups",         # Returns group hierarchy
        
        # Higher complexity - data processing
        "DoItemData",                # Returns item metadata
        
        # Most complex - stateful operations
        "DoCrawl",                   # Complex stateful crawling operation
        "DoGetChanges",               # Track and return changes since checkpoint
        
        # Additional methods for complete interface implementation
        "DoGetServers",              # Returns server list
        "DoGetWebServices",          # Returns web service info
        "DoRealtimeSecurityCheck",   # Security validation
    ]
    
    # Build final ordered list - include ALL interface methods
    ordered_methods = []
    
    # Add methods in difficulty order if they exist in docs AND are interface methods
    for method in difficulty_order:
        if method in methods_from_docs and method in all_interface_methods:
            ordered_methods.append(method)
    
    # Add any remaining interface methods not in our difficulty list
    for method in methods_from_docs:
        if method not in ordered_methods and method in all_interface_methods:
            ordered_methods.append(method)
            print(f"Info: Interface method '{method}' not in difficulty ordering, adding at end")
    
    # Add interface methods that don't have docs yet
    for method in all_interface_methods:
        if method not in ordered_methods:
            ordered_methods.append(method)
            print(f"Info: Interface method '{method}' has no documentation files, adding at end")
    
    return ordered_methods

def check_documentation_files(method_name):
    """Check which documentation files exist for a given method."""
    doc_dir = Path(__file__).parent.parent / "doc" / "per-interface-method"
    
    files = {
        "json": doc_dir / f"{method_name}.json",
        "request_md": doc_dir / f"{method_name}-request.md",
        "response_md": doc_dir / f"{method_name}-response.md"
    }
    
    existing = {}
    for key, path in files.items():
        existing[key] = path.exists()
    
    return existing

def generate_prompt(method_name):
    """Generate hardcode implementation prompt for a single method."""
    docs = check_documentation_files(method_name)
    
    prompt = f"""================================================================================
TASK: Implement Hardcoded {method_name} Method
================================================================================

**Objective**: Implement a hardcoded/mock version of the {method_name} method for the iCustomConnector2 interface.

**Phase 1 Focus**: This is the hardcode-only phase. We are NOT implementing external services yet.

**Prerequisites**:
1. Read @README.md to understand the project architecture
2. Review @doc/C-SHARP_CODE_GUIDELINES.md and @doc/C-SHARP_TESTING.md

**Documentation Files**:"""
    
    if docs["json"]:
        prompt += f"\n• @doc/per-interface-method/{method_name}.json - Example request/response data (USE THIS AS YOUR TEMPLATE)"
    else:
        prompt += f"\n• @doc/per-interface-method/{method_name}.json - [MISSING - check interface definition]"
    
    if docs["request_md"]:
        prompt += f"\n• @doc/per-interface-method/{method_name}-request.md - Request structure"
    
    if docs["response_md"]:
        prompt += f"\n• @doc/per-interface-method/{method_name}-response.md - Response structure"
    
    prompt += f"\n• @iCustomConnector2impl/Models/ICustomConnectorInterfaces.cs - Interface definition and types"
    
    # Check if there's an existing hardcode file to reference
    hardcode_file = Path(__file__).parent.parent / "doc" / "per-interface-method" / f"{method_name}-hardcode.cs"
    if hardcode_file.exists():
        prompt += f"\n• @doc/per-interface-method/{method_name}-hardcode.cs - EXISTING HARDCODE IMPLEMENTATION (copy/adapt this!)"
    
    prompt += f"""

**Files to Create** in /iCustomConnector2impl/Methods/{method_name}/:
1. Handler.cs - Main method implementation that calls Hardcode and includes unit tests
2. Hardcode.cs - Returns hardcoded/mocked responses based on the .json examples

**Implementation Guidelines**:
• The Handler.cs should:
  - Implement the interface method
  - Call Hardcode.cs to get mock data
  - Include comprehensive _TEST_ methods
• The Hardcode.cs should:
  - Return realistic mock data matching the .json examples
  - If a -hardcode.cs file exists in /doc/per-interface-method/, copy and adapt it
  - Include static data that matches the expected response structure
• Use ONLY types from ICustomConnectorInterfaces.cs - don't create new types
• Every .cs file MUST include unit test methods with _TEST_ prefix

**Testing**:
1. Run: `./scripts/unit_test.py --class AiGeneratedConnector.Methods.{method_name}`
2. Ensure all tests pass
3. Verify the hardcoded responses match the expected structure

**Success Criteria**:
✓ Method returns valid hardcoded data
✓ Response structure matches the .json examples
✓ All unit tests pass
✓ Code follows the project guidelines
✓ No external service dependencies (this is hardcode-only phase)

**After Implementation**:
Write a report to `./reports/{method_name}_hardcode.txt` that includes:
1. What the hardest part of this implementation was
2. Any ambiguities or confusions encountered
3. Recommendations for improving documentation and/or project design
4. Specific suggestions that would help future developers

The report should be honest and constructive, focusing on actionable improvements.

"""
    
    return prompt

def main():
    """Generate prompts for all methods and write to numbered files in prompts directory."""
    prompts_dir = Path(__file__).parent.parent / "prompts"
    
    # Create prompts directory if it doesn't exist
    prompts_dir.mkdir(exist_ok=True)
    
    print("Generating implementation prompts...")
    
    methods = get_methods()
    print(f"Found {len(methods)} methods to process")
    
    # Write individual prompt files
    for i, method in enumerate(methods, 1):
        prompt_file = prompts_dir / f"hardcode_{i:03d}.md"
        
        with open(prompt_file, "w") as f:
            f.write("# iCustomConnector2 HARDCODE IMPLEMENTATION PROMPT - PHASE 1\n\n")
            f.write("Phase 1: Hardcoded/Mock Implementation Only (No External Services)\n\n")
            f.write("**IMPORTANT**: Implementing ALL methods from iCustomConnector2 interface for full compatibility\n\n")
            f.write(f"**Prompt {i} of {len(methods)}**\n\n")
            f.write("---\n\n")
            
            docs = check_documentation_files(method)
            status = "✓" if all(docs.values()) else "⚠"
            difficulty = "Simple" if i <= 3 else "Medium" if i <= 8 else "Complex" if i <= 12 else "Advanced"
            f.write(f"## Method: {status} {method} [{difficulty}]\n\n")
            f.write("---\n\n")
            
            prompt = generate_prompt(method)
            # Convert prompt to markdown format
            prompt = prompt.replace("=" * 80, "---")
            prompt = prompt.replace("TASK:", "## TASK:")
            prompt = prompt.replace("**Objective**:", "### Objective")
            prompt = prompt.replace("**Phase 1 Focus**:", "### Phase 1 Focus")
            prompt = prompt.replace("**Prerequisites**:", "### Prerequisites")
            prompt = prompt.replace("**Documentation Files**:", "### Documentation Files")
            prompt = prompt.replace("**Files to Create**", "### Files to Create")
            prompt = prompt.replace("**Implementation Guidelines**:", "### Implementation Guidelines")
            prompt = prompt.replace("**Testing**:", "### Testing")
            prompt = prompt.replace("**Success Criteria**:", "### Success Criteria")
            prompt = prompt.replace("**After Implementation**:", "### After Implementation")
            
            f.write(prompt)
            f.write("\n---\n\n")
            f.write(f"**END OF PROMPT {i:03d}**\n")
    
    # Also create an index file
    index_file = prompts_dir / "index.md"
    with open(index_file, "w") as f:
        f.write("# iCustomConnector2 HARDCODE IMPLEMENTATION PROMPTS INDEX\n\n")
        f.write(f"**Total prompts**: {len(methods)}\n\n")
        f.write("**Order**: Easiest to most difficult\n\n")
        f.write("## METHODS TO IMPLEMENT:\n\n")
        for i, method in enumerate(methods, 1):
            docs = check_documentation_files(method)
            status = "✓" if all(docs.values()) else "⚠"
            difficulty = "Simple" if i <= 3 else "Medium" if i <= 8 else "Complex" if i <= 12 else "Advanced"
            f.write(f"- `hardcode_{i:03d}.md`: {status} **{method}** [{difficulty}]\n")
    
    print(f"✓ Prompts written to: {prompts_dir}/")
    print(f"  Total methods: {len(methods)}")
    print(f"  Files created: hardcode_001.md through hardcode_{len(methods):03d}.md")
    print(f"  Index file: index.md")
    print(f"  Order: Easiest to most difficult")
    
    # Count documentation coverage
    full_docs = 0
    partial_docs = 0
    no_docs = 0
    
    for method in methods:
        docs = check_documentation_files(method)
        if all(docs.values()):
            full_docs += 1
        elif any(docs.values()):
            partial_docs += 1
        else:
            no_docs += 1
    
    print(f"  Full documentation: {full_docs}")
    print(f"  Partial documentation: {partial_docs}")
    print(f"  No documentation: {no_docs}")
    print("\nDifficulty levels:")
    print("  Simple: Basic data returns")
    print("  Medium: Single entity operations")
    print("  Complex: Data processing & validation")
    print("  Advanced: Stateful operations")

if __name__ == "__main__":
    main()