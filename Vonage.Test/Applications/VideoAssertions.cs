#region
using System;
using FluentAssertions;
using Vonage.Applications;
using Vonage.Applications.Capabilities;
#endregion

namespace Vonage.Test.Applications;

internal static class VideoAssertions
{
    private static void ShouldHaveVideoCapabilities(this Application actual)
    {
        actual.Capabilities.Video.Should().NotBeNull();
        actual.Capabilities.Video.Webhooks.Should().BeEmpty();
        actual.Capabilities.Video.Storage.Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoStorage(false, false, false));
    }

    private static void ShouldHaveVideoFullCapabilities(this Application actual)
    {
        actual.Capabilities.Video.Should().NotBeNull();
        actual.Capabilities.Video.Webhooks[VideoWebhookType.ArchiveStatus].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.BroadcastStatus].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.CaptionsStatus].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.ConnectionCreated].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.ConnectionDestroyed].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.RenderStatus].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.SessionCreated].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.SessionDestroyed].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.SessionNotification].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.SipCallCreated].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.SipCallDestroyed].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.SipCallMuteForced].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.SipCallUpdated].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.StreamCreated].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Webhooks[VideoWebhookType.StreamDestroyed].Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoWebhook(new Uri("https://example.com/webhooks/event"),
                true));
        actual.Capabilities.Video.Storage.Should()
            .Be(new Vonage.Applications.Capabilities.Video.VideoStorage(true, true, true));
    }

    internal static void ShouldMatchVideoApplication(this Application actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
        actual.ShouldHaveVideoCapabilities();
    }

    internal static void ShouldMatchVideoFullApplication(this Application actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
        actual.ShouldHaveVideoFullCapabilities();
    }
}