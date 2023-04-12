using Newtonsoft.Json;

namespace Vonage.Accounts;

public class TopUpResult
{
    [JsonProperty("response")]
    public string Response { get; set; }
}