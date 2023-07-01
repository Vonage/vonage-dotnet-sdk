using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Moderation.MuteStream;

internal class MuteStreamRequestBuilder :
    IBuilderForApplicationId,
    IBuilderForStreamId,
    IBuilderForSessionId,
    IVonageRequestBuilder<MuteStreamRequest>
{
    private Guid applicationId;
    private string streamId;
    private string sessionId;

    /// <inheritdoc />
    public Result<MuteStreamRequest> Create() =>
        Result<MuteStreamRequest>.FromSuccess(new MuteStreamRequest
            {
                ApplicationId = this.applicationId,
                StreamId = this.streamId,
                SessionId = this.sessionId,
            })
            .Map(InputEvaluation<MuteStreamRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyStreamId, VerifyApplicationId, VerifySessionId));

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
    public IVonageRequestBuilder<MuteStreamRequest> WithStreamId(string value)
    {
        this.streamId = value;
        return this;
    }

    private static Result<MuteStreamRequest> VerifyApplicationId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<MuteStreamRequest> VerifySessionId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    private static Result<MuteStreamRequest> VerifyStreamId(MuteStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(MuteStreamRequest.StreamId));
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
///     Represents a builder that allows to set the StreamId.
/// </summary>
public interface IBuilderForStreamId
{
    /// <summary>
    ///     Sets the StreamId on the builder.
    /// </summary>
    /// <param name="value">The stream id.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<MuteStreamRequest> WithStreamId(string value);
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