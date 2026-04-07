namespace Vonage.SubAccounts.TransferNumber;

/// <summary>
///     Represents the response from a number transfer operation between accounts.
/// </summary>
/// <param name="Number">The phone number that was transferred.</param>
/// <param name="Country">The two-character country code in ISO 3166-1 alpha-2 format (e.g., "GB", "US").</param>
/// <param name="From">The API key of the account the number was transferred from.</param>
/// <param name="To">The API key of the account the number was transferred to.</param>
public record TransferNumberResponse(string Number, string Country, string From, string To);