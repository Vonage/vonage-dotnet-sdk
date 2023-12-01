using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.NumberInsightV2.FraudCheck;

/// <summary>
///     Represents the response of a Fraud Check request.
/// </summary>
/// <param name="RequestId">Unique UUID for this request for reference.</param>
/// <param name="Type">The type of lookup used in the request. Currently always phone.</param>
/// <param name="Phone">
///     An object containing at least the phone number that was used in the fraud check. If fraud_score was
///     also requested and successful, other phone information (carrier and type) will be returned.
/// </param>
/// <param name="FraudScore">
///     The result of the fraud_score insight operation. The fraud_score object will only be returned
///     if you specified fraud_score as a value in the insights array when the request was made.
/// </param>
/// <param name="SimSwap">
///     The result of the sim_swap insight operation. If successful, it will return swapped: true if the
///     sim was swapped in the last 7 days. The sim_swap object will only be returned if you specified sim_swap as a value
///     in the insights array when the request was made.
/// </param>
public record FraudCheckResponse(Guid RequestId, string Type, PhoneData Phone, FraudScore FraudScore, SimSwap SimSwap);

/// <summary>
///     Represents an object containing at least the phone number that was used in the fraud check.
/// </summary>
/// <param name="Phone">The phone number used in the fraud check operation(s).</param>
/// <param name="Carrier">The name of the network carrier. Included if insights included fraud_score.</param>
/// <param name="Type">
///     Type of phone. Examples include Mobile, Landline, VOIP, PrePaid, Personal, Toll-Free. Included if
///     insights included fraud_score.
/// </param>
public record PhoneData(string Phone, string Carrier, string Type);

/// <summary>
///     Represents the result of the fraud_score insight operation..
/// </summary>
/// <param name="RiskScore">
///     Score derived from evaluating fraud-related data associated with the phone number. risk_score
///     ranges from 0-100, with 0 meaning least risk and 100 meaning highest risk.
/// </param>
/// <param name="RiskRecommendation">Recommended action based on the risk_score.</param>
/// <param name="Label">Mapping of risk score to a verbose description.</param>
/// <param name="Status">The status of the fraud_score call.</param>
public record FraudScore(string RiskScore, string RiskRecommendation,
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<FraudScoreLabel>))]
    FraudScoreLabel Label, string Status);

/// <summary>
///     Represents the mapping of risk score to a verbose description.
/// </summary>
public enum FraudScoreLabel
{
    /// <summary>
    /// </summary>
    [Description("low")] Low,

    /// <summary>
    /// </summary>
    [Description("medium")] Medium,

    /// <summary>
    /// </summary>
    [Description("high")] High,
}

/// <summary>
///     Represents the result of the sim_swap insight operation.
/// </summary>
/// <param name="Status">The status of the sim_swap call.</param>
/// <param name="Swapped">
///     true if the sim was swapped in the last 7 days, false otherwise. Returned only if the sim swap
///     check succeeds.
/// </param>
/// <param name="Reason">The reason for a sim swap error response. Returned only if the sim swap check fails.</param>
public record SimSwap(string Status, bool Swapped, string Reason);