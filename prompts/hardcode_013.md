# iCustomConnector2 HARDCODE IMPLEMENTATION PROMPT - PHASE 1

Phase 1: Hardcoded/Mock Implementation Only (No External Services)

**IMPORTANT**: Implementing ALL methods from iCustomConnector2 interface for full compatibility

**Prompt 13 of 13**

---

## Method: ✓ DoRealtimeSecurityCheck [Advanced]

---

---
## TASK: Implement Hardcoded DoRealtimeSecurityCheck Method
---

### Objective Implement a hardcoded/mock version of the DoRealtimeSecurityCheck method for the iCustomConnector2 interface.

### Phase 1 Focus This is the hardcode-only phase. We are NOT implementing external services yet.

### Prerequisites
1. Read @README.md to understand the project architecture
2. Review @doc/C-SHARP_CODE_GUIDELINES.md and @doc/C-SHARP_TESTING.md

### Documentation Files
• @doc/per-interface-method/DoRealtimeSecurityCheck.json - Example request/response data (USE THIS AS YOUR TEMPLATE)
• @doc/per-interface-method/DoRealtimeSecurityCheck-request.md - Request structure
• @doc/per-interface-method/DoRealtimeSecurityCheck-response.md - Response structure
• @iCustomConnector2impl/Models/ICustomConnectorInterfaces.cs - Interface definition and types
• @doc/per-interface-method/DoRealtimeSecurityCheck-hardcode.cs - EXISTING HARDCODE IMPLEMENTATION (copy/adapt this!)

### Files to Create in /iCustomConnector2impl/Methods/DoRealtimeSecurityCheck/:
1. Handler.cs - Main method implementation that calls Hardcode and includes unit tests
2. Hardcode.cs - Returns hardcoded/mocked responses based on the .json examples

### Implementation Guidelines
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

### Testing
1. Run: `./scripts/unit_test.py --class AiGeneratedConnector.Methods.DoRealtimeSecurityCheck`
2. Ensure all tests pass
3. Verify the hardcoded responses match the expected structure

### Success Criteria
✓ Method returns valid hardcoded data
✓ Response structure matches the .json examples
✓ All unit tests pass
✓ Code follows the project guidelines
✓ No external service dependencies (this is hardcode-only phase)

### After Implementation
Write a report to `./reports/DoRealtimeSecurityCheck_hardcode.txt` that includes:
1. What the hardest part of this implementation was
2. Any ambiguities or confusions encountered
3. Recommendations for improving documentation and/or project design
4. Specific suggestions that would help future developers

The report should be honest and constructive, focusing on actionable improvements.


---

**END OF PROMPT 013**
