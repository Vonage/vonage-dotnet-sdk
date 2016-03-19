Official C#/.NET wrapper for the nexmo API (http://nexmo.com/)

For full API documentation please refer to https://docs.nexmo.com/

Installation:
===========================
* To install Nexmo API Client, run the following command in the Package Manager Console:

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
===========================
* Provide your API key, secret, and nexmo URLs in appSettings:

```XML
<add key="Nexmo.Url.Rest" value="https://rest.nexmo.com" />
<add key="Nexmo.Url.Api" value="https://api.nexmo.com" />
<add key="Nexmo.api_key" value="<YOUR KEY>" />
<add key="Nexmo.api_secret" value="<YOUR SECRET>" />
```

Usage:
===========================
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

We are currently targeting the 4.5.2 - 4.6.1 frameworks and using Visual Studio 2015 Update 1.

1. Get latest code either by cloning the repository or downloading a snapshot of the source.
2. Open "Nexmo.Api.sln"
3. Build! NuGet dependencies should be brought down automatically; check your settings if they are not.

Pull requests are welcome!