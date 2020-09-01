using System.Collections.Generic;

namespace Nexmo.Api.Applications.Capabilities
{
    [System.Obsolete("The Nexmo.Api.Applications.Capabilities.Messages class is obsolete. " +
        "References to it should be switched to the new Vonage.Applications.Capabilities.Messages class.")]
    public class Messages : Capability
    {
        public Messages(IDictionary<Common.Webhook.Type, Common.Webhook> webhooks)
        {
            this.Webhooks = webhooks;
            this.Type = CapabilityType.Messages;
        }
        
    }
}