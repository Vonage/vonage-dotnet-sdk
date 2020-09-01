using System.Collections.Generic;

namespace Nexmo.Api.Applications.Capabilities
{
    [System.Obsolete("The Nexmo.Api.Applications.Capabilities.Voice class is obsolete. " +
        "References to it should be switched to the new Vonage.Applications.Capabilities.Voice class.")]
    public class Voice : Capability
    {
        public Voice(IDictionary<Common.Webhook.Type, Common.Webhook> webhooks)
        {
            this.Webhooks = webhooks;
            this.Type = Capability.CapabilityType.Voice;
        }
    }
}