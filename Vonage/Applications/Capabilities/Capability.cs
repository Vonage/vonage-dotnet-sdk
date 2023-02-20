using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Vonage.Common;
using Vonage.Serialization;

namespace Vonage.Applications.Capabilities
{
    public abstract class Capability
    {
        [JsonProperty("webhooks")]
        [JsonConverter(typeof(WebhookTypeDictionaryConverter))]
        public IDictionary<Webhook.Type, Webhook> Webhooks { get; set; }

        [JsonIgnore] protected CapabilityType Type { get; set; }

        protected enum CapabilityType
        {
            [Description("voice")] Voice,
            [Description("rtc")] Rtc,
            [Description("messages")] Messages,
            [Description("vbc")] Vbc,
            [Description("meetings")] Meetings,
            [Description("video")] Video,
        }
    }
}