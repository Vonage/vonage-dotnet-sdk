using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public abstract class Event : EventBase
    {
        [JsonProperty("uuid")]
        public virtual string Uuid { get; set; }
    }
}