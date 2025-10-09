#region
using System;
using FluentAssertions;
using Vonage.Applications;
using Vonage.Applications.Capabilities;
using Xunit;
using VoiceCapability = Vonage.Applications.Capabilities.Voice;
using MessagesCapability = Vonage.Applications.Capabilities.Messages;
using RtcCapability = Vonage.Applications.Capabilities.Rtc;
using MeetingsCapability = Vonage.Applications.Capabilities.Meetings;
using VerifyCapability = Vonage.Applications.Capabilities.Verify;
using VideoCapability = Vonage.Applications.Capabilities.Video;
#endregion

namespace Vonage.Test.Applications;

public class CapabilitiesTests
{
    private static VoiceCapability Voice => VoiceCapability.Build();
    private static MeetingsCapability Meetings => MeetingsCapability.Build();
    private static MessagesCapability Messages => MessagesCapability.Build();
    private static NetworkApis NetworkApis => NetworkApis.Build();
    private static RtcCapability Rtc => RtcCapability.Build();
    private static VerifyCapability Verify => VerifyCapability.Build();
    private static VideoCapability Video => VideoCapability.Build();

    [Fact]
    public void Meetings_ShouldBeEmpty() =>
        Meetings.Webhooks.Should().BeEmpty();

    [Fact]
    public void Meetings_WithRecordingChanged_ShouldSetWebhook() =>
        Meetings.WithRecordingChanged("https://example.com/recording").ShouldHaveRecordingChangedWebhook();

    [Fact]
    public void Meetings_WithRoomChanged_ShouldSetWebhook() =>
        Meetings.WithRoomChanged("https://example.com/room").ShouldHaveRoomChangedWebhook();

    [Fact]
    public void Meetings_WithSessionChanged_ShouldSetWebhook() =>
        Meetings.WithSessionChanged("https://example.com/session").ShouldHaveSessionChangedWebhook();

    [Fact]
    public void Messages_ShouldBeEmpty() =>
        Messages.Webhooks.Should().BeEmpty();

    [Fact]
    public void Messages_WithInboundUrl_ShouldSetWebhook() =>
        Messages.WithInboundUrl("https://example.com/inbound").ShouldHaveInboundUrlWebhook();

    [Fact]
    public void Messages_WithStatusUrl_ShouldSetWebhook() =>
        Messages.WithStatusUrl("https://example.com/status").ShouldHaveStatusUrlWebhook();

    [Fact]
    public void NetworkApis_ApplicationIdShouldBeEmpty() => NetworkApis.ApplicationId.Should().BeNull();

    [Fact]
    public void NetworkApis_RedirectUriIdShouldBeEmpty() => NetworkApis.RedirectUri.Should().BeNull();

    [Fact]
    public void NetworkApis_ShouldSetApplicationId() =>
        NetworkApis.WithApplicationId("1234").ApplicationId.Should().Be("1234");

    [Fact]
    public void NetworkApis_ShouldSetRedirectUri() => NetworkApis.WithRedirectUri(new Uri("https://example.com"))
        .RedirectUri.Should().Be(new Uri("https://example.com"));

    [Fact]
    public void Rtc_ShouldBeEmpty() =>
        Rtc.Webhooks.Should().BeEmpty();

    [Fact]
    public void Rtc_WithEventUrl_ShouldSetWebhook() =>
        Rtc.WithEventUrl("https://example.com/events", WebhookHttpMethod.Post).ShouldHaveRtcEventUrlWebhook();

    [Fact]
    public void Verify_ShouldBeEmpty() =>
        Verify.Webhooks.Should().BeEmpty();

    [Fact]
    public void Verify_WithStatusUrl_ShouldSetWebhook() =>
        Verify.WithStatusUrl("https://example.com/verify-status").ShouldHaveVerifyStatusUrlWebhook();

    [Fact]
    public void Video_EnableCloudStorage() =>
        Video.EnableCloudStorage().Storage.CloudStorage.Should().BeTrue();

    [Fact]
    public void Video_EnableEndToEndEncryption() =>
        Video.EnableEndToEndEncryption().Storage.EndToEndEncryption.Should().BeTrue();

    [Fact]
    public void Video_EnableServerSideEncryption() =>
        Video.EnableServerSideEncryption().Storage.ServerSideEncryption.Should().BeTrue();

