using System.Text.Json.Serialization;

namespace Vonage.VerifyV2.StartVerification.WhatsAppInteractive;

/// <summary>
///     Represents a verification workflow for WhatsApp Interactive.
/// </summary>
/// <param name="To">
///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone
///     number, start with the country code, for example, 447700900000.
/// </param>
public record WhatsAppInteractiveWorkflow([property: JsonPropertyOrder(1)] string To) : IVerificationWorkflow
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "whatsapp_interactive";
}