using Newtonsoft.Json;
using Vonage.Voice.Nccos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Voice
{
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
