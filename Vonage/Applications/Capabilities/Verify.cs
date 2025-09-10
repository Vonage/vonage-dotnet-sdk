#region
using System.Collections.Generic;
using Vonage.Common;
#endregion

namespace Vonage.Applications.Capabilities;

/// <summary>
/// </summary>
public class Verify : Capability
{
    public Verify(IDictionary<Webhook.Type, Webhook> webhooks)
    {
        this.Webhooks = webhooks;
        this.Type = CapabilityType.Messages;
    }
}