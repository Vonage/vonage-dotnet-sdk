# Getting Started

This guide walks you through installing the SDK, setting up a client, and making your first API call.

## Prerequisites

- A [Vonage account](https://dashboard.nexmo.com/sign-up)
- .NET 6, 7, or 8 (or any runtime that supports `netstandard2.0`)

## Install

```shell
dotnet add package Vonage
```

```powershell
Install-Package Vonage
```

## Configure

The SDK supports two authentication methods depending on the API you're calling:

- **API Key + Secret** used by legacy APIs (SMS, Voice, Number Insight v1, etc.)
- **Application ID + Private Key** used by modern APIs (Verify v2, Messages, Video, Conversations, etc.)

Your credentials are available in the [Vonage Dashboard](https://dashboard.nexmo.com).

### Dependency injection (recommended for .NET Core and above)

Store credentials in `appsettings.json` under the `vonage` key:

```json
{
  "vonage": {
    "Api.Key":         "YOUR_API_KEY",
    "Api.Secret":      "YOUR_API_SECRET",
    "Application.Id":  "YOUR_APP_ID",
    "Application.Key": "YOUR_PRIVATE_KEY_CONTENTS"
  }
}
```

Then register the client in your `IServiceCollection`:

```csharp
// Scoped lifecycle (typical for web apps):
builder.Services.AddVonageClientScoped(builder.Configuration);

// Transient lifecycle:
builder.Services.AddVonageClientTransient(builder.Configuration);
```

This registers `VonageClient` and every sub-client interface (`IMessagesClient`, `IVerifyV2Client`, `IVoiceClient`,
etc.) so they can be injected wherever needed.

If you prefer not to use `appsettings.json`, you can pass credentials directly:

```csharp
var credentials = Credentials.FromAppIdAndPrivateKeyPath("YOUR_APP_ID", "private.key");
builder.Services.AddVonageClientScoped(credentials);
```

> **Note:** `Application.Key` in `appsettings.json` is the literal private key content, not a file path. Use
`FromAppIdAndPrivateKeyPath` when you have a key file.

### Manual initialization (.NET Framework or without DI)

For .NET Framework projects, or when you need a client without a DI container, construct one directly:

```csharp
// API Key + Secret:
var client = new VonageClient(Credentials.FromApiKeyAndSecret("YOUR_API_KEY", "YOUR_API_SECRET"));

// Application ID + Private Key:
var client = new VonageClient(Credentials.FromAppIdAndPrivateKeyPath("YOUR_APP_ID", "private.key"));
```

Individual settings can also be overridden on the `Configuration` singleton, though this is deprecated and will be
removed in a future release:

```csharp
Configuration.Instance.Settings["vonage:Url.Api"] = "https://www.example.com/api";
```

## Configuration reference

| Key                           | Description                                                                                             |
|-------------------------------|---------------------------------------------------------------------------------------------------------|
| `Api.Key`                     | Your API key from the [dashboard](https://dashboard.nexmo.com/settings)                                 |
| `Api.Secret`                  | Your API secret from the [dashboard](https://dashboard.nexmo.com/settings)                              |
| `Application.Id`              | Your application ID                                                                                     |
| `Application.Key`             | Your application's private key (literal key content)                                                    |
| `Security_secret`             | Optional. Signing secret for [signed SMS](https://developer.nexmo.com/concepts/guides/signing-messages) |
| `Signing_method`              | Optional. Method used for signing SMS messages                                                          |
| `Url.Rest`                    | Optional. Defaults to `https://rest.nexmo.com`                                                          |
| `Url.Api`                     | Optional. Defaults to `https://api.nexmo.com`                                                           |
| `Url.Api.EMEA`                | Optional. Defaults to `https://api-eu.vonage.com`                                                       |
| `Url.Api.AMER`                | Optional. Defaults to `https://api-us.vonage.com`                                                       |
| `Url.Api.APAC`                | Optional. Defaults to `https://api-ap.vonage.com`                                                       |
| `Url.Api.Video`               | Optional. Defaults to `https://video.api.vonage.com`                                                    |
| `RequestsPerSecond`           | Optional. Throttle to this many requests per second                                                     |
| `RequestTimeout`              | Optional. Per-request timeout in seconds                                                                |
| `UserAgent`                   | Optional. App identifier in `name/version` format, e.g. `myApp/1.0`                                     |
| `PooledConnectionIdleTimeout` | Optional. Seconds before an idle connection is closed. Defaults to 60                                   |
| `PooledConnectionLifetime`    | Optional. Seconds before a connection is recycled. Defaults to 600                                      |
| `Proxy`                       | Optional. Custom proxy URL for all HTTP requests                                                        |

## Logging

The SDK uses `Microsoft.Extensions.Logging`. Provide an `ILoggerFactory` via `LogProvider.SetLogFactory()`:

```csharp
using Microsoft.Extensions.Logging;
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

## Make your first call

The example below starts a phone-number verification using [Verify v2](xref:Vonage.VerifyV2):

```csharp
using Vonage;
using Vonage.Request;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Sms;

IVerifyV2Client client = new VonageClient(Credentials.FromAppIdAndPrivateKey("YOUR_APP_ID", "YOUR_PRIVATE_KEY"));

// The builder prevents invalid requests from being constructed.
var request = StartVerificationRequest.Build()
    .WithBrand("MyApp")
    .WithWorkflow(SmsWorkflow.Parse("447700900000"))
    .Create();

// All modern API calls return Result<T>.
var result = await client.VerifyV2Client.StartVerificationAsync(request);

// Both outcomes are handled explicitly. No exceptions to catch.
result.Match(
    successOperation: response => Console.WriteLine($"Verification started: {response.RequestId}"),
    failureOperation: failure  => Console.WriteLine($"Error: {failure.GetFailureMessage()}"));
```

Validation failures, HTTP errors, and not-found responses all arrive through the same `Result<T>` return value.
See [Working with Result&lt;T&gt;](working-with-result.md) for more details.

## Next steps

- [Working with Result&lt;T&gt;](working-with-result.md) — handle results, chain calls, and escape hatches
- [API Reference](xref:Vonage) — full type and member documentation
- [Code Snippets](https://github.com/Vonage/vonage-dotnet-code-snippets) — runnable examples for every API
