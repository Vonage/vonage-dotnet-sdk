#region
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.VerifyV2.StartVerification.WhatsApp;

/// <summary>
///     Represents a verification workflow for WhatsApp.
/// </summary>
public readonly struct WhatsAppWorkflow : IVerificationWorkflow
{
    private WhatsAppWorkflow(PhoneNumber to, PhoneNumber from)
    {
        this.To = to;
        this.From = from;
    }

    /// <summary>
    ///     An optional sender number, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber From { get; }

    /// <summary>
    ///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => VerificationChannel.WhatsApp.AsString(EnumFormat.Description);

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

    /// <summary>
    ///     Parses the input into a WhatsAppWorkflow.
    /// </summary>
    /// <param name="to">The phone number to contact.</param>
    /// <param name="from">The sender number.</param>
    /// <returns>Success or failure.</returns>
    public static Result<WhatsAppWorkflow> Parse(string to, string from) =>
        PhoneNumber.Parse(to).Merge(PhoneNumber.Parse(from),
            (toNumber, fromNumber) => new WhatsAppWorkflow(toNumber, fromNumber));
}