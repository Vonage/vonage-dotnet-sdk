using System;
using System.Text.Json.Serialization;

namespace Vonage.SubAccounts;

/// <summary>
///     Represents an account.
/// </summary>
/// <param name="ApiKey">Unique SubAccount ID.</param>
/// <param name="Name">Name of the SubAccount.</param>
/// <param name="PrimaryAccountApiKey">Unique primary account ID.</param>
/// <param name="UsePrimaryAccountBalance">Flag showing if balance is shared with primary account.</param>
/// <param name="CreatedAt">SubAccount creation date and time.</param>
/// <param name="IsSuspended">SubAccount suspension status.</param>
/// <param name="Balance">Balance of the SubAccount. Value is null if balance is shared with primary account.</param>
/// <param name="CreditLimit">Credit limit of the SubAccount. Value is null if balance is shared with primary account.</param>
public record Account(
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
    decimal? Balance,
    [property: JsonPropertyName("credit_limit")]
    decimal? CreditLimit);