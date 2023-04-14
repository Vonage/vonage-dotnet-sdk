using Newtonsoft.Json;

namespace Vonage.Verify;

public class VerifyEvent
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }
}