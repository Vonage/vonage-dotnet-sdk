using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Common.Validation;

namespace Vonage.VerifyV2.StartVerification.SilentAuth;

/// <summary>
///     Represents a verification workflow for SilentAuth.
/// </summary>
public readonly struct SilentAuthWorkflow : IVerificationWorkflow
{
    private SilentAuthWorkflow(PhoneNumber to, string redirectUrl = null)
    {
        this.To = to;
        this.RedirectUrl = string.IsNullOrWhiteSpace(redirectUrl) ? Maybe<string>.None : redirectUrl;
    }

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
    /// Final redirect added at the end of the check_url request/response lifecycle. See the documentation for integrations. Will contain the request_id and code as a url fragment after the URL.
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> RedirectUrl { get; }

    /// <summary>
    ///     Parses the input into a SilentAuthWorkflow.
    /// </summary>
    /// <param name="to">The phone number to use for authentication.</param>
    /// <returns>Success or failure.</returns>
    public static Result<SilentAuthWorkflow> Parse(string to) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new SilentAuthWorkflow(phoneNumber));

    /// <summary>
    ///  Parses the input into a SilentAuthWorkflow.
    /// </summary>
    /// <param name="to">The phone number to use for authentication.</param>
    /// <param name="redirectUrl">The final redirect added at the end of the check_url request/response lifecycle</param>
    /// <returns>Success or failure.</returns>
    public static Result<SilentAuthWorkflow> Parse(string to, string redirectUrl) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new SilentAuthWorkflow(phoneNumber, redirectUrl));
    
    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}