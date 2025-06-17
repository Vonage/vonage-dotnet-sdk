#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.DeleteTemplateFragment;

/// <inheritdoc />
[Builder]
public readonly partial struct DeleteTemplateFragmentRequest : IVonageRequest
{
    /// <summary>
    ///     ID of the template.
    /// </summary>
    [Mandatory(0)]
    public Guid TemplateId { get; internal init; }

    /// <summary>
    ///     ID of the template fragment.
    /// </summary>
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