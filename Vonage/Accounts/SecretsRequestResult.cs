using Newtonsoft.Json;
using Vonage.Common;

namespace Vonage.Accounts;

/// <summary>
///     Represents the response from retrieving API secrets for an account.
/// </summary>
public class SecretsRequestResult
{
    /// <summary>
    ///     The HAL reference links for the secrets collection.
    /// </summary>
    [JsonProperty("_links")]
    public HALLinks Links { get; set; }

    /// <summary>
    ///     The embedded collection of secrets. Access the secrets via <see cref="SecretList.Secrets"/>.
    /// </summary>
    [JsonProperty("_embedded")]
    public SecretList Embedded { get; set; }
}