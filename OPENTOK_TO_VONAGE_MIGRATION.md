# Migration guide from OpenTok .NET SDK to Vonage .NET SDK

## Installation

You can now interact with Vonage's Video API using the `Vonage` package rather than the `OpenTok` one. To do this, either look for "Vonage" is your package manager, or run the following command

```
dotnet add package Vonage
```

Note: not all the Video API features are yet supported in the `Vonage` package . There is a full list
of [Supported Features](#supported-features) later in this document.

## Setup

Whereas the `OpenTok` package used an `ApiKey` and `ApiSecret` for Authorization, the Video API implementation in the `Vonage` packages uses a JWT. The SDK handles JWT generation in the background for you, but will require an `ApplicationId` and `PrivateKey` as credentials in order to generate the token. 
You can obtain these by setting up a Vonage Application, which you can create via the [Developer Dashboard](https://dashboard.nexmo.com/applications) (the Vonage Application is also where you can set other settings such as callback URLs, storage preferences, etc).

These credentials are then loaded from your project settings. For more details on how to do that, feel free to take a look at the [Configuration](https://github.com/Vonage/vonage-dotnet-sdk#configuration) section.

You can then use an instance of 'VonageClient' or `IVideoClient` to interact with the Video API via various methods, for example:

- Create a Session

```ruby
var session = await vonageClient.VideoClient.SessionClient.CreateSessionAsync(...);
// or
var session = await videoClient.SessionClient.CreateSessionAsync(...);
```

- Retrieve a List of Archive Recordings

```ruby
var archives = await vonageClient.VideoClient.ArchiveClient.GetArchivesAsync(...);
// or
var archives = await videoClient.ArchiveClient.GetArchivesAsync(...);
```

## Changed Methods

There are some changes to methods between the `OpenTok` SDK and the Video API implementation in the `Vonage` SDKs.


- Any operation will return a `Result<T>`, indicating whether the operation is a success or a failure. For more details, feel free to take a look at the [Monads](https://github.com/Vonage/vonage-dotnet-sdk#monads) section.
- Creating a request will force you to rely on a builder (ex: `CreateSessionRequest.Build()...`) - all builders provide a fluent API to guide you through mandatory parameters, while proposing optional ones, before building the request using `.Create()`. 
- Methods used to be available in both synchronous and asynchronous versions. The synchronous versions have been removed, leaving only the asynchronous one. If you still want to run that in a synchronous process, please consider using `Task.Wait()` or `Task.Result` on the returned `Task` object.
- Some methods have been renamed and/or moved, for clarity and/or to better reflect what the method does. These are
  listed below:

| OpenTok Method Name                  | Vonage Video Method Name                                  |
|--------------------------------------|-----------------------------------------------------------|
| `OpenTok.GenerateToken`              | `VideoTokenGenerator.GenerateToken`                       |
| `OpenTok.CreateSessionAsync`         | `VonageClient.SessionClient.CreateSessionAsync`           |
| `OpenTok.StartArchiveAsync`          | `VonageClient.ArchiveClient.CreateArchiveAsync`           |
| `OpenTok.StopArchiveAsync`           | `VonageClient.ArchiveClient.StopArchiveAsync`             |
| `OpenTok.GetArchiveAsync`            | `VonageClient.ArchiveClient.GetArchiveAsync`              |
| `OpenTok.DeleteArchiveAsync`         | `VonageClient.ArchiveClient.DeleteArchiveAsync`           |
| `OpenTok.ListArchivesAsync`          | `VonageClient.ArchiveClient.GetArchivesAsync`             |
| `OpenTok.AddStreamToArchiveAsync`    | `VonageClient.ArchiveClient.AddStreamAsync`               |
| `OpenTok.RemoveStreamToArchiveAsync` | `VonageClient.ArchiveClient.RemoveStreamAsync`            |
| `OpenTok.GetStreamAsync`             | `VonageClient.BroadcastClient.GetStreamAsync`             |
| `OpenTok.ListStreamsAsync`           | `VonageClient.BroadcastClient.GetStreamsAsync`            |
| `OpenTok.ForceMuteStreamAsync`       | `VonageClient.ModerationClient.MuteStreamAsync`           |
| `OpenTok.ForceMuteAllAsync`          | `VonageClient.ModerationClient.MuteStreamsAsync`          |
| `OpenTok.ForceDisconnectAsync`       | `VonageClient.ModerationClient.DisconnectConnectionAsync` |
| `OpenTok.StartBroadcastAsync`        | `VonageClient.BroadcastClient.StartBroadcastAsync`        |
| `OpenTok.StopBroadcastAsync`         | `VonageClient.BroadcastClient.StopBroadcastAsync`         |
| `OpenTok.GetBroadcastAsync`          | `VonageClient.BroadcastClient.GetBroadcastAsync`          |
| `OpenTok.SetBroadcastLayout`         | `VonageClient.BroadcastClient.ChangeBroadcastLayoutAsync` |
| `OpenTok.SignalAsync`                | `VonageClient.SignalingClient.SendSignalAsyncAsync`       |
| `OpenTok.PlayDTMFAsync`              | `VonageClient.SipClient.PlayToneIntoCallAsync`            |
| `OpenTok.DialAsync`                  | `VonageClient.SipClient.InitiateCallAsynb`                |

## Supported Features

The following is a list of Vonage Video APIs and whether the SDK provides support for them:

| API                       | Supported? |
|---------------------------|:----------:|
| Session Creation          |     ✅      |
| Stream Management         |     ✅      |
| Signaling                 |     ✅      |
| Moderation                |     ✅      |
| Archiving                 |     ✅      |
| Live Streaming Broadcasts |     ✅      |
| SIP Interconnect          |     ✅      |
| Account Management        |     ❌      |
| Experience Composer       |     ❌      |
| Audio Connector           |     ❌      |
| Live Captions             |     ❌      |
| Custom S3/Azure buckets   |     ❌      |