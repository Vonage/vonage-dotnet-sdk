using Newtonsoft.Json;

namespace Vonage.Voice.EventWebhooks
{
    public class HumanMachine : CallStatusEvent
    {
        /// <summary>
        /// The unique identifier for this call (Note call_uuid, not uuid as in some other endpoints)
        /// </summary>
        [JsonProperty("call_uuid")]
        public override string Uuid { get; set; }

    }
}
