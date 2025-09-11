#region
using System.Net.Http;
using FluentAssertions;
using Vonage.Common;
using VoiceCapability = Vonage.Applications.Capabilities.Voice;
using MessagesCapability = Vonage.Applications.Capabilities.Messages;
using RtcCapability = Vonage.Applications.Capabilities.Rtc;
using MeetingsCapability = Vonage.Applications.Capabilities.Meetings;
using VerifyCapability = Vonage.Applications.Capabilities.Verify;
using VideoCapability = Vonage.Applications.Capabilities.Video;
using static Vonage.Applications.Capabilities.VideoWebhookType;
using VoiceWebhookType = Vonage.Applications.Capabilities.VoiceWebhookType;
#endregion

namespace Vonage.Test.Applications;

internal static class CapabilitiesTestAssertions
{
    internal static void ShouldHaveAnswerUrlWebhook(this VoiceCapability voice)
    {
        voice.Webhooks.Should().ContainKey(VoiceWebhookType.AnswerUrl);
        var webhook = voice.Webhooks[VoiceWebhookType.AnswerUrl];
        webhook.Address.ToString().Should().Be("https://example.com/answer");
        webhook.Method.Should().Be(HttpMethod.Get);
        webhook.ConnectionTimeout.Should().Be(1000);
        webhook.SocketTimeout.Should().Be(2000);
    }

    internal static void ShouldHaveFallbackAnswerUrlWebhook(this VoiceCapability voice)
    {
        voice.Webhooks.Should().ContainKey(VoiceWebhookType.FallbackAnswerUrl);
        var webhook = voice.Webhooks[VoiceWebhookType.FallbackAnswerUrl];
        webhook.Address.ToString().Should().Be("https://example.com/fallback");
        webhook.Method.Should().Be(HttpMethod.Post);
        webhook.ConnectionTimeout.Should().Be(1000);
        webhook.SocketTimeout.Should().Be(500);
    }

    internal static void ShouldHaveEventUrlWebhook(this VoiceCapability voice)
    {
        voice.Webhooks.Should().ContainKey(VoiceWebhookType.EventUrl);
        var webhook = voice.Webhooks[VoiceWebhookType.EventUrl];
        webhook.Address.ToString().Should().Be("https://example.com/events");
        webhook.Method.Should().Be(HttpMethod.Post);
    }

    internal static void ShouldHaveInboundUrlWebhook(this MessagesCapability messages)
    {
        messages.Webhooks.Should().ContainKey(Webhook.Type.InboundUrl);
        var webhook = messages.Webhooks[Webhook.Type.InboundUrl];
        webhook.Address.Should().Be("https://example.com/inbound");
        webhook.Method.Should().Be("POST");
    }

    internal static void ShouldHaveStatusUrlWebhook(this MessagesCapability messages)
    {
        messages.Webhooks.Should().ContainKey(Webhook.Type.StatusUrl);
        var webhook = messages.Webhooks[Webhook.Type.StatusUrl];
        webhook.Address.Should().Be("https://example.com/status");
        webhook.Method.Should().Be("POST");
    }

    internal static void ShouldHaveRtcEventUrlWebhook(this RtcCapability rtc)
    {
        rtc.Webhooks.Should().ContainKey(Webhook.Type.EventUrl);
        var webhook = rtc.Webhooks[Webhook.Type.EventUrl];
        webhook.Address.Should().Be("https://example.com/events");
        webhook.Method.Should().Be("POST");
    }

    internal static void ShouldHaveRecordingChangedWebhook(this MeetingsCapability meetings)
    {
        meetings.Webhooks.Should().ContainKey(Webhook.Type.RecordingChanged);
        var webhook = meetings.Webhooks[Webhook.Type.RecordingChanged];
        webhook.Address.Should().Be("https://example.com/recording");
        webhook.Method.Should().Be("POST");
    }

    internal static void ShouldHaveRoomChangedWebhook(this MeetingsCapability meetings)
    {
        meetings.Webhooks.Should().ContainKey(Webhook.Type.RoomChanged);
        var webhook = meetings.Webhooks[Webhook.Type.RoomChanged];
        webhook.Address.Should().Be("https://example.com/room");
        webhook.Method.Should().Be("POST");
    }

    internal static void ShouldHaveSessionChangedWebhook(this MeetingsCapability meetings)
    {
        meetings.Webhooks.Should().ContainKey(Webhook.Type.SessionChanged);
        var webhook = meetings.Webhooks[Webhook.Type.SessionChanged];
        webhook.Address.Should().Be("https://example.com/session");
        webhook.Method.Should().Be("POST");
    }

