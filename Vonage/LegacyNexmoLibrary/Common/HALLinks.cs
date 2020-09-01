using Newtonsoft.Json;

namespace Nexmo.Api.Common
{
    [System.Obsolete("The Nexmo.Api.Common.HALLinks class is obsolete. " +
        "References to it should be switched to the new Vonage.Common.HALLinks class.")]
    public class HALLinks
    {
        [JsonProperty("self")]
        public Link Self { get; set; }
        
        [JsonProperty("next")]
        public Link Next { get; set; }
        
        [JsonProperty("prev")]
        public Link Prev { get; set; }
        
        [JsonProperty("first")]
        public Link First { get; set; }
        
        [JsonProperty("last")]
        public Link Last { get; set; }
    }
}