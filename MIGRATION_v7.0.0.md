# Breaking changes in version 7.0.0

This document covers all breaking changes introduced with v7.0.0.

If you migrate from v6.X.X to v7.X.X (or above), you will have to handle the following points in your codebase.

## Everything flagged with an `Obsolete` annotation will be removed

### Synchronous methods

So far, a lot of features are available with both synchronous/asynchronous implementation.
In [v7.0.0](https://github.com/Vonage/vonage-dotnet-sdk/releases/tag/v7.0.0), all synchronous methods will be removed.
If you still require to call a Vonage API in a synchronous context, you will have to call the asynchronous version with
either `.Result` or `.Wait()` to wait for the result synchronously.

```csharp
// Using v6.X.X
applicationClient.DeleteApplication(applicationId);

// Using v7.X.X and above
await applicationClient.DeleteApplicationAsync(applicationId);
// Or
applicationClient.DeleteApplicationAsync(applicationId).Wait();
```

### SubAccounts features on `AccountClient`

SubAccounts features have initially been implemented by a contributor on the `AccountClient`, while the product was
still in beta.
With the GA release, all SubAccounts features are available on the `SubAccountsClient`, which makes previous features
obsolete.

```csharp
// Using v6.X.X
var subAccount = await vonageClient.AccountClient.RetrieveSubAccountAsync(subAccountKey);
// Using v7.X.X and above
var request = GetSubAccountRequest.Parse(subAccountKey);
var subAccount = await vonageClient.SubAccountClient.GetSubAccountAsync(request);
```

### `CreateApplicaitonAsync` on `ApplicationClient`

This method contains an obvious typo.

```csharp
// Using v6.X.X
var application = await applicationClient.CreateApplicaitonAsync(request);

// Using v7.X.X and above
var application = await applicationClient.CreateApplicationAsync(request);
```

### `CreateCall` on `VoiceClient`

This method offers 3 different signatures.
Only the one with a `CallCommand` parameter will remain in order to avoid primitive obsession, and rely on a proper
ValueObject.

```csharp
// Using v6.X.X
var callResponse = voiceClient.CreateCallAsync(toNumber, fromNumber, ncco);
var callResponse = voiceClient.CreateCallAsync(toEndpoint, fromNumber, ncco);
var callResponse = voiceClient.CreateCallAsync(callCommand);


// Using v7.X.X and above
var callResponse = voiceClient.CreateCallAsync(callCommand);
```

### Constructors on `Credentials`

Creating a Credentials instance should be done by a factory method or from a `Configuration` instance.
Constructors will be hidden and the object will remain immutable.

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
var credentials = serviceCollection.GetRequiredService<Credentials>();
```

### Access to Vonage URLs

All URLs have been moved to a nested object (`VonageUrls`) to "de-bloat"the `Configuration` class, and allow
multi-region URLs.

```csharp
// Using v6.X.X
var url = configuration.NexmoApiUrl;
var url = configuration.RestApiUrl;
var url = configuration.VideoApiUrl;


// Using v7.X.X and above
var url = configuration.VonageUrls.Nexmo;
var url = configuration.VonageUrls.Rest;
var url = configuration.VonageUrls.Video;
var url = configuration.VonageUrls.Get(VonageUrls.Region.US);
```

## Add new timeouts on Voice Webhooks in Application API

Adding new timeouts requested (`connection_timeout`, `socket_timeout`) on Voice WebHooks.
Creating a specific structure, different from other webhooks, required to break the `Capability` inheritance.

## Rename settings key from `appSettings` to `vonage`

In order to make the settings more explicit and reduce chances of conflict with other libraries, the base key needs to
be updated.

Using v6.X.X

```json
{
  "appSettings": {
    "Vonage.UserAgent": "myApp/1.0"
  }
}
```

Using v7.X.X and above

```json
{
  "vonage": {
    "Vonage.UserAgent": "myApp/1.0"
  }
}
```

## Make StartTime nullable on Answered Webhook

As defined in the [specs](https://developer.vonage.com/en/voice/voice-api/webhook-reference#answered), the `StartTime`
is actually nullable.

## Remove `EventUrl` and `EventMethod` from `ConversationAction`

As defined in the [specs](https://developer.vonage.com/en/voice/voice-api/ncco-reference#conversation),
the `Conversation` action doesn't have an `EventUrl` or an `EventMethod`. 
