using System.Collections.Generic;

namespace Vonage.Applications.Capabilities;

public class Messages : Capability
{
    public Messages(IDictionary<Common.Webhook.Type, Common.Webhook> webhooks)
    {
        this.Webhooks = webhooks;
        this.Type = CapabilityType.Messages;
    }
}