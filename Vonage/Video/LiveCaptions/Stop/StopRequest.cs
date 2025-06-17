#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.LiveCaptions.Stop;

/// <inheritdoc />
public readonly struct StopRequest : IVonageRequest
{
    /// <summary>
    ///     Vonage Application UUID
    /// </summary>
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     ID of the connection used for captions
    /// </summary>
    public string CaptionsId { get; internal init; }

    /// <summary>
    ///     Parses the input into a StopRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="captionsId">ID of the connection used for captions.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<StopRequest> Parse(Guid applicationId, string captionsId) =>
        Result<StopRequest>
            .FromSuccess(new StopRequest
                {ApplicationId = applicationId, CaptionsId = captionsId})
            .Map(InputEvaluation<StopRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyExperienceComposerId, VerifyApplicationId));

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, $"/v2/project/{this.ApplicationId}/captions/{this.CaptionsId}/stop")
        .Build();

    private static Result<StopRequest> VerifyApplicationId(StopRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<StopRequest> VerifyExperienceComposerId(StopRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.CaptionsId, nameof(CaptionsId));
}