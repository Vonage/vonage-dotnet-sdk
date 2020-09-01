using System.Collections.Generic;
using System.ComponentModel;
using Vonage.Common;
using Newtonsoft.Json;

namespace Vonage.Applications.Capabilities
{
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