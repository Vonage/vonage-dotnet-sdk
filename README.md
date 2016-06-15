Nexmo Client Library for C#/.NET
===================================

You can use this C# client library to add [Nexmo's API](#api-coverage) to your application. To use this, you'll
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

* Download or build (see developer instructions) the Nexmo.Api.dll.
* If you have downloaded a release, ensure you are referencing the
Newtonsoft.Json.dll dependency by either including it with your project's
NuGet dependencies or manually referencing it.
* Reference the assembly in your code.

Configuration:
--------------
* Provide your API key, secret, and nexmo URLs in appSettings:

```XML
<add key="Nexmo.Url.Rest" value="https://rest.nexmo.com" />
<add key="Nexmo.Url.Api" value="https://api.nexmo.com" />
<add key="Nexmo.api_key" value="<YOUR KEY>" />
<add key="Nexmo.api_secret" value="<YOUR SECRET>" />
```

Examples
--------
The following examples show how to:
 * [Send a message](#sending-a-message)
 * [Receive a message](#receiving-a-message)
 * [Initiate a call](#initiating-a-call)

### Sending A Message

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

### Initiating a Call

Use [Nexmo's Call API][doc_voice] to initiate a voice call.
```C#
var result = Voice.Call(new Voice.CallCommand
{
    to = "17775551212",
    answer_url = "https://abcdefgh.ngrok.io/content/voiceDemo.xml",
    status_url = "https://abcdefgh.ngrok.io/api/voice",
    from = "15555551212",
});
```

### Additional Examples

* Check out the sample MVC application and tests for more examples.
Make sure to copy web.config.example to web.config and enter your key/secret.

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
* Voice
    * [X] Outbound Calls
    * [X] Inbound Call
    * [X] Text-To-Speech Call
    * [X] Text-To-Speech Prompt

Contributing
------------

We are currently targeting the 4.5.2 - 4.6.1 frameworks and using Visual Studio 2015 Update 1.

1. Get latest code either by cloning the repository or downloading a snapshot of the source.
2. Open "Nexmo.Api.sln"
3. Build! NuGet dependencies should be brought down automatically; check your settings if they are not.

Pull requests are welcome!

License
-------

This library is released under the [MIT License][license]

[create_account]: https://docs.nexmo.com/tools/dashboard#setting-up-your-nexmo-account
[signup]: https://dashboard.nexmo.com/sign-up?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[doc_sms]: https://docs.nexmo.com/api-ref/sms-api?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[doc_voice]: https://docs.nexmo.com/voice/call?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library
[license]: LICENSE.md