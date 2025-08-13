using System;
using AiGeneratedConnector.Services;

namespace AiGeneratedConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "_TEST_")
            {
                if (args.Length > 1)
                {
                    var className = args[1];
                    var type = Type.GetType(className);
                    if (type != null)
                    {
                        LibTest.TestClass(type);
                    }
                    else
                    {
                        Console.WriteLine($"Class {className} not found");
                    }
                }
                else
                {
                    // Run all tests using automatic discovery
                    Console.WriteLine("Running all tests (auto-discovered)...");
                    Console.WriteLine("=" + new string('=', 60));
                    
                    LibTest.TestAllClasses();
                    
                    Console.WriteLine("=" + new string('=', 60));
                    Console.WriteLine("Test run complete. Check output above for results.");
                }
                return;
            }
            
            Console.WriteLine("iCustomConnector2 Implementation - use '_TEST_' argument to run tests");
        }
    }
}