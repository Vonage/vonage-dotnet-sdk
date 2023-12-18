using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Video.Signaling.SendSignal;

internal class SendSignalRequestBuilder :
    IBuilderForApplicationId,
    IBuilderForConnectionId,
    IBuilderForSessionId,
    IBuilderForContent,
    IVonageRequestBuilder<SendSignalRequest>
{
    private Guid applicationId;
    private SignalContent content;
    private string connectionId;
    private string sessionId;

    /// <inheritdoc />
    public Result<SendSignalRequest> Create() =>
        Result<SendSignalRequest>.FromSuccess(new SendSignalRequest
            {
                ApplicationId = this.applicationId,
                ConnectionId = this.connectionId,
                SessionId = this.sessionId,
                Content = this.content,
            })
            .Map(InputEvaluation<SendSignalRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyApplicationId, VerifySessionId, VerifyContentData,
                VerifyContentType, VerifyConnectionId));

    /// <inheritdoc />
    public IBuilderForSessionId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForContent WithConnectionId(string value)
    {
        this.connectionId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<SendSignalRequest> WithContent(SignalContent value)
    {
        this.content = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForConnectionId WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<SendSignalRequest> VerifyApplicationId(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<SendSignalRequest> VerifyConnectionId(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConnectionId, nameof(request.ConnectionId));

    private static Result<SendSignalRequest> VerifyContentData(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Data, nameof(SignalContent.Data));

    private static Result<SendSignalRequest> VerifyContentType(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Type, nameof(SignalContent.Type));

    private static Result<SendSignalRequest> VerifySessionId(SendSignalRequest request) =>
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
    IBuilderForContent WithConnectionId(string value);
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
///     Represents a builder that allows to set the Content.
/// </summary>
public interface IBuilderForContent
{
    /// <summary>
    ///     Sets the Content on the builder.
    /// </summary>
    /// <param name="value">The content.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<SendSignalRequest> WithContent(SignalContent value);
}