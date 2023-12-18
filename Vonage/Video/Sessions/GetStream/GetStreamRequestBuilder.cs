using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Video.Sessions.GetStream;

internal class GetStreamRequestBuilder :
    IVonageRequestBuilder<GetStreamRequest>,
    IBuilderForApplicationId,
    IBuilderForSessionId,
    IBuilderForStreamId
{
    private Guid applicationId;
    private string streamId;
    private string sessionId;

    /// <inheritdoc />
    public Result<GetStreamRequest> Create() =>
        Result<GetStreamRequest>.FromSuccess(new GetStreamRequest
            {
                ApplicationId = this.applicationId,
                SessionId = this.sessionId,
                StreamId = this.streamId,
            })
            .Map(InputEvaluation<GetStreamRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyApplicationId, VerifySessionId, VerifyStreamId));

    /// <inheritdoc />
    public IBuilderForSessionId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForStreamId WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<GetStreamRequest> WithStreamId(string value)
    {
        this.streamId = value;
        return this;
    }

    private static Result<GetStreamRequest> VerifyApplicationId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<GetStreamRequest> VerifySessionId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    private static Result<GetStreamRequest> VerifyStreamId(GetStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(GetStreamRequest.StreamId));
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
    IBuilderForStreamId WithSessionId(string value);
}

/// <summary>
///     Represents a builder that allows to set the StreamId.
/// </summary>
public interface IBuilderForStreamId
{
    /// <summary>
    ///     Sets the StreamId on the builder.
    /// </summary>
    /// <param name="value">The stream id.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<GetStreamRequest> WithStreamId(string value);
}