Vonage Client Library for .NET
===================================

[![](http://img.shields.io/nuget/v/Vonage.svg?style=flat-square)](https://www.nuget.org/packages/Vonage/)
[![Build Status](https://github.com/Vonage/vonage-dotnet-sdk/actions/workflows/net-build.yml/badge.svg)](https://github.com/Vonage/vonage-dotnet-sdk/actions/workflows/net-build.yml/badge.svg)
[![MultiFramework Build Status](https://github.com/Vonage/vonage-dotnet-sdk/actions/workflows/multiframework-build.yml/badge.svg)](https://github.com/Vonage/vonage-dotnet-sdk/actions/workflows/multiframework-build.yml/badge.svg)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Vonage_vonage-dotnet-sdk&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Vonage_vonage-dotnet-sdk)
[![CodeScene Code Health](https://codescene.io/projects/29782/status-badges/code-health)](https://codescene.io/projects/29782)
[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-v2.0%20adopted-ff69b4.svg)](CODE_OF_CONDUCT.md)

You can use this C# client library to integrate [Vonage's APIs](#supported-apis) to your application.
To use this, you'll need a Vonage API account.
Sign up [for free at vonage.com][signup].

* [Installation](#installation)
    * [Migration guides](#migration-guides)
        * [Upgrading 5.x => 6.x](#upgrading-5x--6x)
    * [Targeted frameworks](#targeted-frameworks)
    * [Tested frameworks](#tested-frameworks)
* [Configuration](#configuration)
    * [Setup](#setup)
        * [Provide credentials](#provide-credentials)
        * [Set credentials in settings file](#set-credentials-in-settings-file)
        * [Override values on Configuration singleton](#override-values-in-configuration-singleton)
    * [Configuration reference](#configuration-reference)
    * [Test configuration](#test-configuration)
    * [Logging](#logging)
        * [v5.0.0+](#v500-)
        * [3.1.x, 5.0.0](#-31x-500-)
        * [2.2.0, 3.0.x](#220---30x)
* [Monads](#monads)
    * [Result](#result)
    * [Maybe](#maybe)
    * [What if you don't want to use Monads?](#what-if-you-dont-want-to-use-monads)
* [Examples](#examples)
* [Supported APIs](#supported-apis)
* [FAQ](#faq)
* [Contributing](#contributing)
* [Thanks](#thanks)
* [Licence](#license)

## Installation

To use the client library you'll need to have [created a Vonage account][signup].

To install the C# client library using NuGet:

* Run the following command from your terminal in your projects directory:

```shell
dotnet add package Vonage
```

OR

* Run the following command in the Package Manager Console:

```shell
Install-Package Vonage
```

If you would prefer to run directly from source:

* Clone this repository `git clone https://github.com/vonage/vonage-dotnet-sdk`
* Add the Vonage/Vonage.csproj file to your .sln file
* Add the Vonage/Vonage.csproj file as a project dependency of you project e.g.

```xml
<ItemGroup>
    <ProjectReference Include="..\Vonage\Vonage.csproj" />
</ItemGroup>
```

### Migration guides

#### Upgrading 5.x > 6.x

Changes in version 6.x

* Enum values are now caplitalised in alignment with accepted coding practices and are Pascal Case
* All classes that were marked as deprecated in 5.x are now removed
* Ncco now inherits from List, it no longer has the `Actions` property, to add an action use `ncco.Add(action);`
* Strings with values "true" or "false" are now represented as `bool` in code

### Targeted frameworks

The SDK targets towards `netstandard2.0`.
It is compatible with every [supported version](#tested-frameworks).

### Tested frameworks

We test the SDK against every supported version of the framework.
Therefore, we ensure complete compatibility no matter the version you are using.

* .NET Framework 4.6.2
* .NET Framework 4.7.0
* .NET Framework 4.7.1
* .NET Framework 4.7.2
* .NET Framework 4.8.0
* .NET Framework 4.8.1
* .NET 6.0
* .NET 6.0-Windows
* .NET 7.0
* .NET 7.0-Windows

## Configuration

### Setup

To setup the configuration of the Vonage Client you can do one of the following.

### Provide credentials

Create a Vonage Client instance and pass in credentials in the constructor - this will only affect the security
credentials (Api Key, Api Secret, Signing Secret, Signing Method Private Key, App Id)

```csharp
var credentials = Credentials.FromApiKeyAndSecret(
    VONAGE_API_KEY,
    VONAGE_API_SECRET
    );

var vonageClient = new VonageClient(credentials);
```

### Set credentials in settings file

Provide the vonage URLs, API key, secret, and application credentials (for JWT) in ```appsettings.json```:

```json
{
  "appSettings": {
    "Vonage.UserAgent": "myApp/1.0",
    "Vonage.Url.Rest": "https://rest.nexmo.com",
    "Vonage.Url.Api": "https://api.nexmo.com",
    "Vonage.Meetings.Url.Api": "https://api-eu.vonage.com",
    "Vonage.Video.Url.Api": "https://video.api.vonage.com",
    "Vonage_key": "VONAGE-API-KEY",
    "Vonage_secret": "VONAGE-API-SECRET",    
    "Vonage.Application.Id": "ffffffff-ffff-ffff-ffff-ffffffffffff",
    "Vonage.Application.Key": "VONAGE_APPLICATION_PRIVATE_KEY"
  }
}
```

> Note: In the event multiple configuration files are found, the order of precedence is as follows:
> ```appsettings.json``` which overrides ```settings.json```

### Override values in Configuration singleton

Access the Configuration instance and set the appropriate key in your code for example:

```cshap
Configuration.Instance.Settings["appSettings:Vonage.Url.Api"] = "https://www.example.com/api";
Configuration.Instance.Settings["appSettings:Vonage.Url.Rest"] = "https://www.example.com/rest";
Configuration.Instance.Settings["appSettings:Vonage.Meetings.Url.Api"] = "https://www.meetings.example.com/api";
Configuration.Instance.Settings["appSettings:Vonage.Video.Url.Rest"] = "https://www.video.example.com/rest";
```

> NOTE: Private Key is the literal key - not a path to the file containing the key

### Configuration Reference

| Key                      | Description                                                                                                                      |
|--------------------------|----------------------------------------------------------------------------------------------------------------------------------|
| Vonage_key               | Your API key from the [dashboard](https://dashboard.nexmo.com/settings)                                                          |
| Vonage_secret            | Your API secret from the [dashboard](https://dashboard.nexmo.com/settings)                                                       |
| Vonage.Application.Id    | Your application ID                                                                                                              |
| Vonage.Application.Key   | Your application's private key                                                                                                   |
| Vonage.security_secret   | Optional. This is the signing secret that's used for [signing SMS](https://developer.nexmo.com/concepts/guides/signing-messages) |
| Vonage.signing_method    | Optional. This is the method used for signing SMS messages                                                                       |
| Vonage.Url.Rest          | Optional. Vonage REST API base URL. Defaults to https://rest.nexmo.com                                                           |
| Vonage.Url.Api           | Optional. Vonage API base URL. Defaults to https://api.nexmo.com                                                                 |
| Vonage.Meetings.Url.Api  | Optional. Vonage API base URL for Meetings. Defaults to https://api-eu.vonage.com                                                |
| Vonage.Video.Url.Api     | Optional. Vonage API base URL for Video. Defaults to https://video.api.vonage.com                                                |
| Vonage.RequestsPerSecond | Optional. Throttle to specified requests per second.                                                                             |
| Vonage.UserAgent         | Optional. Your app-specific usage identifier in the format of `name/version`. Example: `"myApp/1.0"`                             |

### Test configuration

Make sure to set `Vonage.Test.RsaPrivateKey` (with a RSA Private Key) in your environment variables.
Some tests rely on that to verify a token can be created.

For security reasons, not RSA Private Key is hardcoded in the repository.

### Logging

#### v5.0.0 +

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

#### [3.1.x, 5.0.0)

The library makes use of [LibLog](https://github.com/damianh/LibLog/wiki) to facilitate logging.

Your application controls if and how logging occurs. Example using [Serilog](https://serilog.net/)
and [Serilog.Sinks.Console](https://www.nuget.org/packages/Serilog.Sinks.Console) v3.x:

```C#
using Vonage.Request;
using Serilog;

// set up logging at startup
var log = new LoggerConfiguration()
  .MinimumLevel.Debug()
  .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name:l}) {Message}")
  .CreateLogger();
Log.Logger = log;

Log.Logger.Debug("start");
var client = new Vonage.VonageClient(new Credentials("example", "password"));
client.Account.GetBalance();
Log.Logger.Debug("end");
```

#### 2.2.0 - 3.0.x

You can request console logging by placing a ```logging.json``` file alongside your ```appsettings.json```
configuration.

Note that logging Vonage messages will very likely expose your key and secret to the console as they can be part of the
query string.

Example ```logging.json``` contents that would log all requests as well as major configuration and authentication
errors:

```json
{
  "IncludeScopes": "true",
  "LogLevel": {
    "Default": "Debug",
    "Vonage": "Debug",
    "Vonage.Authentication": "Error",
    "Vonage.Configuration": "Error"
  }
}
```

You may specify other types of logging (file, etc.).

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

### How to extract the value from the Monad

Given we cannot predict the state at build-time, we need to provide a process for each scenario.

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

#### Map

#### Bind

#### Implicit conversion

#### Async Support

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

## Examples

We are working on a separate repository for .NET
examples. [Check it out here!](https://github.com/Vonage/vonage-dotnet-code-snippets)

The following examples show how to:

* [Send a message](#sending-a-message)
* [Receive a message](#receiving-a-message)
* [Receive a message delivery receipt](#receiving-a-message-delivery-receipt)
* [Redact a message](#redacting-a-message)
* [Initiate a call](#initiating-a-call)
* [Receive a call](#receiving-a-call)
* [Send 2FA code](#sending-2fa-code)
* [Check 2FA code](#checking-2fa-code)

### Sending a Message

Use [Vonage's SMS API][doc_sms] to send an SMS message.

```csharp
var credentials = Credentials.FromApiKeyAndSecret(
    VONAGE_API_KEY,
    VONAGE_API_SECRET
    );

var vonageClient = new VonageClient(credentials);

var response = await vonageClient.SmsClient.SendAnSmsAsync(new Vonage.Messaging.SendSmsRequest()
{
    To = TO_NUMBER,
    From = VONAGE_BRAND_NAME,
    Text = "A text message sent using the Vonage SMS API"
});
```

### Receiving a Message

Use [Vonage's SMS API][doc_sms] to receive an SMS message. Assumes your Vonage webhook endpoint is configured.

The best method for receiving an SMS will vary depending on whether you configure your webhooks to be GET or POST. Will
Also Vary between ASP.NET MVC and ASP.NET MVC Core.

#### ASP.NET MVC Core

##### GET

```csharp
[HttpGet("webhooks/inbound-sms")]
public async Task<IActionResult> InboundSmsGet()
{
    var inbound = Vonage.Utility.WebhookParser.ParseQuery<InboundSms>(Request.Query);
    return NoContent();
}
```

##### POST

```csharp
[HttpPost("webhooks/inbound-sms")]
public async Task<IActionResult> InboundSms()
{
    var inbound = await Vonage.Utility.WebhookParser.ParseWebhookAsync<InboundSms>(Request.Body, Request.ContentType);
    return NoContent();
}
```

#### ASP.NET MVC

##### GET

```csharp
[HttpGet]
[Route("webhooks/inbound-sms")]
public async Task<HttpResponseMessage> GetInbound()
{
    var inboundSms = WebhookParser.ParseQueryNameValuePairs<InboundSms>(Request.GetQueryNameValuePairs());
    return new HttpResponseMessage(HttpStatusCode.NoContent);
}
```

##### POST

```csharp
[HttpPost]
[Route("webhooks/inbound-sms")]
public async Task<HttpResponseMessage> PostInbound()
{
    var inboundSms = WebhookParser.ParseWebhook<InboundSms>(Request);
    return new HttpResponseMessage(HttpStatusCode.NoContent);
}
```

### Receiving a Message Delivery Receipt

Use [Vonage's SMS API][doc_sms] to receive an SMS delivery receipt. Assumes your Vonage webhook endpoint is configured.

The best method for receiving an SMS will vary depending on whether you configure your webhooks to be GET or POST. Will
Also Vary between ASP.NET MVC and ASP.NET MVC Core.

#### ASP.NET MVC Core

##### GET

```csharp
[HttpGet("webhooks/dlr")]
public async Task<IActionResult> InboundSmsGet()
{
    var dlr = Vonage.Utility.WebhookParser.ParseQuery<DeliveryReceipt>(Request.Query);
    return NoContent();
}
```

##### POST

```csharp
[HttpPost("webhooks/dlr")]
public async Task<IActionResult> InboundSms()
{
    var dlr = await Vonage.Utility.WebhookParser.ParseWebhookAsync<DeliveryReceipt>(Request.Body, Request.ContentType);
    return NoContent();
}
```

#### ASP.NET MVC

##### GET

```csharp
[HttpGet]
[Route("webhooks/dlr")]
public async Task<HttpResponseMessage> GetInbound()
{
    var dlr = WebhookParser.ParseQueryNameValuePairs<DeliveryReceipt>(Request.GetQueryNameValuePairs());
    return new HttpResponseMessage(HttpStatusCode.NoContent);
}
```

##### POST

```csharp
[HttpPost]
[Route("webhooks/dlr")]
public async Task<HttpResponseMessage> PostInbound()
{
    var dlr = WebhookParser.ParseWebhook<DeliveryReceipt>(Request);
    return new HttpResponseMessage(HttpStatusCode.NoContent);
}
```

### Redacting a message

Use [Vonage's Redact API][doc_redact] to redact a SMS message.

```csharp
var credentials = Credentials.FromApiKeyAndSecret(VONAGE_API_KEY, VONAGE_API_SECRET);
var client = new VonageClient(credentials);
var request = new RedactRequest() { Id = VONAGE_REDACT_ID, Type = VONAGE_REDACT_TYPE, Product = VONAGE_REDACT_PRODUCT };
var response = await client.RedactClient.RedactAsync(request);
```

### Initiating a Call

Use [Vonage's Voice API][doc_voice] to initiate a voice call.

__NOTE:__ You must have a valid Application ID and private key in order to make voice calls. Use
either ```Vonage.Application``` or Vonage's Node.js-based [CLI tool](https://github.com/nexmo/nexmo-cli) to register.
See the [Application API][doc_app] documentation for details.

```csharp
var creds = Credentials.FromAppIdAndPrivateKeyPath(VONAGE_APPLICATION_ID, VONAGE_PRIVATE_KEY_PATH);
var client = new VonageClient(creds);

var command = new CallCommand() { To = new Endpoint[] { toEndpoint }, From = fromEndpoint, AnswerUrl=new[] { ANSWER_URL}};
var response = await client.VoiceClient.CreateCallAsync(command);
```

### Receiving a Call

Use [Vonage's Voice API][doc_voice] to receive a voice call.

```csharp
[HttpGet("webhooks/answer")]
public string Answer()
{
    var talkAction = new TalkAction()
    {
        Text = $"Thank you for calling from " +
        $"{string.Join(" ", Request.Query["from"].ToString().ToCharArray())}"
    };
    var ncco = new Ncco(talkAction);
    return ncco.ToString();
}
```

### Get Details About a Call

```csharp
var credentials = Credentials.FromAppIdAndPrivateKeyPath(VONAGE_APPLICATION_ID, VONAGE_PRIVATE_KEY_PATH);
var client = new VonageClient(credentials);

var response = await client.VoiceClient.GetCallAsync(UUID);
```

### Sending 2FA Code

Use [Vonage's Verify API][doc_verify] to send 2FA pin code.

```csharp
var credentials = Credentials.FromApiKeyAndSecret(VONAGE_API_KEY, VONAGE_API_SECRET);
var client = new VonageClient(credentials);

var request = new VerifyRequest() { Brand = BRAND_NAME, Number = RECIPIENT_NUMBER };
var response = await client.VerifyClient.VerifyRequestAsync(request);
```

### Checking 2FA Code

Use [Vonage's Verify API][doc_verify] to check 2FA pin code.

```C#
var credentials = Credentials.FromApiKeyAndSecret(VONAGE_API_KEY, VONAGE_API_SECRET);
var client = new VonageClient(credentials);

var request = new VerifyCheckRequest() { Code = CODE, RequestId = REQUEST_ID };
var response = await client.VerifyClient.VerifyCheckAsync(request);
```

### Additional Examples

* Check out the sample MVC application and tests for more examples.
  Make sure to copy appsettings.json.example to appsettings.json and enter your key/secret.

## Supported APIs

The following is a list of Vonage APIs and whether the Vonage .NET SDK provides support for them:

| API                   |  API Release Status  | Supported? |
|-----------------------|:--------------------:|:----------:|
| Account API           | General Availability |     ✅      |
| Alerts API            | General Availability |     ✅      |
| Application API       | General Availability |     ✅      |
| Audit API             |         Beta         |     ❌      |
| Conversation API      |         Beta         |     ❌      |
| Dispatch API          |         Beta         |     ❌      |
| External Accounts API |         Beta         |     ❌      |
| Media API             |         Beta         |     ❌      |
| Meetings API          |         Beta         |     ✅      |
| Messages API          | General Availability |     ✅      |
| Number Insight API    | General Availability |     ✅      |
| Number Management API | General Availability |     ✅      |
| Pricing API           | General Availability |     ✅      |
| Redact API            |  Developer Preview   |     ✅      |
| Reports API           |         Beta         |     ❌      |
| SMS API               | General Availability |     ✅      |
| Verify API            | General Availability |     ✅      |
| Video API             |         Beta         |     ✅      |
| Voice API             | General Availability |     ✅      |

## FAQ

Q: Does the .NET SDK Support the async pattern?
A: Yes. All methods either support asynchronous behaviours by default or provide specific behaviours for each sync/async
option.

## Contributing

Pick your preferred IDE:

- Visual Studio (Community is fine)
- Visual Studio Code
- Jetbrains Rider

Keep in mind the SDK is built on `netstandard2.0` and tested against several framework versions (v4.6.2 & upper, and
.Net6.0).
Therefore, they should be installed on your machine to guarantee compatibility with all supported framework versions.

1. Get the latest code either by cloning the repository or downloading a snapshot of the source.
2. Open "Vonage.sln"
3. Build! NuGet dependencies should be brought down automatically; check your settings if they are not.
4. Tests! Run all the tests to verify everything's fine.

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
