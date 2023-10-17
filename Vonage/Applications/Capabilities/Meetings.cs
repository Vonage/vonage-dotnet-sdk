using System.Collections.Generic;
using Vonage.Common;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the Meetings capability.
/// </summary>
public class Meetings : Capability
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public Meetings()
    {
        this.Type = CapabilityType.Meetings;
        this.Webhooks = new Dictionary<Webhook.Type, Webhook>();
    }

    /// <summary>
    ///     Constructor.
    /// </summary>
    public Meetings(IDictionary<Webhook.Type, Webhook> webhooks) : this()
    {
        this.Webhooks = webhooks;
    }
}