using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.VerifyV2.StartVerification.WhatsApp;

/// <summary>
///    Represents a verification workflow for WhatsApp.
/// </summary>
public struct WhatsAppWorkflow : IVerificationWorkflow
{
    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "whatsapp";

    /// <summary>
    ///     An optional sender number, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonPropertyOrder(3)]
    public Maybe<string> From { get; set; }

    /// <summary>
    ///     An optional sender number, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string To { get; set; }

    public WhatsAppWorkflow(string to) : this() => this.To = to;

    public WhatsAppWorkflow(string to, Maybe<string> from)
    {
        this.To = to;
        this.From = from;
    }
}

/// <summary>
///     Represents a verification workflow for WhatsApp.
/// </summary>
public readonly struct WhatsAppWorkflowNew : IVerificationWorkflow
{
    private WhatsAppWorkflowNew(PhoneNumber to, Maybe<PhoneNumber> from)
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
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonPropertyOrder(3)]
    public Maybe<PhoneNumber> From { get; }

    /// <summary>
    ///     An optional sender number, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    public PhoneNumber To { get; }

    /// <summary>
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static Result<WhatsAppWorkflowNew> Parse(Result<PhoneNumber> to) =>
        to.Map(phoneNumber => new WhatsAppWorkflowNew(phoneNumber, Maybe<PhoneNumber>.None));

    /// <summary>
    /// </summary>
    /// <param name="to"></param>
    /// <param name="from"></param>
    /// <returns></returns>
    public static Result<WhatsAppWorkflowNew> Parse(Result<PhoneNumber> to, Result<PhoneNumber> from) =>
        to.Merge(from, (toNumber, fromNumber) => new WhatsAppWorkflowNew(toNumber, fromNumber));
}