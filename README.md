Vonage Client Library for .NET
===================================

This library allows you to integrate feature from [Vonage's APIs](#supported-apis) to your application.

It requires you to create
a [Vonage account](https://dashboard.nexmo.com/sign-up?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library)
first.

* [Configuration](#configuration)
    * [Setup](#setup)
        * [Lazy registration (recommended for .NET Core and above)](#lazy-registration-recommended-for-net-core-and-above)
        * [Manual initialization (recommended for .NET Framework)](#manual-initialization-recommended-for-net-framework)
    * [Configuration reference](#configuration-reference)
    * [Logging](#logging)
* [Monads](#monads)
    * [Result](#result)
    * [Maybe](#maybe)
    * [What if you don't want to use Monads?](#what-if-you-dont-want-to-use-monads)
* [Supported APIs](#supported-apis)
* [Thanks](#thanks)
* [Licence](#license)

## Installation

## Configuration

### Setup

There are various ways to initialize a `VonageClient` with your custom values.

Overall, we encourage you to specify your configuration (Vonage URLs, credentials, etc.) in `appsettings.json`, in
an `appsettings` section:

```json
{
  "vonage": {
    "UserAgent": "myApp/1.0",
    "Url.Rest": "https://rest.nexmo.com",
    "Url.Api": "https://api.nexmo.com",
    "Url.Api.EMEA": "https://api-eu.vonage.com",
    "Url.Api.AMER": "https://api-us.vonage.com",
    "Url.Api.APAC": "https://api-ap.vonage.com",
    "Url.Api.Video": "https://video.api.vonage.com",
    "Api.Key": "VONAGE-API-KEY",
    "Api.Secret": "VONAGE-API-SECRET",    
    "Application.Id": "ffffffff-ffff-ffff-ffff-ffffffffffff",
    "Application.Key": "VONAGE_APPLICATION_PRIVATE_KEY",
    "PooledConnectionIdleTimeout": "60", 
    "PooledConnectionLifetime": "600"
  }
}
```

The configuration is automatically loaded in the `Configuration` singleton.

#### Lazy registration (recommended for .NET Core and above)

> Note: This implementation is not available for .NET Framework usages, given IConfiguration has been introduced in .NET
> Core.

You can register a client in your `IServiceCollection` using the following extension methods:

- `AddVonageClientScoped`: registers using Scoped registration.
- `AddVonageClientTransient`: registers using Transient registration.

``` csharp
// For 'Scoped' lifecycle
builder.Services.AddVonageClientScoped(builder.Configuration);
// Foor 'Transient' lifecycle
builder.Services.AddVonageClientTransient(builder.Configuration);
```

> Note: Using `builder.Configuration` allow us to use settings you decided to load at runtime, including
> environment-specific settings.

``` csharp
var credentials = ...
// For 'Scoped' lifecycle
builder.Services.AddVonageClientScoped(credentials);
// Foor 'Transient' lifecycle
builder.Services.AddVonageClientTransient(credentials);
```

It will register the main `VonageClient`, but also all sub client interfaces:

- IMessagesClient
- IVerifyV2Client
- IMeetingsClient
- IVoiceClient
- etc.

Finally, you can inject them in any of your components.

#### Manual initialization (recommended for .NET Framework)

Create a Vonage Client instance and pass in credentials in the constructor;
this will only affect the security credentials (Api Key, Api Secret, Signing Secret, Signing Method Private Key, App
Id).

```csharp
var credentials = Credentials.FromApiKeyAndSecret(
    VONAGE_API_KEY,
    VONAGE_API_SECRET
    );

var vonageClient = new VonageClient(credentials);
```

If required, you can override values directly in the `Configuration` singleton:

```cshap
Configuration.Instance.Settings["vonage:Url.Api"] = "https://www.example.com/api";
Configuration.Instance.Settings["vonage:Url.Rest"] = "https://www.example.com/rest";
```

> Note: Private Key is the literal key - not a path to the file containing the key

> Note: Modifying the Configuration instance will be deprecated in the upcoming release, to keep the configuration
> immutable.

### Configuration Reference

| Key                         | Description                                                                                                                      |
|-----------------------------|----------------------------------------------------------------------------------------------------------------------------------|
| Application.Id              | Your application ID                                                                                                              |
| Application.Key             | Your application's private key                                                                                                   |
| Url.Rest                    | Optional. Vonage REST API base URL. Defaults to https://rest.nexmo.com                                                           |
| Url.Api                     | Optional. Vonage API base URL. Defaults to https://api.nexmo.com                                                                 |
| Url.Api.EMEA                | Optional. Vonage API base URL for Europe, Middle East and Africa. Defaults to https://api-eu.vonage.com                          |
| Url.Api.AMER                | Optional. Vonage API base URL for North, Central and South America. Defaults to https://api-us.vonage.com                        |
| Url.Api.APAC                | Optional. Vonage API base URL for Asia and Pacific. Defaults to https://api-ap.vonage.com                                        |
| Url.Api.Video               | Optional. Vonage API base URL for Video. Defaults to https://video.api.vonage.com                                                |
| RequestsPerSecond           | Optional. Throttle to specified requests per second.                                                                             |
| RequestTimeout              | Optional.  The timeout (in seconds) applied to every request. If not provided, the default timeout will be applied.              |
| UserAgent                   | Optional. Your app-specific usage identifier in the format of `name/version`. Example: `"myApp/1.0"`                             |
| PooledConnectionIdleTimeout | Optional. The time (in seconds) that a connection can be idle before it is closed. Defaults to 60 seconds.                       |
| PooledConnectionLifetime    | Optional. The time (in seconds) that a connection can be alive before it is closed. Defaults to 600 seconds.                     |

### Logging

The Library uses Microsoft.Extensions.Logging to preform all of it's logging tasks. To configure logging for you app
simply create a new `ILoggerFactory` and call the `LogProvider.SetLogFactory()` method to tell the Vonage library how to
log. For example, to log to the console with serilog you can do the following:

```csharp
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Vonage.Logger;
using Serilog;

var log = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}]: {Message}\n")
    .CreateLogger();
var factory = new LoggerFactory();
factory.AddSerilog(log);
LogProvider.SetLogFactory(factory);
```

## Monads

Most recent features expose responses/results
using [Monads](https://www.adit.io/posts/2013-04-17-functors,_applicatives,_and_monads_in_pictures.html#just-what-is-a-functor,-really?).
While the name may be scary at first, this is just another data structure that offers various benefits.
We can picture them as a `box` containing our value, and we manipulate the box instead of manipulating a value.

Their purpose is to provide a value that can have multiple states, and deal with each state using a similar workflow.
This is a alternative to exception handling -
see [Railway Oriented Programming](https://fsharpforfunandprofit.com/posts/recipe-part2/).

### Result

The `Result<T>` monad represents a value that can have one of the following states:

- `Success` - indicates the process was a success
- `Failure` - indicates the process failed

The goal is to provide an `exception-free` process.

- Transparency - the signature is fully transparent about the possible outputs (aka. a method **can** fail)
- Less intrusive - no `try-catch` required

```csharp
// This method allows to Divide a number
private static Result<decimal> Divide(decimal value, decimal divider) =>
    divider == 0
        // If divider is 0, failure is returned
        ? Result<decimal>.FromFailure(ResultFailure.FromErrorMessage("Cannot divide by 0."))
        // If divider is allowed, success is returned
        : Result<decimal>.FromSuccess(value / divider);
```

### Maybe

The `Maybe<T>` monad represents a value that can have one of the following states:

- `Some` - indicates the presence of a value
- `None` - indicates the absence of a value

The goal is to mitigate the 'billion-dollar mistake': `null`.

```csharp
// This method allows to retrieve phone number
private static Maybe<PhoneNumber> Find(string number) =>
    this.numbers.FirstOrDefault(phoneNumber => phoneNumber.Number.Equals(number)) 
    ?? Maybe<PhoneNumber>.None;
```

### How to extract a value from a Monad?

Given we cannot predict the state at build-time, we need to provide an operation for **each** scenario.

The following methods are all available for both `Result<T>` and `Maybe<T>`, but examples will focus on `Result<T>`.

#### Match

`.Match()` expects two operations, and will evaluate the one corresponding to the current state.
It supports transformation, and can be used with both `Actions` and `Functions`.

```csharp
Result<int> monad = ...
// Supports transformation    
string result = monad.Match(
    // Will be executed if Result<int>.IsSuccess
    some => $"The process is a success: {some}.",
    // Will be executed if Result<int>.IsFailure 
    failure => $"The process is a failure: {failure.GetFailureMessage()}");

// Using Actions    
monad.Match(
    some => Console.WriteLine($"The process is a success: {some}."),
    failure => Console.WriteLine($"The process is a failure: {failure.GetFailureMessage()}"));
```

#### IfFailure / IfNone

If you want to retrieve the value without transforming it, you can use `.IfFailure()` which expects a fallback value in
case the state is `Failure`.

```csharp
Result<int> monad = ...
// Using the failure reason (recommended)
string result = monad.IfFailure(failure => $"The process is a failure: {failure.GetFailureMessage()}");
// Using a default value
string result = monad.IfFailure("Something failed.");
// Using an Action
monad.IfFailure(failure => Console.WriteLine($"The process is a failure: {failure.GetFailureMessage()}"));    
```

### Features

Both `Result<T>` and `Maybe<T>` also exposes more capabilities:

- `Map`: transforms the value (if success/some) that will wrap the result into a new Monad.
- `Bind`: transforms the value (if success/some) that will return a new Monad.
- `Merge`: merges (or flatten) two monads.
- `IfSuccess` / `IfSome`: provide an operation that will be executed if the Monad is in the expected state.
- `IfFailure` / `IfNone`: provide an operation that will be executed if the Monad is in the expected state.
- Implicit operators
- Async support
- etc.

You can see how to benefit from these capabilities
in [our Tests](https://github.com/Vonage/vonage-dotnet-sdk/tree/main/Vonage.Common.Test/Monads).

### What if you don't want to use Monads?

You can use `GetUnsafe()` if you prefer having an exception thrown when a Monad is not in the desired state.

```csharp
Result<int> result = ...
try
{
    int output = result.GetSuccessUnsafe();
}
// The exception type cannot be defined at build-time
// It depends of the failure reason:
// - Authentication failure
// - Serialization/Deserialization failure
// - Http failure
// - Validation failure
// - etc.
catch (Exception exception)
{
    ...
}
```

```csharp
Maybe<int> maybe = ...
try
{
    int output = maybe.GetUnsafe();
}
catch (NoneStateException exception)
{
    ...
}
```

## Supported APIs

The following is a list of Vonage APIs and whether the Vonage .NET SDK provides support for them:

| API                     |  API Release Status  | Supported? |
|-------------------------|:--------------------:|:----------:|
| Number Verification API | General Availability |     ✅      |
| SimSwap API             | General Availability |     ✅      |

Pull requests are welcome!

## License

This library is released under [the MIT License][license].

[signup]: https://dashboard.nexmo.com/sign-up?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library

[doc_sms]: https://developer.nexmo.com/api/sms?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library

[doc_voice]: https://developer.nexmo.com/voice/voice-api/overview?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library

[doc_verify]: https://developer.nexmo.com/verify/overview?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library

[doc_app]: https://developer.nexmo.com/concepts/guides/applications?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library

[doc_redact]: https://developer.nexmo.com/api/redact?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library

[license]: LICENSE.md
