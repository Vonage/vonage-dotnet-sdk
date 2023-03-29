using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.VerifyV2.StartVerification.WhatsApp;

/// <summary>
///     Represents a verification workflow for WhatsApp.
/// </summary>
/// <param name="Channel">The channel name.</param>
/// <param name="To">
///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone
///     number, start with the country code, for example, 447700900000.
/// </param>
/// <param name="From">
///     An optional sender number, in the E.164 format. Don't use a leading + or 00 when entering a phone
///     number, start with the country code, for example, 447700900000.
/// </param>
public record WhatsAppWorkflow([property: JsonPropertyOrder(1)] string To,
    [property: JsonPropertyOrder(3)]
    [property: JsonConverter(typeof(MaybeJsonConverter<string>))]
    Maybe<string> From) : IVerificationWorkflow
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "whatsapp";

    /// <inheritdoc />
    public WhatsAppWorkflow(string to)
        : this(to, Maybe<string>.None)
    {
    }
}