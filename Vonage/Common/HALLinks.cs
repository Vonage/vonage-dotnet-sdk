using Newtonsoft.Json;

namespace Vonage.Common;

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