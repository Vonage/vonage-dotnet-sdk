#region
using System;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
/// </summary>
public abstract class RcsMessageBase : MessageRequestBase
{
    /// <summary>
    /// </summary>
    [JsonPropertyName("rcs")]
    [JsonPropertyOrder(90)]
    public MessageRcs? Rcs { get; set; }

    /// <summary>
    ///     Allows to skip fraud checks on a per-message basis. The feature is feature-flagged and must be enabled for the api
    ///     key.
    /// </summary>
    [JsonIgnore]
    [Obsolete("Favor 'TrustedRecipient' instead.")]
    public bool TrustedNumber { get; set; }

    /// <summary>
    ///     Setting this parameter to true overrides, on a per-message basis, any protections set up via Fraud Defender
    ///     (Traffic Rules, SMS Burst Protection, AIT Protection). This parameter only has any effect for accounts subscribed
    ///     to Fraud Defender Premium.
    /// </summary>
    [JsonPropertyOrder(91)]
    [JsonPropertyName("trusted_recipient")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool TrustedRecipient { get; set; }

    /// <summary>
    ///     The duration in seconds the delivery of a message will be attempted. By default, Vonage attempts delivery for 72
    ///     hours, however the maximum effective value depends on the operator and is typically 24 - 48 hours. We recommend
    ///     this value should be kept at its default or at least 30 minutes.
    /// </summary>
    [JsonPropertyName("ttl")]
    [JsonPropertyOrder(92)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int TimeToLive { get; set; }
}