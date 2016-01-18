Official C#/.NET wrapper for the nexmo API (http://nexmo.com/)

For full API documentation please refer to https://docs.nexmo.com/

Usage Instructions: 
===========================
_NOTE: NuGet package coming soon!_

* Download or build (see developer instructions) the Nexmo.Api.dll.
* If you have downloaded a release, ensure you are referencing the
Newtonsoft.Json.dll dependency by either including it with your project's
NuGet dependencies or manually referencing it.
* Reference the assembly in your code.
* Provide your API key, secret, and nexmo URLs in appSettings:

```XML
<add key="Nexmo.Url.Rest" value="https://rest.nexmo.com" />
<add key="Nexmo.Url.Api" value="https://api.nexmo.com" />
<add key="Nexmo.api_key" value="<YOUR KEY>" />
<add key="Nexmo.api_secret" value="<YOUR SECRET>" />
```

* Use the methods provided to exercise the API. Example:

```C#
Nexmo.Api.NumberVerify.Verify(new NumberVerify.VerifyRequest
{
    number = "15555551212",
    brand = "Test Brand"
})
```

* Check out the sample MVC application for a working example.
Make sure to copy web.config.example to web.config and enter your key/secret.

Developer Instructions: 
===========================

We are currently targeting the 4.5.2 framework and using Visual Studio 2013 Update 5.

1. Get latest code either by cloning the repository or downloading a snapshot of the source
2. Open "Nexmo.Api.sln"
3. Build! NuGet dependencies should be brought down automatically; check your settings if they are not.

API support status:
===================

* Account
  * [x] Balance
  * [x] Pricing
  * [X] Settings
  * [X] Top Up
  * [X] Numbers
* Number
  * [X] Search
  * [X] Buy
  * [X] Cancel
  * [X] Update
* NumberInsight
  * [x] Request
  * [x] Response
* NumberVerify
  * [x] Verify
  * [x] Check
  * [x] Search
* Search
  * [X] Message
  * [X] Messages
  * [X] Rejections
* Short Code
  * [X] 2FA
  * [X] Alerts
  * [ ] Marketing
* SMS
  * [X] Send
  * [X] Receipt
  * [X] Inbound
* Voice
  * [ ] Call
  * [ ] TTS/TTS Prompt
  * [ ] SIP