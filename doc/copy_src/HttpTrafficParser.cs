using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DataConnector.Services;

/// <summary>
/// A reusable HTTP traffic parser for analyzing captured HTTP/SOAP conversations.
/// Can be used independently in any project that needs to parse HTTP traffic JSON files.
/// </summary>
public static class HttpTrafficParser
{
    /// <summary>
    /// Represents a single traffic entry from captured network data
    /// </summary>
    public class TrafficEntry
    {
        public int id { get; set; }
        public string stamp { get; set; } = "";
        public string direction { get; set; } = ""; // ">" for client-to-server, "<" for server-to-client
        public string data { get; set; } = "";
    }
    
    /// <summary>
    /// Parsed HTTP conversation with separated headers and bodies
    /// </summary>
    public class HttpConversation
    {
        public string? FirstRequestHeaders { get; set; }
        public string? FirstRequestBody { get; set; }
        public string? FirstResponseHeaders { get; set; }
        public string? FirstResponseBody { get; set; }
        
        // Additional useful extracts
        public string? RequestMethod { get; set; }
        public string? RequestPath { get; set; }
        public int? ResponseStatusCode { get; set; }
        public string? ResponseStatusText { get; set; }
    }
    
    /// <summary>
    /// Parse a JSON file containing HTTP traffic captures
    /// </summary>
    public static HttpConversation ParseFile(string jsonPath)
    {
        var jsonContent = File.ReadAllText(jsonPath);
        return ParseJson(jsonContent);
    }
    
    /// <summary>
    /// Parse JSON content containing HTTP traffic captures
    /// </summary>
    public static HttpConversation ParseJson(string jsonContent)
    {
        var entries = JsonSerializer.Deserialize<TrafficEntry[]>(jsonContent);
        
        if (entries == null || entries.Length == 0)
        {
            throw new InvalidOperationException("No traffic entries found in JSON");
        }
        
        return ParseEntries(entries);
    }
    
    /// <summary>
    /// Parse an array of traffic entries into a conversation
    /// </summary>
    public static HttpConversation ParseEntries(TrafficEntry[] entries)
    {
        var result = new HttpConversation();
        
        // Parse request (direction ">")
        ParseRequest(entries, result);
        
        // Parse response (direction "<")
        ParseResponse(entries, result);
        
        return result;
    }
    
    private static void ParseRequest(TrafficEntry[] entries, HttpConversation result)
    {
        var bodyParts = new List<string>();
        bool foundHeaders = false;
        
        foreach (var entry in entries.Where(e => e.direction == ">"))
        {
            if (!foundHeaders && ContainsHttpMethod(entry.data))
            {
                result.FirstRequestHeaders = entry.data;
                foundHeaders = true;
                
                // Extract method and path
                var firstLine = entry.data.Split('\n')[0].Trim();
                var parts = firstLine.Split(' ');
                if (parts.Length >= 2)
                {
                    result.RequestMethod = parts[0];
                    result.RequestPath = parts[1];
                }
                
                // Check if body is in same entry (after double newline)
                var doubleLf = entry.data.IndexOf("\n\n");
                var doubleCrlf = entry.data.IndexOf("\r\n\r\n");
                var bodyStart = Math.Max(doubleLf, doubleCrlf);
                
                if (bodyStart > 0)
                {
                    var bodyPart = entry.data.Substring(bodyStart).Trim();
                    if (!string.IsNullOrWhiteSpace(bodyPart))
                    {
                        bodyParts.Add(bodyPart);
                        break;
                    }
                }
            }
            else if (foundHeaders)
            {
                // This is body or body continuation
                if (IsXmlContent(entry.data) || IsSoapContent(entry.data) || IsJsonContent(entry.data))
                {
                    bodyParts.Add(entry.data.Trim());
                    break; // Usually request body is in one piece
                }
                else if (!string.IsNullOrWhiteSpace(entry.data))
                {
                    // Could be form data or other content
                    bodyParts.Add(entry.data.Trim());
                }
            }
        }
        
        if (bodyParts.Count > 0)
        {
            result.FirstRequestBody = string.Join("", bodyParts);
        }
    }
    
