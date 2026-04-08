#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Serialization;
#endregion

namespace Vonage.VerifyV2.UpdateTemplateFragment;

/// <summary>
///     Represents a request to update the text content of a template fragment.
/// </summary>
public readonly struct UpdateTemplateFragmentRequest : IVonageRequest
{
    /// <summary>
    ///     The new message text content with optional placeholders: ${code}, ${brand}, ${time-limit}, and ${time-limit-unit}.
    /// </summary>
    public string Text { get; internal init; }

    /// <summary>
    ///     The unique identifier (UUID) of the parent template.
    /// </summary>
    [JsonIgnore]
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     The unique identifier (UUID) of the template fragment to update.
    /// </summary>
    [JsonIgnore]
    public Guid TemplateFragmentId { get; internal init; }

    /// <summary>
    ///     Creates a new builder to construct an <see cref="UpdateTemplateFragmentRequest"/>.
    /// </summary>
    /// <returns>A builder interface to set the template ID.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = UpdateTemplateFragmentRequest.Build()
    ///     .WithId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    ///     .WithFragmentId(Guid.Parse("c70f446e-997a-4313-a081-60a02a31dc19"))
    ///     .WithText("Your ${brand} verification code is ${code}. Valid for ${time-limit} ${time-limit-unit}.")
    ///     .Create();
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
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