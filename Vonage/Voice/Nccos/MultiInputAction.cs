using Newtonsoft.Json;

namespace Vonage.Voice.Nccos
{
    public class MultiInputAction : NccoAction
    {
        /// <summary>
        /// Vonage sends the digits pressed by the callee to this URL 1) after timeOut pause in activity or when # is pressed for DTMF or 2) after user stops speaking or 30 seconds of speech for speech input.
        /// </summary>
        [JsonProperty("eventUrl")]
        public string[] EventUrl { get; set; }

        /// <summary>
        /// The HTTP method used to send event information to event_url The default value is POST.
        /// </summary>
        [JsonProperty("eventMethod")]
        public string EventMethod { get; set; }

        /// <summary>
        /// DTMF settings. Should be specified to enable DTMF input. If all the DTMF input settings will have default values, it should be specified as empty object
        /// NOTE: this is serialized as an empty object if not initalized by the user
        /// </summary>
        [JsonProperty("dtmf", DefaultValueHandling = DefaultValueHandling.Include)]
        public DtmfSettings Dtmf { get; set; }

        /// <summary>
        /// Speech recognition settings. Should be specified to enable speech input.
        /// </summary>
        [JsonProperty("speech")]
        public SpeechSettings Speech { get; set; }

        public MultiInputAction()
        {
            Dtmf = new DtmfSettings();
            this.Action = ActionType.input;
        }
    }
}
