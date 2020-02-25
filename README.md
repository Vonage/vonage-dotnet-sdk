Nexmo Client Library for C#/.NET
===================================

[![](http://img.shields.io/nuget/v/Nexmo.Csharp.Client.svg?style=flat-square)](http://www.nuget.org/packages/Nexmo.Csharp.Client)
[![Build status](https://ci.appveyor.com/api/projects/status/qy0rkyi084vgjmir/branch/master?svg=true)](https://ci.appveyor.com/project/slorello89/nexmo-dotnet/branch/master)
[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-v2.0%20adopted-ff69b4.svg)](CODE_OF_CONDUCT.md)

You can use this C# client library to integrate [Nexmo's APIs](#api-coverage) to your application. To use this, you'll
need a Nexmo account. Sign up [for free at nexmo.com][signup].

 * [Installation](#installation)
 * [Configuration](#configuration)
 * [Examples](#examples)
 * [Coverage](#api-coverage)
 * [Contributing](#contributing)

Installation:
-------------
To use the client library you'll need to have [created a Nexmo account][signup].

To install the C# client library using NuGet:

* Run the following command in the Package Manager Console:

```
    Install-Package Nexmo.Csharp.Client
```

Alternatively:

* Download or build (see developer instructions) the `Nexmo.Api.dll`.
* If you have downloaded a release, ensure you are referencing the required dependencies by
either including them with your project's NuGet dependencies or manually referencing them.
* Reference the assembly in your code.

Targeted frameworks:
--------------

* 4.5.2
> NOTE: for 4.5.2 frameworks you will need to enable TLS 1.2 either via [registry](https://docs.microsoft.com/en-us/dotnet/framework/network-programming/tls#for-net-framework-35---452-and-not-wcf) or by setting it globablly - `System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;`
* 4.6, 4.6.1, 4.6.2
* .NET Standard 1.6, 2.0
* .NET Core 2.0, 2.1, 2.2, 3.0, 3.1

Configuration:
--------------

To setup the configuration of the Nexmo Client you can do one of the following.

* Create a Nexmo Client instance and pass in credentials in the constructor - this will only affect the security credentials (Api Key, Api Secret, Signing Secret, Signing Method Private Key, App Id)

```csharp
var client = new Client(creds: new Nexmo.Api.Request.Credentials
                {
                    ApiKey = "NEXMO-API-KEY",
                    ApiSecret = "NEXMO-API-SECRET"
                });
```

```csharp
var results = client.SMS.Send(request: new SMS.SMSRequest
                {
 
                    from = NEXMO_NUMBER,
                    to = TO_NUMBER,
                    text = "Hello, I'm an SMS sent to you using Nexmo"
                });
```

Or

* Provide the nexmo URLs, API key, secret, and application credentials (for JWT) in ```appsettings.json```:

```json
{
  "appSettings": {
    "Nexmo.UserAgent": "myApp/1.0",
    "Nexmo.Url.Rest": "https://rest.nexmo.com",
    "Nexmo.Url.Api": "https://api.nexmo.com",
    "Nexmo.api_key": "NEXMO-API-KEY",
    "Nexmo.api_secret": "NEXMO-API-SECRET",
    
    "Nexmo.Application.Id": "ffffffff-ffff-ffff-ffff-ffffffffffff",
    "Nexmo.Application.Key": "NEXMO_APPLICATION_PRIVATE_KEY"
  }
}
```
> Note: In the event multiple configuration files are found, the order of precedence is as follows:

	* ```appsettings.json``` which overrides
	* ```settings.json```
Or

* Access the Configuration instance and set the appropriate key in your code for example:
```cshap
Configuration.Instance.Settings["appSettings:Nexmo.Url.Api"] = "https://www.example.com/api";
Configuration.Instance.Settings["appSettings:Nexmo.Url.Rest"] = "https://www.example.com/rest";
```

> NOTE: Private Key is the literal key - not a path to the file containing the key

### Configuration Reference

Key | Description
----|------------
Nexmo.api_key | Your API key from the [dashboard](https://dashboard.nexmo.com/settings)
Nexmo.api_secret | Your API secret from the [dashboard](https://dashboard.nexmo.com/settings)
Nexmo.Application.Id | Your application ID
Nexmo.Application.Key | Your application's private key
Nexmo.security_secret | Optional. This is the signing secret that's used for [signing SMS](https://developer.nexmo.com/concepts/guides/signing-messages)
Nexmo.signing_method | Optional. This is the method used for signing SMS messages
Nexmo.Url.Rest | Optional. Nexmo REST API base URL. Defaults to https://rest.nexmo.com
Nexmo.Url.Api | Optional. Nexmo API base URL. Defaults to https://api.nexmo.com
Nexmo.Api.RequestsPerSecond | Optional. Throttle to specified requests per second.
Nexmo.Api.EnsureSuccessStatusCode | Optional. Defaults to `false`. If `true`, `EnsureSuccessStatusCode` will be called against each response. If the response has a failure HTTP status code, a `HttpRequestException` will be thrown.
Nexmo.UserAgent | Optional. Your app-specific usage identifier in the format of `name/version`. Example: `"myApp/1.0"`

### Logging

#### 3.1.x+

The library makes use of [LibLog](https://github.com/damianh/LibLog/wiki) to facilitate logging.

Your application controls if and how logging occurs. Example using [Serilog](https://serilog.net/) and [Serilog.Sinks.Console](https://www.nuget.org/packages/Serilog.Sinks.Console) v3.x:

```C#
using Nexmo.Api.Request;
using Serilog;

// set up logging at startup
var log = new LoggerConfiguration()
  .MinimumLevel.Debug()
  .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name:l}) {Message}")
  .CreateLogger();
Log.Logger = log;

Log.Logger.Debug("start");
var client = new Nexmo.Api.Client(new Credentials("example", "password"));
client.Account.GetBalance();
Log.Logger.Debug("end");
```

#### 2.2.0 - 3.0.x

You can request console logging by placing a ```logging.json``` file alongside your ```appsettings.json``` configuration.

Note that logging Nexmo.Api messages will very likely expose your key and secret to the console as they can be part of the query string.

Example ```logging.json``` contents that would log all requests as well as major configuration and authentication errors:

```json
{
  "IncludeScopes": "true",
  "LogLevel": {
    "Default": "Debug",
    "Nexmo.Api": "Debug",
    "Nexmo.Api.Authentication": "Error",
    "Nexmo.Api.Configuration": "Error"
  }
}
```

You may specify other types of logging (file, etc.). The ```Nexmo.Samples.Coverage``` project contains an example that logs to a file with the assistance of ```Serilog.Extensions.Logging.File```.

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

Use [Nexmo's SMS API][doc_sms] to send an SMS message.

```C#
var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                  ApiKey = "NEXMO_API_KEY",
                  ApiSecret = "NEXMO_API_SECRET"
            });
```

```C#
var results = client.SMS.Send(new SMS.SMSRequest
{
    from = NEXMO_NUMBER,
    to = TO_NUMBER,
    text = "this is a test"
});
```

### Receiving a Message

Use [Nexmo's SMS API][doc_sms] to receive an SMS message. Assumes your Nexmo endpoint is configured.

```C#
public ActionResult Get([FromUri]SMS.SMSInbound response)
{
    return new HttpStatusCodeResult(HttpStatusCode.OK);
}
```

### Receiving a Message Delivery Receipt

Use [Nexmo's SMS API][doc_sms] to receive an SMS delivery receipt. Assumes your Nexmo endpoint is configured.

```C#
public ActionResult DLR([FromUri]SMS.SMSDeliveryReceipt response)
{
    Debug.WriteLine("-------------------------------------------------------------------------");
    Debug.WriteLine("DELIVERY RECEIPT");
    Debug.WriteLine("Message ID: " + response.messageId);
    Debug.WriteLine("From: " + response.msisdn);
    Debug.WriteLine("To: " + response.to);
    Debug.WriteLine("Status: " + response.status);
    Debug.WriteLine("-------------------------------------------------------------------------");

    return new HttpStatusCodeResult(HttpStatusCode.OK);
}
```

__NOTE:__ ```[FromUri]``` is deprecated in .NET Core; ```[FromQuery]``` works in this case.

### Redacting a message

Use [Nexmo's Redact API][doc_redact] to redact a SMS message.

```C#
var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                  ApiKey = "NEXMO_API_KEY",
                  ApiSecret = "NEXMO_API_SECRET"
            });
```

```C#
client.Redact.RedactTransaction(new Redact.RedactRequest(MESSAGE_ID, "sms", "outbound"));
```

### Initiating a Call

Use [Nexmo's Voice API][doc_voice] to initiate a voice call.

__NOTE:__ You must have a valid Application ID and private key in order to make voice calls. Use either ```Nexmo.Api.Application``` or Nexmo's Node.js-based [CLI tool](https://github.com/nexmo/nexmo-cli) to register. See the [Application API][doc_app] documentation for details.

```C#
var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = "NEXMO_API_KEY",
                ApiSecret = "NEXMO_API_SECRET",
                ApplicationId = "NEXMO_APPLICATION_ID",
                ApplicationKey = "NEXMO_APPLICATION_PRIVATE_KEY"
            }
```

```C#
using Nexmo.Api.Voice;

client.Call.Do(new Call.CallCommand
{
    to = new[]
    {
        new Call.Endpoint {
            type = "phone",
            number = TO_NUMBER
        }
    },
    from = new Call.Endpoint
    {
        type = "phone",
        number = NEXMO_NUMBER
    },
    answer_url = new[]
    {
        "https://nexmo-community.github.io/ncco-examples/first_call_talk.json"
    }
});
```
### Receiving a Call

Use [Nexmo's Voice API][doc_voice] to receive a voice call.

```C#
var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = "NEXMO_API_KEY",
                ApiSecret = "NEXMO_API_SECRET",
                ApplicationId = "NEXMO_APPLICATION_ID",
                ApplicationKey = "NEXMO_APPLICATION_PRIVATE_KEY"
            }
```

```C#
using Nexmo.Api.Voice;

public ActionResult GetCall(string id)
{
    var call = client.Call.Get(id);
    // Do something with call.
}
```
### Sending 2FA Code

Use [Nexmo's Verify API][doc_verify] to send 2FA pin code.

```C#
var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = "NEXMO_API_KEY",
                ApiSecret = "NEXMO_API_SECRET"
            }
```

```C#

public ActionResult Start(string to)
{
    var start = client.NumberVerify.Verify(new NumberVerify.VerifyRequest
    {
        number = to,
        brand = "NexmoQS"
    });
    Session["requestID"] = start.request_id;

    return View();
}
```
### Checking 2FA Code

Use [Nexmo's Verify API][doc_verify] to check 2FA pin code.

```C#
var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = "NEXMO_API_KEY",
                ApiSecret = "NEXMO_API_SECRET"
            }
```

```C#

public ActionResult Check(string code)
{
    var result = client.NumberVerify.Check(new NumberVerify.CheckRequest
    {
        request_id = Session["requestID"].ToString(),
        code = code
    });
   
    if (result.status == "0")
    {
        ViewBag.Message = "Verification Sucessful";
    }
    else
    {
        ViewBag.Message = result.error_text;
    }
    return View();
}
```

### Additional Examples

* Check out the sample MVC application and tests for more examples.
Make sure to copy appsettings.json.example to appsettings.json and enter your key/secret.

API Coverage
------------

* Account
    * [X] Balance
    * [X] Pricing
    * [X] Settings
    * [X] Top Up
    * [X] Numbers
        * [X] Search
        * [X] Buy
        * [X] Cancel
        * [X] Update
* Number Insight
    * [X] Basic
    * [X] Standard
    * [X] Advanced
    * [X] Webhook Notification
* Verify
    * [X] Verify
    * [X] Check
    * [X] Search
    * [X] Control
* Search
    * [X] Message
    * [X] Messages
    * [X] Rejections
* Messaging
    * [X] Send
    * [X] Delivery Receipt
    * [X] Inbound Messages
    * [X] Search
        * [X] Message
        * [X] Messages
        * [X] Rejections
    * US Short Codes
        * [X] Two-Factor Authentication
        * [X] Event Based Alerts
            * [X] Sending Alerts
            * [X] Campaign Subscription Management
* Application
	* [X] Create
	* [X] List
	* [X] Update
	* [X] Delete
* Call
    * [X] Outbound
    * [X] Get
    * [X] List
    * [X] Edit
    * [X] TTS
    * [X] Stream
    * [X] DTMF

Contributing
------------

Visual Studio 2017 is required (Community is fine). v15.5+ is recommended.

1. Get the latest code either by cloning the repository or downloading a snapshot of the source.
2. Open "Nexmo.Api.sln"
3. Build! NuGet dependencies should be brought down automatically; check your settings if they are not.

Pull requests are welcome!

Thanks
------

Special thanks to our contributors:

* [jdpearce](https://github.com/jdpearce)
* [jonferreira](https://github.com/jonferreira)
* [fauna5](https://github.com/fauna5)

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
