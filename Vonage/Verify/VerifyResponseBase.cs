using Newtonsoft.Json;

namespace Vonage.Verify;

/// <summary>
///     Base class for verification responses, containing common status and error properties.
/// </summary>
public abstract class VerifyResponseBase
{
    /// <summary>
    ///     A human-readable error message explaining why the request failed. Only populated when <see cref="Status"/> is non-zero.
    /// </summary>
    [JsonProperty("error_text")]
    public string ErrorText { get; set; }

    /// <summary>
    ///     The status code of the response. A value of "0" indicates success; any other value indicates an error. Check <see cref="ErrorText"/> for details when non-zero.
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }
}