using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    [System.Obsolete("The Nexmo.Api.Voice.EventWebhooks.Event class is obsolete. " +
        "References to it should be switched to the new Vonage.Voice.EventWebhooks.Event class.")]
    public abstract class Event : EventBase
    {
        /// <summary>
        /// The unique identifier for this call
        /// </summary>
        [JsonProperty("uuid")]
        public virtual string Uuid { get; set; }
    }
}