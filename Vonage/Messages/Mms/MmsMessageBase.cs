#region
using System;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Mms;

/// <summary>
/// </summary>
public abstract class MmsMessageBase : MessageRequestBase
{
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
    [JsonPropertyOrder(99)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool TrustedRecipient { get; set; }
}