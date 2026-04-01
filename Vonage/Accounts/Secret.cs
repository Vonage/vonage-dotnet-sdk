using Newtonsoft.Json;
using Vonage.Common;

namespace Vonage.Accounts;

/// <summary>
///     Represents an API secret associated with a Vonage account.
///     Secrets are used for authenticating API requests and should be rotated periodically for security.
/// </summary>
public class Secret
{
    /// <summary>
    ///     The HAL reference links for the secret resource.
    /// </summary>
    [JsonProperty("_links")]
    public HALLinks Links { get; set; }

    /// <summary>
    ///     The unique identifier for this secret.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    ///     The timestamp when this secret was created, in ISO 8601 format.
    /// </summary>
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }
}