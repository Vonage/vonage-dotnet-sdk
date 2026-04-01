using Newtonsoft.Json;

namespace Vonage.Accounts;

/// <summary>
///     Represents a subaccount under a primary Vonage account.
///     Subaccounts allow you to segment your usage and manage access for different parts of your organization.
/// </summary>
public class SubAccount
{
    /// <summary>
    ///     The unique API key for this subaccount.
    /// </summary>
    [JsonProperty("api_key")]
    public string ApiKey { get; set; }

    /// <summary>
    ///     The current balance of the subaccount, in EUR.
    ///     Only present if the subaccount does not use the primary account balance.
    /// </summary>
    [JsonProperty("balance")]
    public double? Balance { get; set; }

    /// <summary>
    ///     The timestamp when this subaccount was created, in ISO 8601 format.
    /// </summary>
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }

    /// <summary>
    ///     The credit limit for this subaccount.
    ///     Only applicable when the subaccount does not use the primary account balance.
    /// </summary>
    [JsonProperty("credit_limit")]
    public double? CreditLimit { get; set; }

    /// <summary>
    ///     The friendly name of the subaccount.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     The API key of the primary account that owns this subaccount.
    /// </summary>
    [JsonProperty("primary_account_api_key")]
    public string PrimaryAccountApiKey { get; set; }

    /// <summary>
    ///     The API secret for this subaccount. Only returned when creating a new subaccount.
    /// </summary>
    [JsonProperty("secret")]
    public string Secret { get; set; }

    /// <summary>
    ///     Indicates whether this subaccount is currently suspended.
    ///     Suspended subaccounts cannot make API calls.
    /// </summary>
    [JsonProperty("suspended")]
    public bool Suspended { get; set; }

    /// <summary>
    ///     Indicates whether this subaccount uses the primary account's balance for API calls.
    ///     When <c>true</c>, charges are deducted from the primary account.
    /// </summary>
    [JsonProperty("use_primary_account_balance")]
    public bool UsePrimaryAccountBalance { get; set; }
}