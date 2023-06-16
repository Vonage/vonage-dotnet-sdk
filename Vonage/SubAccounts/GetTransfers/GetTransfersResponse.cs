using System.Text.Json.Serialization;

namespace Vonage.SubAccounts.GetTransfers;

internal record GetTransfersResponse(
    [property: JsonPropertyName("balance_transfers")]
    Transfer[] BalanceTransfers,
    [property: JsonPropertyName("credit_transfers")]
    Transfer[] CreditTransfers);