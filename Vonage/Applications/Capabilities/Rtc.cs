using System.Collections.Generic;

namespace Vonage.Applications.Capabilities;

public class Rtc : Capability
{
    public Rtc(IDictionary<Common.Webhook.Type, Common.Webhook> webhooks)
    {
        this.Webhooks = webhooks;
        this.Type = CapabilityType.Rtc;
    }
}