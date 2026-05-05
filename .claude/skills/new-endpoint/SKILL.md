---
name: new-endpoint
description: >
  Add a new endpoint, use case, or product to the Vonage .NET SDK. Use this
  skill when the user asks to "implement an endpoint", "add a new API", "add a
  new use case", "scaffold [Action] for [Product]", "add [Product] to the SDK",
  or anything that extends the public surface of the SDK with a new HTTP call.
  Covers screaming architecture layout, TDD with the four-file test structure
  (E2E / Request / RequestBuilder / Serialization), monadic Result/Maybe flow,
  source-generated builders with `[Builder]`/`[Mandatory]`/`[Optional]`, input
  validation, and the XML documentation bar.
---

# New Endpoint Skill

The goal is to make every new endpoint feel native to the SDK: same folder
structure, same monadic flow, same validation story, same test layout, same
documentation bar. A customer should never be able to tell which engineer wrote
a use case.

The SDK follows **screaming architecture**: products are modules, and every
module is a collection of use cases. A single use case owns a folder and
contains one or two request types, an optional builder, and an optional
response. Tests mirror this layout exactly.

---

## Before you touch any code

### 1. STOP and gather inputs

Ask the user, then wait for answers:

1. **What is the product?** (existing module like `VerifyV2`, `Conversations`,
   or a new module.)
2. **What is the use case?** (e.g., `CreateTemplate`, `GetBroadcasts`.) Use an
   imperative verb + subject — matches folder/class naming.
3. **Is there an OpenAPI spec?** If yes, ask for the path. Use it as the source
   of truth for: endpoint path, HTTP method, required vs optional fields,
   character limits, enum values, formats, and response shape.
4. **Code-snippets URL.** The canonical pattern is
   `https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/<Product>`.
   Assume this URL using the product name and **ask the user to confirm it's
   correct** before committing — the snippets repo isn't always in sync with
   the SDK, so don't add a `<seealso>` to a URL you haven't verified.
5. **Authentication**: which header does this API use? Basic / Bearer (JWT) /
   OIDC-bearer / custom. Determines which `VonageHttpClientConfiguration` to
   wire up in `VonageClient.PropagateCredentials()`.
6. **Base URL / region**: Nexmo API, Video API, OIDC API, or a regional variant
   (`EMEA` / `APAC` / `AMER`)?

Do **not** start coding until you have at least answers 1, 2, 4, 5, 6. Guessing
the endpoint path or auth type means rewriting everything later.

### 2. Locate the closest blueprint

Pick the existing use case that most resembles what you're about to build and
read it end-to-end before writing a line. Blueprints by shape:

| Shape | Blueprint |
|---|---|
| Mandatory-only body (POST/PUT) | `Vonage/VerifyV2/CreateTemplate/` |
| Mandatory + Optional mix (POST) | `Vonage/VerifyV2/CreateTemplateFragment/` |
| Path-only DELETE with Guid (returns body) | `Vonage/Reports/CancelReport/` |
| Path-only DELETE returning Unit | `Vonage/VerifyV2/Cancel/` |
| GET with query-string pagination | `Vonage/VerifyV2/GetTemplates/` |
| GET with cursor/date filters | `Vonage/Conversations/GetConversations/` |
| Phone-number parsing + scope | `Vonage/SimSwap/Check/` |
| Mixed body with nested dictionaries | `Vonage/IdentityInsights/GetInsights/` |
| Complex hand-rolled builder (no `[Builder]`) | `Vonage/Conversations/CreateConversation/` |
| Multi-step flow (auth → call) | `Vonage/SimSwap/` |

Matching a blueprint is not optional — it keeps the SDK homogeneous.

---

## Folder layout (screaming architecture)

For a new use case `Xxx` under product `Product`:

```
Vonage/
  Product/
    IProductClient.cs               # interface (public)
    ProductClient.cs                # implementation (internal)
    XxxResponse.cs                  # ONLY if shared across use cases (e.g. GetXxx + CancelXxx return same type)
    Xxx/
      XxxRequest.cs                 # readonly partial struct, [Builder] if applicable
      XxxRequestBuilder.cs          # ONLY if NOT using [Builder] generator
      XxxResponse.cs                # record or readonly struct; omit for Unit responses or if shared at product level
Vonage.Test/
  Product/
    E2EBase.cs                      # shared helper per product
    XxxResponseSerializationTest.cs # ONLY if response type is shared across use cases
    Data/
      ShouldDeserialize200-response.json  # ONLY if serialization test lives at product level
    Xxx/
      E2ETest.cs
      RequestTest.cs
      RequestBuilderTest.cs
      SerializationTest.cs          # ONLY for use-case-specific request/response shapes
      Data/
        ShouldSerialize-request.json
        ShouldDeserialize200-response.json
        (more as needed)
```

**Never** put two use cases in the same folder, and **never** flatten the
structure even for trivial endpoints — screaming architecture is the rule.

**Group related support types into subfolders.** When a product namespace
accumulates many related types that are not themselves use cases — capability
records, webhook types, discriminated-union members, configuration POCOs — move
them into a named subfolder with a matching sub-namespace (e.g.
`Vonage/Product/Capabilities/` with namespace `Vonage.Product.Capabilities`).
The threshold is roughly 5+ cohesive types; a flat directory of 20+ files with
no grouping is a smell. The sub-namespace must be added as a `using` wherever
those types are referenced.

