#region
using System;
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Applications;
using Vonage.Applications.Capabilities;
using Vonage.Common;
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
        };

    private static Keys CreateBasicKeys() =>
        new Keys
        {
            PublicKey = PublicKey,
        };

    private static Meetings CreateMeetingsCapability() =>
        new Meetings(new Dictionary<Webhook.Type, Webhook>
        {
            {Webhook.Type.RoomChanged, new Webhook {Address = "http://example.com", Method = "POST"}},
            {Webhook.Type.SessionChanged, new Webhook {Address = "http://example.com", Method = "POST"}},
            {
                Webhook.Type.RecordingChanged,
                new Webhook {Address = "https://54eba990d025.ngrok.app/recordings", Method = "POST"}
            },
        });

    private static Vonage.Applications.Capabilities.Messages CreateMessagesCapability() =>
        new Vonage.Applications.Capabilities.Messages(new Dictionary<Webhook.Type, Webhook>
        {
            {
                Webhook.Type.InboundUrl,
                new Webhook {Address = "https://example.com/webhooks/inbound", Method = "POST"}
            },
            {
                Webhook.Type.StatusUrl,
                new Webhook {Address = "https://example.com/webhooks/status", Method = "POST"}
            },
        });

    private static Rtc CreateRtcCapability() =>
        new Rtc(new Dictionary<Webhook.Type, Webhook>
        {
            {Webhook.Type.EventUrl, new Webhook {Address = "https://example.com/webhooks/events", Method = "POST"}},
        });

    private static Vonage.Applications.Capabilities.Voice CreateVoiceCapability() =>
        new Vonage.Applications.Capabilities.Voice
        {
            Webhooks = new Dictionary<VoiceWebhookType, Vonage.Applications.Capabilities.Voice.VoiceWebhook>
            {
                {
                    VoiceWebhookType.AnswerUrl,
                    new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
                        new Uri("https://example.com/webhooks/answer"), HttpMethod.Get)
                },
                {
                    VoiceWebhookType.EventUrl,
                    new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
                        new Uri("https://example.com/webhooks/events"), HttpMethod.Post)
                },
                {
                    VoiceWebhookType.FallbackAnswerUrl,
                    new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
                        new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get)
                },
            },
        };

    internal static CreateApplicationRequest CreateRequest() =>
        new CreateApplicationRequest
        {
            Capabilities = CreateBasicCapabilities(),
            Keys = CreateBasicKeys(),
            Name = "My Application",
            Privacy = new PrivacySettings(true),
        };

    internal static CreateApplicationRequest CreateRequestWithVoiceTimeouts()
    {
        var answerWebhook = new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
            new Uri("https://example.com/webhooks/answer"), HttpMethod.Get, 300, 2000);
        var eventWebhook = new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
            new Uri("https://example.com/webhooks/events"), HttpMethod.Post, 500, 3000);
        var fallbackWebhook = new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
            new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get, 800, 4000);
        return new CreateApplicationRequest
        {
            Capabilities = new ApplicationCapabilities
            {
                Voice = new Vonage.Applications.Capabilities.Voice
                {
                    Webhooks = new Dictionary<VoiceWebhookType, Vonage.Applications.Capabilities.Voice.VoiceWebhook>
                    {
                        {VoiceWebhookType.AnswerUrl, answerWebhook},
                        {VoiceWebhookType.EventUrl, eventWebhook},
                        {VoiceWebhookType.FallbackAnswerUrl, fallbackWebhook},
                    },
                },
            },
        };
    }
}