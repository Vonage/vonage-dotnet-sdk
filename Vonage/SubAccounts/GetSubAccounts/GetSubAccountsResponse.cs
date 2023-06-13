using System.Text.Json.Serialization;

namespace Vonage.SubAccounts.GetSubAccounts;

/// <summary>
///     Represents the primary account and its subaccounts.
/// </summary>
/// <param name="PrimaryAccount">The primary account.</param>
/// <param name="SubAccounts">All subaccounts.</param>
public record GetSubAccountsResponse(
    [property: JsonPropertyName("primary_account")]
    SubAccount PrimaryAccount,
    [property: JsonPropertyName("subaccounts")]
    SubAccount[] SubAccounts);