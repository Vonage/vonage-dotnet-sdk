#region
using System.Collections.Generic;
using Vonage.Common;
#endregion

namespace Vonage.Applications.Capabilities;

/// <summary>
/// </summary>
public class Verify : Capability
{
    public Verify()
    {
        this.Type = CapabilityType.Verify;
        this.Webhooks = new Dictionary<Webhook.Type, Webhook>();
    }

    public Verify(IDictionary<Webhook.Type, Webhook> webhooks)
    {
        this.Webhooks = webhooks;
        this.Type = CapabilityType.Verify;
    }

    /// <summary>
    ///     Sets the status URL webhook for Verify capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <returns>The Verify capability instance for fluent chaining.</returns>
    public Verify WithStatusUrl(string url)
    {
        this.Webhooks[Webhook.Type.StatusUrl] = new Webhook
        {
            Address = url,
            Method = nameof(WebhookHttpMethod.Post).ToUpperInvariant(),
        };
        return this;
    }
}