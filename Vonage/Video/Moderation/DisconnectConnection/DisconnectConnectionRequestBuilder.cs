using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Video.Moderation.DisconnectConnection;

internal class DisconnectConnectionRequestBuilder :
    IBuilderForApplicationId,
    IBuilderForConnectionId,
    IBuilderForSessionId,
    IVonageRequestBuilder<DisconnectConnectionRequest>
{
    private Guid applicationId;
    private string connectionId;
    private string sessionId;

    /// <inheritdoc />
    public Result<DisconnectConnectionRequest> Create() =>
        Result<DisconnectConnectionRequest>.FromSuccess(new DisconnectConnectionRequest
            {
                ApplicationId = this.applicationId,
                ConnectionId = this.connectionId,
                SessionId = this.sessionId,
            })
            .Map(InputEvaluation<DisconnectConnectionRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyApplicationId, VerifySessionId, VerifyConnectionId));

    /// <inheritdoc />
    public IBuilderForSessionId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<DisconnectConnectionRequest> WithConnectionId(string value)
    {
        this.connectionId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForConnectionId WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<DisconnectConnectionRequest> VerifyApplicationId(DisconnectConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<DisconnectConnectionRequest> VerifyConnectionId(DisconnectConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConnectionId, nameof(request.ConnectionId));

    private static Result<DisconnectConnectionRequest> VerifySessionId(DisconnectConnectionRequest request) =>
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
    IVonageRequestBuilder<DisconnectConnectionRequest> WithConnectionId(string value);
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