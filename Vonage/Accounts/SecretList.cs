using Newtonsoft.Json;

namespace Vonage.Accounts;

/// <summary>
///     Represents a collection of API secrets associated with an account.
/// </summary>
public class SecretList
{
    /// <summary>
    ///     The array of secrets associated with the account. A maximum of two secrets can exist per API key.
    /// </summary>
    [JsonProperty("secrets")]
    public Secret[] Secrets { get; set; }
}