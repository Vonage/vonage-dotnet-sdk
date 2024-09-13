#region
using System;
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.VerifyV2.StartVerification.SilentAuth;

/// <summary>
///     Represents a verification workflow for SilentAuth.
/// </summary>
public readonly struct SilentAuthWorkflow : IVerificationWorkflow
{
    private SilentAuthWorkflow(PhoneNumber to, Uri redirectUrl = null)
    {
        this.To = to;
        this.RedirectUrl = redirectUrl ?? Maybe<Uri>.None;
    }

    /// <summary>
    /// Final redirect added at the end of the check_url request/response lifecycle. See the documentation for integrations. Will contain the request_id and code as a url fragment after the URL.
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Uri> RedirectUrl { get; }

    /// <summary>
    ///     The phone number to use for authentication, in the E.164 format. Don't use a leading + or 00 when entering a phone
    ///     number, start with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => VerificationChannel.SilentAuth.AsString(EnumFormat.Description);

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

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
    public static Result<SilentAuthWorkflow> Parse(string to, Uri redirectUrl) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new SilentAuthWorkflow(phoneNumber, redirectUrl));
}