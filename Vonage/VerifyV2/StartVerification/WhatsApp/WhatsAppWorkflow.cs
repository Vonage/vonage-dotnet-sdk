using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.VerifyV2.StartVerification.WhatsApp;

/// <summary>
///     Represents a verification workflow for WhatsApp.
/// </summary>
public readonly struct WhatsAppWorkflow : IVerificationWorkflow
{
    private WhatsAppWorkflow(PhoneNumber to, Maybe<PhoneNumber> from)
    {
        this.To = to;
        this.From = from;
    }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "whatsapp";

    /// <summary>
    ///     An optional sender number, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<PhoneNumber>))]
    [JsonPropertyOrder(3)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<PhoneNumber> From { get; }

    /// <summary>
    ///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <summary>
    /// Parses the input into a WhatsAppWorkflow.
    /// </summary>
    /// <param name="to">The phone number to contact.</param>
    /// <returns>Success or failure.</returns>
    public static Result<WhatsAppWorkflow> Parse(string to) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new WhatsAppWorkflow(phoneNumber, Maybe<PhoneNumber>.None));

    /// <summary>
    /// Parses the input into a WhatsAppWorkflow.
    /// </summary>
    /// <param name="to">The phone number to contact.</param>
    /// <param name="from">The sender number.</param>
    /// <returns>Success or failure.</returns>
    public static Result<WhatsAppWorkflow> Parse(string to, string from) =>
        PhoneNumber.Parse(to)
            .Merge(PhoneNumber.Parse(from), (toNumber, fromNumber) => new WhatsAppWorkflow(toNumber, fromNumber));

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}