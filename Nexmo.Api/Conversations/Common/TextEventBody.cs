using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class TextEventBody
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
