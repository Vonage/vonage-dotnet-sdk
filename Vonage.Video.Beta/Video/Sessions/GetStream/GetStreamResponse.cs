using Newtonsoft.Json;

namespace Vonage.Video.Beta.Video.Sessions.GetStream;

public struct GetStreamResponse
{
    public GetStreamResponse(string id, string videoType, string name, string[] layoutClassList)
    {
        this.Id = id;
        this.VideoType = videoType;
        this.Name = name;
        this.LayoutClassList = layoutClassList;
    }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("videoType")] public string VideoType { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("layoutClassList")] public string[] LayoutClassList { get; set; }
}