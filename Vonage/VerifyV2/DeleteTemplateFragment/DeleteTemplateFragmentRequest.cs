#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
#endregion

namespace Vonage.VerifyV2.DeleteTemplateFragment;

/// <inheritdoc />
public readonly struct DeleteTemplateFragmentRequest : IVonageRequest
{
    /// <summary>
    ///     ID of the template.
    /// </summary>
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     ID of the template fragment.
    /// </summary>
    public Guid TemplateFragmentId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/verify/templates/{this.TemplateId}/template_fragments/{this.TemplateFragmentId}";

    /// <summary>
    ///     Initializes a builder for DeleteTemplateFragmentRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForTemplateId Build() => new DeleteTemplateFragmentRequestBuilder();
}