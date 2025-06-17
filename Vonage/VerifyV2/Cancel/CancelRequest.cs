#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.VerifyV2.Cancel;

/// <inheritdoc />
public readonly struct CancelRequest : IVonageRequest
{
    private CancelRequest(Guid requestId) => this.RequestId = requestId;

    /// <summary>
    ///     ID of the verify request.
    /// </summary>
    public Guid RequestId { get; internal init; }

    /// <summary>
    ///     Parses the input into a CancelRequest.
    /// </summary>
    /// <param name="requestId">The verify request identifier.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
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