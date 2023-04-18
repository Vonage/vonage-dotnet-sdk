using Newtonsoft.Json;

namespace Vonage.ShortCodes;

public class AlertRequest
{
    [JsonProperty("to")]
    public string To { get; set; }

    [JsonProperty("status-report-req")]
    public string StatusReportReq { get; set; }

    [JsonProperty("client-ref")]
    public string ClientRef { get; set; }

    [JsonProperty("template")]
    public string Template { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("custom")]
    public object Custom { get; set; }
}