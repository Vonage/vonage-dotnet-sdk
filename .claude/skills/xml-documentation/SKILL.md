---
name: xml-documentation
description: >
  Improve XML documentation in the Vonage .NET SDK. Use when the user mentions
  "improve XML docs", "document [namespace]", "XML documentation", "add missing
  docs", or references documenting C# classes, properties, enums, or interfaces
  in this SDK. Covers IntelliSense summaries, OAS-based constraints, code
  examples, and builder struct documentation.
---

# XML Documentation Improvement Skill

The goal is to make the Vonage .NET SDK **self-explanatory**. Customers should
be able to understand and use the SDK entirely from IntelliSense, without
ping-ponging between their code and the developer portal.

This means every public method needs a clear explanation, every parameter and
property needs its data described (constraints, formats, supported values),
every key method needs a code example, and client methods need a link to the
snippets repository. The OAS file is the source of truth for property details
because it captures constraints that aren't visible from the code alone.

Private and internal methods are out of scope — they aren't part of the
customer-facing surface. The focus is entirely on what developers see in
IntelliSense.

---

## Workflow

### CRITICAL: Do NOT modify any files until steps 1-3 are complete.

### 1. Ask for the OAS file path — STOP and wait for the answer

You MUST ask the user: "What is the path to the OAS file for this product?"

Do NOT guess. Do NOT proceed. Do NOT read any `.cs` files yet. Wait for the
user to respond with the path.

### 2. Read the OAS file

Once you have the path, read it. Understand the schemas, required fields,
character limits, supported formats, and enum values. This is your source of
truth for property descriptions and constraints.

### 3. Ask for the code snippets link — STOP and wait for the answer

You MUST ask the user: "Is this product exposed on the code snippets
repository? If yes, give me the product folder URL. If not, give me the repo
root URL."

Do NOT proceed until you have the URL. You will use it for `<seealso>` links
on client methods.

### 4. Scan the target folder

Now scan the folder to inventory all `.cs` files and understand the class
hierarchy (base classes, interfaces, request types, enums, records).

### 5. Process all files in one pass

Process every public `.cs` file in the target folder in a single pass. Follow
the priority order below so that base types are documented before their
dependents:

1. Interfaces
2. Enums
3. Base/abstract classes
4. Records and response types
5. Concrete request classes
6. Client classes (these get code examples)
7. Webhook types

### 6. Verify

After processing, run this grep to confirm no empty summaries remain:

```bash
grep -rn "/// <summary>\s*$" --include="*.cs" <target-folder>
```

Report the results to the user.

---

## Scope

- **Document**: all public classes, interfaces, enums, records, properties,
  and methods.
- **Leave alone**: private methods, internal helpers, `[ValidationRule]`
  methods, and private implementation details like `GetRequestContent()` or
  `BuildInsights()`. For `BuildRequestMessage()` that implements an interface,
  use `<inheritdoc />`.

---

## Documentation Patterns

Apply these patterns to every file you touch.

### Class summaries

Pattern: **[What it is] + [When to use it / Key context]**

```csharp
/// <summary>
///     Represents a text message request to be sent via Facebook Messenger.
/// </summary>
```

- Never leave empty `/// <summary></summary>` tags.
- For base classes, explain what they provide.
- For client classes, describe capabilities and mention the API version.

### Interface summaries

Describe the contract and what implementations provide:

```csharp
/// <summary>
///     Exposes methods for sending messages across multiple channels (SMS, MMS, WhatsApp, Messenger, Viber, RCS).
/// </summary>
```

### Property documentation

- **Simple properties** — concise one-liner.
- **Properties with constraints** — include limits, formats, or supported
  values from the OAS.
- **Attachment properties** — mention supported file formats.
- **Channel-specific configuration** — explain what it configures.

```csharp
/// <summary>
///     The text of message to send; limited to 1000 characters. The Messages API automatically
///     detects unicode characters and encodes accordingly.
/// </summary>
```

If a property has **no OAS match and no obvious meaning**, write a best-guess
summary and mark it with a TODO:

```csharp
/// <summary>
///     The auxiliary data associated with the request.
///     TODO: No OAS match found — verify this description.
/// </summary>
```

### Enum documentation

- **Enum type** — describe what it defines.
- **Every enum value** — explain its purpose or when to use it. Never just
  repeat the name.

```csharp
/// <summary>
///     Response to a user-initiated conversation. Must be sent within 24 hours of the user's message.
/// </summary>
[Description("response")] Response = 0,
```

### Record documentation

Use `<param>` tags for record parameters:

```csharp
/// <summary>
///     Represents the response from sending a message through the Messages API.
/// </summary>
/// <param name="MessageUuid">The unique identifier for the message. Use this to track delivery status via webhooks.</param>
```

### Method documentation with examples

For key public methods on client classes, include:

- Summary of what it does.
- Parameter descriptions with `<see cref=""/>` links to related types.
- Return value description.
- Code example using `<![CDATA[...]]>` for proper formatting.
- `<seealso>` link using the URL the user provided.

