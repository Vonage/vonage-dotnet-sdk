using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Video.Signaling.SendSignals;

internal class SendSignalsRequestBuilder :
    IBuilderForApplicationId,
    IBuilderForSessionId,
    IBuilderForContent,
    IVonageRequestBuilder<SendSignalsRequest>
{
    private Guid applicationId;
    private SignalContent content;
    private string sessionId;

    /// <inheritdoc />
    public Result<SendSignalsRequest> Create() =>
        Result<SendSignalsRequest>.FromSuccess(new SendSignalsRequest
            {
                ApplicationId = this.applicationId,
                SessionId = this.sessionId,
                Content = this.content,
            })
            .Map(InputEvaluation<SendSignalsRequest>.Evaluate)
            .Bind(evaluation =>
                evaluation.WithRules(VerifyApplicationId, VerifySessionId, VerifyContentData, VerifyContentType));

    /// <inheritdoc />
    public IBuilderForSessionId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<SendSignalsRequest> WithContent(SignalContent value)
    {
        this.content = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForContent WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    private static Result<SendSignalsRequest> VerifyApplicationId(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<SendSignalsRequest> VerifyContentData(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Data, nameof(SignalContent.Data));

    private static Result<SendSignalsRequest> VerifyContentType(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Type, nameof(SignalContent.Type));

    private static Result<SendSignalsRequest> VerifySessionId(SendSignalsRequest request) =>
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
    IBuilderForContent WithSessionId(string value);
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
    IVonageRequestBuilder<SendSignalsRequest> WithContent(SignalContent value);
}