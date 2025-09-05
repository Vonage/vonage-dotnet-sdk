#region
using System;
using System.Collections.Generic;
using System.Net.Http;
using FluentAssertions;
using Vonage.Applications;
using Vonage.Applications.Capabilities;
using Vonage.Common;
#endregion

namespace Vonage.Test.Applications;

internal static class ApplicationAssertions
{
    private static void ShouldHaveExpectedBasicProperties(this Application actual)
    {
        actual.Id.Should().Be("78d335fa323d01149c3dd6f0d48968cf");
        actual.Name.Should().Be("My Application");
    }

    private static void ShouldHaveExpectedCapabilities(this Application actual)
    {
        actual.ShouldHaveExpectedVoiceCapabilities();
        actual.ShouldHaveExpectedMessagesCapabilities();
        actual.ShouldHaveExpectedRtcCapabilities();
        actual.ShouldHaveExpectedMeetingsCapabilities();
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

    private static void ShouldHaveExpectedRtcCapabilities(this Application actual)
    {
        actual.Capabilities.Rtc.Should().NotBeNull();
        actual.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Address
            .Should().Be("https://example.com/webhooks/event");
        actual.Capabilities.Rtc.Webhooks[Webhook.Type.EventUrl].Method
            .Should().Be("POST");
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
                new Vonage.Applications.Capabilities.Voice.VoiceWebhook(new Uri("https://example.com/webhooks/event"),
                    HttpMethod.Post));
    }

    internal static void ShouldMatchExpectedApplication(this Application actual)
    {
        actual.ShouldHaveExpectedBasicProperties();
        actual.ShouldHaveExpectedCapabilities();
    }

    internal static void ShouldHaveExpectedMeetingsCapabilities(this Application actual)
    {
        actual.Capabilities.Meetings.Should().NotBeNull();
        actual.Capabilities.Meetings.Webhooks.Should().BeEquivalentTo(new Dictionary<Webhook.Type, Webhook>
        {
            {Webhook.Type.RoomChanged, new Webhook {Address = "http://example.com", Method = "POST"}},
            {Webhook.Type.SessionChanged, new Webhook {Address = "http://example.com", Method = "POST"}},
            {
                Webhook.Type.RecordingChanged,
                new Webhook {Address = "https://54eba990d025.ngrok.app/recordings", Method = "POST"}
            },
        });
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
}