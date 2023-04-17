using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Common.Validation;

namespace Vonage.VerifyV2.StartVerification.Sms;

/// <summary>
///     Represents a verification workflow for SMS.
/// </summary>
public readonly struct SmsWorkflow : IVerificationWorkflow
{
    private SmsWorkflow(PhoneNumber to, Maybe<string> hash)
    {
        this.Hash = hash;
        this.To = to;
    }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "sms";

    /// <summary>
    ///     Optional Android Application Hash Key for automatic code detection on a user's device.
    /// </summary>
    [JsonPropertyOrder(3)]
    [JsonPropertyName("app_hash")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Hash { get; }

    /// <summary>
    ///     The phone number to contact, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <summary>
    ///     Parses the input into a SmsWorkflow.
    /// </summary>
    /// <param name="to">The phone number to contact.</param>
    /// <param name="hash">The Android application hash key.</param>
    /// <returns>Success or failure.</returns>
    public static Result<SmsWorkflow> Parse(string to, string hash) =>
        PhoneNumber.Parse(to)
            .Map(phoneNumber => new SmsWorkflow(phoneNumber, hash))
            .Bind(VerifyWorkflowHashNotEmpty)
            .Bind(VerifyWorkflowHashLength);

    /// <summary>
    ///     Parses the input into a SmsWorkflow.
    /// </summary>
    /// <param name="to">The phone number to contact.</param>
    /// <returns>Success or failure.</returns>
    public static Result<SmsWorkflow> Parse(string to) =>
        PhoneNumber.Parse(to)
            .Map(phoneNumber => new SmsWorkflow(phoneNumber, Maybe<string>.None))
            .Bind(VerifyWorkflowHashNotEmpty)
            .Bind(VerifyWorkflowHashLength);

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

    private static Result<SmsWorkflow> VerifyWorkflowHashLength(
        SmsWorkflow request) =>
        request.Hash.Match(some => InputValidation.VerifyLength(request, some, 11, nameof(request.Hash)),
            () => request);

    private static Result<SmsWorkflow> VerifyWorkflowHashNotEmpty(
        SmsWorkflow request) =>
        request.Hash.Match(some => InputValidation.VerifyNotEmpty(request, some, nameof(request.Hash)), () => request);
}