**When a response type is shared** across multiple use cases (e.g. `GET /reports/{id}` and
`DELETE /reports/{id}` both return the same shape), place the response record at the
product level (`Vonage/Product/XxxResponse.cs`) and its serialization test at
`Vonage.Test/Product/XxxResponseSerializationTest.cs` with the fixture in
`Vonage.Test/Product/Data/`. Do **not** duplicate the serialization test inside each
use case folder — that's dead weight. The `E2EBase` for that product can then initialise
its `SerializationTestHelper` from the product-level namespace so E2E tests reference
the shared fixture via `nameof(XxxResponseSerializationTest.ShouldDeserialize200)`.

---

## Outside-in TDD order

Write the tests first, in this order. Each step's test drives the next layer
down.

### Step 1 — `SerializationTest.cs` and fixtures

This is the contract with the wire. Write it first because both the request
shape and the response type are implied by the JSON fixtures.

1. Create `Data/ShouldSerialize-request.json` from the OAS example (or the
   API docs). Copy the JSON verbatim; include exactly the keys the builder is
   expected to emit.
2. Create `Data/ShouldDeserialize200-response.json` from the OAS example.
3. Write `ShouldSerialize` — builds the request with the builder, calls
   `GetStringContent()`, compares to `this.helper.GetRequestJson()`.
4. Write `ShouldDeserialize200` — deserializes the fixture into the response
   type and asserts key fields.

Pattern:

```csharp
[Trait("Category", "Serialization")]
[Trait("Product", "Product")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase()); // or BuildWithCamelCase() — match the API

    [Fact]
    public void ShouldSerialize() =>
        XxxRequest.Build()
            .WithFoo("bar")
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldDeserialize200() => this.helper.Serializer
        .DeserializeObject<XxxResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyExpectedResponse);

    private static void VerifyExpectedResponse(XxxResponse response) { /* field asserts */ }
}
```

Rules:
- One request fixture per scenario you intend to serialize (`ShouldSerialize`,
  `ShouldSerializeWithRequiredFieldsOnly` …). The filename must match
  the test name: `<testName>-request.json` or `<testName>-response.json`.
- **`ShouldSerialize` must set every field** (all mandatory + all optional) and the
  fixture must contain all of them. A partial fixture is a lie — it hides wiring bugs
  where optional fields are silently omitted from the body. If there are many optional
  fields, also add `ShouldSerializeWithRequiredFieldsOnly` to test the minimal body.
  Expose `BuildRequest()` and `BuildRequestWithRequiredFieldsOnly()` as
  `internal static` on `RequestBuilderTest` so the serialization test and E2E test
  can reuse the same builder call without duplication.
- Fixtures named after the test allow `E2ETest` to re-use them via
  `this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize))`.
- If multiple E2E tests build the same request, expose a helper returning
  `Result<XxxRequest>` marked `internal static` on `SerializationTest` —
  `CreateConversation/SerializationTest.cs` does this with `BuildRequest()`.
- For request-only endpoints (e.g. DELETE), only the deserialize-response test
  is needed if there's a response body; otherwise skip.
- **Every variant of a polymorphic or union-like field needs its own serialization
  test.** When a request or response contains a field that can hold different sub-types
  (e.g. a capabilities object whose properties are each a different type, a discriminated
  union, or a collection of independent optional sub-objects), add one dedicated
  `[Fact]` per variant — not just one test that happens to combine two of them. One
  combined fixture that exercises Voice + Messages does not prove that RTC, Verify,
  Video, Meetings, NetworkApis, or VBC serialize correctly. The name pattern is
  `ShouldSerializeWith<Variant>` and each has its own matching fixture file in
  `Data/`. No variant may be left untested — a missing variant means zero guarantee
  it round-trips through the serializer.

### Step 2 — `RequestBuilderTest.cs`

Drive out the builder. One test per rule, success and failure paths. Use
`[Theory]` / `[InlineData]` for repeated value matrices.

Mandatory coverage — **no exceptions**:

- **Happy path** for every setter: `Create_ShouldSet<Property>`.
- **Defaults** for every optional property: `Create_ShouldHaveNo<Property>_GivenDefault`
  (asserts `Maybe.None`) or `Create_ShouldSet<Property>ToDefault_...` (for
  `[OptionalWithDefault]`). Every `[Optional]` field needs both its default test AND
  its setter test. Skipping "less interesting" optional fields is a TDD violation.
- **Every validation rule**, asserting the exact failure message with
  `BeParsingFailure("<message>.")`.
- When many optional fields create repetition, extract a `BuildBase()` private static
  helper that returns `IBuilderForOptional` with mandatory fields pre-filled, so each
  test is one fluent chain.
- **No comments of any kind** in test files. This includes section markers like
  `// DateStart`, `// Optional fields`, or `#region`. Test methods are self-documenting
  through their names; comments become misleading as code is reorganised.

Phrasing of failure messages is generated by `InputValidation` helpers, so
copy-paste exactly:

