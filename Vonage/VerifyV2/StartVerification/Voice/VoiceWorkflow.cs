using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.VerifyV2.StartVerification.Voice;

/// <summary>
///     Represents a verification workflow for Voice.
/// </summary>
public readonly struct VoiceWorkflow : IVerificationWorkflow
{
    private VoiceWorkflow(PhoneNumber to) => this.To = to;

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "voice";

    /// <summary>
    ///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <summary>
    ///     Parses the input into a VoiceWorkflow.
    /// </summary>
    /// <param name="to">The phone number to contact.</param>
    /// <returns>Success or failure.</returns>
    public static Result<VoiceWorkflow> Parse(string to) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new VoiceWorkflow(phoneNumber));

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}