    [Fact]
    public void Video_WithArchiveStatus_ShouldSetWebhook() =>
        Video.WithArchiveStatus("https://example.com/archive").ShouldHaveArchiveStatusWebhook();

    [Fact]
    public void Video_WithBroadcastStatus_ShouldSetWebhook() =>
        Video.WithBroadcastStatus("https://example.com/broadcast-status").ShouldHaveBroadcastStatusWebhook();

    [Fact]
    public void Video_WithCaptionsStatus_ShouldSetWebhook() =>
        Video.WithCaptionsStatus("https://example.com/captions-status").ShouldHaveCaptionsStatusWebhook();

    [Fact]
    public void Video_WithConnectionCreated_ShouldSetWebhook() =>
        Video.WithConnectionCreated("https://example.com/connection-created").ShouldHaveConnectionCreatedWebhook();

    [Fact]
    public void Video_WithConnectionDestroyed_ShouldSetWebhook() =>
        Video.WithConnectionDestroyed("https://example.com/connection-destroyed")
            .ShouldHaveConnectionDestroyedWebhook();

    [Fact]
    public void Video_WithRenderStatus_ShouldSetWebhook() =>
        Video.WithRenderStatus("https://example.com/render-status").ShouldHaveRenderStatusWebhook();

    [Fact]
    public void Video_WithSessionCreated_ShouldSetWebhook() =>
        Video.WithSessionCreated("https://example.com/session-created").ShouldHaveSessionCreatedWebhook();

    [Fact]
    public void Video_WithSessionDestroyed_ShouldSetWebhook() =>
        Video.WithSessionDestroyed("https://example.com/session-destroyed").ShouldHaveSessionDestroyedWebhook();

    [Fact]
    public void Video_WithSessionNotification_ShouldSetWebhook() =>
        Video.WithSessionNotification("https://example.com/session-notification")
            .ShouldHaveSessionNotificationWebhook();

    [Fact]
    public void Video_WithSipCallCreated_ShouldSetWebhook() =>
        Video.WithSipCallCreated("https://example.com/stream-destroyed").ShouldHaveSipCallCreatedWebhook();

    [Fact]
    public void Video_WithSipCallDestroyed_ShouldSetWebhook() =>
        Video.WithSipCallDestroyed("https://example.com/stream-destroyed").ShouldHaveSipCallDestroyedWebhook();

    [Fact]
    public void Video_WithSipCallMuteForced_ShouldSetWebhook() =>
        Video.WithSipCallMuteForced("https://example.com/stream-destroyed").ShouldHaveSipCallMuteForcedWebhook();

    [Fact]
    public void Video_WithSipCallUpdated_ShouldSetWebhook() =>
        Video.WithSipCallUpdated("https://example.com/stream-destroyed").ShouldHaveSipCallUpdatedWebhook();

    [Fact]
    public void Video_WithStreamCreated_ShouldSetWebhook() =>
        Video.WithStreamCreated("https://example.com/stream-created").ShouldHaveStreamCreatedWebhook();

    [Fact]
    public void Video_WithStreamDestroyed_ShouldSetWebhook() =>
        Video.WithStreamDestroyed("https://example.com/stream-destroyed").ShouldHaveStreamDestroyedWebhook();

    [Fact]
    public void VideoStorage_ShouldBeNull() =>
        Video.Storage.Should().BeNull();

    [Fact]
    public void VideoWebhooks_ShouldBeEmpty() =>
        Video.Webhooks.Should().BeEmpty();

    [Fact]
    public void Voice_ShouldBeEmpty() =>
        Voice.Webhooks.Should().BeEmpty();

    [Fact]
    public void Voice_WithAnswerUrl_ShouldSetWebhook() =>
        Voice.WithAnswerUrl("https://example.com/answer", WebhookHttpMethod.Get, 1000, 2000)
            .ShouldHaveAnswerUrlWebhook();

    [Fact]
    public void Voice_WithEventUrl_ShouldSetWebhook() =>
        Voice.WithEventUrl("https://example.com/events", WebhookHttpMethod.Post).ShouldHaveEventUrlWebhook();

    [Fact]
    public void Voice_WithFallbackAnswerUrl_ShouldSetWebhook() =>
        Voice.WithFallbackAnswerUrl("https://example.com/fallback", WebhookHttpMethod.Post)
            .ShouldHaveFallbackAnswerUrlWebhook();
}