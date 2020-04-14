using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyEvent
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}