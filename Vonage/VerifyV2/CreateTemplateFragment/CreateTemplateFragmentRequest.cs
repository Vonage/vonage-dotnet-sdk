#region
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Common.Validation;
using Vonage.Serialization;
using Vonage.VerifyV2.StartVerification;
#endregion

namespace Vonage.VerifyV2.CreateTemplateFragment;

/// <summary>
///     Represents a request to create a template fragment containing the message text for a specific channel and locale combination.
/// </summary>
[Builder]
public readonly partial struct CreateTemplateFragmentRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the message text content with optional placeholders: ${code}, ${brand}, ${time-limit}, and ${time-limit-unit}.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithText("Your ${brand} verification code is ${code}. Valid for ${time-limit} ${time-limit-unit}.")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(2)]
    [Mandatory(1)]
    public string Text { get; internal init; }

    /// <summary>
    ///     Sets the unique identifier (UUID) of the parent template this fragment belongs to.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithTemplateId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    /// ]]></code>
    /// </example>
    [JsonIgnore]
    [Mandatory(0)]
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     Sets the IETF BCP 47 locale code for this fragment (e.g., "en-us", "de-de").
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithLocale(Locale.EnUs)
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(1)]
    [Mandatory(2)]
    public Locale Locale { get; internal init; }

    /// <summary>
    ///     Sets the verification channel this fragment applies to. Only SMS and Voice channels are supported for templates.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannel(VerificationChannel.Sms)
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<VerificationChannel>))]
    [Mandatory(3)]
    public VerificationChannel Channel { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/v2/verify/templates/{this.TemplateId}/template_fragments")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    private static bool IsChannelSupported(VerificationChannel channel) =>
        new[] {VerificationChannel.Sms, VerificationChannel.Voice}.Contains(channel);

    [ValidationRule]
    internal static Result<CreateTemplateFragmentRequest> VerifyText(
        CreateTemplateFragmentRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Text, nameof(request.Text));

    [ValidationRule]
    internal static Result<CreateTemplateFragmentRequest> VerifyTemplate(
        CreateTemplateFragmentRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.TemplateId, nameof(request.TemplateId));

    [ValidationRule]
    internal static Result<CreateTemplateFragmentRequest> VerifyChannel(
        CreateTemplateFragmentRequest request) =>
        IsChannelSupported(request.Channel)
            ? request
            : Result<CreateTemplateFragmentRequest>.FromFailure(
                ResultFailure.FromErrorMessage(
                    $"{nameof(request.Channel)} must be one of {VerificationChannel.Sms}, {VerificationChannel.Voice} or {VerificationChannel.Email}."));
}