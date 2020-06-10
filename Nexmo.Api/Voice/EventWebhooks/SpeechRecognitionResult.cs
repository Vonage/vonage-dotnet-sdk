using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class SpeechRecognitionResult
    {
        /// <summary>
        /// Transcript text representing the words that the user spoke.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// The confidence estimate between 0.0 and 1.0. A higher number indicates an estimated greater likelihood that the recognized words are correct.
        /// </summary>
        [JsonProperty("confidence")]
        public string Confidence { get; set; }
    }
}
