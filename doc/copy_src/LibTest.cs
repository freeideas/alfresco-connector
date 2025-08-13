using System;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace DataConnector.Services
{
    public static class LibTest
    {
        public static void Asrt(bool condition, string message)
        {
            if (!condition)
            {
                var frame = new StackFrame(1);
                var method = frame.GetMethod();
                var fileName = frame.GetFileName();
                var lineNumber = frame.GetFileLineNumber();
                var location = $"{fileName}:{lineNumber} in {method?.DeclaringType?.Name}.{method?.Name}";
                throw new AssertionException($"ASSERTION FAILED: {message} at {location}");
            }
        }

        public static void AsrtEq<T>(T expected, T actual, string message)
        {
            if (!Equals(expected, actual))
            {
                var frame = new StackFrame(1);
                var method = frame.GetMethod();
                var fileName = frame.GetFileName();
                var lineNumber = frame.GetFileLineNumber();
                var location = $"{fileName}:{lineNumber} in {method?.DeclaringType?.Name}.{method?.Name}";
                var details = $"Expected: {expected}, Actual: {actual}";
                throw new AssertionException($"ASSERTION FAILED: {message} - {details} at {location}");
            }
        }

        public static bool TestClass(Type type)
        {
            var testMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                .Where(m => m.Name.EndsWith("_TEST_") && m.ReturnType == typeof(bool) && m.GetParameters().Length == 0)
                .ToList();

            var wholeClassTest = type.GetMethod("__TEST__", BindingFlags.NonPublic | BindingFlags.Static);
            if (wholeClassTest != null) testMethods.Add(wholeClassTest);

            if (testMethods.Count == 0)
            {
                Console.WriteLine($"No tests found in {type.Name}");
                return true;
            }

            int passed = 0;
            int failed = 0;

            foreach (var method in testMethods)
            {
                try
                {
                    var result = (bool)method.Invoke(null, null)!;
                    if (result)
                    {
                        Console.WriteLine($"✓ {type.Name}.{method.Name}");
                        passed++;
                    }
                    else
                    {
                        Console.WriteLine($"✗ {type.Name}.{method.Name} - returned false");
                        failed++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"✗ {type.Name}.{method.Name} - {e.InnerException?.Message ?? e.Message}");
                    failed++;
                }
            }

            Console.WriteLine($"{type.Name}: {passed} passed, {failed} failed");
            return failed == 0;
        }

        public static void TestAllClasses(string? classFilter = null)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes()
                .Where(t => t.IsClass)
                .Where(t => t.GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public)
                    .Any(m => m.Name.EndsWith("_TEST_") || m.Name == "__TEST__"));

            // Apply class filter if provided
            if (!string.IsNullOrEmpty(classFilter))
            {
                types = types.Where(t => t.FullName?.Contains(classFilter, StringComparison.OrdinalIgnoreCase) == true);
                Console.WriteLine($"Filtering tests for classes matching: {classFilter}");
            }

            var typeList = types.ToList();

            if (typeList.Count == 0)
            {
                if (!string.IsNullOrEmpty(classFilter))
                {
                    Console.WriteLine($"No test classes found matching filter: {classFilter}");
                    Console.WriteLine("\nAvailable test classes:");
                    var allTestClasses = assembly.GetTypes()
                        .Where(t => t.IsClass)
                        .Where(t => t.GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public)
                            .Any(m => m.Name.EndsWith("_TEST_") || m.Name == "__TEST__"))
                        .Select(t => t.FullName)
                        .OrderBy(n => n);
                    foreach (var className in allTestClasses)
                    {
                        Console.WriteLine($"  - {className}");
                    }
                }
                else
                {
                    Console.WriteLine("No test classes found in assembly");
                }
                return;
            }

            int totalPassed = 0;
            int totalFailed = 0;

            foreach (var type in typeList)
            {
                var success = TestClass(type);
                if (success) totalPassed++;
                else totalFailed++;
            }

            Console.WriteLine($"Overall: {totalPassed} classes passed, {totalFailed} classes failed");
        }

        private static bool Asrt_TEST_()
        {
            Asrt(true, "This should pass");
            
            try
            {
                Asrt(false, "This should fail");
                return false;
            }
            catch (AssertionException) { return true; }
        }

        private static bool AsrtEq_TEST_()
        {
            AsrtEq(5, 5, "Integers should be equal");
            AsrtEq("hello", "hello", "Strings should be equal");
            
            try
            {
                AsrtEq(5, 10, "This should fail");
                return false;
            }
            catch (AssertionException) { return true; }
        }

        public static bool RunTests() { return TestClass(typeof(LibTest)); }
    }

    public class AssertionException : Exception
    {
        public AssertionException(string message) : base(message) { }
    }
}