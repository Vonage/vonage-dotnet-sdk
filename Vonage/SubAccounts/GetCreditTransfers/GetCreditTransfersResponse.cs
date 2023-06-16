using System.Text.Json.Serialization;

namespace Vonage.SubAccounts.GetCreditTransfers;

internal record GetCreditTransfersResponse(
    [property: JsonPropertyName("credit_transfers")]
    Transfer[] CreditTransfers);