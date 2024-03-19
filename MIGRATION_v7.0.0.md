# Breaking changes in version 7.0.0

This document covers all breaking changes introduced with v7.0.0.

If you migrate from v6.X.X to v7.X.X (or above), you will have to handle the following points in your codebase.

### Everything flagged with an 'Obsolete' annotation has been removed

When upgrading to a newer release, you may have noticed a warning indicating you're using an obsolete method.
Indeed, we flagged these changes with an `Obsolete` tag.

Please note that there's always a substitute when we decommission something.

#### Synchronous methods

So far, many features are available with both synchronous/asynchronous implementation.
All synchronous methods have been removed in [v7.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.0.0).
If you are still requires to call a Vonage API in a synchronous context, you will have to call the asynchronous version
with
either `.Result` or `.Wait()` to wait for the result synchronously.

```csharp
// Using v6.X.X
applicationClient.DeleteApplication(applicationId);

// Using v7.X.X and above
await applicationClient.DeleteApplicationAsync(applicationId);
// Or
applicationClient.DeleteApplicationAsync(applicationId).Wait();
```

#### SubAccounts features on 'AccountClient'

SubAccounts features were initially implemented by a contributor on the `AccountClient` while the product was still in
beta.
With the GA release, all SubAccounts features are available on the `SubAccountsClient`, which makes previous features
obsolete.

```csharp
// Using v6.X.X
var subAccount = await vonageClient.AccountClient.RetrieveSubAccountAsync(subAccountKey);
// Using v7.X.X and above
var request = GetSubAccountRequest.Parse(subAccountKey);
var subAccount = await vonageClient.SubAccountClient.GetSubAccountAsync(request);
```

#### 'CreateApplicaitonAsync' on 'ApplicationClient'

The current method contains a typo.

```csharp
// Using v6.X.X
var application = await applicationClient.CreateApplicaitonAsync(request);

// Using v7.X.X and above
var application = await applicationClient.CreateApplicationAsync(request);
```

#### 'CreateCall' on 'VoiceClient'

This method offers three different signatures.
Only the one with a `CallCommand` parameter will remain to avoid primitive obsession and rely on a proper
ValueObject.

```csharp
// Using v6.X.X
var callResponse = voiceClient.CreateCallAsync(toNumber, fromNumber, ncco);
var callResponse = voiceClient.CreateCallAsync(toEndpoint, fromNumber, ncco);
var callResponse = voiceClient.CreateCallAsync(callCommand);


// Using v7.X.X and above
var callResponse = voiceClient.CreateCallAsync(callCommand);
```

#### Constructors on 'Credentials'

Creating a Credentials instance should be done using a factory method or from a `Configuration` instance.
Constructors will be hidden, and the object will remain immutable.

