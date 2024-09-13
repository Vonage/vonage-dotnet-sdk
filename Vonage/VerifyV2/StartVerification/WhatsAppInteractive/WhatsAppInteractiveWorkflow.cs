#region
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.VerifyV2.StartVerification.WhatsAppInteractive;

/// <summary>
///     Represents a verification workflow for WhatsApp Interactive.
/// </summary>
public readonly struct WhatsAppInteractiveWorkflow : IVerificationWorkflow
{
    private WhatsAppInteractiveWorkflow(PhoneNumber to) => this.To = to;

    /// <summary>
    ///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => VerificationChannel.WhatsAppInteractive.AsString(EnumFormat.Description);

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

    /// <summary>
    ///     Parses the input into a WhatsAppInteractiveWorkflow.
    /// </summary>
    /// <param name="to">The phone number to contact.</param>
    /// <returns>Success or failure.</returns>
    public static Result<WhatsAppInteractiveWorkflow> Parse(string to) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new WhatsAppInteractiveWorkflow(phoneNumber));
}