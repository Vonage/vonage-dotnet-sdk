using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.VerifyV2.StartVerification.Sms;

/// <summary>
///     Represents a verification workflow.
/// </summary>
/// <param name="Channel">The channel name.</param>
/// <param name="To">
///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone
///     number, start with the country code, for example, 447700900000.
/// </param>
/// <param name="Hash">Optional Android Application Hash Key for automatic code detection on a user's device.</param>
public record SmsWorkflow([property: JsonPropertyOrder(1)] string To,
    [property: JsonPropertyOrder(3)]
    [property: JsonPropertyName("app_hash")]
    [property: JsonConverter(typeof(MaybeJsonConverter<string>))]
    Maybe<string> Hash) : IVerificationWorkflow
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "sms";

    /// <inheritdoc />
    public SmsWorkflow(string to)
        : this(to, Maybe<string>.None)
    {
    }
}