We highly recommend you to use
our [extension methods](https://developer.vonage.com/en/blog/implicit-configuration-in-net) to dynamically
initialize `Credentials` from your `appsettings.json` file.

```csharp
// Using v6.X.X
var credentials = new Credentials()
{
    ApiKey = apiKey,
};
var credentials = new Credentials(apiKey, apiSecret);
var credentials = Credentials.FromApiKeyAndSecret(apiKey, apiSecret);
var credentials = Credentials.FromAppIdAndPrivateKey(apiKey, apiSecret);


// Using v7.X.X and above
var credentials = Credentials.FromApiKeyAndSecret(apiKey, apiSecret);
var credentials = Credentials.FromAppIdAndPrivateKey(apiKey, apiSecret);
var credentials = configuration.BuildCredentials();
// Recommended
var credentials = serviceCollection.GetRequiredService<Credentials>();
```

#### Access to Vonage URLs

All URLs have been moved to a nested object (`VonageUrls`) to "de-bloat" the `Configuration` class and allow
multi-region URLs.

```csharp
// Using v6.X.X
var url = configuration.NexmoApiUrl;
var url = configuration.RestApiUrl;
var url = configuration.VideoApiUrl;
var url = configuration.EuropeApiUrl;

// Using v7.X.X and above
var url = configuration.VonageUrls.Nexmo;
var url = configuration.VonageUrls.Rest;
var url = configuration.VonageUrls.Video;
var url = configuration.VonageUrls.Get(VonageUrls.Region.EMEA);
```

#### Remove 'Input' webhook object

This item has been rendered obsolete due to the new multi-input functionality. Please add the `Dtmf` arguments to your
input action and use the `MultiInput` object.

More details [here](https://developer.nexmo.com/voice/voice-api/ncco-reference#dtmf-input-settings).

#### Remove 'Portuguese" language in Meetings API

This language has been removed in favor or 'Portuguese-Brazilian'.

```csharp
// Using v6.X.X
var language = UserInterfaceLanguage.Pt;


// Using v7.X.X and above
var language = UserInterfaceLanguage.PtBr;
```

#### Remove 'VoiceName' from 'TalkCommand' and 'TalkAction' in Voice

This parameter has been made obsolete by the language and style fields.

More details [here](https://developer.nexmo.com/voice/voice-api/guides/text-to-speech#locale).

### Add new timeouts on Voice Webhooks in Application API

Adding new timeouts requested (`connection_timeout`, `socket_timeout`) on Voice Webhooks.
Creating a specific structure, different from other webhooks, required to break the `Capability` inheritance.

### Rename settings key from 'appSettings' to 'vonage'

In order to make the settings more explicit and reduce chances of conflict with other libraries, the base key has been
updated.

Also, "Vonage." has been removed from every key to de-clutter the section. Finally, a few keys have been completely
renamed:

| Old Key                      | New Key      |
|------------------------------|--------------|
| Vonage.Vonage_key            | Api.Key      |
| Vonage.Vonage_secret         | Api.Secret   |
| Vonage.Vonage.Url.Api.Europe | Url.Api.EMEA |
| N/A                          | Url.Api.AMER |
| N/A                          | Url.Api.APAC |

Using v6.X.X

```json
{
  "appSettings": {
    "Vonage.UserAgent": "...",
    "Vonage.Url.Rest": "...",
    "Vonage.Url.Api": "...",
    "Vonage.Url.Api.Europe": "...",
    "Vonage.Url.Api.Video": "...",
    "Vonage.Vonage_key": "...",
    "Vonage.Vonage_secret": "...",
    "Vonage.Application.Id": "...",
    "Vonage.Application.Key": "...",
    "Vonage.Security_secret": "...",
    "Vonage.Signing_method": "...",
    "Vonage.RequestsPerSecond": "...",
    "Vonage.RequestTimeout": "..."
  }
}
```

Using v7.X.X and above

```json
{
  "vonage": {
    "UserAgent": "...",
    "Url.Rest": "...",
    "Url.Api": "...",
    "Url.Api.EMEA": "...",
    "Url.Api.AMER": "...", 
    "Url.Api.APAC": "...",
    "Url.Api.Video": "...",
    "Api.Key": "...",
    "Api.Secret": "...",
    "Application.Id": "...",
    "Application.Key": "...",
    "Security_secret": "...",
    "Signing_method": "...",
    "RequestsPerSecond": "...",
    "RequestTimeout": "..."
  }
}
```

### Make StartTime nullable on Answered Webhook

As defined in the [specs](https://developer.vonage.com/en/voice/voice-api/webhook-reference#answered), the `StartTime`
is now nullable.

### Remove 'EventUrl' and 'EventMethod' from 'ConversationAction'

As defined in the [specs](https://developer.vonage.com/en/voice/voice-api/ncco-reference#conversation),
the `Conversation` action doesn't have an `EventUrl` or an `EventMethod`.

### Make 'From' mandatory in VerifyV2 WhatsApp workflow

The `From` property used to be optional - it is now mandatory.

```csharp
// Using v6.X.X
var workflow =  WhatsAppWorkflow.Parse(ValidToNumber);
var workflow =  WhatsAppWorkflow.Parse(ValidToNumber, ValidFromNumber);

// Using v7.X.X and above
var workflow =  WhatsAppWorkflow.Parse(ValidToNumber, ValidFromNumber);
```