    private static void ParseResponse(TrafficEntry[] entries, HttpConversation result)
    {
        var bodyParts = new List<string>();
        bool foundHeaders = false;
        bool inBody = false;
        
        foreach (var entry in entries.Where(e => e.direction == "<"))
        {
            // Skip 100 Continue responses
            if (entry.data.Contains("HTTP/1.1 100 Continue") || entry.data.Contains("HTTP/1.0 100 Continue"))
            {
                continue;
            }
            
            if (!foundHeaders && entry.data.StartsWith("HTTP/"))
            {
                result.FirstResponseHeaders = entry.data;
                foundHeaders = true;
                
                // Extract status code and text
                var firstLine = entry.data.Split('\n')[0].Trim();
                var match = Regex.Match(firstLine, @"HTTP/[\d\.]+ (\d+)\s*(.*)");
                if (match.Success)
                {
                    result.ResponseStatusCode = int.Parse(match.Groups[1].Value);
                    result.ResponseStatusText = match.Groups[2].Value.Trim();
                }
                
                // Check if body starts in same entry
                var bodyStart = FindBodyStart(entry.data);
                if (bodyStart > 0)
                {
                    var bodyPart = entry.data.Substring(bodyStart);
                    if (!string.IsNullOrWhiteSpace(bodyPart))
                    {
                        bodyParts.Add(bodyPart);
                        inBody = true;
                        
                        // Check if this is complete
                        if (IsCompleteXmlDocument(bodyPart) || IsCompleteSoapEnvelope(bodyPart))
                        {
                            break;
                        }
                    }
                }
            }
            else if (foundHeaders && !inBody)
            {
                // Looking for body start
                if (IsBodyContent(entry.data))
                {
                    bodyParts.Add(entry.data);
                    inBody = true;
                    
                    if (IsCompleteResponse(string.Join("", bodyParts)))
                    {
                        break;
                    }
                }
            }
            else if (inBody)
            {
                // Continuation of body
                // Stop if we see a new HTTP message
                if (entry.data.StartsWith("HTTP/") || ContainsHttpMethod(entry.data))
                {
                    break;
                }
                
                bodyParts.Add(entry.data);
                
                // Check if we have a complete response
                if (IsCompleteResponse(string.Join("", bodyParts)))
                {
                    break;
                }
            }
        }
        
        if (bodyParts.Count > 0)
        {
            result.FirstResponseBody = CleanResponseBody(string.Join("", bodyParts));
        }
    }
    
    private static bool ContainsHttpMethod(string data)
    {
        return data.StartsWith("GET ") || data.StartsWith("POST ") || 
               data.StartsWith("PUT ") || data.StartsWith("DELETE ") ||
               data.StartsWith("HEAD ") || data.StartsWith("OPTIONS ") ||
               data.StartsWith("PATCH ") || data.StartsWith("CONNECT ");
    }
    
    private static bool IsXmlContent(string data)
    {
        return data.TrimStart().StartsWith("<?xml") || data.TrimStart().StartsWith("<");
    }
    
    private static bool IsSoapContent(string data)
    {
        return data.Contains("<soap:") || data.Contains("<s:Envelope") || 
               data.Contains("</soap:") || data.Contains("</s:Envelope");
    }
    
    private static bool IsJsonContent(string data)
    {
        var trimmed = data.TrimStart();
        return trimmed.StartsWith("{") || trimmed.StartsWith("[");
    }
    
    private static bool IsBodyContent(string data)
    {
        return IsXmlContent(data) || IsSoapContent(data) || IsJsonContent(data) || 
               !string.IsNullOrWhiteSpace(data);
    }
    
