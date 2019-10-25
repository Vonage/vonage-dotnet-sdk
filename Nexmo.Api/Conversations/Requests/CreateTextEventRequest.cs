using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class CreateTextEventRequest : CreateEventRequestBase
    {
        [JsonProperty("type")]
        public string Type { get; private set; } = "text";

        [JsonProperty("body")]
        public TextEventBody Body { get; set; }        
    }
}
