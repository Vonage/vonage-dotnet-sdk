using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class CreateCustomEventRequest <T> : CreateEventRequestBase where T : class
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("body")]
        public T Body { get; set; }        
    }
}