| Helper | Message template |
|---|---|
| `VerifyNotEmpty(string)` | `"<Name> cannot be null or whitespace."` |
| `VerifyNotEmpty(Guid)` / `VerifyNotEmpty(IEnumerable)` | `"<Name> cannot be empty."` |
| `VerifyNotNegative` | `"<Name> cannot be negative."` |
| `VerifyNotNull` | `"<Name> cannot be null."` |
| `VerifyLengthHigherOrEqualThan` | `"<Name> length cannot be lower than <N>."` |
| `VerifyLengthLowerOrEqualThan` | `"<Name> length cannot be higher than <N>."` |
| `VerifyLength` | `"<Name> length should be <N>."` |
| `VerifyHigherOrEqualThan` | `"<Name> cannot be lower than <N>."` |
| `VerifyLowerOrEqualThan` | `"<Name> cannot be higher than <N>."` |
| `VerifyCountLowerOrEqualThan` | `"<Name> count cannot be higher than <N>."` |

Use `.BeSuccess(value => ...)` for nested assertions and `.BeSuccess(expected)`
when you can compare with equality.

### Step 3 — `RequestTest.cs`

Thin test that asserts `BuildRequestMessage().RequestUri` resolves to the right
path (including any interpolated IDs, query string, etc.).

**HAL link → request round-trip.** Whenever a paginated response exposes a `next`,
`prev`, `first`, or `last` link, customers must be able to reconstruct a new request
directly from that URL — otherwise they have to decompose and re-assemble the query
string themselves, which is terrible DX.

The pattern: replace the plain `ReportLink` (or similar href-only type) in the response
with a typed HAL link record that owns a `BuildRequest()` method. `BuildRequest()` calls
`HttpUtility.ParseQueryString(uri.Query)`, extracts every recognised parameter, and
feeds them through the same `[Builder]`-generated fluent interface used by customers:

```csharp
public record XxxHalLink(Uri Href)
{
    public Result<XxxRequest> BuildRequest()
    {
        var p = ParseQueryParameters(this.Href);
        IBuilderForOptional builder = XxxRequest.Build()
            .WithMandatoryField(p.MandatoryValue);
        builder = p.OptionalField.Match(builder.WithOptionalField, () => builder);
        // ... repeat for every field present in the URL
        return builder.Create();
    }
    ...
}
```

Include these `RequestTest` cases for the HAL link:
1. **Happy path** — constructs a `XxxHalLink` from a URL that includes all relevant
   query parameters and asserts the resulting request has the expected field values.
2. **Missing mandatory field** — constructs from a URL where a required parameter is
   absent and asserts `BeParsingFailure("FieldName cannot be null or whitespace.")`.

**All-parameters URI test.** For GET endpoints with many optional query parameters,
add one `[Fact]` that builds a request with every optional field set to a representative
value, then parses the resulting URI with `HttpUtility.ParseQueryString` and asserts
each key-value pair. This catches wiring bugs (missing `IfSome`, wrong key name) that
per-field setter tests do not catch because they test one field in isolation. Parsing the
query string (rather than string-matching the full URL) avoids encoding fragility.
Requires `using System.Linq;` and `using System.Web;`.

```csharp
[Fact]
public void RequestUri_ShouldContainAllParameters() =>
    XxxRequest.Build()
        .WithMandatory("value")
        .WithOptionalA("a")
        .WithOptionalB(EnumValue)
        ...
        .Create()
        .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
        .Should()
        .BeSuccess(uri =>
        {
            var q = HttpUtility.ParseQueryString(uri.Split('?').Last());
            q["optional_a"].Should().Be("a");
            q["optional_b"].Should().Be("enum-description");
            ...
        });
```

**Pagination E2E test.** When a response contains HAL navigation links, add an E2E
test that:
1. Mocks the first-page response (containing a `next` link).
2. Extracts the next request via `firstPage.Bind(r => r.Links.Next.BuildRequest())`.
3. Passes that directly to the client method and asserts the second call succeeds.

This proves the round-trip works end-to-end through WireMock — the link URL is
deserialized, parsed back into a request, serialized as query parameters, and matched
by the server stub. Use `Result<T>.Bind` (not `Map`) so parsing failures propagate.

Use `HalLinks<XxxHalLink>` (from `Vonage.Common`) for the response `Links` property
when the API returns multiple navigation links (`self`, `next`, `prev`, etc.). Use
snake_case serializer: `Self` → `"self"`, `Next` → `"next"`. When the fixture URL must
contain URL-encoded characters (e.g. `+` in a date offset), encode them explicitly in
the fixture string so deserialization round-trips correctly.

```csharp
[Fact]
public void RequestUri_ShouldReturnApiEndpoint() =>
    XxxRequest.Build()
        .WithId(new Guid("06547d61-7ac0-43bb-94bd-503b24b2a3a5"))
        .Create()
        .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
        .Should()
        .BeSuccess("/v2/verify/06547d61-7ac0-43bb-94bd-503b24b2a3a5");
```

### Step 4 — `E2ETest.cs`

Uses WireMock via the shared `E2EBase` for the product. Asserts the full
round-trip: that the SDK emits the exact bytes expected by the API, hits the
right URL with the right auth header, and deserializes the response correctly.

Pattern:

```csharp
[Trait("Category", "E2E")]
[Trait("Product", "Product")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task Xxx()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/verify/.../xxx")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ProductClient
            .XxxAsync(XxxRequest.Build().With...().Create())
            .Should()
            .BeSuccessAsync(response => /* assert */);
    }
}
```

**Stream response assertions.** When the method under test returns `Task<Result<Stream>>`, do not assert `BeAssignableTo<Stream>()` — it is trivially true and verifies nothing about the actual response content. Instead, read the stream and compare it to the value the WireMock stub returned:

