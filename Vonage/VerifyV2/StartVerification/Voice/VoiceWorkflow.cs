#region
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.VerifyV2.StartVerification.Voice;

/// <summary>
///     Represents a verification workflow for Voice.
/// </summary>
public readonly struct VoiceWorkflow : IVerificationWorkflow
{
    private VoiceWorkflow(PhoneNumber to) => this.To = to;

    /// <summary>
    ///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => VerificationChannel.Voice.AsString(EnumFormat.Description);

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

    /// <summary>
    ///     Parses the input into a VoiceWorkflow.
    /// </summary>
    /// <param name="to">The phone number to contact.</param>
    /// <returns>Success or failure.</returns>
    public static Result<VoiceWorkflow> Parse(string to) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new VoiceWorkflow(phoneNumber));
}