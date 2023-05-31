using Newtonsoft.Json;

namespace Vonage.Accounts;

public class Links
{
    [JsonProperty("self")]
    public Self Self { get; set; }
}
