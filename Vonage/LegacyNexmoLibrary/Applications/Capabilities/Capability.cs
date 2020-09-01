using System.Collections.Generic;
using System.ComponentModel;
using Nexmo.Api.Common;
using Newtonsoft.Json;

namespace Nexmo.Api.Applications.Capabilities
{
    [System.Obsolete("The Nexmo.Api.Applications.Capabilities.Capability class is obsolete. " +
        "References to it should be switched to the new Vonage.Applications.Capabilities.Capability class.")]
    public abstract class Capability
    {
        [JsonProperty("webhooks")]
        public IDictionary<Common.Webhook.Type,Common.Webhook> Webhooks { get; set; }
        
        [JsonIgnore]
        public CapabilityType Type { get; protected set; }

        public enum CapabilityType
        {
            [Description("voice")]
            Voice,
            [Description("rtc")]
            Rtc,
            [Description("messages")]
            Messages,
            [Description("vbc")]
            Vbc
        }
    }
}