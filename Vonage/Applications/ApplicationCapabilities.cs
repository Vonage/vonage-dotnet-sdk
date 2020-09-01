using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vonage.Applications.Capabilities;
namespace Vonage.Applications
{
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
