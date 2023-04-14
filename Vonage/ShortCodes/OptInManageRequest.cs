using Newtonsoft.Json;

namespace Vonage.ShortCodes;

public class OptInManageRequest
{
    [JsonProperty("msisdn")]
    public string Msisdn { get; set; }
}