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
            Verify = CreateVerifyCapability(),
            NetworkApis = CreateNetworkApisCapability(),
            Video = CreateVideoCapability(),
        };

    private static Keys CreateBasicKeys() =>
        new Keys
        {
            PublicKey = PublicKey,
        };

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

    private static Vonage.Applications.Capabilities.Video CreateVideoCapability() =>
        new Vonage.Applications.Capabilities.Video();

    private static Vonage.Applications.Capabilities.Video CreateVideoFullCapability() =>
        new Vonage.Applications.Capabilities.Video()
            .WithArchiveStatus("https://example.com/webhooks/event")
            .WithConnectionCreated("https://example.com/webhooks/event")
            .WithConnectionDestroyed("https://example.com/webhooks/event")
            .WithSessionCreated("https://example.com/webhooks/event")
            .WithSessionDestroyed("https://example.com/webhooks/event")
            .WithSessionNotification("https://example.com/webhooks/event")
            .WithStreamCreated("https://example.com/webhooks/event")
            .WithStreamDestroyed("https://example.com/webhooks/event")
            .WithRenderStatus("https://example.com/webhooks/event")
            .WithCaptionsStatus("https://example.com/webhooks/event")
            .WithBroadcastStatus("https://example.com/webhooks/event")
            .WithSipCallCreated("https://example.com/webhooks/event")
            .WithSipCallUpdated("https://example.com/webhooks/event")
            .WithSipCallDestroyed("https://example.com/webhooks/event")
            .WithSipCallMuteForced("https://example.com/webhooks/event")
            .EnableCloudStorage()
            .EnableEndToEndEncryption()
            .EnableServerSideEncryption();

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

    internal static CreateApplicationRequest CreateVideoRequest() =>
        new CreateApplicationRequest
        {
            Capabilities = new ApplicationCapabilities
            {
                Video = CreateVideoCapability(),
            },
            Keys = CreateBasicKeys(),
            Name = "My Application",
            Privacy = new PrivacySettings(true),
        };

    internal static CreateApplicationRequest CreateVideoFullRequest() =>
        new CreateApplicationRequest
        {
            Capabilities = new ApplicationCapabilities
            {
                Video = CreateVideoFullCapability(),
            },
            Keys = CreateBasicKeys(),
            Name = "My Application",
            Privacy = new PrivacySettings(true),
        };
}