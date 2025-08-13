#!/home/ace/bin/uvrun
# /// script
# requires-python = ">=3.8"
# dependencies = [
#     "colorama",
# ]
# ///
"""
Distribution builder for iCustomConnector2 implementation.
Copies the implementation files to the dist folder for legacy developers.
Does NOT modify DataConnector.asmx.cs or README.md in dist/.
"""

import shutil
import os
import zipfile
from datetime import datetime
from pathlib import Path
from colorama import init, Fore, Style

# Initialize colorama
init(autoreset=True)

def clean_dist_implementation():
    """Remove only the implementation directory from dist, preserving README and DataConnector.asmx.cs"""
    dist_impl_dir = Path("./dist/iCustomConnector2impl")
    if dist_impl_dir.exists():
        print(f"{Fore.YELLOW}→ Removing old implementation: {dist_impl_dir}")
        shutil.rmtree(dist_impl_dir)
        print(f"{Fore.GREEN}✓ Cleaned old implementation")
    else:
        print(f"{Fore.CYAN}→ No existing implementation to clean")

def copy_implementation():
    """Copy the implementation files from source to dist"""
    source_dir = Path("./iCustomConnector2impl")
    dest_dir = Path("./dist/iCustomConnector2impl")
    
    if not source_dir.exists():
        print(f"{Fore.RED}✗ Source directory not found: {source_dir}")
        print(f"{Fore.YELLOW}  Please create the implementation in {source_dir}/ first")
        return False
    
    # Copy everything except build artifacts
    print(f"{Fore.YELLOW}→ Copying implementation from {source_dir} to {dest_dir}")
    
    def ignore_patterns(dir, files):
        """Ignore build artifacts and project files that legacy devs don't need"""
        ignored = []
        for file in files:
            # Ignore build directories
            if file in ['bin', 'obj', '.vs']:
                ignored.append(file)
            # Ignore project files (legacy devs will add to their own project)
            elif file.endswith('.csproj') or file.endswith('.csproj.user'):
                ignored.append(file)
            # Ignore solution files
            elif file.endswith('.sln'):
                ignored.append(file)
        return ignored
    
    shutil.copytree(source_dir, dest_dir, ignore=ignore_patterns)
    print(f"{Fore.GREEN}✓ Implementation copied successfully")
    
    # Count what was copied
    cs_files = list(dest_dir.rglob("*.cs"))
    method_dirs = [d for d in (dest_dir / "Methods").iterdir() if d.is_dir()] if (dest_dir / "Methods").exists() else []
    
    print(f"\n{Fore.CYAN}Distribution Summary:")
    print(f"  • Total .cs files: {len(cs_files)}")
    print(f"  • Method implementations: {len(method_dirs)}")
    
    if method_dirs:
        print(f"\n{Fore.CYAN}Methods included:")
        for method_dir in sorted(method_dirs):
            files = list(method_dir.glob("*.cs"))
            status = "✓" if files else "✗"
            print(f"  {status} {method_dir.name}: {len(files)} files")
    
    return True

def verify_dist_structure():
    """Verify that the dist folder has everything needed"""
    print(f"\n{Fore.CYAN}Verifying distribution structure...")
    
    required_files = [
        Path("./dist/DataConnector.asmx.cs"),
        Path("./dist/README.md"),
    ]
    
    missing = []
    for file in required_files:
        if file.exists():
            print(f"{Fore.GREEN}  ✓ {file}")
        else:
            print(f"{Fore.RED}  ✗ {file} (MISSING)")
            missing.append(file)
    
    # Check for implementation
    impl_dir = Path("./dist/iCustomConnector2impl")
    if impl_dir.exists():
        print(f"{Fore.GREEN}  ✓ {impl_dir}/")
        
        # Check for key files
        key_files = [
            impl_dir / "CustomConnector.cs",
            impl_dir / "Models" / "ICustomConnectorInterfaces.cs",
            impl_dir / "Services" / "LibTest.cs",
        ]
        
        for file in key_files:
            if file.exists():
                print(f"{Fore.GREEN}    ✓ {file.relative_to(Path('./dist'))}")
            else:
                print(f"{Fore.YELLOW}    ⚠ {file.relative_to(Path('./dist'))} (optional)")
    else:
        print(f"{Fore.RED}  ✗ {impl_dir}/ (MISSING)")
        missing.append(impl_dir)
    
    if missing:
        print(f"\n{Fore.RED}⚠ Distribution is incomplete. Missing files:")
        for file in missing:
            print(f"  - {file}")
        return False
    
    print(f"\n{Fore.GREEN}✅ Distribution is ready for legacy developers!")
    return True

def create_zip_archive():
    """Create a timestamped zip file of the dist contents (excluding zip files)"""
    print(f"\n{Fore.CYAN}Creating ZIP archive...")
    
    # Generate timestamp
    timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
    zip_filename = f"iCustomConnector2_dist_{timestamp}.zip"
    zip_path = Path("./dist") / zip_filename
    
    # Create the zip file
    with zipfile.ZipFile(zip_path, 'w', zipfile.ZIP_DEFLATED) as zipf:
        dist_dir = Path("./dist")
        files_added = 0
        
        # Walk through all files in dist, excluding zip files
        for file_path in dist_dir.rglob("*"):
            if file_path.is_file() and not file_path.suffix == '.zip':
                # Calculate the relative path from dist directory
                arcname = file_path.relative_to(dist_dir)
                zipf.write(file_path, arcname)
                files_added += 1
                print(f"{Fore.GREEN}  + {arcname}")
        
        print(f"\n{Fore.GREEN}✓ Added {files_added} files to archive")
    
    # Get file size
    file_size = zip_path.stat().st_size / 1024  # Size in KB
    if file_size > 1024:
        file_size_str = f"{file_size/1024:.2f} MB"
    else:
        file_size_str = f"{file_size:.2f} KB"
    
    print(f"{Fore.GREEN}✓ Created: {zip_filename} ({file_size_str})")
    return zip_path

def main():
    """Main distribution builder"""
    print(f"{Fore.CYAN}{'='*60}")
    print(f"{Fore.CYAN}iCustomConnector2 Distribution Builder")
    print(f"{Fore.CYAN}{'='*60}\n")
    
    # Ensure we're in the right directory
    if not Path("./scripts/dist.py").exists():
        print(f"{Fore.RED}✗ Please run this script from the project root directory")
        return 1
    
    # Ensure dist directory exists
    dist_dir = Path("./dist")
    if not dist_dir.exists():
        print(f"{Fore.YELLOW}→ Creating dist directory")
        dist_dir.mkdir()
        print(f"{Fore.GREEN}✓ Created dist directory")
    
    # Clean old implementation (preserves DataConnector.asmx.cs and README.md)
    clean_dist_implementation()
    
    # Copy new implementation
    if not copy_implementation():
        return 1
    
    # Verify structure
    if not verify_dist_structure():
        print(f"\n{Fore.YELLOW}Note: DataConnector.asmx.cs and README.md in dist/ are managed manually")
        print(f"{Fore.YELLOW}      They should already be in place and won't be modified by this script")
        return 1
    
    # Create ZIP archive
    zip_path = create_zip_archive()
    
    print(f"\n{Fore.CYAN}{'='*60}")
    print(f"{Fore.GREEN}Distribution package ready in ./dist/")
    print(f"{Fore.GREEN}ZIP archive created: {zip_path.name}")
    print(f"{Fore.CYAN}{'='*60}\n")
    
    return 0

if __name__ == "__main__":
    exit(main())