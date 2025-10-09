#region
using System;
using System.Net.Http;
using FluentAssertions;
using Vonage.Applications;
using Vonage.Applications.Capabilities;
using Vonage.Common;
using Xunit;
using VoiceCapability = Vonage.Applications.Capabilities.Voice;
using MessagesCapability = Vonage.Applications.Capabilities.Messages;
using RtcCapability = Vonage.Applications.Capabilities.Rtc;
using VerifyCapability = Vonage.Applications.Capabilities.Verify;
using VideoCapability = Vonage.Applications.Capabilities.Video;
#endregion

namespace Vonage.Test.Applications;

public class CapabilitiesTests
{
    private const string TestUrl = "https://example.com/webhook";
    private static VoiceCapability Voice => VoiceCapability.Build();
    private static MessagesCapability Messages => MessagesCapability.Build();
    private static NetworkApis NetworkApis => NetworkApis.Build();
    private static RtcCapability Rtc => RtcCapability.Build();
    private static VerifyCapability Verify => VerifyCapability.Build();
    private static VideoCapability Video => VideoCapability.Build();

    [Fact]
    public void Messages_ShouldBeEmpty() =>
        Messages.Webhooks.Should().BeEmpty();

    [Fact]
    public void Messages_WithInboundUrl_ShouldSetWebhook() =>
        Messages.WithInboundUrl(TestUrl).ShouldHaveWebhook(Webhook.Type.InboundUrl, TestUrl, "POST");

    [Fact]
    public void Messages_WithStatusUrl_ShouldSetWebhook() =>
        Messages.WithStatusUrl(TestUrl).ShouldHaveWebhook(Webhook.Type.StatusUrl, TestUrl, "POST");

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
        Rtc.WithEventUrl(TestUrl, WebhookHttpMethod.Post).ShouldHaveWebhook(Webhook.Type.EventUrl, TestUrl, "POST");

    [Fact]
    public void Verify_ShouldBeEmpty() =>
        Verify.Webhooks.Should().BeEmpty();

    [Fact]
    public void Verify_WithStatusUrl_ShouldSetWebhook() =>
        Verify.WithStatusUrl(TestUrl).ShouldHaveWebhook(Webhook.Type.StatusUrl, TestUrl, "POST");

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
        Video.WithArchiveStatus(TestUrl).ShouldHaveWebhook(VideoWebhookType.ArchiveStatus, TestUrl, true);

    [Fact]
    public void Video_WithBroadcastStatus_ShouldSetWebhook() =>
        Video.WithBroadcastStatus(TestUrl).ShouldHaveWebhook(VideoWebhookType.BroadcastStatus, TestUrl, true);

    [Fact]
    public void Video_WithCaptionsStatus_ShouldSetWebhook() =>
        Video.WithCaptionsStatus(TestUrl).ShouldHaveWebhook(VideoWebhookType.CaptionsStatus, TestUrl, true);

    [Fact]
    public void Video_WithConnectionCreated_ShouldSetWebhook() =>
        Video.WithConnectionCreated(TestUrl).ShouldHaveWebhook(VideoWebhookType.ConnectionCreated, TestUrl, true);

    [Fact]
    public void Video_WithConnectionDestroyed_ShouldSetWebhook() =>
        Video.WithConnectionDestroyed(TestUrl).ShouldHaveWebhook(VideoWebhookType.ConnectionDestroyed, TestUrl, true);

    [Fact]
    public void Video_WithRenderStatus_ShouldSetWebhook() =>
        Video.WithRenderStatus(TestUrl).ShouldHaveWebhook(VideoWebhookType.RenderStatus, TestUrl, true);

    [Fact]
    public void Video_WithSessionCreated_ShouldSetWebhook() =>
        Video.WithSessionCreated(TestUrl).ShouldHaveWebhook(VideoWebhookType.SessionCreated, TestUrl, true);

    [Fact]
    public void Video_WithSessionDestroyed_ShouldSetWebhook() =>
        Video.WithSessionDestroyed(TestUrl).ShouldHaveWebhook(VideoWebhookType.SessionDestroyed, TestUrl, true);

    [Fact]
    public void Video_WithSessionNotification_ShouldSetWebhook() =>
        Video.WithSessionNotification(TestUrl).ShouldHaveWebhook(VideoWebhookType.SessionNotification, TestUrl, true);

    [Fact]
    public void Video_WithSipCallCreated_ShouldSetWebhook() =>
        Video.WithSipCallCreated(TestUrl).ShouldHaveWebhook(VideoWebhookType.SipCallCreated, TestUrl, true);

    [Fact]
    public void Video_WithSipCallDestroyed_ShouldSetWebhook() =>
        Video.WithSipCallDestroyed(TestUrl).ShouldHaveWebhook(VideoWebhookType.SipCallDestroyed, TestUrl, true);

    [Fact]
    public void Video_WithSipCallMuteForced_ShouldSetWebhook() =>
        Video.WithSipCallMuteForced(TestUrl).ShouldHaveWebhook(VideoWebhookType.SipCallMuteForced, TestUrl, true);

    [Fact]
    public void Video_WithSipCallUpdated_ShouldSetWebhook() =>
        Video.WithSipCallUpdated(TestUrl).ShouldHaveWebhook(VideoWebhookType.SipCallUpdated, TestUrl, true);

    [Fact]
    public void Video_WithStreamCreated_ShouldSetWebhook() =>
        Video.WithStreamCreated(TestUrl).ShouldHaveWebhook(VideoWebhookType.StreamCreated, TestUrl, true);

    [Fact]
    public void Video_WithStreamDestroyed_ShouldSetWebhook() =>
        Video.WithStreamDestroyed(TestUrl).ShouldHaveWebhook(VideoWebhookType.StreamDestroyed, TestUrl, true);

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
        Voice.WithAnswerUrl(TestUrl, WebhookHttpMethod.Get, 2000, 2000)
            .ShouldHaveWebhook(VoiceWebhookType.AnswerUrl,
                new VoiceCapability.VoiceWebhook(new Uri(TestUrl), HttpMethod.Get, 2000, 2000));

    [Fact]
    public void Voice_WithEventUrl_ShouldSetWebhook() =>
        Voice.WithEventUrl(TestUrl, WebhookHttpMethod.Post, 2000, 2000)
            .ShouldHaveWebhook(VoiceWebhookType.EventUrl,
                new VoiceCapability.VoiceWebhook(new Uri(TestUrl), HttpMethod.Post, 2000, 2000));

    [Fact]
    public void Voice_WithFallbackAnswerUrl_ShouldSetWebhook() =>
        Voice.WithFallbackAnswerUrl(TestUrl, WebhookHttpMethod.Post, 2000, 2000)
            .ShouldHaveWebhook(VoiceWebhookType.FallbackAnswerUrl,
                new VoiceCapability.VoiceWebhook(new Uri(TestUrl), HttpMethod.Post, 2000, 2000));
}