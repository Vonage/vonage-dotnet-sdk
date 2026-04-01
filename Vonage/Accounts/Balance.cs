using Newtonsoft.Json;

namespace Vonage.Accounts;

/// <summary>
///     Represents the balance information for a Vonage account.
/// </summary>
public class Balance
{
    /// <summary>
    ///     The current balance of the account, in EUR.
    /// </summary>
    [JsonProperty("value")]
    public decimal Value { get; set; }

    /// <summary>
    ///     Indicates whether the account has auto-reload enabled.
    ///     When enabled, the account is automatically topped up when the balance falls below a configured threshold.
    /// </summary>
    [JsonProperty("autoReload")]
    public bool AutoReload { get; set; }
}