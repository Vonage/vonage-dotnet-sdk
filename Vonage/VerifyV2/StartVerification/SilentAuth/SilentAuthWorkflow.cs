using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.VerifyV2.StartVerification.SilentAuth;

/// <summary>
///     Represents a verification workflow for SilentAuth.
/// </summary>
public readonly struct SilentAuthWorkflow : IVerificationWorkflow
{
    private SilentAuthWorkflow(PhoneNumber to) => this.To = to;

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "silent_auth";

    /// <summary>
    ///     The phone number to use for authentication, in the E.164 format. Don't use a leading + or 00 when entering a phone
    ///     number, start with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <summary>
    ///     Parses the input into a SilentAuthWorkflow.
    /// </summary>
    /// <param name="to">The phone number to use for authentication.</param>
    /// <returns>Success or failure.</returns>
    public static Result<SilentAuthWorkflow> Parse(string to) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new SilentAuthWorkflow(phoneNumber));

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}