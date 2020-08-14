using Newtonsoft.Json;
using Nexmo.Api.Voice.Nccos;

namespace Nexmo.Api.Voice
{
    [System.Obsolete("The Nexmo.Api.Voice.Destination class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.Destination class.")]
    public class Destination
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "ncco";

        [JsonProperty("url")]
        public string[] Url { get; set; }

        [JsonProperty("ncco")]
        public Ncco Ncco { get; set; }
    }
}
