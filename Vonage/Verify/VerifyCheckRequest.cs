using Newtonsoft.Json;

namespace Vonage.Verify;

public class VerifyCheckRequest
{
    /// <summary>
    /// The Verify request to check. This is the request_id you received in the response to the Verify request
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    /// <summary>
    /// The verification code entered by your user.
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// (This field is no longer used)
    /// </summary>
    [JsonProperty("ip_address")]
    public string IpAddress { get; set; }
}