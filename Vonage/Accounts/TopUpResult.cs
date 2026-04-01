using Newtonsoft.Json;

namespace Vonage.Accounts;

/// <summary>
///     Represents the response from a top-up request.
/// </summary>
public class TopUpResult
{
    /// <summary>
    ///     The response message indicating the result of the top-up operation.
    /// </summary>
    [JsonProperty("response")]
    public string Response { get; set; }
}