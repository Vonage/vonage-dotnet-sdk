using System.Text.Json.Serialization;

namespace Vonage.AccountsNew.TopUpBalance;

/// <summary>
///     Represents the response from a top-up balance request.
/// </summary>
public record TopUpBalanceResponse(
    [property: JsonPropertyName("error-code")] string ErrorCode,
    [property: JsonPropertyName("error-code-label")] string ErrorCodeLabel);
