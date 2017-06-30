Nexmo Client Library for C#/.NET
===================================

[![](http://img.shields.io/nuget/v/Nexmo.Csharp.Client.svg?style=flat-square)](http://www.nuget.org/packages/Nexmo.Csharp.Client)
[![](http://img.shields.io/nuget/vpre/Nexmo.Csharp.Client.svg?style=flat-square)](http://www.nuget.org/packages/Nexmo.Csharp.Client)

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

Configuration:
--------------
* Provide the nexmo URLs, API key, secret, and application credentials (for JWT) in ```appsettings.json```:

```json
{
  "appSettings": {
    "Nexmo.UserAgent": "myApp/1.0",
    "Nexmo.Url.Rest": "https://rest.nexmo.com",
    "Nexmo.Url.Api": "https://api.nexmo.com",
    "Nexmo.api_key": "<YOUR KEY>",
    "Nexmo.api_secret": "<YOUR SECRET>",
    
    "Nexmo.Application.Id": "ffffffff-ffff-ffff-ffff-ffffffffffff",
    "Nexmo.Application.Key": "c:\\path\\to\\your\\application\\private.key"
  }
}
```

* In v2.1.0+, you may also continue to use ```web.config``` for configuration:

```xml
<appSettings>
  <add key="Nexmo.UserAgent" value="myApp/1.0" />
  <add key="Nexmo.Url.Rest" value="https://rest.nexmo.com" />
  <add key="Nexmo.Url.Api" value="https://api.nexmo.com" />
  <add key="Nexmo.api_key" value="<YOUR KEY>" />
  <add key="Nexmo.api_secret" value="<YOUR SECRET>" />
</appSettings>
```

* In the event multiple configuration files are found, the order of precedence is as follows:

	* ```appsettings.json``` which overrides
	* ```settings.json``` which overrides
	* ```<executing process name>.config``` which overrides
	* ```app.config``` which overrides
	* ```web.config```

* As you are able, please move your project to JSON configuration as XML
configuration will be going away in a future release.

### Configuration Reference

Key | Description
----|------------
Nexmo.api_key | Your API key from the [dashboard](https://dashboard.nexmo.com/settings)
Nexmo.api_secret | Your API secret from the [dashboard](https://dashboard.nexmo.com/settings)
Nexmo.Application.Id | Your application ID
Nexmo.Application.Key | Path to your application key
Nexmo.Url.Rest | Optional. Nexmo REST API base URL. Defaults to https://rest.nexmo.com
Nexmo.Url.Api | Optional. Nexmo API base URL. Defaults to https://api.nexmo.com
Nexmo.Api.RequestsPerSecond | Optional. Throttle to specified requests per second.
Nexmo.UserAgent | Optional. Your app-specific usage identifier in the format of `name/version`. Example: `"myApp/1.0"`

### Logging

From 2.2.0 onward, you can request console logging by placing a ```logging.json``` file alongside your ```appsettings.json``` configuration.

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

Examples
--------
We are working on a separate repository for .NET examples. [Check it out here!](https://github.com/nexmo-community/nexmo-dotnet-quickstart)

The following examples show how to:
 * [Send a message](#sending-a-message)
 * [Receive a message](#receiving-a-message)
 * [Initiate a call](#initiating-a-call)

### Sending a Message

Use [Nexmo's SMS API][doc_sms] to send a SMS message.

```C#
var results = SMS.Send(new SMS.SMSRequest
{
    from = "15555551212",
    to = "17775551212",
    text = "this is a test"
});
```

### Receiving a Message

Use [Nexmo's SMS API][doc_sms] to receive a SMS message. Assumes your Nexmo endpoint is configured.

```C#
public ActionResult Get([FromUri]SMS.SMSDeliveryReceipt response)
{
    return new HttpStatusCodeResult(HttpStatusCode.OK);
}
```

__NOTE:__ ```[FromUri]``` is deprecated in .NET Core; ```[FromQuery]``` works in this case.

### Initiating a Call

Use [Nexmo's Voice API][doc_voice] to initiate a voice call.

__NOTE:__ You must have a valid Application ID and private key in order to make voice calls. Use either ```Nexmo.Api.Application``` or Nexmo's Node.js-based [CLI tool](https://github.com/nexmo/nexmo-cli) to register. See the [Application API][doc_app] documentation for details.

```C#
using Nexmo.Api.Voice;

Call.Do(new Call.CallCommand
{
    to = new[]
    {
        new Call.Endpoint {
            type = "phone",
            number = "15555551212"
        }
    },
    from = new Call.Endpoint
    {
        type = "phone",
        number = "15557772424"
    },
    answer_url = new[]
    {
        "https://nexmo-community.github.io/ncco-examples/first_call_talk.json"
    }
});
```

### Additional Examples

* Check out the sample MVC application and tests for more examples.
Make sure to copy appsettings.json.example/web.config.example to appsettings.json/web.config and enter your key/secret.

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

Targeted frameworks:

* 4.5.2
* 4.6, 4.6.1, 4.6.2
* .NET Standard 1.6

Visual Studio 2015 is required (Community should be fine). Update 3 is recommended.

1. Get latest code either by cloning the repository or downloading a snapshot of the source.
2. Open "Nexmo.Api.sln"
3. Build! NuGet dependencies should be brought down automatically; check your settings if they are not.

Pull requests are welcome!

Thanks
------

Special thanks to our contributors:

* [jdpearce](https://github.com/jdpearce)
* [jonferreira](https://github.com/jonferreira)

License
-------

This library is released under the [MIT License][license]

[create_account]: https://docs.nexmo.com/tools/dashboard#setting-up-your-nexmo-account
[signup]: https://dashboard.nexmo.com/sign-up?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[doc_sms]: https://docs.nexmo.com/api-ref/sms-api?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[doc_voice]: https://docs.nexmo.com/voice/voice-api?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[doc_app]: https://docs.nexmo.com/tools/application-api?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[license]: LICENSE.md
