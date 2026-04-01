using Newtonsoft.Json;

namespace Vonage.Accounts;

/// <summary>
///     Represents a request to top up an account balance when auto-reload is enabled.
/// </summary>
public class TopUpRequest
{
    /// <summary>
    ///     The transaction reference from when balance was added and auto-reload was enabled on your account.
    ///     This reference is provided in the payment confirmation when auto-reload was first configured.
    /// </summary>
    [JsonProperty("trx")]
    public string Trx { get; set; }
}