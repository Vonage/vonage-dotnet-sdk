using Newtonsoft.Json;
using Vonage.Voice.Nccos;

namespace Vonage.Voice;

public class Destination
{
    [JsonProperty("ncco")] public Ncco Ncco { get; set; }

    [JsonProperty("type")] public string Type { get; set; } = "ncco";

    [JsonProperty("url")] public string[] Url { get; set; }
}