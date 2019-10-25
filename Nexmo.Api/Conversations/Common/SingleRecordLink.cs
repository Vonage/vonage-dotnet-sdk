using Newtonsoft.Json;

namespace Nexmo.Api.Conversations
{
    public class SingleRecordLink
    {
        [JsonProperty("self")]
        public Link Self { get; set; }
    }
}
