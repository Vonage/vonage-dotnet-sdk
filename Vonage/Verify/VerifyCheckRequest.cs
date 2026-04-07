using Newtonsoft.Json;

namespace Vonage.Verify;

/// <summary>
///     Represents a request to validate a PIN code entered by the user against an active verification request.
/// </summary>
public class VerifyCheckRequest
{
    /// <summary>
    ///     The unique identifier of the verification request to validate against. This is the <see cref="VerifyResponse.RequestId"/> received from <see cref="IVerifyClient.VerifyRequestAsync"/>.
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    /// <summary>
    ///     The PIN code entered by the user. Must match the code sent in the verification SMS or voice call.
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    ///     Deprecated. This field is no longer used and will be ignored.
    /// </summary>
    [JsonProperty("ip_address")]
    public string IpAddress { get; set; }
}