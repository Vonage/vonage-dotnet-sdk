using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    [System.Obsolete("The Nexmo.Api.Voice.EventWebhooks.HumanMachine class is obsolete. " +
        "References to it should be switched to the new Vonage.Voice.EventWebhooks.HumanMachine class.")]
    public class HumanMachine : CallStatusEvent
    {
        public enum status
        {
            human,
            machine
        }

        /// <summary>
        /// The unique identifier for this call (Note call_uuid, not uuid as in some other endpoints)
        /// </summary>
        [JsonProperty("call_uuid")]
        public override string Uuid { get; set; }

    }
}
