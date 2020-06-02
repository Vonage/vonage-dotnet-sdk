using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos
{
    public class SpeechSettings
    {
        /// <summary>
        /// The unique ID of the Call leg for the user to capture the speech of.
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Controls how long the system will wait after user stops speaking to decide the input is completed. The default value is 2 (seconds). The range of possible values is between 1 second and 10 seconds.
        /// </summary>
        [JsonProperty("endOnSilence")]
        public int? EndOnSilence { get; set; }

        /// <summary>
        /// Expected language of the user's speech. Format: BCP-47. Default: en-US see list of supported languages: https://developer.nexmo.com/voice/voice-api/guides/asr#language
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Array of hints (strings) to improve recognition quality if certain words are expected from the user.
        /// </summary>
        [JsonProperty("context")]
        public string[] Context { get; set; }

        /// <summary>
        /// Controls how long the system will wait for the user to start speaking. The range of possible values is between 1 second and 10 seconds.
        /// </summary>
        [JsonProperty("startTimeout")]
        public int? StartTimeout { get; set; }

        /// <summary>
        /// Controls maximum speech duration (from the moment user starts speaking). The default value is 60 (seconds). The range of possible values is between 1 and 60 seconds.
        /// </summary>
        [JsonProperty("maxDuration")]
        public int? MaxDuration { get; set; }
    }
}
