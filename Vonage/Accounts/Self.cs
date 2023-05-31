using Newtonsoft.Json;

namespace Vonage.Accounts;

public class Self
{
    [JsonProperty("href")]
    public string Href { get; set; }
}