```csharp
await this.Helper.VonageClient.ProductClient
    .DownloadXxxAsync(XxxRequest.Build().WithFileId(fileId).Create())
    .Should()
    .BeSuccessAsync(stream =>
    {
        using var reader = new StreamReader(stream);
        reader.ReadToEnd().Should().Be("expected-body-content");
    });
```

Set the WireMock stub's `.WithBody("expected-body-content")` to a short, known string (e.g. `"zip-content"`) — not real binary — so the assertion stays readable and deterministic.

Add an `E2EBase.cs` for the product the first time you add one:

```csharp
public class E2EBase
{
    internal readonly TestingContext Helper;
    internal readonly SerializationTestHelper Serialization;

    protected E2EBase(string serializationNamespace) : this() => this.Serialization =
        new SerializationTestHelper(serializationNamespace, JsonSerializerBuilder.BuildWithSnakeCase());

    protected E2EBase() => this.Helper = TestingContext.WithBearerCredentials(); // or WithBasicCredentials()
}
```

Pick credentials to match the product's auth type (Bearer for JWT-based APIs,
Basic for api_key/api_secret APIs).

---

## Production code

### Request type

Always a `public readonly partial struct` (the `partial` is required for the
source generator). Implements `IVonageRequest`.

```csharp
[Builder]
public readonly partial struct XxxRequest : IVonageRequest
{
    // properties with [Mandatory] / [Optional] etc...

    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/v2/things/{this.Id}")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8, "application/json");

    [ValidationRule]
    internal static Result<XxxRequest> VerifyFoo(XxxRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Foo, nameof(request.Foo));
}
```

Conventions:

- **C# property names must be customer-friendly, not OAS/wire names.** When the
  OAS field name is an abbreviation or jargon (`trx`, `moCallBackUrl`,
  `drCallBackUrl`, `msisdn`, etc.), expand it to a clear English name
  (`TransactionReference`, `InboundSmsCallbackUrl`,
  `DeliveryReceiptCallbackUrl`, `PhoneNumber`, …). The wire name stays
  unchanged — put it in `[JsonPropertyName]`, the `GetUrlEncoded()` string
  literal, or the query-parameter key. Customers read the C# API; they never
  see the transport encoding.
- Properties use `{ get; internal init; }` — public get, internal init so the
  only way to construct is via the builder (and thus go through validation).
- `readonly partial struct` — never `class`, never `public init`.
- Use `JsonPropertyOrder` on every serialized property when field order in the
  output JSON matters (it often does for assertions and fixtures).
- Use `JsonIgnore` + `MaybeJsonConverter<T>` on `Maybe<T>` properties that
  should be omitted when `None`.
- Use `EnumDescriptionJsonConverter<T>` for enums that serialize via
  `[Description]` attributes.
- URI interpolation goes in `BuildRequestMessage()`. Query strings go through
  `UriHelpers.BuildUri(path, dict)`.
- **Do not use nullable reference annotations (`T?`) on reference types unless
  `#nullable enable` is active in the project.** The project does *not* enable
  nullable reference types, so writing `string?`, `VoiceCapability?`, or any
  other reference-type nullable triggers CS8632 warnings. Reference types are
  implicitly nullable without the annotation — just write `string`,
  `VoiceCapability`, etc. The only `?` allowed here is on genuine value types:
  `int?`, `bool?`, `Guid?`, enum types (which are value types and use
  `Nullable<T>`). Check for `#nullable enable` in the `.csproj` before
  annotating any reference type; when in doubt, `grep -r "Nullable" Vonage/Vonage.csproj`.
- **Aggregate types own their own predicates.** If the request must decide
  whether to include an aggregate (e.g. only add `"capabilities"` to the body
  when at least one capability is set), put that predicate on the aggregate type
  itself — not inside `GetRequestContent()`. A `HasCapabilities` / `IsEmpty`
  bool property on the aggregate keeps the request's assembly logic clean and
  makes the predicate independently testable. Test it with dedicated `[Theory]`
  cases covering the empty state and every individual field that makes it true.
- **When rewriting or paralleling an existing type, audit what the original
  encodes.** Before writing a new version of a capability, webhook, or
  configuration type, read the existing implementation for:
  - Default values (e.g. Voice webhook timeouts default to 1000 ms connection /
    500 ms socket — drop this and callers silently get server defaults instead).
  - HTTP-method restrictions (e.g. Messages, Verify, and Meetings webhooks are
    POST-only — the old code hard-coded POST to prevent invalid state; model
    this with a dedicated `PostOnlyWebhook` type rather than a generic webhook
    that accepts GET).
  - Other invariants or constraints encoded as narrower types.
  Silently dropping these business rules is invisible to tests unless the tests
  are also written with them in mind.
- Pick `BuildWithSnakeCase()` or `BuildWithCamelCase()` to match the product.
  When in doubt, copy the setting from the sibling use case in the same
  product — every Vonage API team has its own conventions and company-wide
  rules aren't always enforced, so never assume based on "the norm". Current
  per-product defaults below (verify by reading a neighbour before you copy):

