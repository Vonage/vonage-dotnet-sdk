using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class Media
    {
        [JsonProperty("audio_settings")]
        public MediaAudioSettings AudioSettings { get; set; }
        public class MediaAudioSettings
        {
            [JsonProperty("enabled")]
            public bool Enabled { get; set; }

            [JsonProperty("earmuffed")]
            public bool Earmuffed { get; set; }

            [JsonProperty("muted")]
            public bool Muted { get; set; }
        }
    }
}
