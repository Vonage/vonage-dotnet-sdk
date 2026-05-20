#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
///     Base class for all RCS (Rich Communication Services) message requests.
/// </summary>
public abstract class RcsMessageBase : MessageRequestBase
{
    private const int TtlMin = 20;
    private const int TtlMax = 259200;
    private const int ToMinLength = 7;
    private const int ToMaxLength = 15;

    /// <inheritdoc />
    public override IEnumerable<string> GetErrors() =>
        this.ValidateFrom()
            .Concat(this.ValidateTo())
            .Concat(this.ValidateTtl())
            .Concat(this.ValidateWebhookVersion());

    private IEnumerable<string> ValidateFrom()
    {
        if (string.IsNullOrEmpty(this.From))
            yield return "From must not be null or empty.";
    }

    private IEnumerable<string> ValidateTo()
    {
        if (string.IsNullOrEmpty(this.To))
            yield return "To must not be null or empty.";
        else if (this.To.Length is < ToMinLength or > ToMaxLength)
            yield return $"To length must be between {ToMinLength} and {ToMaxLength} characters.";
    }

    private IEnumerable<string> ValidateTtl()
    {
        if (this.TimeToLive != 0 && this.TimeToLive is < TtlMin or > TtlMax)
            yield return $"TimeToLive must be between {TtlMin} and {TtlMax}.";
    }

    private IEnumerable<string> ValidateWebhookVersion()
    {
        if (this.WebhookVersion != null && this.WebhookVersion != "v0.1" && this.WebhookVersion != "v1")
            yield return "WebhookVersion must be 'v0.1' or 'v1'.";
    }

    /// <summary>
    ///     RCS-specific configuration including card orientation, width, and image alignment.
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