| Product | Serializer |
|---|---|
| Conversations | snake_case |
| IdentityInsights | snake_case |
| Messages | snake_case |
| NumberInsightV2 | snake_case |
| NumberVerification | snake_case |
| SimSwap | snake_case |
| SubAccounts | snake_case |
| Users | snake_case |
| VerifyV2 | snake_case |
| Video (all sub-clients) | **camelCase** |
| Voice.Emergency | **camelCase** |

### Builders — prefer the source generator

Use `[Builder]` on the struct and attribute each settable property. The
generator emits:

- An interface `IBuilderForX` per mandatory field (staged / phantom types so
  missing mandatory fields are a compile error).
- A final `IBuilderForOptional` interface with every optional setter plus
  `Create()`.
- The internal `XxxRequestBuilder` struct.

| Attribute | Use for | Example |
|---|---|---|
| `[Mandatory(order)]` | Required value the caller provides directly. `order` defines the staged builder sequence (0 first). | `[Mandatory(0)] public Guid Id { get; internal init; }` |
| `[MandatoryWithParsing(order, nameof(ParseXxx))]` | Required value that is parsed from a string (e.g., `PhoneNumber`). The parser must be `internal static Result<T> ParseXxx(string value)`. | `[MandatoryWithParsing(0, nameof(ParsePhoneNumber))] public PhoneNumber PhoneNumber {...}` |
| `[Optional]` | Optional value, exposed as `Maybe<T>`. | `[Optional] public Maybe<string> Name { get; internal init; }` |
| `[OptionalWithDefault("type", "defaultValue")]` | Optional value with a default; not wrapped in `Maybe`. | `[OptionalWithDefault("int", "50")] public int PageSize {...}` |
| `[OptionalBoolean(defaultValue, "MethodName")]` | Boolean toggle exposed as a **parameterless** builder method with a semantic name (often inverts the property meaning). | `[OptionalBoolean(true, "DisableSharedBalance")] public bool UsePrimary {...}` |
| `[OptionalWithParsing(nameof(ParseXxx))]` | Optional value parsed from a string. | |
| `[ValidationRule]` on `internal static Result<T>` method | Runs after property assignment, with `InputEvaluation<T>` aggregating failures into `ParsingFailure`. | see below |

**Compound optional fields: prefer individual optionals over a single aggregate.**
When a request field is itself an aggregate type whose sub-components are each
independently optional (e.g. a capabilities object where Voice, Messages, RTC, …
are each optional), do *not* model it as a single `[Optional] Maybe<CapabilitiesType>`
that forces the caller to construct the aggregate manually. Instead, expose each
sub-component as its own `[Optional]` property on the request struct:

```csharp
[Optional] public Maybe<VoiceCapability>    Voice      { get; internal init; }
[Optional] public Maybe<MessagesCapability> Messages   { get; internal init; }
[Optional] public Maybe<RtcCapability>      Rtc        { get; internal init; }
```

The generator emits individual `WithVoice(...)`, `WithMessages(...)`, `WithRtc(...)`
methods — callers chain only what they need. The `GetRequestContent()` implementation
assembles the aggregate internally using `with` expressions, then decides whether to
include it in the body based on a `HasXxx` predicate (see responsibility placement
below). This pattern gives far better DX than forcing callers to construct a large
initializer block for the aggregate.

**When to hand-roll the builder instead.** Always prefer `[Builder]` when it
fits. Drop it and write an explicit `XxxRequestBuilder.cs` only when the shape
genuinely can't be expressed with the existing attributes — for example:

- Multiple builder methods mutating the same collection (`WithNumber(...)` is
  called repeatedly — see `CreateConversationRequest`).
- Conditional validation that inspects another property (requires matching on
  `Maybe` inside a rule).
- Parsing that needs to cross-reference another input.

The hand-rolled builder follows the same structure — phantom interfaces for
mandatory stages, final `IBuilderForOptional`, an `internal struct` named
`XxxRequestBuilder` that implements both. See `Vonage/Conversations/CreateConversation/`.

**We own the source generator.** The `Vonage.SourceGenerator` project lives in
this repo, so if a near-miss pattern shows up a second or third time, extending
the generator (new attribute, variant, or behaviour) is often the right call
rather than hand-rolling yet another builder. Before writing a bespoke
builder, ask the user whether it's worth teaching the generator the new trick.
Past additions (`MandatoryWithParsing`, `OptionalBoolean`, `OptionalWithDefault`,
`OptionalWithParsing`) all started as one-offs that became patterns.

### Validation

Place each rule as an `internal static Result<T>` method on the request struct,
decorated with `[ValidationRule]`. The generator wires them into the builder's
`Create()`. `InputEvaluation<T>.WithRules(...)` runs all rules and aggregates
failures into `ParsingFailure` — do not short-circuit by hand.

```csharp
[ValidationRule]
internal static Result<XxxRequest> VerifyName(XxxRequest request) =>
    InputValidation.VerifyNotEmpty(request, request.Name, nameof(request.Name));

[ValidationRule]
internal static Result<XxxRequest> VerifyNameLength(XxxRequest request) =>
    InputValidation.VerifyLengthLowerOrEqualThan(request, request.Name, 80, nameof(request.Name));
```

Favor the helpers in `InputValidation` over raw `ResultFailure.FromErrorMessage`,
so error messages stay consistent.

### Response type

Prefer `public record XxxResponse(...)` with positional params. Use
`[JsonPropertyName]` only when the API field name doesn't match the default
snake-case translation. For paginated responses, follow `GetTemplatesResponse`
(HAL-style `EmbeddedResponse<T>`, `PageResponse`, `_links`).

