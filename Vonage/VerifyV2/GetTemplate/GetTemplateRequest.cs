#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.GetTemplate;

/// <inheritdoc />
public readonly struct GetTemplateRequest : IVonageRequest
{
    /// <summary>
    ///     ID of the template.
    /// </summary>
    public Guid TemplateId { get; private init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/verify/templates/{this.TemplateId}";

    /// <summary>
    ///     Parses the input into a GetTemplateRequest.
    /// </summary>
    /// <param name="templateId">The template identifier.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetTemplateRequest> Parse(Guid templateId) =>
        Result<GetTemplateRequest>
            .FromSuccess(new GetTemplateRequest {TemplateId = templateId})
            .Map(InputEvaluation<GetTemplateRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyRequestId));

    private static Result<GetTemplateRequest> VerifyRequestId(GetTemplateRequest templateRequest) =>
        InputValidation.VerifyNotEmpty(templateRequest, templateRequest.TemplateId, nameof(TemplateId));
}