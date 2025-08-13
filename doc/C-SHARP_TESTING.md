# C# Testing Strategy

This project uses a simple, home-grown testing approach that keeps test code close to the implementation for better maintainability and visibility.

## Key Principles

### 1. Test Methods Next to Implementation

Test code is located directly in the same class file as the methods being tested. This provides immediate visibility into which methods have test coverage and makes it easy to maintain tests alongside the code they verify.

### 2. Test Method Naming Convention

Test methods follow a specific naming pattern:
- Method name ends with `_TEST_`
- Must be `private static`
- Returns `bool` (true for pass, false for fail)

Example:
```csharp
private static bool MethodName_TEST_() {
    // Test implementation
    return true; // or false if test fails
}
```

### 3. Whole-Class Testing

For testing an entire class, use the special method signature:
```csharp
private static bool __TEST__() {
    // Test entire class functionality
    return true;
}
```

### 4. Every Class Should Be Testable

Since C# only allows one entry point per project, classes expose their tests through a standardized static method:
```csharp
public static bool RunTests() {
    return LibTest.TestClass(typeof(YourClassName));
}
```

### 5. Project Entry Point

The project's single `Main` method (typically in `Program.cs`) handles test execution for all classes:

```csharp
public static void Main(string[] args) {
    if (args.Length > 0 && args[0] == "_TEST_") {
        // Run tests for a specific class if provided
        if (args.Length > 1) {
            var className = args[1];
            var type = Type.GetType(className);
            if (type != null) {
                LibTest.TestClass(type);
            }
        } else {
            // Run all tests
            LibTest.TestAllClasses();
        }
        return;
    }
    // Regular application implementation (e.g., start MCP server)
    var server = new McpServer();
    var port = server.Start();
    Console.WriteLine($"MCP server listening on port: {port}");
    Console.ReadLine();
}
```

## Integration Testing with Test Classes

Beyond unit tests that live alongside methods, you can create integration test classes for testing interactions between multiple components or end-to-end scenarios. These test classes follow a simple naming convention:

- Class name ends with `_TEST_`
- Contains the same `_TEST_` methods as regular classes
- Automatically discovered and executed by LibTest

Example:
```csharp
public class PaymentWorkflow_TEST_
{
    private static bool ProcessOrder_TEST_()
    {
        // Test order creation, payment processing, and fulfillment
        var order = new Order();
        var payment = new PaymentProcessor();
        var fulfillment = new FulfillmentService();
        
        // Test the full workflow
        LibTest.Asrt(order.Create(123), "Order creation");
        LibTest.Asrt(payment.Process(order), "Payment processing");
        LibTest.Asrt(fulfillment.Ship(order), "Order fulfillment");
        
        return true;
    }
    
    private static bool __TEST__()
    {
        // Run all integration tests for this workflow
        return true;
    }
}
```

This approach keeps integration tests organized while leveraging the same testing infrastructure, making it seamless to test both individual units and their interactions.

## Test Execution

The `LibTest.TestClass()` method automatically:
1. Discovers all methods ending with `_TEST_` in the calling class
2. Discovers all classes ending with `_TEST_` for integration testing
3. Executes tests in declaration order
4. Reports results with clear pass/fail status and error locations

### Running Tests

**IMPORTANT**: Never create separate test files or debug classes. Every C# class contains its own test methods.

To run tests:
```bash
# Run all tests (from csproj directory)
dotnet run -- _TEST_

# Run tests for a specific class
dotnet run -- _TEST_ YourNamespace.YourClassName

# Or directly with the compiled executable
./bin/Debug/net8.0/MyApp.exe _TEST_
./bin/Debug/net8.0/MyApp.exe _TEST_ YourNamespace.YourClassName
```

### No Test File Pollution

This testing philosophy means:
- **NO** separate test projects
- **NO** temporary debug files (DebugTest.cs, QuickTest.cs, etc.)
- **RARE** test-only classes; these are suffixed with "_TEST_" and exist for integration and end-to-end tests only
- Every `.cs` file is self-contained with its own tests (unit tests)
- End-to-end tests can go into a `tests` directory under `src`
- If you need to debug something, add a `_TEST_` method to the relevant class instead of writing a script file

### Exception: Temp Namespace

The `Temp` namespace is reserved for temporary test files:
- Developers can safely delete all files in this namespace at any time
- Use this for quick experiments or debugging specific issues
- Files in this namespace are not part of the production codebase
- These files should not be referenced by any production code
- This namespace should be in the .gitignore file

## Assertion Strategy

This project provides custom assertion methods through the `LibTest` class that mirror the Java testing approach:

### LibTest Assertion Methods

- **`LibTest.Asrt(condition, message)`** - Asserts a boolean condition is true
- **`LibTest.AsrtEq(expected, actual, message)`** - Asserts two values are equal

Example usage:
```csharp
private static bool MyMethod_TEST_()
{
    LibTest.Asrt(1 > 0, "Basic math check");
    LibTest.AsrtEq("hello", "hello", "String equality");
    LibTest.AsrtEq(42, CalculateAnswer(), "Verify calculation");
    return true;
}
```

Benefits of LibTest assertions:
- Consistent API across Java and C# codebases
- Clean "expected vs actual" error messages
- Work within the test return flow (can catch exceptions from expected failures)
- Simpler syntax than Trace.Assert for equality checks

### Alternative: Trace.Assert

For production code (non-test methods), you can also use the standard .NET approach:
- **`Trace.Assert(condition)`** - Throws exception if condition is false
- **`Trace.Assert(condition, message)`** - Includes custom message on failure

Benefits of Trace.Assert:
- Part of the standard .NET framework
- Can be configured via trace listeners
- Works in all build configurations (Debug and Release)

## Testing Philosophy

Focus testing on behaviors that are needed now, not what might be needed in the future:
- Test the happy path - components working correctly with valid inputs
- Avoid testing edge cases that may never occur in practice
- Don't test error handling unless it represents critical business logic
- Time spent testing hypothetical scenarios is better spent improving necessary functionality

## Benefits

- **Proximity**: Tests live next to the code they test
- **Visibility**: Easy to see which methods have test coverage
- **Simplicity**: No external testing framework dependencies
- **Discoverability**: Test methods are automatically found by convention
- **Maintainability**: Tests move with the code during refactoring
- **AI-Friendly**: Test methods serve as usage examples within the same file, enabling AI tools to understand APIs without loading multiple files, making better use of limited context windows