### Never throw

Every public entry point returns `Result<T>`. Errors are values:

- Parsing failure at build time → `ParsingFailure` (aggregated).
- Single parse error → `ResultFailure.FromErrorMessage(...)`.
- HTTP / API error → the `VonageHttpClient<TError>` already maps status codes
  to `HttpFailure` / `TError`.
- Deserialization failure → `DeserializationFailure` (handled by the client).

Do not add `try/catch` around builder calls, serializer calls, or HTTP calls.
The monadic pipeline handles them; catching swallows the failure type.

### Client method

On `IProductClient`:

```csharp
/// <summary>
///     Does the thing.
/// </summary>
/// <param name="request">The request...</param>
/// <returns>... or failure if ...</returns>
/// <example>
/// <code><![CDATA[
/// var request = XxxRequest.Build().With...().Create();
/// var result = await client.XxxAsync(request);
/// ]]></code>
/// </example>
/// <seealso href="{snippets-url-if-any}">More examples in the snippets repository</seealso>
Task<Result<Response>> XxxAsync(Result<XxxRequest> request);
```

On `ProductClient`, one-liner:

```csharp
/// <inheritdoc />
public Task<Result<XxxResponse>> XxxAsync(Result<XxxRequest> request) =>
    this.vonageClient.SendWithResponseAsync<XxxRequest, XxxResponse>(request);
```

Pick the send helper:

- `SendAsync(request)` → returns `Task<Result<Unit>>` (no response body — DELETEs, cancellations).
- `SendWithResponseAsync<TRequest, TResponse>(request)` → returns `Task<Result<TResponse>>`.
- `SendWithRawResponseAsync<TRequest>(request)` → returns `Task<Result<string>>` for endpoints that return raw text payloads.
- `SendWithStreamResponseAsync<TRequest>(request)` → returns `Task<Result<Stream>>` for binary/octet-stream endpoints (e.g. ZIP archives). Uses `HttpCompletionOption.ResponseHeadersRead` so the HTTP body is never fully buffered in memory — the caller controls streaming. The caller is responsible for disposing the returned stream.

**`byte[]` vs `Stream` for binary responses.** When an endpoint returns `application/octet-stream`, prefer `Stream` over `byte[]`. `byte[]` forces the entire body into memory before returning — for files that can grow without bound, this means unbounded heap allocation. `Stream` lets the caller pipeline the bytes directly to disk or another network socket without holding everything in RAM. Return `byte[]` only when the response is demonstrably bounded and small.

### Wiring a brand-new product into `VonageClient`

1. Add `IProductClient ProductClient { get; private set; }` on `VonageClient`
   with a proper XML summary.
2. Register a matching `using` in `VonageClient.cs`.
3. In `PropagateCredentials()`, instantiate the client against the right HTTP
   client factory and auth type. Examples:
   - `currentConfiguration.BuildHttpClientForNexmo()` → most APIs.
   - `currentConfiguration.BuildHttpClientForVideo()` → Video API.
   - `currentConfiguration.BuildHttpClientForRegion(VonageUrls.Region.EU)` →
     SimSwap, NumberVerification.
   - `currentConfiguration.BuildHttpClientForOidc()` → NumberVerification auth step.
4. Keep the client class `internal` and the interface `public`. Constructor
   takes `VonageHttpClientConfiguration` and any extra context (e.g., primary
   ApiKey for SubAccounts).

---

## Developer experience / public surface

Expose only what the customer needs to call the SDK:

- Request types are `public` with `internal init`. No public constructors.
- Builder interfaces are `public`; the builder struct is `internal`.
- Client class is `internal`, interface is `public`.
- Validation rule methods are `internal static`. Never `public`.
- `Parse(...)` factories (for request types with no builder) are `public`.
- Never surface `Result<T>.GetSuccessUnsafe()` style APIs in samples — show
  `Match`, `IfSuccess`, `Map`, `Bind`.

---

## XML documentation bar

Apply the conventions from the `xml-documentation` skill. Short version:

- Every `public` type, property, and method has a `<summary>`. No empty summary
  tags ever.
- Every property on a `[Builder]` struct has both a `<summary>` **and** a
  focused `<example>` showing **only** the single builder method call that
  property generates. The generator copies this XML onto the builder method,
  so the summary should read as a method description ("Sets the ...",
  "Includes ...", "Enables ...") — not a property description.
- Client methods get `<summary>`, `<param>`, `<returns>`, an `<example>` with a
  full `Build()...Create()` pipeline, and a `<seealso href="...">` pointing to
  the snippets repo when applicable.
- For `[OptionalBoolean(default, "Name")]`, the `<example>` must use the
  explicit method name from the attribute (e.g., `.DisableSharedBalance()`),
  not `.WithXxx()`.
- Use `<see cref="..."/>` when referencing related request or response types.
- Never copy-paste XML docs between channels/products; always tailor them. The
  OAS is your source of truth for constraints.

If no OAS is available, mark fields with:
```csharp
/// <summary>
///     Best-guess description.
///     TODO: No OAS match found — verify this description.
/// </summary>
```

---

## Checklist before declaring a use case done

Run through this before reporting "done" to the user:

- [ ] Folder layout matches the blueprint (one folder per use case, nothing
      flattened).
