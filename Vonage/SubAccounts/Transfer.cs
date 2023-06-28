using System;
using System.Text.Json.Serialization;

namespace Vonage.SubAccounts;

/// <summary>
///     Represents a transfer.
/// </summary>
/// <param name="Id">Unique transfer ID</param>
/// <param name="Amount">Transfer amount</param>
/// <param name="From">Account the amount is transferred from</param>
/// <param name="To">Account the amount is transferred to</param>
/// <param name="Reference">Reference for the transfer</param>
/// <param name="CreatedAt">The date and time when the transfer was executed</param>
public record Transfer(
    Guid Id,
    decimal Amount,
    string From,
    string To,
    string Reference,
    [property: JsonPropertyName("created_at")]
    DateTimeOffset CreatedAt);