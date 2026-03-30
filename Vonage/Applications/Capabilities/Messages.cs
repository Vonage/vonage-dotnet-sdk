#region
using System.Collections.Generic;
using Vonage.Common;
#endregion

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the Messages capability for an application. Configures webhooks for inbound messages and message
///     status updates.
/// </summary>
public class Messages : Capability
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Messages" /> class.
    /// </summary>
    public Messages()
    {
        this.Type = CapabilityType.Messages;
        this.Webhooks = new Dictionary<Webhook.Type, Webhook>();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Messages" /> class with the specified webhooks.
    /// </summary>
    /// <param name="webhooks">The webhook configuration dictionary.</param>
    public Messages(IDictionary<Webhook.Type, Webhook> webhooks)
    {
        this.Webhooks = webhooks;
        this.Type = CapabilityType.Messages;
    }

    /// <summary>
    ///     Creates a new Messages capability builder for fluent configuration.
    /// </summary>
    /// <returns>A new Messages capability instance.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var messagesCapability = Messages.Build()
    ///     .WithInboundUrl("https://example.com/webhooks/inbound")
    ///     .WithStatusUrl("https://example.com/webhooks/status");
    /// ]]></code>
    /// </example>
    public static Messages Build() => new Messages();

    /// <summary>
    ///     Sets the inbound URL webhook. Vonage will forward inbound messages to this URL.
    /// </summary>
    /// <param name="url">The webhook URL that will receive inbound messages.</param>
    /// <returns>The Messages capability instance for fluent chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var messages = Messages.Build()
    ///     .WithInboundUrl("https://example.com/webhooks/inbound");
    /// ]]></code>
    /// </example>
    public Messages WithInboundUrl(string url)
    {
        this.Webhooks[Webhook.Type.InboundUrl] = new Webhook
        {
            Address = url,
            Method = nameof(WebhookHttpMethod.Post).ToUpperInvariant(),
        };
        return this;
    }

    /// <summary>
    ///     Sets the status URL webhook. Vonage will send message status updates (e.g. delivered, seen) to this URL.
    /// </summary>
    /// <param name="url">The webhook URL that will receive message status updates.</param>
    /// <returns>The Messages capability instance for fluent chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var messages = Messages.Build()
    ///     .WithStatusUrl("https://example.com/webhooks/status");
    /// ]]></code>
    /// </example>
    public Messages WithStatusUrl(string url)
    {
        this.Webhooks[Webhook.Type.StatusUrl] = new Webhook
        {
            Address = url,
            Method = nameof(WebhookHttpMethod.Post).ToUpperInvariant(),
        };
        return this;
    }
}