- [ ] Request is `readonly partial struct`, implements `IVonageRequest`.
- [ ] `[Builder]` used unless the shape truly requires a hand-rolled builder.
- [ ] Every property has the correct attribute (`[Mandatory]`,
      `[MandatoryWithParsing]`, `[Optional]`, `[OptionalWithDefault]`,
      `[OptionalBoolean]`, `[OptionalWithParsing]`).
- [ ] Every constraint from the OAS has a matching `[ValidationRule]` and a
      matching failure test.
- [ ] Four test files exist: `E2ETest`, `RequestTest`, `RequestBuilderTest`,
      `SerializationTest`, all with `[Trait("Category", ...)]` and
      `[Trait("Product", ...)]`.
- [ ] Every test's JSON fixture lives in `Data/` and is named after the test
      (`<testName>-request.json` or `-response.json`).
- [ ] Every fixture file is registered in `Vonage.Test/Vonage.Test.csproj` with
      `<None Update="Path\To\Data\File.json"><CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory></None>`.
- [ ] E2E test uses `E2EBase` and asserts against the same fixture the
      serialization test uses.
- [ ] Client interface method has `<summary>`, `<param>`, `<returns>`,
      `<example>` (with `CDATA`), and `<seealso>` (if snippets exist).
- [ ] Client implementation is a `/// <inheritdoc />` one-liner.
- [ ] If new product: wired into `VonageClient.PropagateCredentials()` with
      correct auth type and HTTP client factory.
- [ ] If new product: `IXxxClient` registered in both `RegisterScopedServices`
      and `RegisterTransientServices` in `Vonage/Extensions/ServiceCollectionExtensions.cs`,
      and a matching `yield return [typeof(IXxxClient)]` added to
      `GetRegisteredTypes()` in `Vonage.Test/Extensions/ServiceCollectionExtensionsTest.cs`.
- [ ] JSON response fixtures use the exact example values from the OAS — every
      optional field that has an OAS example must appear in at least one fixture
      so it is exercised by the deserialization test.
- [ ] `dotnet build Vonage.sln` and `dotnet test` pass.
- [ ] No empty `/// <summary></summary>` remain:
      `grep -rn "/// <summary>\s*$" Vonage --include="*.cs"` returns nothing.
- [ ] No `throw` statements introduced; all failures flow through `Result<T>`.
- [ ] No `public` setters or public constructors on request/response types.
- [ ] No OAS abbreviations leaked into C# property names (`trx`, `moCallBackUrl`,
      `drCallBackUrl`, `msisdn` …) — all expanded to customer-readable names;
      wire keys stay in `[JsonPropertyName]` / `GetUrlEncoded()` / query dicts.
- [ ] Every variant of a polymorphic/union field has its own `ShouldSerializeWith<Variant>`
      test and fixture — no variant is left covered only by a combined multi-field fixture.
- [ ] No single-letter or abbreviated lambda parameters (`v =>`, `r =>`, `p =>`) or local
      variables (`caps`, `req`) anywhere in production or test code.
- [ ] No nullable reference annotations (`T?`) on reference types; only value types
      (`int?`, `bool?`, `Guid?`, enums) use `?`.
- [ ] Related support types (capabilities, webhook sub-types, etc.) grouped into a
      subfolder when there are 5+ of them; the subfolder has a matching sub-namespace.
- [ ] Aggregate predicates (`HasXxx`, `IsEmpty`) live on the aggregate type, not in
      callers, and are tested directly.

---

## Common pitfalls

- **Copying OAS abbreviations into C# property names.** Field names like `trx`,
  `moCallBackUrl`, `drCallBackUrl`, or `msisdn` are fine on the wire but
  meaningless to customers in IntelliSense. Always expand them:
  `trx` → `TransactionReference`, `moCallBackUrl` → `InboundSmsCallbackUrl`,
  `drCallBackUrl` → `DeliveryReceiptCallbackUrl`, `msisdn` → `PhoneNumber`.
  Keep the original key in `[JsonPropertyName]` / the `GetUrlEncoded()` literal
  / the query-parameter dictionary — never in the C# property name.
- **Forgetting `partial`** on a `[Builder]` struct — the generator silently
  does nothing.
- **Mixing snake_case and camelCase** serializers between the request and the
  test helper. They must match, otherwise `ShouldSerialize` will drift from
  `ShouldDeserialize` without obvious errors.
- **Ordering `[Mandatory]` fields arbitrarily**. The `order` defines the staged
  builder chain that the customer sees. Put the most "anchoring" field (IDs
  first, then the payload) at lower orders.
- **Using `ResultFailure.FromErrorMessage` directly from a `[ValidationRule]`**
  when an `InputValidation` helper would do. The helpers ensure consistent
  messages and match the existing test failure strings.
- **Writing the E2E test before the fixtures**. The E2E test reads
  `SerializationTest` fixture names — the fixtures must exist first.
- **Adding public types for intermediate state** (e.g., exposing the builder
  struct). Keep the builder internal; customers only ever see the interfaces.
- **`Maybe<T>` as a public setter**. Use `Optional` attribute so the generated
  `WithXxx(T value)` unwraps it; customers should not build a `Maybe` manually.
- **Returning Tasks that swallow failures** (`Task<TResponse>` instead of
  `Task<Result<TResponse>>`). Every client method returns `Task<Result<T>>`.
