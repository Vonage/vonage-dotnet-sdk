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
    internal static void ShouldHaveWebhook(this VerifyCapability capability, Webhook.Type type, string address,
        string method)
    {
        capability.Webhooks.Should().ContainKey(type);
        var webhook = capability.Webhooks[type];
        webhook.Address.Should().Be(address);
        webhook.Method.Should().Be(method);
    }

    internal static void ShouldHaveWebhook(this VideoCapability capability, VideoWebhookType type, string address,
        bool active)
    {
        capability.Webhooks.Should().ContainKey(type);
        var webhook = capability.Webhooks[type];
        webhook.Address.ToString().Should().Be(address);
        webhook.Active.Should().Be(active);
    }

    internal static void ShouldHaveWebhook(this MessagesCapability capability, Webhook.Type type, string address,
        string method)
    {
        capability.Webhooks.Should().ContainKey(type);
        var webhook = capability.Webhooks[type];
        webhook.Address.Should().Be(address);
        webhook.Method.Should().Be(method);
    }

    internal static void ShouldHaveWebhook(this RtcCapability capability, Webhook.Type type, string address,
        string method)
    {
        capability.Webhooks.Should().ContainKey(type);
        var webhook = capability.Webhooks[type];
        webhook.Address.Should().Be(address);
        webhook.Method.Should().Be(method);
    }

    internal static void ShouldHaveWebhook(this VoiceCapability capability, VoiceWebhookType type,
        VoiceCapability.VoiceWebhook webhook)
    {
        capability.Webhooks.Should().ContainKey(type);
        capability.Webhooks[type].Should().Be(webhook);
    }
}