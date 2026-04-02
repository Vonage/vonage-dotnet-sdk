using Newtonsoft.Json;

namespace Vonage.Numbers;

/// <summary>
///     Represents the response from a number transaction operation such as buying, canceling, or updating a number.
/// </summary>
public class NumberTransactionResponse
{
    /// <summary>
    /// The status code of the response. 200 indicates a successful request.
    /// </summary>
    [JsonProperty("error-code")]
    public string ErrorCode { get; set; }

    /// <summary>
    /// The status code description
    /// </summary>
    [JsonProperty("error-code-label")]
    public string ErrorCodeLabel { get; set; }
}