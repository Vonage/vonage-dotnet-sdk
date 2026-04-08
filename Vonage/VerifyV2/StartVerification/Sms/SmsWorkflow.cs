#region
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.StartVerification.Sms;

/// <summary>
///     Represents a verification workflow that delivers the PIN code via SMS text message. Supports optional Android app hash for auto-detection and entity_id/content_id for Indian carrier compliance.
/// </summary>
public readonly struct SmsWorkflow : IVerificationWorkflow
{
    private const int MaxEntityIdLength = 200;
    private const int MaxContentIdLength = 200;

    private SmsWorkflow(PhoneNumber to, Maybe<string> hash, Maybe<string> entityId, Maybe<string> contentId,
        Maybe<PhoneNumber> from)
    {
        this.Hash = hash;
        this.EntityId = entityId;
        this.ContentId = contentId;
        this.From = from;
        this.To = to;
    }

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
    ///     An optional sender number, in the E.164 format. Don't use a leading + or 00 when entering a phone number, start
    ///     with the country code, for example, 447700900000. If no from number is given, the request will default to the
    ///     brand.
    /// </summary>
    [JsonPropertyOrder(5)]
    [JsonPropertyName("from")]
    [JsonConverter(typeof(MaybeJsonConverter<PhoneNumber>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<PhoneNumber> From { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => VerificationChannel.Sms.AsString(EnumFormat.Description);

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

    /// <summary>
    ///     Creates a new SMS verification workflow.
    /// </summary>
    /// <param name="to">The recipient phone number in E.164 format without leading + or 00 (e.g., "447700900000").</param>
    /// <param name="hash">Optional 11-character Android application hash key for SMS auto-detection using the SMS Retriever API.</param>
    /// <param name="entityId">Optional Principal Entity ID (PEID) required for SMS delivery to Indian phone numbers (1-200 characters).</param>
    /// <param name="contentId">Optional content template ID required for SMS delivery to Indian phone numbers (1-200 characters).</param>
    /// <param name="from">Optional sender number in E.164 format (1-15 characters). If not provided, the brand name is used as the sender ID.</param>
    /// <returns>A <see cref="Result{T}"/> containing the workflow if successful, or validation errors if the input is invalid.</returns>
    /// <example>
    /// <code><![CDATA[
    /// // Basic SMS workflow
    /// var workflow = SmsWorkflow.Parse("447700900000");
    ///
    /// // SMS workflow with Android app hash for auto-detection
    /// var workflow = SmsWorkflow.Parse("447700900000", hash: "ABC123def45");
    ///
    /// // SMS workflow for Indian carriers
    /// var workflow = SmsWorkflow.Parse("919876543210", entityId: "1101407360000017170", contentId: "1107158078772563946");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<SmsWorkflow> Parse(string to, string hash = null, string entityId = null,
        string contentId = null, string from = null)
    {
        var fromNumber = MaybeExtensions.FromNonEmptyString(from)
            .Match(some => PhoneNumber
                    .Parse(some)
                    .Match(success => Result<Maybe<PhoneNumber>>.FromSuccess(success),
                        Result<Maybe<PhoneNumber>>.FromFailure),
                () => Result<Maybe<PhoneNumber>>.FromSuccess(Maybe<PhoneNumber>.None));
        return PhoneNumber.Parse(to)
            .Merge(fromNumber, (number, phoneNumber) => new SmsWorkflow(number,
                hash ?? Maybe<string>.None,
                entityId ?? Maybe<string>.None,
                contentId ?? Maybe<string>.None,
                phoneNumber))
            .Bind(VerifyWorkflowHashNotEmpty)
            .Bind(VerifyWorkflowHashLength)
            .Bind(VerifyWorkflowEntityIdNotEmpty)
            .Bind(VerifyWorkflowEntityIdLength)
            .Bind(VerifyWorkflowContentIdNotEmpty)
            .Bind(VerifyWorkflowContentIdLength);
    }

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