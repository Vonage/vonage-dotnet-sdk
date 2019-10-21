using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos
{
    public class TalkAction : NccoAction
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("bargeIn")]
        public string BargeIn { get; set; }

        [JsonProperty("loop")]
        public string Loop { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("voiceName")]
        public string VoiceName { get; set; }

        public TalkAction()
        {
            Action = ActionType.talk;
        }
    }
}
