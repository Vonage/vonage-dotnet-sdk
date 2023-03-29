using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.VerifyV2.StartVerification.WhatsAppInteractive;

/// <summary>
///     Represents a verification workflow for WhatsApp Interactive.
/// </summary>
public readonly struct WhatsAppInteractiveWorkflow : IVerificationWorkflow
{
    private WhatsAppInteractiveWorkflow(PhoneNumber to) => this.To = to;

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "whatsapp_interactive";

    /// <summary>
    ///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <summary>
    ///     Parses the input into a WhatsAppInteractiveWorkflow.
    /// </summary>
    /// <param name="to">The phone number to contact.</param>
    /// <returns>Success or failure.</returns>
    public static Result<WhatsAppInteractiveWorkflow> Parse(string to) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new WhatsAppInteractiveWorkflow(phoneNumber));
}