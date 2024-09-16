#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Serialization;
using Vonage.Serialization;
using Vonage.VerifyV2.StartVerification;
#endregion

namespace Vonage.VerifyV2.CreateTemplateFragment;

/// <inheritdoc />
public readonly struct CreateTemplateFragmentRequest : IVonageRequest
{
    /// <summary>
    ///     The template text. There are 4 reserved variables available to use: ${code}, ${brand}, ${time-limit} and
    ///     ${time-limit-unit}
    /// </summary>
    [JsonPropertyOrder(2)]
    public string Text { get; internal init; }

    /// <summary>
    ///     ID of the template.
    /// </summary>
    [JsonIgnore]
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     The locale code.
    /// </summary>
    [JsonPropertyOrder(1)]
    public Locale Locale { get; internal init; }

    /// <summary>
    ///     The channel name.
    /// </summary>
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<VerificationChannel>))]
    public VerificationChannel Channel { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/verify/templates/{this.TemplateId}/template_fragments";

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns></returns>
    public static IBuilderForTemplateId Build() => new CreateTemplateFragmentRequestBuilder();
}