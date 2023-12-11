using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Video.Sessions.GetStreams;

internal class GetStreamsRequestBuilder :
    IBuilderForApplicationId,
    IBuilderForSessionId,
    IVonageRequestBuilder<GetStreamsRequest>
{
    private Guid applicationId;
    private string sessionId;

    /// <inheritdoc />
    public Result<GetStreamsRequest> Create() =>
        Result<GetStreamsRequest>.FromSuccess(new GetStreamsRequest
            {
                ApplicationId = this.applicationId,
                SessionId = this.sessionId,
            })
            .Map(InputEvaluation<GetStreamsRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyApplicationId, VerifySessionId));

    /// <inheritdoc />
    public IBuilderForSessionId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<GetStreamsRequest> WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<GetStreamsRequest> VerifyApplicationId(GetStreamsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<GetStreamsRequest> VerifySessionId(GetStreamsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}

/// <summary>
///     Represents a builder that allows to set the ApplicationId.
/// </summary>
public interface IBuilderForApplicationId
{
    /// <summary>
    ///     Sets the ApplicationId on the builder.
    /// </summary>
    /// <param name="value">The application id.</param>
    /// <returns>The builder.</returns>
    IBuilderForSessionId WithApplicationId(Guid value);
}

/// <summary>
///     Represents a builder that allows to set the SessionId.
/// </summary>
public interface IBuilderForSessionId
{
    /// <summary>
    ///     Sets the SessionId on the builder.
    /// </summary>
    /// <param name="value">The session id.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<GetStreamsRequest> WithSessionId(string value);
}