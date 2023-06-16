using System;
using System.Text.Json.Serialization;

namespace Vonage.SubAccounts;

/// <summary>
///     Represents a balance transfer.
/// </summary>
/// <param name="Id">Unique balance transfer ID</param>
/// <param name="Amount">Balance transfer amount</param>
/// <param name="From">Account the balance is transferred from</param>
/// <param name="To">Account the balance is transferred to</param>
/// <param name="Reference">Reference for the balance transfer</param>
/// <param name="CreatedAt">The date and time when the balance transfer was executed</param>
public record BalanceTransfer(
    [property: JsonPropertyName("balance_transfer_id")]
    Guid Id,
    decimal Amount,
    string From,
    string To,
    string Reference,
    [property: JsonPropertyName("created_at")]
    DateTimeOffset CreatedAt);