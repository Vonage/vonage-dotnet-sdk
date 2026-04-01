using Newtonsoft.Json;

namespace Vonage.Accounts;

/// <summary>
///     Represents a request to create a new subaccount under the primary account.
/// </summary>
public class CreateSubAccountRequest
{
    /// <summary>
    ///     The friendly name for the new subaccount. Maximum 80 characters.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     The API secret for the new subaccount. Must meet the same requirements as <see cref="CreateSecretRequest.Secret"/>.
    /// </summary>
    [JsonProperty("secret")]
    public string Secret { get; set; }

    /// <summary>
    ///     Whether the subaccount should use the primary account's balance for API calls.
    ///     Defaults to <c>true</c>. When <c>false</c>, the subaccount maintains its own separate balance.
    /// </summary>
    [JsonProperty("use_primary_account_balance")]
    public bool UsePrimaryAccountBalance { get; set; } = true;
}