using Newtonsoft.Json;
using Vonage.Common;
namespace Vonage.ShortCodes;

public class AlertResponse
{
    [JsonProperty("message-count")]
    public string MessageCount { get; set; }

        
    public Message[] Messages { get; set; }
}