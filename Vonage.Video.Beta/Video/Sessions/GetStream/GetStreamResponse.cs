using Newtonsoft.Json;

namespace Vonage.Video.Beta.Video.Sessions.GetStream;

public struct GetStreamResponse
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("videoType")] public string VideoType { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("layoutClassList")] public string[] LayoutClassList { get; set; }
}