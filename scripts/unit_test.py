#!/home/ace/bin/uvrun
# /// script
# requires-python = ">=3.8"
# dependencies = [
#     "colorama",
#     "tabulate",
# ]
# ///
"""
Run all unit tests for the DataConnector ASP.NET Core project.
Uses the LibTest framework built into the C# codebase.
"""

import subprocess
import sys
import time
import re
from pathlib import Path
from colorama import init, Fore, Style
from tabulate import tabulate

# Initialize colorama
init(autoreset=True)

class UnitTestRunner:
    """Runner for C# unit tests using LibTest framework."""
    
    def __init__(self):
        self.csproj_dir = Path(__file__).parent.parent / "iCustomConnector2impl"
        self.test_results = []
        
    def build_project(self):
        """Build the ASP.NET Core project."""
        print(f"{Fore.CYAN}{'='*80}")
        print(f"{Fore.CYAN}DataConnector Unit Test Suite")
        print(f"{Fore.CYAN}{'='*80}")
        print(f"\n{Fore.YELLOW}→ Building project...")
        
        result = subprocess.run(
            ["dotnet", "build"],
            cwd=str(self.csproj_dir),
            capture_output=True,
            text=True
        )
        
        if result.returncode != 0:
            print(f"{Fore.RED}❌ Build failed:")
            print(result.stderr)
            return False
        
        print(f"{Fore.GREEN}✓ Build successful\n")
        return True
    
    def run_all_tests(self):
        """Run all unit tests."""
        print(f"{Fore.YELLOW}→ Running all unit tests...\n")
        
        start_time = time.time()
        result = subprocess.run(
            ["dotnet", "run", "--framework", "net8.0", "--", "_TEST_"],
            cwd=str(self.csproj_dir),
            capture_output=True,
            text=True
        )
        elapsed = time.time() - start_time
        
        return self.parse_test_output(result.stdout, result.stderr, elapsed)
    
    def run_specific_test(self, class_name):
        """Run tests for a specific class."""
        print(f"{Fore.YELLOW}→ Running tests for {class_name}...\n")
        
        start_time = time.time()
        result = subprocess.run(
            ["dotnet", "run", "--framework", "net8.0", "--", "_TEST_", class_name],
            cwd=str(self.csproj_dir),
            capture_output=True,
            text=True
        )
        elapsed = time.time() - start_time
        
        return self.parse_test_output(result.stdout, result.stderr, elapsed)
    
    def parse_test_output(self, stdout, stderr, elapsed):
        """Parse test output and extract results."""
        output = stdout + stderr
        lines = output.split('\n')
        
        # Updated patterns for actual test output format
        test_pass_pattern = re.compile(r'✓\s+([\w\.]+\.)?([\w]+)_TEST_')
        test_fail_pattern = re.compile(r'✗\s+([\w\.]+\.)?([\w]+)_TEST_')
        class_summary_pattern = re.compile(r'([\w\.]+):\s+(\d+)\s+passed,\s+(\d+)\s+failed')
        
        tests_found = []
        total_passed = 0
        total_failed = 0
        
        for line in lines:
            # Check for passing tests
            pass_match = test_pass_pattern.search(line)
            if pass_match:
                class_prefix = pass_match.group(1) or ''
                test_name = pass_match.group(2)
                # Determine class from context or prefix
                class_name = class_prefix.rstrip('.') if class_prefix else 'Handler'
                tests_found.append({
                    'class': class_name,
                    'test': test_name,
                    'status': 'PASS',
                    'time': 0
                })
            
            # Check for failing tests
            fail_match = test_fail_pattern.search(line)
            if fail_match:
                class_prefix = fail_match.group(1) or ''
                test_name = fail_match.group(2)
                class_name = class_prefix.rstrip('.') if class_prefix else 'Handler'
                tests_found.append({
                    'class': class_name,
                    'test': test_name,
                    'status': 'FAIL',
                    'time': 0
                })
            
            # Check for class summaries to get totals
            summary_match = class_summary_pattern.search(line)
            if summary_match:
                class_name = summary_match.group(1)
                passed = int(summary_match.group(2))
                failed = int(summary_match.group(3))
                total_passed += passed
                total_failed += failed
        
        # Print output for debugging
        if not tests_found:
            print(f"{Fore.YELLOW}Test output:")
            print(output)
        
        # Use totals from summaries if available, otherwise count individual tests
        if total_passed > 0 or total_failed > 0:
            passed = total_passed
            failed = total_failed
        else:
            passed = sum(1 for t in tests_found if t['status'] == 'PASS')
            failed = sum(1 for t in tests_found if t['status'] == 'FAIL')
        
        return {
            'tests': tests_found,
            'passed': passed,
            'failed': failed,
            'total_time': elapsed,
            'raw_output': output
        }
    
    def display_results(self, results):
        """Display test results in a formatted table."""
        print(f"\n{Fore.CYAN}{'='*80}")
        print(f"{Fore.CYAN}Test Results")
        print(f"{Fore.CYAN}{'='*80}\n")
        
        if results['tests']:
            # Group tests by class
            by_class = {}
            for test in results['tests']:
                if test['class'] not in by_class:
                    by_class[test['class']] = []
                by_class[test['class']].append(test)
            
            # Display each class
            for class_name, tests in by_class.items():
                print(f"\n{Fore.CYAN}{class_name}:")
                for test in tests:
                    symbol = '✅' if test['status'] == 'PASS' else '❌'
                    color = Fore.GREEN if test['status'] == 'PASS' else Fore.RED
                    print(f"  {color}{symbol} {test['test']}_TEST_")
        
        # Summary statistics
        print(f"\n{Fore.CYAN}Summary:")
        print(f"  Total Tests: {results['passed'] + results['failed']}")
        print(f"  {Fore.GREEN}Passed: {results['passed']}")
        print(f"  {Fore.RED}Failed: {results['failed']}")
        print(f"  Time: {results['total_time']:.2f}s")
        
        # Overall result
        print(f"\n{Fore.CYAN}{'='*80}")
        if results['failed'] == 0 and results['passed'] > 0:
            print(f"{Fore.GREEN}✅ ALL TESTS PASSED!")
        elif results['passed'] == 0 and results['failed'] == 0:
            print(f"{Fore.RED}❌ NO TESTS WERE RUN!")
        else:
            print(f"{Fore.RED}❌ {results['failed']} TESTS FAILED")
        print(f"{Fore.CYAN}{'='*80}\n")
        
        return results['failed'] == 0 and results['passed'] > 0
    
    def list_test_classes(self):
        """List all available test classes by running a discovery command."""
        print(f"\n{Fore.CYAN}Discovering test classes...")
        print(f"{Fore.CYAN}{'='*60}\n")
        
        # Run a special command to list available test classes
        result = subprocess.run(
            ["dotnet", "run", "--framework", "net8.0", "--", "_TEST_", "--list"],
            cwd=str(self.csproj_dir),
            capture_output=True,
            text=True
        )
        
        if result.returncode != 0 or "--list" in result.stdout:
            # Fallback: parse from the error message when class not found
            print(f"{Fore.YELLOW}To see available test classes, try running a non-existent class:")
            print(f"{Fore.YELLOW}scripts/unit_test.py --class AiGeneratedConnector.Methods.DUMMY\n")
            print(f"{Fore.YELLOW}This will show all available test classes in the error message.\n")
        else:
            # Parse and display the output
            lines = result.stdout.split('\n')
            for line in lines:
                if line.strip().startswith('-'):
                    print(f"  {line.strip()}")
        
        print(f"\n{Fore.YELLOW}Use --class <name> to test a specific class")
        print(f"{Fore.YELLOW}Example: scripts/unit_test.py --class AiGeneratedConnector.Methods.DoCrawl.Handler\n")


def main():
    """Main test execution."""
    import argparse
    
    parser = argparse.ArgumentParser(description='Run C# unit tests for DataConnector')
    parser.add_argument('--class', dest='class_name',
                        help='Run tests for a specific class')
    parser.add_argument('--list', action='store_true',
                        help='List all available test classes')
    parser.add_argument('--verbose', action='store_true',
                        help='Show detailed test output')
    
    args = parser.parse_args()
    
    runner = UnitTestRunner()
    
    if args.list:
        runner.list_test_classes()
        return 0
    
    # Build the project
    if not runner.build_project():
        return 1
    
    # Run tests
    if args.class_name:
        results = runner.run_specific_test(args.class_name)
    else:
        results = runner.run_all_tests()
    
    # Show raw output if verbose or no tests found
    if args.verbose or not results['tests']:
        print(f"\n{Fore.YELLOW}Raw test output:")
        print(f"{Fore.YELLOW}{'='*60}")
        print(results['raw_output'])
        print(f"{Fore.YELLOW}{'='*60}\n")
    
    # Display results
    success = runner.display_results(results)
    
    return 0 if success else 1


if __name__ == "__main__":
    sys.exit(main())