# XML Documentation Improvement Guide

This document describes the standards and patterns for improving XML documentation in the Vonage .NET SDK. The goal is to make the SDK more developer-friendly and AI-friendly, reducing reliance on external documentation.

## Objectives

1. **Developer Experience**: Enable IntelliSense to provide meaningful context without leaving the IDE
2. **AI/LLM Compatibility**: Provide sufficient context for AI tools to understand and use the SDK correctly
3. **Consistency**: Maintain uniform documentation style across all SDK components
4. **Accuracy**: Ensure documentation matches the OpenAPI Specification (OAS)

## Reference Materials

- **OpenAPI Specification**: Located at root level (e.g., `messages.yml`) - use this as the source of truth for property descriptions, constraints, and formats
- **Code Snippets Repository**: https://github.com/Vonage/vonage-dotnet-code-snippets - reference for code examples

---

## Documentation Patterns

### 1. Class Summaries

Every public class must have a meaningful summary. Never leave empty `/// <summary></summary>` tags.

**Pattern**: `[What it is] + [When to use it / Key context]`

```csharp
// BAD - Empty summary
/// <summary>
/// </summary>
public class MessengerTextRequest : MessageRequestBase

// GOOD - Descriptive summary
/// <summary>
///     Represents a text message request to be sent via Facebook Messenger.
/// </summary>
public class MessengerTextRequest : MessageRequestBase
```

**For base classes**, explain what they provide:

```csharp
/// <summary>
///     Base class for all WhatsApp message requests.
/// </summary>
public abstract class WhatsAppMessageBase : MessageRequestBase
```

**For client classes**, describe capabilities:

```csharp
/// <summary>
///     Client for sending messages across multiple channels (SMS, MMS, WhatsApp, Messenger, Viber, RCS).
///     Implements the Vonage Messages API v1.
/// </summary>
public class MessagesClient : IMessagesClient
```

### 2. Interface Summaries

Describe the contract and what implementations provide:

```csharp
/// <summary>
///     Exposes methods for sending messages across multiple channels (SMS, MMS, WhatsApp, Messenger, Viber, RCS).
/// </summary>
public interface IMessagesClient
```

### 3. Property Documentation

**Simple properties** - concise description:

```csharp
/// <summary>
///     The text content of the message.
/// </summary>
public string Text { get; set; }
```

**Properties with constraints** - include limits, formats, or supported values:

```csharp
/// <summary>
///     The text of message to send; limited to 1000 characters. The Messages API automatically
///     detects unicode characters and encodes accordingly.
/// </summary>
public string Text { get; set; }
```

**Attachment properties** - mention supported formats when known:

```csharp
/// <summary>
///     The image attachment. Supported formats: .jpg, .jpeg, .png.
/// </summary>
public CaptionedAttachment Image { get; set; }
```

**Channel-specific configuration** - explain what it configures:

```csharp
/// <summary>
///     Messenger-specific settings including message category and tag.
/// </summary>
[JsonPropertyName("messenger")]
public MessengerRequestData Data { get; set; }
```

### 4. Enum Documentation

**Enum type** - describe what it defines:

```csharp
/// <summary>
///     Defines the messaging channel to use for sending a message.
/// </summary>
public enum MessagesChannel
```

**Enum values** - explain what each value means or when to use it:

```csharp
/// <summary>
///     Response to a user-initiated conversation. Must be sent within 24 hours of the user's message.
/// </summary>
[Description("response")] Response = 0,

/// <summary>
///     Proactive message sent outside the 24-hour window. Requires special permissions.
/// </summary>
[Description("update")] Update = 1,
```

### 5. Record Documentation

Use `<param>` tags for record parameters:

```csharp
/// <summary>
///     Represents the response from sending a message through the Messages API.
/// </summary>
/// <param name="MessageUuid">The unique identifier for the message. Use this to track delivery status via webhooks.</param>
/// <param name="WorkflowId">The ID of the failover workflow. Only present if the request included failover messages.</param>
public record MessagesResponse(Guid MessageUuid, string WorkflowId = null);
```

### 6. Method Documentation with Examples

