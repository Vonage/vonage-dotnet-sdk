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
    private const int MaxEntityIdLength = 200;
    private const int MaxContentIdLength = 200;

    private SmsWorkflow(PhoneNumber to, Maybe<string> hash, Maybe<string> entityId, Maybe<string> contentId)
    {
        this.Hash = hash;
        this.EntityId = entityId;
        this.ContentId = contentId;
        this.To = to;
    }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "sms";

    /// <summary>
    ///     Optional Android Application Hash Key for automatic code detection on a user's device.
    /// </summary>
    [JsonPropertyOrder(2)]
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
    ///     Optional PEID required for SMS delivery using Indian Carriers
    /// </summary>
    [JsonPropertyOrder(3)]
    [JsonPropertyName("entity_id")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> EntityId { get; }

    /// <summary>
    ///     Optional value corresponding to a TemplateID for SMS delivery using Indian Carriers
    /// </summary>
    [JsonPropertyOrder(4)]
    [JsonPropertyName("content_id")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> ContentId { get; }

    /// <summary>
    ///     Parses the input into a SmsWorkflow.
    /// </summary>
    /// <param name="to">The phone number to contact.</param>
    /// <param name="hash">The Android application hash key.</param>
    /// <param name="entityId">Optional PEID required for SMS delivery using Indian Carriers</param>
    /// <param name="contentId">Optional value corresponding to a TemplateID for SMS delivery using Indian Carriers</param>
    /// <returns>Success or failure.</returns>
    public static Result<SmsWorkflow> Parse(string to, string hash = null, string entityId = null,
        string contentId = null) =>
        PhoneNumber.Parse(to)
            .Map(phoneNumber => new SmsWorkflow(phoneNumber,
                hash ?? Maybe<string>.None,
                entityId ?? Maybe<string>.None,
                contentId ?? Maybe<string>.None))
            .Bind(VerifyWorkflowHashNotEmpty)
            .Bind(VerifyWorkflowHashLength)
            .Bind(VerifyWorkflowEntityIdNotEmpty)
            .Bind(VerifyWorkflowEntityIdLength)
            .Bind(VerifyWorkflowContentIdNotEmpty)
            .Bind(VerifyWorkflowContentIdLength);

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

    private static Result<SmsWorkflow> VerifyWorkflowHashLength(
        SmsWorkflow request) =>
        request.Hash.Match(some => InputValidation.VerifyLength(request, some, 11, nameof(request.Hash)),
            () => request);

    private static Result<SmsWorkflow> VerifyWorkflowHashNotEmpty(
        SmsWorkflow request) =>
        request.Hash.Match(some => InputValidation.VerifyNotEmpty(request, some, nameof(request.Hash)), () => request);

    private static Result<SmsWorkflow> VerifyWorkflowEntityIdNotEmpty(
        SmsWorkflow request) =>
        request.EntityId.Match(some => InputValidation.VerifyNotEmpty(request, some, nameof(request.EntityId)),
            () => request);

    private static Result<SmsWorkflow> VerifyWorkflowEntityIdLength(
        SmsWorkflow request) =>
        request.EntityId.Match(
            some => InputValidation.VerifyLengthLowerOrEqualThan(request, some, MaxEntityIdLength,
                nameof(request.EntityId)),
            () => request);

    private static Result<SmsWorkflow> VerifyWorkflowContentIdLength(
        SmsWorkflow request) =>
        request.ContentId.Match(
            some => InputValidation.VerifyLengthLowerOrEqualThan(request, some, MaxContentIdLength,
                nameof(request.ContentId)),
            () => request);

    private static Result<SmsWorkflow> VerifyWorkflowContentIdNotEmpty(
        SmsWorkflow request) =>
        request.ContentId.Match(some => InputValidation.VerifyNotEmpty(request, some, nameof(request.ContentId)),
            () => request);
}