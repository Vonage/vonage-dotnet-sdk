using System.Collections.Generic;

namespace Nexmo.Api.Applications.Capabilities
{
    public class Voice : Capability
    {
        public Voice(IDictionary<Common.Webhook.Type, Common.Webhook> webhooks)
        {
            this.Webhooks = Webhooks;
            this.Type = Capability.CapabilityType.Voice;
        }
    }
}