```csharp
/// <summary>
///     Sends a message through the specified channel.
/// </summary>
/// <param name="message">The message to send. Can be any implementation of <see cref="IMessage"/> such as <see cref="Sms.SmsRequest"/>.</param>
/// <returns>A response containing the message UUID for tracking delivery status.</returns>
/// <example>
/// <code><![CDATA[
/// var message = new SmsRequest { To = "447700900000", From = "Vonage", Text = "Hello!" };
/// var response = await client.SendAsync(message);
/// ]]></code>
/// </example>
/// <seealso href="URL_FROM_USER">More examples in the snippets repository</seealso>
```

### Builder struct documentation (`[Builder]` attribute)

The codebase uses a **custom source generator** for builder patterns. When a
struct is decorated with `[Builder]`, the generator reads each property's
builder attribute and XML docs, then emits builder interfaces and an internal
builder struct. **The XML docs you write on the property are copied verbatim
onto the generated builder method** — on both the interface and the builder
struct. This means docs must read as method descriptions, not property
descriptions.

**Only document public members.** Private methods, internal helpers,
`[ValidationRule]` methods, and `BuildRequestMessage()` implementations should
be left alone (use `<inheritdoc />` for `BuildRequestMessage()` if the
interface already documents it).

#### Builder attributes and generated method names

| Attribute | Generated method name | Signature |
|---|---|---|
| `[Mandatory(order)]` | `With` + PropertyName | `WithId(int value)` |
| `[MandatoryWithParsing(order, parserName)]` | `With` + PropertyName | `WithPhoneNumber(string value)` (takes string, parser converts) |
| `[Optional]` | `With` + PropertyName | `WithName(string value)` |
| `[OptionalWithParsing]` | `With` + PropertyName | Takes string, parser converts |
| `[OptionalWithDefault]` | `With` + PropertyName | Has a default value |
| `[OptionalBoolean(default, "MethodName")]` | The explicit name from the attribute | `EnableVerbose()` or `Hide()` (parameterless toggle) |

The naming rule: all attributes generate `With` + PropertyName **except**
`[OptionalBoolean]`, which uses the explicit method name provided in the
attribute (second argument). This name can reverse the boolean meaning
(e.g., property `Verbose` → method `EnableVerbose`, property `Hidden` →
method `Hide`).

#### Documentation rules for builder properties

1. **Write summaries as builder method descriptions** — use "Sets the...",
   "Includes...", or "Enables..." phrasing depending on the method semantics.
2. **Every property gets a focused `<example>`** — regardless of attribute type.
   Show only that single method call, never the full builder chain.
3. **The example must match the generated method name** — for `[OptionalBoolean]`,
   use the explicit name from the attribute, not `With` + PropertyName.

#### Examples by attribute type

**`[Mandatory]`** — simple value, example shows `.WithPropertyName(value)`:

```csharp
/// <summary>
///     Sets the unique identifier for the request.
/// </summary>
/// <example>
/// <code><![CDATA[
/// .WithId(42)
/// ]]></code>
/// </example>
[Mandatory(0)]
public int Id { get; internal init; }
```

**`[MandatoryWithParsing]`** — takes a string, parser converts it:

```csharp
/// <summary>
///     Sets the phone number to retrieve insights for. The number should follow E.164 format.
/// </summary>
/// <example>
/// <code><![CDATA[
/// .WithPhoneNumber("+14155552671")
/// ]]></code>
/// </example>
[MandatoryWithParsing(0, nameof(ParsePhoneNumber))]
public PhoneNumber PhoneNumber { get; internal init; }
```

**`[Optional]`** — wraps value in `Maybe<T>`:

```csharp
/// <summary>
///     Sets the name associated with the request.
/// </summary>
/// <example>
/// <code><![CDATA[
/// .WithName("John Doe")
/// ]]></code>
/// </example>
[Optional]
public Maybe<string> Name { get; internal init; }
```

**`[OptionalBoolean]`** — parameterless toggle, uses the explicit method name:

```csharp
/// <summary>
///     Enables verbose output for debugging purposes.
/// </summary>
/// <example>
/// <code><![CDATA[
/// .EnableVerbose()
/// ]]></code>
/// </example>
[OptionalBoolean(false, "EnableVerbose")]
public bool Verbose { get; internal init; }
```

### Using `<inheritdoc />`

Use `<inheritdoc />` for overridden members where the base/interface
documentation is already good. Do **not** use it at the class level if the base
class has poor documentation — write a proper summary instead.

---

## Common Mistakes to Watch For

- **Empty summaries** — find and fill every one.
- **Copy-paste channel errors** — e.g., "Viber" mentioned in a WhatsApp class.
  Cross-check the class name, namespace, and file path.
- **Vague descriptions** — replace "The file information" with specifics like
  "The image attachment. Supported formats: .jpg, .jpeg, .png".
- **Missing enum value descriptions** — every value needs a meaningful
  explanation.
- **Overly broad builder examples** — each example must show only the single
  method it documents.
