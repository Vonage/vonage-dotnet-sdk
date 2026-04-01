using Newtonsoft.Json;

namespace Vonage.Accounts;

/// <summary>
///     Represents a request to create a new API secret for an account.
/// </summary>
public class CreateSecretRequest
{
    /// <summary>
    ///     The new secret value. Must meet the following requirements:
    ///     <list type="bullet">
    ///         <item><description>Minimum 8 characters</description></item>
    ///         <item><description>Maximum 25 characters</description></item>
    ///         <item><description>At least 1 lowercase character</description></item>
    ///         <item><description>At least 1 uppercase character</description></item>
    ///         <item><description>At least 1 digit</description></item>
    ///     </list>
    /// </summary>
    [JsonProperty("secret")]
    public string Secret { get; set; }
}