#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.GetTemplateFragment;

/// <summary>
///     Represents a request to retrieve a single template fragment by its template ID and fragment ID.
/// </summary>
public readonly struct GetTemplateFragmentRequest : IVonageRequest
{
    /// <summary>
    ///     The unique identifier (UUID) of the parent template.
    /// </summary>
    public Guid TemplateId { get; private init; }

    /// <summary>
    ///     The unique identifier (UUID) of the template fragment to retrieve.
    /// </summary>
    public Guid TemplateFragmentId { get; private init; }

    /// <summary>
    ///     Creates a new request to retrieve a template fragment.
    /// </summary>
    /// <param name="templateId">The UUID of the parent template.</param>
    /// <param name="templateFragmentId">The UUID of the template fragment to retrieve.</param>
    /// <returns>A <see cref="Result{T}"/> containing the request if successful, or validation errors if any ID is empty.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetTemplateFragmentRequest.Parse(
    ///     Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"),
    ///     Guid.Parse("c70f446e-997a-4313-a081-60a02a31dc19"));
    /// var response = await client.GetTemplateFragmentAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<GetTemplateFragmentRequest> Parse(Guid templateId, Guid templateFragmentId) =>
        Result<GetTemplateFragmentRequest>
            .FromSuccess(new GetTemplateFragmentRequest
                {TemplateId = templateId, TemplateFragmentId = templateFragmentId})
            .Map(InputEvaluation<GetTemplateFragmentRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyTemplateId, VerifyTemplateFragmentId));

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get,
            $"/v2/verify/templates/{this.TemplateId}/template_fragments/{this.TemplateFragmentId}")
        .Build();

    private static Result<GetTemplateFragmentRequest> VerifyTemplateFragmentId(
        GetTemplateFragmentRequest templateRequest) =>
        InputValidation.VerifyNotEmpty(templateRequest, templateRequest.TemplateFragmentId, nameof(TemplateFragmentId));

    private static Result<GetTemplateFragmentRequest> VerifyTemplateId(GetTemplateFragmentRequest templateRequest) =>
        InputValidation.VerifyNotEmpty(templateRequest, templateRequest.TemplateId, nameof(TemplateId));
}