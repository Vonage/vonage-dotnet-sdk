using System.Text.Json.Serialization;

namespace Vonage.AccountsNew.GetBalance;

/// <summary>
///     Represents the balance information for a Vonage account.
/// </summary>
public record GetBalanceResponse(
    [property: JsonPropertyName("value")] decimal Value,
    [property: JsonPropertyName("autoReload")] bool AutoReload);
