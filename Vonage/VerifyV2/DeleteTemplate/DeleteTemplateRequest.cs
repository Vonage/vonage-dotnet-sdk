#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.DeleteTemplate;

/// <inheritdoc />
public readonly struct DeleteTemplateRequest : IVonageRequest
{
    /// <summary>
    ///     ID of the template.
    /// </summary>
    public Guid TemplateId { get; private init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/verify/templates/{this.TemplateId}";

    /// <summary>
    ///     Parses the input into a DeleteRequest.
    /// </summary>
    /// <param name="templateId">The template identifier.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<DeleteTemplateRequest> Parse(Guid templateId) =>
        Result<DeleteTemplateRequest>
            .FromSuccess(new DeleteTemplateRequest {TemplateId = templateId})
            .Map(InputEvaluation<DeleteTemplateRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyRequestId));

    private static Result<DeleteTemplateRequest> VerifyRequestId(DeleteTemplateRequest templateRequest) =>
        InputValidation.VerifyNotEmpty(templateRequest, templateRequest.TemplateId, nameof(TemplateId));
}