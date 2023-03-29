using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.VerifyV2.StartVerification.Sms;

/// <summary>
/// Represents a verification workflow for SMS.
/// </summary>
public struct SmsWorkflow : IVerificationWorkflow
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "sms";

    /// <summary>
    ///     Optional Android Application Hash Key for automatic code detection on a user's device.
    /// </summary>
    [JsonPropertyOrder(3)]
    [JsonPropertyName("app_hash")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    public Maybe<string> Hash { get; set; }

    /// <summary>
    ///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string To { get; set; }

    public SmsWorkflow(string to) : this() => this.To = to;

    public SmsWorkflow(string to, Maybe<string> hash)
    {
        this.To = to;
        this.Hash = hash;
    }
}