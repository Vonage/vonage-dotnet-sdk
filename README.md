Vonage Client Library for .NET
===================================

[![](http://img.shields.io/nuget/v/Vonage.svg?style=flat-square)](https://www.nuget.org/packages/Vonage/)
[![Build Status](https://github.com/Vonage/vonage-dotnet/workflows/.NET%20Core/badge.svg)](https://github.com/Nexmo/nexmo-dotnet/actions?query=workflow%3A%22.NET+Core%22)
[![codecov](https://codecov.io/gh/Vonage/vonage-dotnet-sdk/branch/master/graph/badge.svg)](https://codecov.io/gh/Vonage/vonage-dotnet-sdk)
[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-v2.0%20adopted-ff69b4.svg)](CODE_OF_CONDUCT.md)

<img src="https://developer.nexmo.com/assets/images/Vonage_Nexmo.svg" height="48px" alt="Nexmo is now known as Vonage" />

You can use this C# client library to integrate [Vonage's APIs](#api-coverage) to your application. To use this, you'll
need a Vonage API account. Sign up [for free at vonage.com][signup].

 * [Installation](#installation)
 * [Configuration](#configuration)
 * [Examples](#examples)
 * [Coverage](#api-coverage)
 * [Contributing](#contributing)

## Installation

To use the client library you'll need to have [created a Vonage account][signup].

To install the C# client library using NuGet:

* Run the following command in the Package Manager Console:

```shell
    Install-Package Vonage.Dotnet.Client
```

Alternatively:

* Download or build (see developer instructions) the `Vonage.Dotnet.Client.dll`.
* If you have downloaded a release, ensure you are referencing the required dependencies by
either including them with your project's NuGet dependencies or manually referencing them.
* Reference the assembly in your code.

## Targeted frameworks


* 4.5.2
> NOTE: for 4.5.2 frameworks you will need to enable TLS 1.2 either via [registry](https://docs.microsoft.com/en-us/dotnet/framework/network-programming/tls#for-net-framework-35---452-and-not-wcf) or by setting it globablly - `System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;`
* 4.6
* .NET Standard 2.0 - supports everything 4.6.1 and above

Configuration:
--------------

To setup the configuration of the Vonage Client you can do one of the following.

* Create a Vonage Client instance and pass in credentials in the constructor - this will only affect the security credentials (Api Key, Api Secret, Signing Secret, Signing Method Private Key, App Id)

```csharp
var credentials = Credentials.FromApiKeyAndSecret(
    VONAGE_API_KEY,
    VONAGE_API_SECRET
    );

var vonageClient = new VonageClient(credentials);
```

```csharp
var results = client.SMS.Send(request: new SMS.SMSRequest
var response = vonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
{
    To = TO_NUMBER,
    From = VONAGE_BRAND_NAME,
    Text = "A text message sent using the Vonage SMS API"
});
```

Or

* Provide the vonage URLs, API key, secret, and application credentials (for JWT) in ```appsettings.json```:

```json
{
  "appSettings": {
    "Vonage.UserAgent": "myApp/1.0",
    "Vonage.Url.Rest": "https://rest.nexmo.com",
    "Vonage.Url.Api": "https://api.nexmo.com",
    "Vonage_key": "VONAGE-API-KEY",
    "Vonage_secret": "VONAGE-API-SECRET",    
    "Vonage.Application.Id": "ffffffff-ffff-ffff-ffff-ffffffffffff",
    "Vonage.Application.Key": "VONAGE_APPLICATION_PRIVATE_KEY"
  }
}
```
> Note: In the event multiple configuration files are found, the order of precedence is as follows:

	* ```appsettings.json``` which overrides
	* ```settings.json```
Or

* Access the Configuration instance and set the appropriate key in your code for example:
```cshap
Configuration.Instance.Settings["appSettings:Vonage.Url.Api"] = "https://www.example.com/api";
Configuration.Instance.Settings["appSettings:Vonage.Url.Rest"] = "https://www.example.com/rest";
```

> NOTE: Private Key is the literal key - not a path to the file containing the key

### Configuration Reference

Key | Description
----|------------
Vonage_key | Your API key from the [dashboard](https://dashboard.nexmo.com/settings)
Vonage_secret | Your API secret from the [dashboard](https://dashboard.nexmo.com/settings)
Vonage.Application.Id | Your application ID
Vonage.Application.Key | Your application's private key
Vonage.security_secret | Optional. This is the signing secret that's used for [signing SMS](https://developer.nexmo.com/concepts/guides/signing-messages)
Vonage.signing_method | Optional. This is the method used for signing SMS messages
Vonage.Url.Rest | Optional. Vonage REST API base URL. Defaults to https://rest.nexmo.com
Vonage.Url.Api | Optional. Vonage API base URL. Defaults to https://api.nexmo.com
Vonage.RequestsPerSecond | Optional. Throttle to specified requests per second.
Vonage.UserAgent | Optional. Your app-specific usage identifier in the format of `name/version`. Example: `"myApp/1.0"`

### Logging

#### v5.0.0 +

The Library uses Microsoft.Extensions.Logging to preform all of it's logging tasks. To configure logging for you app simply create a new `ILoggerFactory` and call the `LogProvider.SetLogFactory()` method to tell the Vonage library how to log. For example, to log to the console with serilog you can do the following:

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

Your application controls if and how logging occurs. Example using [Serilog](https://serilog.net/) and [Serilog.Sinks.Console](https://www.nuget.org/packages/Serilog.Sinks.Console) v3.x:

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

You can request console logging by placing a ```logging.json``` file alongside your ```appsettings.json``` configuration.

Note that logging Vonage messages will very likely expose your key and secret to the console as they can be part of the query string.

Example ```logging.json``` contents that would log all requests as well as major configuration and authentication errors:

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

Examples
--------
We are working on a separate repository for .NET examples. [Check it out here!](https://github.com/nexmo-community/nexmo-dotnet-quickstart)

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

var response = vonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
{
    To = TO_NUMBER,
    From = VONAGE_BRAND_NAME,
    Text = "A text message sent using the Vonage SMS API"
});
```

### Receiving a Message

Use [Vonage's SMS API][doc_sms] to receive an SMS message. Assumes your Vonage webhook endpoint is configured.

The best method for receiving an SMS will vary depending on whether you configure your webhooks to be GET or POST. Will Also Vary between ASP.NET MVC and ASP.NET MVC Core.

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

The best method for receiving an SMS will vary depending on whether you configure your webhooks to be GET or POST. Will Also Vary between ASP.NET MVC and ASP.NET MVC Core.

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
var response = client.RedactClient.Redact(request);
```

### Initiating a Call

Use [Vonage's Voice API][doc_voice] to initiate a voice call.

__NOTE:__ You must have a valid Application ID and private key in order to make voice calls. Use either ```Vonage.Application``` or Vonage's Node.js-based [CLI tool](https://github.com/nexmo/nexmo-cli) to register. See the [Application API][doc_app] documentation for details.

```csharp
var creds = Credentials.FromAppIdAndPrivateKeyPath(VONAGE_APPLICATION_ID, VONAGE_PRIVATE_KEY_PATH);
var client = new VonageClient(creds);

var command = new CallCommand() { To = new Endpoint[] { toEndpoint }, From = fromEndpoint, AnswerUrl=new[] { ANSWER_URL}};
var response = client.VoiceClient.CreateCall(command);
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

var response = client.VoiceClient.GetCall(UUID);
```

### Sending 2FA Code

Use [Vonage's Verify API][doc_verify] to send 2FA pin code.

```csharp
var credentials = Credentials.FromApiKeyAndSecret(VONAGE_API_KEY, VONAGE_API_SECRET);
var client = new VonageClient(credentials);

var request = new VerifyRequest() { Brand = BRAND_NAME, Number = RECIPIENT_NUMBER };
var response = client.VerifyClient.VerifyRequest(request);
```

### Checking 2FA Code

Use [Vonage's Verify API][doc_verify] to check 2FA pin code.

```C#
var credentials = Credentials.FromApiKeyAndSecret(VONAGE_API_KEY, VONAGE_API_SECRET);
var client = new VonageClient(credentials);

var request = new VerifyCheckRequest() { Code = CODE, RequestId = REQUEST_ID };
var response = client.VerifyClient.VerifyCheck(request);
```

### Additional Examples

* Check out the sample MVC application and tests for more examples.
Make sure to copy appsettings.json.example to appsettings.json and enter your key/secret.

## Supported APIs

The following is a list of Vonage APIs and whether the Vonage .NET SDK provides support for them:

| API   | API Release Status |  Supported?
|----------|:---------:|:-------------:|
| Account API | General Availability |✅|
| Alerts API | General Availability |✅|
| Application API | General Availability |✅|
| Audit API | Beta |❌|
| Conversation API | Beta |❌|
| Dispatch API | Beta |❌|
| External Accounts API | Beta |❌|
| Media API | Beta | ❌|
| Messages API | Beta |❌|
| Number Insight API | General Availability |✅|
| Number Management API | General Availability |✅|
| Pricing API | General Availability |✅|
| Redact API | Developer Preview |✅|
| Reports API | Beta |❌|
| SMS API | General Availability |✅|
| Verify API | General Availability |✅|
| Voice API | General Availability |✅|

Contributing
------------

Visual Studio 2017 is required (Community is fine). v15.5+ is recommended.

1. Get the latest code either by cloning the repository or downloading a snapshot of the source.
2. Open "Vonage.sln"
3. Build! NuGet dependencies should be brought down automatically; check your settings if they are not.

Pull requests are welcome!

Thanks
------

Special thanks to our contributors:

* [jdpearce](https://github.com/jdpearce)
* [jonferreira](https://github.com/jonferreira)
* [fauna5](https://github.com/fauna5)
* [taylus](https://github.com/taylus)

License
-------

This library is released under [the MIT License][license].

[signup]: https://dashboard.nexmo.com/sign-up?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[doc_sms]: https://developer.nexmo.com/api/sms?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[doc_voice]: https://developer.nexmo.com/voice/voice-api/overview?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[doc_verify]: https://developer.nexmo.com/verify/overview?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[doc_app]: https://developer.nexmo.com/concepts/guides/applications?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[doc_redact]: https://developer.nexmo.com/api/redact?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[license]: LICENSE.md
