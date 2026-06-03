# Working with Result&lt;T&gt;

`Result<T>` is the return type for all modern SDK operations. It holds one of two states:

- **Success**: the operation succeeded and carries a value of type `T`
- **Failure**: the operation failed and carries data about what went wrong

The goal is an exception-free workflow: the method signature is fully transparent about the possibility of failure, and
no `try/catch` is required. This is an alternative to exception handling.
See [Railway Oriented Programming](https://fsharpforfunandprofit.com/posts/recipe-part2/).

## React to Success or Failure

### Handle both paths: Match

`Match` is the most explicit form. It accepts one operation for each state, and the compiler enforces that both are
provided.

It supports both transformation (returning a value) and side effects (Actions):

```csharp
Result<StartVerificationResponse> result = await client.VerifyV2Client.StartVerificationAsync(request);

// Transformation — both branches produce the same output type
string message = result.Match(
    successOperation: response => $"Verification started: {response.RequestId}",
    failureOperation: failure  => $"Error: {failure.GetFailureMessage()}");

// Side effects
result.Match(
    successOperation: response => Console.WriteLine($"Request ID: {response.RequestId}"),
    failureOperation: failure  => Console.WriteLine($"Error: {failure.GetFailureMessage()}"));
```

### Handle only success: IfSuccess

When you only need to react to a successful outcome and can ignore failure:

```csharp
result.IfSuccess(response => Console.WriteLine($"Request ID: {response.RequestId}"));
```

### Handle only failure: IfFailure

When you only need to react to failure. For example, to log it:

```csharp
result.IfFailure(failure => logger.LogError(failure.GetFailureMessage()));
```

You can also produce a fallback value that replaces the result on failure:

```csharp
// Fallback via a function (receives the failure reason):
string display = result.IfFailure(failure => $"unavailable ({failure.GetFailureMessage()})");

// Fallback via a constant:
string display = result.IfFailure("unavailable");
```

## Transforming values

### Transform the response: Map

`Map` transforms the success value without unwrapping the result. A failure bypasses the transformation and propagates
unchanged:

```csharp
Result<StartVerificationResponse> response = await client.VerifyV2Client.StartVerificationAsync(request);
Result<Guid> requestId = response.Map(success => success.RequestId);
```

### Chain dependant calls: Bind

`Bind` chains a step that itself returns a `Result<T>`. Use it when the next operation can also fail:

```csharp
Result<StartVerificationResponse> response = await client.VerifyV2Client.StartVerificationAsync(request);
Result<Unit> verified = response.BindAsync(success => client.VerifyV2Client.VerifyCodeAsync(
        VerifyCodeRequest.Build()
            .WithRequestId(success.RequestId)
            .WithCode(codeFromUser)
            .Create()));

// verified is Success, or whichever failure occurred first.
```

## One failure channel for everything

Validation errors, HTTP 4xx/5xx responses, network timeouts, and deserialization failures all surface as a failed
`Result<T>`. A single handler covers every origin:

```csharp
result.IfFailure(failure => Console.WriteLine(failure.GetFailureMessage()));
// e.g. "Validation: phone number is invalid"
//      "HTTP 404: Not Found"
//      "Serialization: unexpected token at position 12"
```

When a single message isn't enough, switch on the concrete failure type. The failure parameter is typed as
`IResultFailure` — a C# switch expression turns it into a typed value you can act on:

```csharp
result.Match(
    successOperation: response => Console.WriteLine($"Request ID: {response.RequestId}"),
    failureOperation: failure  => Console.WriteLine(failure switch
    {
        ParsingFailure     parsing => $"Fix your input: {parsing.GetFailureMessage()}",
        AuthenticationFailure      => "Check your API key or application credentials.",
        HttpFailure        http    => $"API returned {(int) http.Code}: {http.Message}",
        DeserializationFailure     => "Unexpected response format — check the SDK version.",
        _                          => failure.GetFailureMessage()
    }));
```

`HttpFailure` carries a status code, so you can go one level deeper with property patterns:

```csharp
using System.Net;

failureOperation: failure => failure switch
{
    HttpFailure { Code: HttpStatusCode.Unauthorized }    => HandleUnauthorized(),
    HttpFailure { Code: HttpStatusCode.TooManyRequests } => ScheduleRetry(),
    HttpFailure http                                     => $"HTTP {(int) http.Code}: {http.Message}",
    ParsingFailure parsing                               => $"Invalid input: {parsing.GetFailureMessage()}",
    AuthenticationFailure                                => "Missing or invalid credentials.",
    _                                                    => failure.GetFailureMessage()
}
```

The concrete types:

| Type                     | When it occurs                                                                 |
|--------------------------|--------------------------------------------------------------------------------|
| `HttpFailure`            | The API returned a 4xx or 5xx response. Carries `Code`, `Message`, and `Json`. |
| `ParsingFailure`         | A request builder rejected the input during validation.                        |
| `AuthenticationFailure`  | Credentials are missing, expired, or do not match the API's requirements.      |
| `DeserializationFailure` | The API response could not be parsed into the expected type.                   |
| `SystemFailure`          | An unexpected exception was caught inside the SDK.                             |
| `ResultFailure`          | A general failure with a plain error message.                                  |

## Escape hatch: GetSuccessUnsafe

If you prefer exceptions over monadic handling, call `GetSuccessUnsafe()`. It returns the value on success and throws on
failure. The exception type reflects the failure reason and cannot be determined at compile time:

```csharp
try
{
    StartVerificationResponse response = result.GetSuccessUnsafe();
    Console.WriteLine(response.RequestId);
}
catch (Exception ex)
{
    // Exception type depends on the failure: validation, HTTP, serialization, etc.
    Console.WriteLine(ex.Message);
}
```

## Builders propagate failures automatically

Request builders return `Result<TRequest>`. If any builder step fails, the failure propagates through `.Create()` and
short-circuits the API call — no HTTP request is made:

```csharp
// Parse fails here — the invalid phone number never reaches the network.
var request = StartVerificationRequest.Build()
    .WithBrand("MyApp")
    .WithWorkflow(SmsWorkflow.Parse("not-a-phone-number"))
    .Create();

// Already a Failure, so the SDK skips the HTTP call entirely.
var result = await client.VerifyV2Client.StartVerificationAsync(request);
```

## Implicit conversion

Client methods accept `Result<TRequest>`. A builder result passes through directly; a plain value is implicitly
promoted:

```csharp
// From a builder (most common):
Result<StartVerificationRequest> request = StartVerificationRequest.Build()...Create();
var result = await client.VerifyV2Client.StartVerificationAsync(request);

// From a plain value, implicitly converted to Result<T>:
StartVerificationRequest request = ...;
var result = await client.VerifyV2Client.StartVerificationAsync(request);
```

## Maybe&lt;T&gt;

`Maybe<T>` is a companion type used in parts of the SDK where the absence of a value is a normal outcome, rather than an
error or `null`. It holds one of two states:

- **Some**: a value is present
- **None**: no value; the .NET equivalent of `null`, but safe

```csharp
// A lookup that may or may not find a result:
Maybe<PhoneNumber> phoneNumber = phoneBook.Find("447700900000");

// Handle both states:
string display = phoneNumber.Match(
    // Some state
    some => some.Label,
    // None state
    ()     => "Not found");

// Fallback on None:
PhoneNumber number = phoneNumber.IfNone(PhoneNumber.Unknown);

// Escape hatch, throws NoneStateException if None:
PhoneNumber number = phoneNumber.GetUnsafe();
```

`Maybe<T>` supports the same operations as `Result<T>`. The state-agnostic ones, like `Match`, `Map`, `Bind`, are
identical. The state-specific ones are named after each type's state: where `Result<T>` has `IfSuccess`/`IfFailure`,
`Maybe<T>` has `IfSome`/`IfNone`.

## See also

- [Getting Started](getting-started.md) — authenticate and make your first call
- [API Reference](xref:Vonage) — all client interfaces and request types
