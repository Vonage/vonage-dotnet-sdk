using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.NumberInsightV2.FraudCheck;

/// <summary>
///     Represents the response from a fraud check request to the Number Insight V2 API.
/// </summary>
/// <param name="RequestId">Unique identifier for this request, useful for tracking and support inquiries.</param>
/// <param name="Type">The type of lookup performed. Currently always "phone" for phone number lookups.</param>
/// <param name="Phone">Phone number details including carrier and type information when fraud_score insight is requested.</param>
/// <param name="FraudScore">
///     The fraud score result, present only when "fraud_score" was requested in <see cref="FraudCheckRequest.Insights"/>.
///     Contains risk score (0-100), risk recommendation, and risk label.
/// </param>
/// <param name="SimSwap">
///     The SIM swap check result, present only when "sim_swap" was requested in <see cref="FraudCheckRequest.Insights"/>.
///     Indicates whether the SIM was swapped in the last 7 days.
/// </param>
public record FraudCheckResponse(Guid RequestId, string Type, PhoneData Phone,
    [property: JsonConverter(typeof(MaybeJsonConverter<FraudScore>))]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    Maybe<FraudScore> FraudScore,
    [property: JsonConverter(typeof(MaybeJsonConverter<SimSwap>))]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    Maybe<SimSwap> SimSwap);

/// <summary>
///     Contains phone number information returned from the fraud check.
/// </summary>
/// <param name="Phone">The phone number that was checked, in E.164 format.</param>
/// <param name="Carrier">The network carrier name (e.g., "Vodafone", "AT&amp;T"). Only populated when fraud_score insight is requested.</param>
/// <param name="Type">The phone line type: Mobile, Landline, VOIP, PrePaid, Personal, or Toll-Free. Only populated when fraud_score insight is requested.</param>
public record PhoneData(string Phone, string Carrier, string Type);

/// <summary>
///     Contains the fraud score analysis results for a phone number.
/// </summary>
/// <param name="RiskScore">Fraud risk score from 0 (lowest risk) to 100 (highest risk), derived from fraud-related data associated with the phone number.</param>
/// <param name="RiskRecommendation">The recommended action (allow, flag, or block) based on the risk score.</param>
/// <param name="Label">Human-readable risk category: low, medium, or high.</param>
/// <param name="Status">The status of the fraud score operation (e.g., "completed").</param>
public record FraudScore(
    [property: JsonPropertyName("risk_score")]
    string RiskScore,
    [property: JsonPropertyName("risk_recommendation")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<RiskRecommendation>))]
    RiskRecommendation RiskRecommendation,
    [property: JsonPropertyName("label")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<FraudScoreLabel>))]
    FraudScoreLabel Label,
    [property: JsonPropertyName("status")] string Status);

/// <summary>
///     Categorizes the fraud risk level based on the computed risk score.
/// </summary>
public enum FraudScoreLabel
{
    /// <summary>
    ///     Low fraud risk. The phone number shows minimal indicators of fraudulent activity.
    /// </summary>
    [Description("low")] Low,

    /// <summary>
    ///     Medium fraud risk. The phone number shows some indicators that warrant additional verification.
    /// </summary>
    [Description("medium")] Medium,

    /// <summary>
    ///     High fraud risk. The phone number shows strong indicators of potential fraudulent activity.
    /// </summary>
    [Description("high")] High,
}

/// <summary>
///     Defines the recommended action to take based on the fraud risk score.
/// </summary>
public enum RiskRecommendation
{
    /// <summary>
    ///     Allow the transaction or interaction. The phone number has low fraud risk.
    /// </summary>
    [Description("allow")] Allow,

    /// <summary>
    ///     Flag for additional review. The phone number shows moderate fraud risk and may require further verification.
    /// </summary>
    [Description("flag")] Flag,

    /// <summary>
    ///     Block the transaction or interaction. The phone number has high fraud risk.
    /// </summary>
    [Description("block")] Block,
}

/// <summary>
///     Indicates the outcome of the SIM swap check operation.
/// </summary>
public enum SimSwapStatus
{
    /// <summary>
    ///     The SIM swap check completed successfully. The <see cref="SimSwap.Swapped"/> property contains the result.
    /// </summary>
    [Description("completed")] Completed,

    /// <summary>
    ///     The SIM swap check failed. Check the <see cref="SimSwap.Reason"/> property for failure details.
    /// </summary>
    [Description("failed")] Failed,
}

/// <summary>
///     Contains the SIM swap check results indicating whether the phone number's SIM was recently changed.
/// </summary>
/// <param name="Status">The outcome of the SIM swap check: completed or failed.</param>
/// <param name="Swapped">True if the SIM was swapped in the last 7 days, false otherwise. Only valid when <see cref="Status"/> is <see cref="SimSwapStatus.Completed"/>.</param>
/// <param name="Reason">The error reason when the SIM swap check fails. Only populated when <see cref="Status"/> is <see cref="SimSwapStatus.Failed"/>.</param>
public record SimSwap(
    [property: JsonPropertyName("status")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<SimSwapStatus>))]
    SimSwapStatus Status,
    [property: JsonPropertyName("swapped")]
    bool Swapped,
    [property: JsonPropertyName("reason")] string Reason);