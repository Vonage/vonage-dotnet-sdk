using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Video.Sip.PlayToneIntoConnection;

internal class PlayToneIntoConnectionRequestBuilder :
    IBuilderForApplicationId,
    IBuilderForConnectionId,
    IBuilderForSessionId,
    IBuilderForDigits,
    IVonageRequestBuilder<PlayToneIntoConnectionRequest>
{
    private Guid applicationId;
    private string connectionId;
    private string sessionId;
    private string digits;

    /// <inheritdoc />
    public Result<PlayToneIntoConnectionRequest> Create() =>
        Result<PlayToneIntoConnectionRequest>.FromSuccess(new PlayToneIntoConnectionRequest
            {
                ApplicationId = this.applicationId,
                ConnectionId = this.connectionId,
                SessionId = this.sessionId,
                Digits = this.digits,
            })
            .Map(InputEvaluation<PlayToneIntoConnectionRequest>.Evaluate)
            .Bind(evaluation =>
                evaluation.WithRules(VerifyApplicationId, VerifySessionId, VerifyDigits, VerifyConnectionId));

    /// <inheritdoc />
    public IBuilderForSessionId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForDigits WithConnectionId(string value)
    {
        this.connectionId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<PlayToneIntoConnectionRequest> WithDigits(string value)
    {
        this.digits = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForConnectionId WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<PlayToneIntoConnectionRequest> VerifyApplicationId(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<PlayToneIntoConnectionRequest> VerifyConnectionId(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConnectionId, nameof(request.ConnectionId));

    private static Result<PlayToneIntoConnectionRequest> VerifyDigits(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Digits, nameof(request.Digits));

    private static Result<PlayToneIntoConnectionRequest> VerifySessionId(PlayToneIntoConnectionRequest request) =>
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
///     Represents a builder that allows to set the ConnectionId.
/// </summary>
public interface IBuilderForConnectionId
{
    /// <summary>
    ///     Sets the ConnectionId on the builder.
    /// </summary>
    /// <param name="value">The connection id.</param>
    /// <returns>The builder.</returns>
    IBuilderForDigits WithConnectionId(string value);
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
    IBuilderForConnectionId WithSessionId(string value);
}

/// <summary>
///     Represents a builder that allows to set the Digits.
/// </summary>
public interface IBuilderForDigits
{
    /// <summary>
    ///     Sets the Digits on the builder.
    /// </summary>
    /// <param name="value">The digits.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<PlayToneIntoConnectionRequest> WithDigits(string value);
}