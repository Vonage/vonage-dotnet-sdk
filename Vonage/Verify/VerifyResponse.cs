using Newtonsoft.Json;

namespace Vonage.Verify;

/// <summary>
///     Represents the response from initiating a verification request, containing the request ID for subsequent operations.
/// </summary>
public class VerifyResponse : VerifyResponseBase
{
    /// <summary>
    ///     The unique identifier of the verification request. Use this ID in <see cref="VerifyCheckRequest"/> to validate the user's PIN code. May be empty if the request failed.
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    /// <summary>
    ///     The mobile network code (MNC) of the carrier that blocked the request, if applicable. Only populated for certain error conditions.
    /// </summary>
    [JsonProperty("network")]
    public string Network { get; set; }
}