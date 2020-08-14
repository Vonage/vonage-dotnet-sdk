using Newtonsoft.Json;
using Nexmo.Api.Applications.Capabilities;
namespace Nexmo.Api.Applications
{
    [System.Obsolete("The Nexmo.Api.Applications.ApplicationCapabilities class is obsolete. " +
        "References to it should be switched to the new Vonage.Applications.ApplicationCapabilities class.")]
    public class ApplicationCapabilities
    {
        /// <summary>
        /// Voice application webhook config
        /// </summary>
        [JsonProperty("voice")]
        public Capabilities.Voice Voice { get; set; }

        /// <summary>
        /// RTC / Client SDK application webhook config
        /// </summary>
        [JsonProperty("rtc")]
        public Rtc Rtc { get; set; }

        /// <summary>
        /// Specify vbc capability to enable zero-rated calls for VBC number programmability service applications. This must be an empty object.
        /// </summary>
        [JsonProperty("vbc")]
        public Vbc Vbc { get; set; }

        /// <summary>
        /// Messages and Dispatch application webhook config
        /// </summary>
        [JsonProperty("messages")]
        public Messages Messages { get; set; }
    }
}
