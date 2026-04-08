#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.DeleteTemplateFragment;

/// <summary>
///     Represents a request to delete a template fragment from a template.
/// </summary>
[Builder]
public readonly partial struct DeleteTemplateFragmentRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the unique identifier (UUID) of the parent template containing the fragment.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithTemplateId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     Sets the unique identifier (UUID) of the template fragment to delete.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithTemplateFragmentId(Guid.Parse("c70f446e-997a-4313-a081-60a02a31dc19"))
    /// ]]></code>
    /// </example>
    [Mandatory(1)]
    public Guid TemplateFragmentId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/verify/templates/{this.TemplateId}/template_fragments/{this.TemplateFragmentId}";

    [ValidationRule]
    internal static Result<DeleteTemplateFragmentRequest> VerifyTemplateId(
        DeleteTemplateFragmentRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.TemplateId, nameof(request.TemplateId));

    [ValidationRule]
    internal static Result<DeleteTemplateFragmentRequest> VerifyTemplateFragmentId(
        DeleteTemplateFragmentRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.TemplateFragmentId, nameof(request.TemplateFragmentId));
}