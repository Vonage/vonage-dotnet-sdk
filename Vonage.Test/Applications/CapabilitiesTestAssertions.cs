#region
using FluentAssertions;
using Vonage.Applications.Capabilities;
using Vonage.Common;
using VoiceCapability = Vonage.Applications.Capabilities.Voice;
using MessagesCapability = Vonage.Applications.Capabilities.Messages;
using RtcCapability = Vonage.Applications.Capabilities.Rtc;
using VerifyCapability = Vonage.Applications.Capabilities.Verify;
using VideoCapability = Vonage.Applications.Capabilities.Video;
using VoiceWebhookType = Vonage.Applications.Capabilities.VoiceWebhookType;
#endregion

namespace Vonage.Test.Applications;

internal static class CapabilitiesTestAssertions
{
    internal static void ShouldHaveWebhook(this VerifyCapability capability, Webhook.Type type, Webhook webhook)
    {
        capability.Webhooks.Should().ContainKey(type);
        capability.Webhooks[type].Address.Should().Be(webhook.Address);
        capability.Webhooks[type].Method.Should().Be(webhook.Method);
    }

    internal static void ShouldHaveWebhook(this VideoCapability capability, VideoWebhookType type,
        VideoCapability.VideoWebhook webhook)
    {
        capability.Webhooks.Should().ContainKey(type);
        capability.Webhooks[type].Address.Should().Be(webhook.Address);
        capability.Webhooks[type].Active.Should().Be(webhook.Active);
    }

    internal static void ShouldHaveWebhook(this MessagesCapability capability, Webhook.Type type, Webhook webhook)
    {
        capability.Webhooks.Should().ContainKey(type);
        capability.Webhooks[type].Address.Should().Be(webhook.Address);
        capability.Webhooks[type].Method.Should().Be(webhook.Method);
    }

    internal static void ShouldHaveWebhook(this RtcCapability capability, Webhook.Type type, Webhook webhook)
    {
        capability.Webhooks.Should().ContainKey(type);
        capability.Webhooks[type].Address.Should().Be(webhook.Address);
        capability.Webhooks[type].Method.Should().Be(webhook.Method);
    }

    internal static void ShouldHaveWebhook(this VoiceCapability capability, VoiceWebhookType type,
        VoiceCapability.VoiceWebhook webhook)
    {
        capability.Webhooks.Should().ContainKey(type);
        capability.Webhooks[type].Should().Be(webhook);
    }
}