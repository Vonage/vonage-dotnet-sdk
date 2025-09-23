#region
using System;
using Vonage.Applications;
using Vonage.Applications.Capabilities;
#endregion

namespace Vonage.Test.Applications;

internal static class ApplicationTestData
{
    private const string PublicKey = "some public key";

    private static ApplicationCapabilities CreateBasicCapabilities() =>
        new ApplicationCapabilities
        {
            Messages = CreateMessagesCapability(),
            Rtc = CreateRtcCapability(),
            Voice = CreateVoiceCapability(),
            Vbc = Vbc.Build(),
            Meetings = CreateMeetingsCapability(),
            Verify = CreateVerifyCapability(),
            NetworkApis = CreateNetworkApisCapability(),
        };

    private static Keys CreateBasicKeys() =>
        new Keys
        {
            PublicKey = PublicKey,
        };

    private static Meetings CreateMeetingsCapability() =>
        Meetings.Build()
            .WithRoomChanged("http://example.com")
            .WithSessionChanged("http://example.com")
            .WithRecordingChanged("https://54eba990d025.ngrok.app/recordings");

    private static Vonage.Applications.Capabilities.Messages CreateMessagesCapability() =>
        Vonage.Applications.Capabilities.Messages.Build()
            .WithInboundUrl("https://example.com/webhooks/inbound")
            .WithStatusUrl("https://example.com/webhooks/status");

    private static NetworkApis CreateNetworkApisCapability() =>
        new NetworkApis().WithApplicationId("2bzfIFqRG128IcjSj1YhZNtw6LADG")
            .WithRedirectUri(new Uri("https://my-redirect-uri.example.com"));

    private static Rtc CreateRtcCapability() =>
        Rtc.Build().WithEventUrl("https://example.com/webhooks/events", WebhookHttpMethod.Post);

    private static Vonage.Applications.Capabilities.Verify CreateVerifyCapability() =>
        Vonage.Applications.Capabilities.Verify.Build()
            .WithStatusUrl("https://example.com/webhooks/status");

    private static Vonage.Applications.Capabilities.Voice CreateVoiceCapability() =>
        Vonage.Applications.Capabilities.Voice.Build()
            .WithAnswerUrl("https://example.com/webhooks/answer", WebhookHttpMethod.Get)
            .WithEventUrl("https://example.com/webhooks/events", WebhookHttpMethod.Post)
            .WithFallbackAnswerUrl("https://fallback.example.com/webhooks/answer", WebhookHttpMethod.Get);

    internal static CreateApplicationRequest CreateRequest() =>
        new CreateApplicationRequest
        {
            Capabilities = CreateBasicCapabilities(),
            Keys = CreateBasicKeys(),
            Name = "My Application",
            Privacy = new PrivacySettings(true),
        };
}