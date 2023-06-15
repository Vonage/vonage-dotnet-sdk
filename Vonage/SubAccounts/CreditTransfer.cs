using System;
using System.Text.Json.Serialization;

namespace Vonage.SubAccounts;

/// <summary>
///     Represents a credit transfer.
/// </summary>
/// <param name="Id">Unique credit transfer ID</param>
/// <param name="Amount">Credit transfer amount</param>
/// <param name="From">Account the credit is transferred from</param>
/// <param name="To">Account the credit is transferred to</param>
/// <param name="Reference">Reference for the credit transfer</param>
/// <param name="CreatedAt">The date and time when the credit transfer was executed</param>
public record CreditTransfer(
    [property: JsonPropertyName("credit_transfer_id")]
    Guid Id,
    decimal Amount,
    string From,
    string To,
    string Reference,
    [property: JsonPropertyName("created_at")]
    DateTimeOffset CreatedAt);