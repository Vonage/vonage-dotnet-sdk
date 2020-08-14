using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    [System.Obsolete("The Nexmo.Api.Voice.EventWebhooks.DtmfResult class is obsolete. " +
        "References to it should be switched to the new Vonage.Voice.EventWebhooks.DtmfResult class.")]
    public class DtmfResult
    {
        /// <summary>
        /// the dtmf digits input by the user
        /// </summary>
        [JsonProperty("digits")]
        public string Digits { get; set; }

        /// <summary>
        /// indicates whether the dtmf input timed out
        /// </summary>
        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

    }
}
