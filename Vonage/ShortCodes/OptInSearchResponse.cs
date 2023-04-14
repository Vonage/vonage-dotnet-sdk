using Newtonsoft.Json;

namespace Vonage.ShortCodes;

public class OptInSearchResponse
{
    [JsonProperty("opt-in-count")]
    public string OptInCount { get; set; }

    [JsonProperty("opt-in-list")]
    public OptInRecord[] OptInList { get; set; }
}