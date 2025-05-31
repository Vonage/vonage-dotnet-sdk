#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.NextWorkflow;

/// <inheritdoc />
public readonly struct NextWorkflowRequest : IVonageRequest
{
    private NextWorkflowRequest(Guid requestId) => this.RequestId = requestId;

    /// <summary>
    ///     ID of the verify request.
    /// </summary>
    public Guid RequestId { get; internal init; }

    /// <summary>
    ///     Parses the input into a NextWorkflowRequest.
    /// </summary>
    /// <param name="requestId">The verify request identifier.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<NextWorkflowRequest> Parse(Guid requestId) =>
        Result<NextWorkflowRequest>
            .FromSuccess(new NextWorkflowRequest(requestId))
            .Map(InputEvaluation<NextWorkflowRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyRequestId));

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/v2/verify/{this.RequestId}/next_workflow")
        .Build();

    private static Result<NextWorkflowRequest> VerifyRequestId(NextWorkflowRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.RequestId, nameof(RequestId));
}