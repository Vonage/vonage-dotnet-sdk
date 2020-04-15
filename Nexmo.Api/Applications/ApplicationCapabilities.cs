using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexmo.Api.Applications.Capabilities;
namespace Nexmo.Api.Applications
{
    public class ApplicationCapabilities
    {
        [JsonProperty("voice")]
        public Capabilities.Voice Voice { get; set; }

        [JsonProperty("rtc")]
        public Rtc Rtc { get; set; }

        [JsonProperty("vbc")]
        public Vbc Vbc { get; set; }

        [JsonProperty("messages")]
        public Messages Messages { get; set; }
    }
}
