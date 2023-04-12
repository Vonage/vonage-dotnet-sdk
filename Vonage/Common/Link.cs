using Newtonsoft.Json;

namespace Vonage.Common;

public class Link
{
    [JsonProperty("href")]
    public string Href { get; set; }
}