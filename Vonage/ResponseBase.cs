using Newtonsoft.Json;

namespace Vonage;

public class ResponseBase
{
    [JsonProperty("error-code")]
    public string ErrorCode { get; set; }
    [JsonProperty("error-code-label")]
    public string ErrorCodeLabel { get; set; }
}