    private static int FindBodyStart(string data)
    {
        // Look for double newline (end of headers)
        var indices = new[]
        {
            data.IndexOf("\r\n\r\n"),
            data.IndexOf("\n\n")
        };
        
        var minIndex = indices.Where(i => i > 0).DefaultIfEmpty(-1).Min();
        
        if (minIndex > 0)
        {
            // Account for the newlines themselves
            if (data.IndexOf("\r\n\r\n") == minIndex)
                return minIndex + 4;
            else
                return minIndex + 2;
        }
        
        // Sometimes body starts with XML declaration directly
        var xmlIndex = data.IndexOf("<?xml");
        if (xmlIndex > 0)
        {
            return xmlIndex;
        }
        
        return -1;
    }
    
    private static bool IsCompleteXmlDocument(string data)
    {
        return data.Contains("<?xml") && 
               (data.TrimEnd().EndsWith(">") || Regex.IsMatch(data, @"</\w+>\s*$"));
    }
    
    private static bool IsCompleteSoapEnvelope(string data)
    {
        return data.Contains("</soap:Envelope>") || data.Contains("</s:Envelope>");
    }
    
    private static bool IsCompleteResponse(string data)
    {
        return IsCompleteXmlDocument(data) || IsCompleteSoapEnvelope(data) ||
               (IsJsonContent(data) && IsCompleteJson(data));
    }
    
