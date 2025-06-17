#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Serialization;
#endregion

namespace Vonage.VerifyV2.UpdateTemplateFragment;

/// <inheritdoc />
public readonly struct UpdateTemplateFragmentRequest : IVonageRequest
{
    /// <summary>
    ///     The template text. There are 4 reserved variables available to use: ${code}, ${brand}, ${time-limit} and
    ///     ${time-limit-unit}
    /// </summary>
    public string Text { get; internal init; }

    /// <summary>
    ///     ID of the template.
    /// </summary>
    [JsonIgnore]
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     ID of the template fragment.
    /// </summary>
    [JsonIgnore]
    public Guid TemplateFragmentId { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns></returns>
    public static IBuilderForId Build() => new UpdateTemplateFragmentRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(new HttpMethod("PATCH"),
            $"/v2/verify/templates/{this.TemplateId}/template_fragments/{this.TemplateFragmentId}")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");
}