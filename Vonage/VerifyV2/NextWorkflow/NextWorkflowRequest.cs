#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.NextWorkflow;

/// <summary>
///     Represents a request to advance a verification to its next workflow channel. Use this to skip the current delivery method and immediately try the next fallback channel.
/// </summary>
public readonly struct NextWorkflowRequest : IVonageRequest
{
    private NextWorkflowRequest(Guid requestId) => this.RequestId = requestId;

    /// <summary>
    ///     The unique identifier (UUID) of the verification request to advance.
    /// </summary>
    public Guid RequestId { get; internal init; }

    /// <summary>
    ///     Creates a new request to advance to the next workflow.
    /// </summary>
    /// <param name="requestId">The UUID of the verification request to advance (obtained from <see cref="StartVerification.StartVerificationResponse.RequestId"/>).</param>
    /// <returns>A <see cref="Result{T}"/> containing the request if successful, or validation errors if the ID is empty.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = NextWorkflowRequest.Parse(Guid.Parse("c11236f4-00bf-4b89-84ba-88b25df97315"));
    /// var response = await client.NextWorkflowAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
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