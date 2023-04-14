using Newtonsoft.Json;

namespace Vonage.Voice.EventWebhooks;

public abstract class Event : EventBase
{
    /// <summary>
    /// The unique identifier for this call
    /// </summary>
    [JsonProperty("uuid")]
    public virtual string Uuid { get; set; }
}