    internal static void ShouldHaveVerifyStatusUrlWebhook(this VerifyCapability verify)
    {
        verify.Webhooks.Should().ContainKey(Webhook.Type.StatusUrl);
        var webhook = verify.Webhooks[Webhook.Type.StatusUrl];
        webhook.Address.Should().Be("https://example.com/verify-status");
        webhook.Method.Should().Be("POST");
    }

    internal static void ShouldHaveArchiveStatusWebhook(this VideoCapability video)
    {
        video.Webhooks.Should().ContainKey(ArchiveStatus);
        var webhook = video.Webhooks[ArchiveStatus];
        webhook.Address.ToString().Should().Be("https://example.com/archive");
        webhook.Active.Should().Be(true);
    }

    internal static void ShouldHaveConnectionCreatedWebhook(this VideoCapability video)
    {
        video.Webhooks.Should().ContainKey(ConnectionCreated);
        var webhook = video.Webhooks[ConnectionCreated];
        webhook.Address.ToString().Should().Be("https://example.com/connection-created");
        webhook.Active.Should().Be(true);
    }

    internal static void ShouldHaveConnectionDestroyedWebhook(this VideoCapability video)
    {
        video.Webhooks.Should().ContainKey(ConnectionDestroyed);
        var webhook = video.Webhooks[ConnectionDestroyed];
        webhook.Address.ToString().Should().Be("https://example.com/connection-destroyed");
        webhook.Active.Should().Be(true);
    }

    internal static void ShouldHaveSessionCreatedWebhook(this VideoCapability video)
    {
        video.Webhooks.Should().ContainKey(SessionCreated);
        var webhook = video.Webhooks[SessionCreated];
        webhook.Address.ToString().Should().Be("https://example.com/session-created");
        webhook.Active.Should().Be(true);
    }

    internal static void ShouldHaveSessionDestroyedWebhook(this VideoCapability video)
    {
        video.Webhooks.Should().ContainKey(SessionDestroyed);
        var webhook = video.Webhooks[SessionDestroyed];
        webhook.Address.ToString().Should().Be("https://example.com/session-destroyed");
        webhook.Active.Should().Be(true);
    }

    internal static void ShouldHaveSessionNotificationWebhook(this VideoCapability video)
    {
        video.Webhooks.Should().ContainKey(SessionNotification);
        var webhook = video.Webhooks[SessionNotification];
        webhook.Address.ToString().Should().Be("https://example.com/session-notification");
        webhook.Active.Should().Be(true);
    }

    internal static void ShouldHaveStreamCreatedWebhook(this VideoCapability video)
    {
        video.Webhooks.Should().ContainKey(StreamCreated);
        var webhook = video.Webhooks[StreamCreated];
        webhook.Address.ToString().Should().Be("https://example.com/stream-created");
        webhook.Active.Should().Be(true);
    }

    internal static void ShouldHaveStreamDestroyedWebhook(this VideoCapability video)
    {
        video.Webhooks.Should().ContainKey(StreamDestroyed);
        var webhook = video.Webhooks[StreamDestroyed];
        webhook.Address.ToString().Should().Be("https://example.com/stream-destroyed");
        webhook.Active.Should().Be(true);
    }

    internal static void ShouldHaveRenderStatusWebhook(this VideoCapability video)
    {
        video.Webhooks.Should().ContainKey(RenderStatus);
        var webhook = video.Webhooks[RenderStatus];
        webhook.Address.ToString().Should().Be("https://example.com/render-status");
        webhook.Active.Should().Be(true);
    }

    internal static void ShouldHaveCaptionsStatusWebhook(this VideoCapability video)
    {
        video.Webhooks.Should().ContainKey(CaptionsStatus);
        var webhook = video.Webhooks[CaptionsStatus];
        webhook.Address.ToString().Should().Be("https://example.com/captions-status");
        webhook.Active.Should().Be(true);
    }

    internal static void ShouldHaveBroadcastStatusWebhook(this VideoCapability video)
    {
        video.Webhooks.Should().ContainKey(BroadcastStatus);
        var webhook = video.Webhooks[BroadcastStatus];
        webhook.Address.ToString().Should().Be("https://example.com/broadcast-status");
        webhook.Active.Should().Be(true);
    }
}