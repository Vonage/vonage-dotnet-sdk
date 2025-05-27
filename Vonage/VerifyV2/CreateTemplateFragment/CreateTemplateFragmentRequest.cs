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

/// <inheritdoc />
[Builder]
public readonly partial struct CreateTemplateFragmentRequest : IVonageRequest
{
    /// <summary>
    ///     The template text. There are 4 reserved variables available to use: ${code}, ${brand}, ${time-limit} and
    ///     ${time-limit-unit}
    /// </summary>
    [JsonPropertyOrder(2)]
    [Mandatory(1, nameof(VerifyText))]
    public string Text { get; internal init; }

    /// <summary>
    ///     ID of the template.
    /// </summary>
    [JsonIgnore]
    [Mandatory(0, nameof(VerifyTemplate))]
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     The locale code.
    /// </summary>
    [JsonPropertyOrder(1)]
    [Mandatory(2)]
    public Locale Locale { get; internal init; }

    /// <summary>
    ///     The channel name.
    /// </summary>
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<VerificationChannel>))]
    [Mandatory(3, nameof(VerifyChannel))]
    public VerificationChannel Channel { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/v2/verify/templates/{this.TemplateId}/template_fragments")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    internal static Result<CreateTemplateFragmentRequest> VerifyText(
        CreateTemplateFragmentRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Text, nameof(request.Text));

    internal static Result<CreateTemplateFragmentRequest> VerifyTemplate(
        CreateTemplateFragmentRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.TemplateId, nameof(request.TemplateId));

    internal static Result<CreateTemplateFragmentRequest> VerifyChannel(
        CreateTemplateFragmentRequest request) =>
        IsChannelSupported(request.Channel)
            ? request
            : Result<CreateTemplateFragmentRequest>.FromFailure(
                ResultFailure.FromErrorMessage(
                    $"{nameof(request.Channel)} must be one of {VerificationChannel.Sms}, {VerificationChannel.Voice} or {VerificationChannel.Email}."));

    internal static bool IsChannelSupported(VerificationChannel channel) =>
        new[] {VerificationChannel.Sms, VerificationChannel.Voice}.Contains(channel);
}