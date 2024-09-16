#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.GetTemplateFragment;

/// <inheritdoc />
public readonly struct GetTemplateFragmentRequest : IVonageRequest
{
    /// <summary>
    ///     ID of the template.
    /// </summary>
    public Guid TemplateId { get; private init; }

    /// <summary>
    ///     ID of the template fragment.
    /// </summary>
    public Guid TemplateFragmentId { get; private init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/verify/templates/{this.TemplateId}/template_fragments/{this.TemplateFragmentId}";

    /// <summary>
    ///     Parses the input into a GetTemplateFragmentRequest.
    /// </summary>
    /// <param name="templateId">The template identifier.</param>
    /// <param name="templateFragmentId">The template fragment identifier.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetTemplateFragmentRequest> Parse(Guid templateId, Guid templateFragmentId) =>
        Result<GetTemplateFragmentRequest>
            .FromSuccess(new GetTemplateFragmentRequest
                {TemplateId = templateId, TemplateFragmentId = templateFragmentId})
            .Map(InputEvaluation<GetTemplateFragmentRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyTemplateId, VerifyTemplateFragmentId));

    private static Result<GetTemplateFragmentRequest> VerifyTemplateId(GetTemplateFragmentRequest templateRequest) =>
        InputValidation.VerifyNotEmpty(templateRequest, templateRequest.TemplateId, nameof(TemplateId));

    private static Result<GetTemplateFragmentRequest> VerifyTemplateFragmentId(
        GetTemplateFragmentRequest templateRequest) =>
        InputValidation.VerifyNotEmpty(templateRequest, templateRequest.TemplateFragmentId, nameof(TemplateFragmentId));
}