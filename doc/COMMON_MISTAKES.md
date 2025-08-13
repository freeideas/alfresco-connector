# Common SOAP Server Implementation Mistakes

## 1. GetInterfaceVersion Must Return 500 Error (WITHOUT Implementation)

**Problem**: Legacy clients require GetInterfaceVersion to return a specific 500 error, but developers often misunderstand this as needing to implement a method that returns an error.

**Symptoms**: Client disconnects if server returns success OR wrong error format.

**The Correct Approach**: 
- DO NOT implement GetInterfaceVersion as a method in your code
- ASP.NET Core automatically returns 500 for unrecognized operations
- GetInterfaceVersion should be "unrecognized" - that's the whole point
- The framework's automatic 500 response is exactly what the client expects

**Testing Requirements**:
- MUST test that GetInterfaceVersion returns the correct 500 error
- Compare response with `doc/per-interface-method/GetInterfaceVersion.json`
- This tests that your server properly handles unimplemented methods

## 2. Not Testing Expected Errors

**Problem**: Developers skip testing that unimplemented methods return proper errors.

**Example**: GetInterfaceVersion MUST return a 500 error when called - this needs testing even though it's not implemented.

**Fix**:
- Test that GetInterfaceVersion returns the expected 500 error
- Verify the complete SOAP fault format matches the captured response
- This validates your server's handling of unrecognized operations
- Include this test in your end-to-end test suite

## 3. HTTP 100-Continue Protocol

**What It Is**: Protocol handshake required by legacy clients:
1. Client sends headers with `Expect: 100-continue`
2. Server responds `HTTP/1.1 100 Continue`
3. Client sends body
4. Server sends final response

**Common Mistakes**:
- Server waits for body before sending 100 Continue (wrong - client won't send body until it gets 100 Continue)
- Server closes connection after 100 Continue
- Tests use HTTP clients that handle this automatically (must test with raw sockets)

**Testing**: Use raw TCP socket to verify exact protocol flow. Create a test script in `scripts/test_protocol.py` if needed.
