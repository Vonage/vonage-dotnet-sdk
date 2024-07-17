#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.ExperienceComposer.GetSession;

/// <summary>
///     Represents a request to retrieve a session.
/// </summary>
public readonly struct GetSessionRequest : IVonageRequest, IHasApplicationId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     ID of the Experience Composer instance
    /// </summary>
    public string ExperienceComposerId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/render/{this.ExperienceComposerId}";

    /// <summary>
    ///     Parses the input into a GetSessionRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="experienceComposerId">The experience composer Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetSessionRequest> Parse(Guid applicationId, string experienceComposerId) =>
        Result<GetSessionRequest>
            .FromSuccess(new GetSessionRequest
                {ApplicationId = applicationId, ExperienceComposerId = experienceComposerId})
            .Map(InputEvaluation<GetSessionRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyExperienceComposerId, VerifyApplicationId));

    private static Result<GetSessionRequest> VerifyExperienceComposerId(GetSessionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ExperienceComposerId, nameof(ExperienceComposerId));

    private static Result<GetSessionRequest> VerifyApplicationId(GetSessionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));
}