For key public methods, include:
- Summary of what it does
- Parameter descriptions with `<see cref=""/>` links to related types
- Return value description
- Code example using CDATA for proper formatting
- Link to snippets repository

```csharp
/// <summary>
///     Sends a message through the specified channel.
/// </summary>
/// <param name="message">The message to send. Can be any implementation of <see cref="IMessage"/> such as <see cref="Sms.SmsRequest"/>, <see cref="WhatsApp.WhatsAppTextRequest"/>, etc.</param>
/// <returns>A response containing the message UUID for tracking delivery status.</returns>
/// <example>
/// <code><![CDATA[
/// var message = new SmsRequest { To = "447700900000", From = "Vonage", Text = "Hello!" };
/// var response = await client.SendAsync(message);
/// ]]></code>
/// </example>
/// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/main/SnippetSamples/Messages">More examples in the snippets repository</seealso>
Task<MessagesResponse> SendAsync(IMessage message);
```

### 7. Using `<inheritdoc />`

Use `<inheritdoc />` for overridden members where the base/interface documentation is sufficient:

```csharp
/// <inheritdoc />
public override MessagesChannel Channel => MessagesChannel.WhatsApp;
```

**Do not use** `<inheritdoc />` at the class level if the base class has poor documentation - write a proper summary instead.

---

## Common Issues to Fix

### 1. Empty Summaries
Find and replace all empty `/// <summary></summary>` or `/// <summary>\n/// </summary>` patterns.

### 2. Incorrect Channel Names
Watch for copy-paste errors where the wrong channel is mentioned (e.g., "Viber" in a WhatsApp class).

### 3. Vague Property Descriptions
Replace generic descriptions with specific ones:
- "The file information" → "The image attachment. Supported formats: .jpg, .jpeg, .png"
- "The data" → "Messenger-specific settings including message category and tag"

### 4. Missing Enum Value Descriptions
Every enum value should explain its purpose, not just repeat the name.

---

## Checklist for Documentation Updates

When documenting a new folder/feature:

- [ ] Read the relevant OAS file for accurate property descriptions and constraints
- [ ] Add class-level summaries to all public classes
- [ ] Document all public properties (check OAS for constraints like character limits)
- [ ] Document all enum types and their values
- [ ] Add `<param>` tags to all record types
- [ ] Add code examples to main client methods
- [ ] Add `<seealso>` links to snippets repository for client methods
- [ ] Use `<inheritdoc />` appropriately for overridden members
- [ ] Verify no empty summary tags remain
- [ ] Check for copy-paste errors (wrong channel names, etc.)

---

## Files Structure Reference (Messages Example)

```
Messages/
├── Root level (interfaces, base classes, enums, response types)
│   ├── IMessagesClient.cs      - Main client interface (add examples here)
│   ├── MessagesClient.cs       - Client implementation
│   ├── IMessage.cs             - Core message interface
│   ├── MessageRequestBase.cs   - Base class for all requests
│   ├── MessagesChannel.cs      - Channel enum
│   ├── MessagesMessageType.cs  - Message type enum
│   └── MessagesResponse.cs     - API response record
├── Channel folders (Sms/, Mms/, WhatsApp/, Messenger/, Viber/, Rcs/)
│   ├── *MessageBase.cs         - Channel-specific base class
│   ├── *TextRequest.cs         - Text message request
│   ├── *ImageRequest.cs        - Image message request
│   └── ...                     - Other message types
└── Webhooks/
    ├── MessageWebhookResponse.cs   - Inbound message webhook
    └── MessageStatusResponse.cs    - Status webhook
```

---

## Quality Metrics

After completing documentation for a folder, verify:

1. **No empty summaries**: `grep -r "/// <summary>\s*$" --include="*.cs"` should return nothing
2. **All public types documented**: Every public class, interface, enum, and record has a summary
3. **Constraints included**: Character limits, format requirements, and supported file types are documented where applicable
4. **Examples present**: Main client methods have code examples

---

## Version History

| Date | Folder | Changes |
|------|--------|---------|
| 2024-XX-XX | /Messages | Initial documentation pass - 60+ files updated |
