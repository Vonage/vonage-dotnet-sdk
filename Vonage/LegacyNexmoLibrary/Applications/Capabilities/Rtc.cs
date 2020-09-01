using System.Collections.Generic;

namespace Nexmo.Api.Applications.Capabilities
{
    [System.Obsolete("The Nexmo.Api.Applications.Capabilities.Rtc class is obsolete. " +
        "References to it should be switched to the new Vonage.Applications.Capabilities.Rtc class.")]
    public class Rtc : Capability
    {
        public Rtc(IDictionary<Common.Webhook.Type, Common.Webhook> webhooks)
        {
            this.Webhooks = webhooks;
            this.Type = CapabilityType.Rtc;
        }
    }
}