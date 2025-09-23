#region
using System.Collections.Generic;
using Vonage.Common;
#endregion

namespace Vonage.Applications.Capabilities;

public class Messages : Capability
{
    public Messages()
    {
        this.Type = CapabilityType.Messages;
        this.Webhooks = new Dictionary<Webhook.Type, Webhook>();
    }

    public Messages(IDictionary<Webhook.Type, Webhook> webhooks)
    {
        this.Webhooks = webhooks;
        this.Type = CapabilityType.Messages;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public static Messages Build() => new Messages();

    /// <summary>
    ///     Sets the inbound URL webhook for Messages capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <returns>The Messages capability instance for fluent chaining.</returns>
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
    ///     Sets the status URL webhook for Messages capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <returns>The Messages capability instance for fluent chaining.</returns>
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