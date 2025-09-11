#region
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
            Vbc = new Vbc(),
            Meetings = CreateMeetingsCapability(),
            Verify = CreateVerifyCapability(),
        };

    private static Keys CreateBasicKeys() =>
        new Keys
        {
            PublicKey = PublicKey,
        };

    private static Meetings CreateMeetingsCapability() =>
        new Meetings()
            .WithRoomChanged("http://example.com")
            .WithSessionChanged("http://example.com")
            .WithRecordingChanged("https://54eba990d025.ngrok.app/recordings");

    private static Vonage.Applications.Capabilities.Messages CreateMessagesCapability() =>
        new Vonage.Applications.Capabilities.Messages()
            .WithInboundUrl("https://example.com/webhooks/inbound")
            .WithStatusUrl("https://example.com/webhooks/status");

    private static Rtc CreateRtcCapability() =>
        new Rtc().WithEventUrl("https://example.com/webhooks/events", WebhookHttpMethod.Post);

    private static Vonage.Applications.Capabilities.Verify CreateVerifyCapability() =>
        new Vonage.Applications.Capabilities.Verify()
            .WithStatusUrl("https://example.com/webhooks/status");

    private static Vonage.Applications.Capabilities.Voice CreateVoiceCapability() =>
        new Vonage.Applications.Capabilities.Voice()
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