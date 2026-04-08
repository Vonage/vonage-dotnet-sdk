#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.Cancel;

/// <summary>
///     Represents a request to cancel an in-progress verification. Cancellation is only possible 30 seconds after the request started and before the second delivery event has occurred.
/// </summary>
public readonly struct CancelRequest : IVonageRequest
{
    private CancelRequest(Guid requestId) => this.RequestId = requestId;

    /// <summary>
    ///     The unique identifier (UUID) of the verification request to cancel.
    /// </summary>
    public Guid RequestId { get; internal init; }

    /// <summary>
    ///     Creates a new cancellation request.
    /// </summary>
    /// <param name="requestId">The UUID of the verification request to cancel (obtained from <see cref="StartVerificationResponse.RequestId"/>).</param>
    /// <returns>A <see cref="Result{T}"/> containing the request if successful, or validation errors if the ID is empty.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CancelRequest.Parse(Guid.Parse("c11236f4-00bf-4b89-84ba-88b25df97315"));
    /// var response = await client.CancelAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<CancelRequest> Parse(Guid requestId) =>
        Result<CancelRequest>
            .FromSuccess(new CancelRequest(requestId))
            .Map(InputEvaluation<CancelRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyRequestId));

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, $"/v2/verify/{this.RequestId}")
        .Build();

    private static Result<CancelRequest> VerifyRequestId(CancelRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.RequestId, nameof(RequestId));
}