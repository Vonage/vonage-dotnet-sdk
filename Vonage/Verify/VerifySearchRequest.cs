using Newtonsoft.Json;

namespace Vonage.Verify;

/// <summary>
///     Represents a request to retrieve the status and details of an existing verification request.
/// </summary>
public class VerifySearchRequest
{
    /// <summary>
    ///     The unique identifier of the verification request to look up. This is the <see cref="VerifyResponse.RequestId"/> received from <see cref="IVerifyClient.VerifyRequestAsync"/>.
    /// </summary>
    [JsonProperty("request_id")]
    public string RequestId { get; set; }
}