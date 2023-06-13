using System;
using System.Text.Json.Serialization;

namespace Vonage.SubAccounts;

public record SubAccount(
    [property: JsonPropertyName("api_key")]
    string ApiKey,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("primary_account_api_key")]
    string PrimaryAccountApiKey,
    [property: JsonPropertyName("use_primary_account_balance")]
    bool UsePrimaryAccountBalance,
    [property: JsonPropertyName("created_at")]
    DateTimeOffset CreatedAt,
    [property: JsonPropertyName("suspended")]
    bool IsSuspended,
    [property: JsonPropertyName("balance")]
    decimal Balance,
    [property: JsonPropertyName("credit_limit")]
    decimal CreditLimit);