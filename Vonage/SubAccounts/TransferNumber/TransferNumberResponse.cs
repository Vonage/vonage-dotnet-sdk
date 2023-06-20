namespace Vonage.SubAccounts.TransferNumber;

/// <summary>
/// </summary>
/// <param name="Number"></param>
/// <param name="Country"></param>
/// <param name="From"></param>
/// <param name="To"></param>
public record TransferNumberResponse(string Number, string Country, string From, string To);