using Newtonsoft.Json;
using Vonage.Common;

namespace Vonage.ShortCodes;

public class TwoFactorAuthResponse
{
    [JsonProperty("message-count")]
    public string MessageCount { get; set; }

        
    public Message[] Messages { get; set; }
}