using Newtonsoft.Json;
using System;

namespace Vonage.Accounts;

public class PrimaryAccount
{
    /// <summary>
    ///     the primary API Key
    /// </summary>
    [JsonProperty("api_key")]
    public string ApiKey { get; set; }

    /// <summary>
    ///    the name of the account
    /// </summary>

    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     the current account balance
    /// </summary>

    [JsonProperty("balance")]
    public float Balance { get; set; }

    /// <summary>
    ///    the credit limit of the account
    /// </summary>

    [JsonProperty("credit_limit")]
    public float CreditLimit { get; set; }

    /// <summary>
    ///     the suspension state of the account
    /// </summary>

    [JsonProperty("suspended")]
    public bool Suspended{ get; set; }

    /// <summary>
    ///     the creation time of the account
    /// </summary>

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

}
