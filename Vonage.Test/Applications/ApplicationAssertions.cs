#region
using System;
using System.Net.Http;
using FluentAssertions;
using Vonage.Applications;
using Vonage.Applications.Capabilities;
using Vonage.Common;
#endregion

namespace Vonage.Test.Applications;

internal static class ApplicationAssertions
{
    public static void ShouldHaveExpectedNetworkApisCapabilities(this Application actual)
    {
        actual.Capabilities.NetworkApis.ApplicationId.Should().Be("2bzfIFqRG128IcjSj1YhZNtw6LADG");
        actual.Capabilities.NetworkApis.RedirectUri.Should().Be(new Uri("https://my-redirect-uri.example.com"));
    }

    private static void ShouldHaveExpectedCapabilities(this Application actual)
    {
        actual.ShouldHaveExpectedVoiceCapabilities();
        actual.ShouldHaveExpectedMessagesCapabilities();
        actual.ShouldHaveRtcCapabilities();
    }

    private static void ShouldHaveExpectedMessagesCapabilities(this Application actual)
    {
        actual.Capabilities.Messages.Should().NotBeNull();
        actual.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Address
            .Should().Be("https://example.com/webhooks/inbound");
        actual.Capabilities.Messages.Webhooks[Webhook.Type.InboundUrl].Method
            .Should().Be("POST");
        actual.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Address
            .Should().Be("https://example.com/webhooks/status");
        actual.Capabilities.Messages.Webhooks[Webhook.Type.StatusUrl].Method
            .Should().Be("POST");
    }

    private static void ShouldHaveExpectedPaginationProperties(this ApplicationPage actual)
    {
        actual.TotalItems.Should().Be(6);
        actual.TotalPages.Should().Be(1);
        actual.PageSize.Should().Be(10);
        actual.Page.Should().Be(1);
    }

    private static void ShouldHaveExpectedVoiceCapabilities(this Application actual)
    {
        actual.Capabilities.Voice.Should().NotBeNull();
        actual.Capabilities.Voice.Webhooks[VoiceWebhookType.AnswerUrl]
            .Should().Be(
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                    HttpMethod.Get));
        actual.Capabilities.Voice.Webhooks[VoiceWebhookType.FallbackAnswerUrl]
            .Should().Be(
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
                    new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get));
        actual.Capabilities.Voice.Webhooks[VoiceWebhookType.EventUrl]
            .Should().Be(
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/events"),
                    HttpMethod.Post));
    }

    internal static void ShouldHaveExpectedBasicProperties(this Application actual)
    {
        actual.Id.Should().Be("78d335fa323d01149c3dd6f0d48968cf");
        actual.Name.Should().Be("My Application");
    }

    internal static void ShouldMatchExpectedApplication(this Application actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
        actual.ShouldHaveExpectedCapabilities();
    }

    internal static void ShouldHaveExpectedVoiceTimeouts(this Application actual)
    {
        var expectedAnswer = new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
            new Uri("https://example.com/webhooks/answer"), HttpMethod.Get, 300, 2000);
        var expectedEvent = new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
            new Uri("https://example.com/webhooks/events"), HttpMethod.Post, 500, 3000);
        var expectedFallback = new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
            new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get, 800, 4000);
        actual.Capabilities.Voice.Webhooks[VoiceWebhookType.AnswerUrl].Should().Be(expectedAnswer);
        actual.Capabilities.Voice.Webhooks[VoiceWebhookType.EventUrl].Should().Be(expectedEvent);
        actual.Capabilities.Voice.Webhooks[VoiceWebhookType.FallbackAnswerUrl].Should().Be(expectedFallback);
    }

    internal static void ShouldHaveExpectedApplicationInList(this ApplicationPage actual)
    {
        var application = actual.Embedded.Applications[0];
        application.ShouldMatchExpectedApplication();
        actual.ShouldHaveExpectedPaginationProperties();
    }

    internal static void ShouldMatchVerifyFullApplication(this Application actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
        actual.ShouldHaveVerifyFullCapabilities();
    }

    internal static void ShouldMatchVerifyApplication(this Application actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
        actual.ShouldHaveVerifyCapabilities();
    }

    internal static void ShouldMatchRtcApplication(this Application actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
        actual.ShouldHaveRtcCapabilities();
    }

    internal static void ShouldMatchRtcFullApplication(this Application actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
        actual.ShouldHaveRtcFullCapabilities();
    }

    internal static void ShouldMatchVoiceApplication(this Application actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
        actual.ShouldHaveVoiceCapabilities();
    }

    internal static void ShouldMatchVoiceFullApplication(this Application actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
        actual.ShouldHaveVoiceFullCapabilities();
    }

    private static void ShouldHaveVerifyFullCapabilities(this Application actual)
    {
        actual.Capabilities.Verify.Should().NotBeNull();
        actual.Capabilities.Verify.Webhooks[Webhook.Type.StatusUrl].Should()
            .BeEquivalentTo(new Webhook("https://example.com/webhooks/status", HttpMethod.Post));
        actual.Capabilities.Verify.Version.Should().Be("v2");
    }

    private static void ShouldHaveVerifyCapabilities(this Application actual)
    {
        actual.Capabilities.Verify.Should().NotBeNull();
        actual.Capabilities.Verify.Webhooks.Should().BeEmpty();
        actual.Capabilities.Verify.Version.Should().BeNull();
    }

    private static void ShouldHaveRtcCapabilities(this Application actual)
    {
        actual.Capabilities.Rtc.Should().NotBeNull();
        actual.Capabilities.Rtc.Webhooks.Should().BeEmpty();
        actual.Capabilities.Rtc.SignedCallbacks.Should().BeFalse();
    }

    private static void ShouldHaveVoiceCapabilities(this Application actual)
    {
        actual.Capabilities.Voice.Should().NotBeNull();
        actual.Capabilities.Voice.Webhooks.Should().BeEmpty();
        actual.Capabilities.Voice.SignedCallbacks.Should().BeFalse();
        actual.Capabilities.Voice.ConversationsTimeToLive.Should().Be(0);
        actual.Capabilities.Voice.LegPersistenceTime.Should().Be(0);
        actual.Capabilities.Voice.Region.Should().BeNull();
    }

    private static void ShouldHaveVoiceFullCapabilities(this Application actual)
    {
        actual.Capabilities.Voice.Should().NotBeNull();
        actual.Capabilities.Voice.Webhooks[VoiceWebhookType.AnswerUrl]
            .Should().Be(
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/answer"),
                    HttpMethod.Get));
        actual.Capabilities.Voice.Webhooks[VoiceWebhookType.FallbackAnswerUrl]
            .Should().Be(
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(
                    new Uri("https://fallback.example.com/webhooks/answer"), HttpMethod.Get));
        actual.Capabilities.Voice.Webhooks[VoiceWebhookType.EventUrl]
            .Should().Be(
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/events"),
                    HttpMethod.Post));
        actual.Capabilities.Voice.SignedCallbacks.Should().BeTrue();
        actual.Capabilities.Voice.ConversationsTimeToLive.Should().Be(12);
        actual.Capabilities.Voice.LegPersistenceTime.Should().Be(10);
        actual.Capabilities.Voice.Region.Should().Be("eu-west");
    }

    private static void ShouldHaveRtcFullCapabilities(this Application actual)
    {
        actual.Capabilities.Rtc.Should().NotBeNull();
        actual.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Should()
            .BeEquivalentTo(new Webhook("https://example.com/webhooks/events", HttpMethod.Post));
        actual.Capabilities.Rtc.SignedCallbacks.Should().BeTrue();
    }
}