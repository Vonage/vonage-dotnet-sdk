using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexmo.Api.Conversations
{
    public class CreateCustomEventResponse
    {
        [JsonProperty("body")]
        public Dictionary<string,string> Body { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
