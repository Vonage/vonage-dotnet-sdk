using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos
{
    public class StreamAction : NccoAction
    {
        [JsonProperty("streamUrl")]
        public string StreamUrl { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("bargeIn")]
        public string BargeIn { get; set; }

        [JsonProperty("loop")]
        public string Loop { get; set; }

        public StreamAction()
        {
            Action = ActionType.stream;
        }
    }
}