- **Forgetting to add `[Trait]` attributes** — they're used by CI to filter
  test runs (`Category=E2E` is skipped locally without a WireMock server in
  some configurations).
- **Forgetting to register JSON fixtures in `Vonage.Test.csproj`**. Every file
  under `Data/` must have a corresponding `<None Update="...">` entry with
  `<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>`. Without it,
  `SerializationTestHelper.GetResponseJson()` silently returns an empty string
  and the deserialization test fails with a confusing "Unable to deserialize ''"
  message. Always add the entry immediately after creating the fixture file.
- **Leaving empty placeholder files** (comment-only `.cs` stubs, zero-byte files).
  If a type moves to a shared location, delete the old file with `rm` via Bash — do not
  leave a file that exists only to say it was moved. The build system picks up every
  `.cs` file automatically; dead files create confusion and noise in diffs.
- **Breaking changes on released public APIs**. Renaming or moving a public type (e.g.
  `CancelReportResponse` → `ReportResponse`) is a silent compile-time breaking change
  for anyone already using the SDK. Before renames or return-type changes on existing
  public members: confirm the product has been published in a release. If it has,
  keep the old name with an `[Obsolete]` alias or introduce the shared type additively
  and migrate the old method in a separate PR. Only freely rename/move types on
  unreleased products.
- **Forgetting `ServiceCollectionExtensions`** when adding a new product. Every
  new `IXxxClient` must be registered in both `RegisterScopedServices` and
  `RegisterTransientServices` in `Vonage/Extensions/ServiceCollectionExtensions.cs`.
  The companion test in `Vonage.Test/Extensions/ServiceCollectionExtensionsTest.cs`
  must also get a `yield return [typeof(IXxxClient)]` entry — otherwise the
  client is invisible to DI consumers and the gap goes undetected.
- **Using `Parse` instead of `[Builder]` for simple path-only requests**.
  Even a request with a single mandatory `Guid` should use `[Builder]` with
  `[Mandatory(0)]` — it's less code, consistent with the rest of the SDK, and
  allows the request to grow without a structural rewrite. The `Parse` factory
  pattern should only be used for legacy or `VerifyV2/Cancel`-style types that
  predate the generator; never introduce new ones.
- **Using `byte[]` instead of `Stream` for binary responses**. `byte[]` forces
  the entire payload into memory before returning. Use `SendWithStreamResponseAsync`
  and return `Task<Result<Stream>>` whenever the response content type is
  `application/octet-stream` or otherwise unbounded. See the send-helper section above.
- **Trivially-true stream assertions in E2E tests**. `stream.Should().BeAssignableTo<Stream>()`
  always passes and verifies nothing. Read the stream content and compare it to the
  known string returned by the WireMock stub (see the Stream response assertions section above).
- **Single-letter or abbreviated variable names.** Never use single-letter lambda
  parameters (`v =>`, `r =>`, `p =>`) or abbreviated local variables (`caps`, `req`,
  `r`). Use the full, descriptive name: `request =>`, `capabilities =>`, `voice =>`,
  `pageSize =>`, etc. This applies equally to production code and tests. Abbreviated
  names force the reader to derive meaning from context; full names make the intent
  unambiguous. The extra characters have zero runtime cost. Examples:
  - `this.Voice.IfSome(v => ...)` → `this.Voice.IfSome(voice => ...)`
  - `this.NetworkApis.IfSome(v => ...)` → `this.NetworkApis.IfSome(networkApis => ...)`
  - `var caps = new ApplicationCapabilities()` → `var capabilities = new ApplicationCapabilities()`
  - `.Map(r => r.BuildRequestMessage())` → `.Map(request => request.BuildRequestMessage())`
  - `.BeSuccess(k => k.PublicKey...)` → `.BeSuccess(applicationKeys => applicationKeys.PublicKey...)`
  - Inner lambdas: `.BeSome(p => p.ImproveAi...)` → `.BeSome(settings => settings.ImproveAi...)`
- **Using nullable reference annotations without `#nullable enable`.** Writing `T?` on
  reference types (e.g. `string?`, `VoiceCapability?`) when nullable reference types are
  not enabled in the project causes CS8632 warnings and implies a semantic contract the
  compiler doesn't enforce. Only `int?`, `bool?`, `Guid?`, and other genuine value types
  need `?` — they use `Nullable<T>`. Reference types are implicitly nullable without any
  annotation. Always check `grep -r "Nullable" Vonage/Vonage.csproj` before annotating
  reference types.
- **Dropping existing defaults and validation when rewriting a type.** Before writing a
  new version of an existing capability, webhook, or configuration type, read the old
  implementation. Silent drops include: numeric defaults (Voice webhook timeouts: 1000 ms
  connection, 500 ms socket), HTTP-method constraints (Messages/Verify/Meetings accept
  POST only — model with a `PostOnlyWebhook` type rather than a generic webhook), and
  range constraints. Tests won't catch these unless they were written with the constraint
  in mind.
- **Putting aggregate-level predicates in the caller.** If `GetRequestContent()` needs
  a `hasCapabilities` flag, that flag belongs on the aggregate type as a `HasCapabilities`
  property — not in the request method. The aggregate knows whether it has content; the
  request should just ask it. Move such predicates to the aggregate and test them directly
  with a dedicated `[Fact]` (empty state) + `[Theory]` (one entry per field that makes it
  true).
