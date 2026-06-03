# Vonage .NET SDK

Official .NET client library for [Vonage APIs](https://developer.vonage.com); SMS, Voice, Video, Verify, Messages, and
more.

> This site documents the **latest release**. If you are pinned to an older version, the compiled XML docs in your
> installed package (IntelliSense) match your version.

## Install

```shell
dotnet add package Vonage
```

```powershell
Install-Package Vonage
```

## Quick start

A minimal verification call. For authentication options, dependency injection, configuration, and logging,
see [Getting Started](docs/getting-started.md).

```csharp
using Vonage;
using Vonage.Request;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Sms;

var client = new VonageClient(
    Credentials.FromAppIdAndPrivateKeyPath("YOUR_APP_ID", "private.key"));

var result = await client.VerifyV2Client.StartVerificationAsync(
    StartVerificationRequest.Build()
        .WithBrand("MyApp")
        .WithWorkflow(SmsWorkflow.Parse("447700900000"))
        .Create());

result.Match(
    successOperation: response => Console.WriteLine($"Request ID: {response.RequestId}"),
    failureOperation: failure  => Console.WriteLine($"Error: {failure.GetFailureMessage()}"));
```

## Where to go next

- [Getting Started](docs/getting-started.md): install, authenticate, and make your first call
- [Working with Result&lt;T&gt;](docs/working-with-result.md): the SDK's error-handling pattern
- [API Reference](xref:Vonage): full type and member documentation
- [Code Snippets](https://github.com/Vonage/vonage-dotnet-code-snippets): runnable examples for every API
- [Vonage Developer Portal](https://developer.vonage.com): API guides and account setup