    private static bool IsCompleteJson(string data)
    {
        try
        {
            using var doc = JsonDocument.Parse(data);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    private static string CleanResponseBody(string data)
    {
        // Remove HTTP chunking artifacts
        var lines = data.Split('\n');
        var cleanedLines = new List<string>();
        bool inContent = false;
        
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            
            // Skip chunk size indicators (hex numbers)
            if (!inContent && trimmed.Length > 0 && trimmed.Length <= 8 && 
                Regex.IsMatch(trimmed, @"^[0-9a-fA-F]+$"))
            {
                continue;
            }
            
            // Mark start of content
            if (IsXmlContent(line) || IsSoapContent(line) || IsJsonContent(line))
            {
                inContent = true;
            }
            
            if (inContent || !string.IsNullOrWhiteSpace(line))
            {
                cleanedLines.Add(line);
            }
        }
        
        var result = string.Join("\n", cleanedLines).Trim();
        
        // Final cleanup - remove any remaining chunk markers
        result = Regex.Replace(result, @"^\s*[0-9a-fA-F]+\s*$", "", RegexOptions.Multiline);
        
        return result.Trim();
    }
    
    // Helper method to validate SOAP envelope structure
    private static void ValidateSoapEnvelope(string body, string context)
    {
        LibTest.Asrt(!string.IsNullOrEmpty(body), 
            $"{context}: Body must not be null or empty");
        
        var bodyLower = body.ToLower();
        
        // Check for opening envelope
        LibTest.Asrt(bodyLower.Contains("<soap:envelope") || 
                     bodyLower.Contains("<s:envelope") ||
                     bodyLower.Contains("<soapenv:envelope"),
            $"{context}: Must have opening SOAP envelope tag");
        
        // Check for closing envelope
        LibTest.Asrt(bodyLower.Contains("</soap:envelope>") || 
                     bodyLower.Contains("</s:envelope>") ||
                     bodyLower.Contains("</soapenv:envelope>"),
            $"{context}: Must have closing SOAP envelope tag");
        
        // Check for body element
        LibTest.Asrt(bodyLower.Contains("<soap:body") || 
                     bodyLower.Contains("<s:body") ||
                     bodyLower.Contains("<soapenv:body"),
            $"{context}: Must have opening SOAP body tag");
        
        LibTest.Asrt(bodyLower.Contains("</soap:body>") || 
                     bodyLower.Contains("</s:body>") ||
                     bodyLower.Contains("</soapenv:body>"),
            $"{context}: Must have closing SOAP body tag");
    }
    
    // Helper method to validate SOAP response content
    private static void ValidateSoapResponseContent(string body, string context)
    {
        var bodyLower = body.ToLower();
        
        // Must have either a response/result or a fault
        var hasProperResponse = bodyLower.Contains("response>") || 
                              bodyLower.Contains("result>") ||
                              bodyLower.Contains("return>");
        var hasFault = bodyLower.Contains("<soap:fault") || 
                      bodyLower.Contains("<s:fault");
        
        LibTest.Asrt(hasProperResponse || hasFault,
            $"{context}: Must contain either a response element or a SOAP fault");
    }
    
    // Comprehensive unit tests
    private static bool ParseFile_TEST_()
    {
        var testFiles = new[]
        {
            // fileName, requireRequestHeaders, requireRequestBody, requireResponseHeaders, requireResponseBody
            ("DoDescribe", true, true, true, true),
            ("DoGetWebServices", true, true, true, true),
            ("DoGetAvailableDatastores", true, true, true, true),
            ("DoGetDatastoreTypes", true, true, true, true),
            ("DoGetServers", true, true, true, true),
            ("DoGetUsers", true, true, true, true),
            ("DoGetGroups", true, true, true, true),
            ("DoGetGroupsUsers", true, true, true, true),
            ("DoGetGroupsGroups", true, true, true, true),
            ("DoCrawl", true, true, true, true),
            ("DoGetChanges", true, true, true, true),
            ("DoItemData", true, true, true, true),
            ("DoRealtimeSecurityCheck", true, true, true, true),
            ("GetInterfaceVersion", true, true, true, true) // Treat the same as all others
        };
        
        Console.WriteLine("Testing HttpTrafficParser with all JSON files:");
        
        foreach (var (fileName, reqHeaders, reqBody, respHeaders, respBody) in testFiles)
        {
            var jsonPath = Path.Combine("..", "doc", "per-interface-method", $"{fileName}.json");
            
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"  [{fileName}] ERROR - Required file not found: {jsonPath}");
                return false; // FAIL if any required file is missing
            }
            
            Console.WriteLine($"  [{fileName}] Testing...");
            
            try
            {
                var parsed = ParseFile(jsonPath);
                
                // STRICT: Check all required components exist
                if (reqHeaders)
                {
                    LibTest.Asrt(parsed.FirstRequestHeaders != null,
                        $"{fileName}: MUST have request headers");
                    LibTest.Asrt(parsed.FirstRequestHeaders!.Contains("POST") || 
                                 parsed.FirstRequestHeaders.Contains("GET"),
                        $"{fileName}: Request headers must contain HTTP method");
                    Console.WriteLine($"    ✓ Request headers: {parsed.RequestMethod} {parsed.RequestPath}");
                }
                
                if (reqBody)
                {
                    LibTest.Asrt(parsed.FirstRequestBody != null,
                        $"{fileName}: MUST have request body");
                    
                    // Use helper method to validate SOAP structure
                    ValidateSoapEnvelope(parsed.FirstRequestBody!, $"{fileName} request");
                    
                    Console.WriteLine($"    ✓ Request body: {parsed.FirstRequestBody.Length} chars (valid SOAP envelope)");
                }
                
                if (respHeaders)
                {
                    LibTest.Asrt(parsed.FirstResponseHeaders != null,
                        $"{fileName}: MUST have response headers");
                    LibTest.Asrt(parsed.ResponseStatusCode != null,
                        $"{fileName}: Must extract status code");
                    Console.WriteLine($"    ✓ Response headers: HTTP {parsed.ResponseStatusCode} {parsed.ResponseStatusText}");
                }
                
                if (respBody)
                {
                    LibTest.Asrt(parsed.FirstResponseBody != null,
                        $"{fileName}: MUST have response body");
                    
                    // Use helper methods to validate SOAP structure and content
                    ValidateSoapEnvelope(parsed.FirstResponseBody!, $"{fileName} response");
                    ValidateSoapResponseContent(parsed.FirstResponseBody!, $"{fileName} response");
                    
                    Console.WriteLine($"    ✓ Response body: {parsed.FirstResponseBody.Length:N0} chars (valid SOAP with proper structure)");
                }
                
                Console.WriteLine($"    ✓ PASS - All required components found");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    ✗ FAIL: {ex.Message}");
                return false;
            }
        }
        
        Console.WriteLine("All files parsed successfully with required components!");
        return true;
    }
    
    public static bool RunTests()
    {
        return LibTest.TestClass(typeof(HttpTrafficParser));
    }
}