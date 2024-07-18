#region
using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.LiveCaptions.Start;

internal struct StartRequestBuilder : IBuilderForApplicationId, IBuilderForOptional, IBuilderForToken,
    IBuilderForSessionId
{
    private const string DefaultLanguage = "en-US";
    private const int DefaultMaxDuration = 14400;

    private const int MinimalMaxDuration = 300;
    private Guid applicationId = Guid.Empty;
    private string language = DefaultLanguage;
    private int maxDuration = DefaultMaxDuration;
    private bool partialCaptions = true;
    private string sessionId = string.Empty;
    private Maybe<Uri> statusCallbackUrl;
    private string token = string.Empty;

    public StartRequestBuilder()
    {
    }

    public IBuilderForSessionId WithApplicationId(Guid value) => this with {applicationId = value};

    public Result<StartRequest> Create() => Result<StartRequest>.FromSuccess(new StartRequest
        {
            ApplicationId = this.applicationId,
            SessionId = this.sessionId,
            Token = this.token,
            StatusCallbackUrl = this.statusCallbackUrl,
            Language = this.language,
            MaxDuration = this.maxDuration,
            PartialCaptions = this.partialCaptions,
        })
        .Map(InputEvaluation<StartRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(
            VerifyToken,
            VerifyApplicationId,
            VerifySessionId,
            VerifyMinimumDuration,
            VerifyMaximumDuration));

    public IBuilderForOptional WithStatusCallbackUrl(Uri value) => this with {statusCallbackUrl = value};

    public IBuilderForOptional WithLanguage(string value) => this with {language = value};

    public IBuilderForOptional WithMaxDuration(int value) => this with {maxDuration = value};

    public IBuilderForOptional DisablePartialCaptions() => this with {partialCaptions = false};

    public IBuilderForToken WithSessionId(string value) => this with {sessionId = value};

    public IBuilderForOptional WithToken(string value) => this with {token = value};

    private static Result<StartRequest> VerifyApplicationId(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<StartRequest> VerifySessionId(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    private static Result<StartRequest> VerifyToken(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Token, nameof(request.Token));

    private static Result<StartRequest> VerifyMinimumDuration(StartRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.MaxDuration, MinimalMaxDuration,
            nameof(request.MaxDuration));

    private static Result<StartRequest> VerifyMaximumDuration(StartRequest request) =>
        InputValidation.VerifyLowerOrEqualThan(request, request.MaxDuration, DefaultMaxDuration,
            nameof(request.MaxDuration));
}

/// <summary>
///     Represents a builder for application Id.
/// </summary>
public interface IBuilderForApplicationId
{
    /// <summary>
    ///     Sets the application Id on the builder.
    /// </summary>
    /// <param name="value">The application Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForSessionId WithApplicationId(Guid value);
}

/// <summary>
///     Represents a builder for session Id.
/// </summary>
public interface IBuilderForSessionId
{
    /// <summary>
    ///     Sets the session Id on the builder.
    /// </summary>
    /// <param name="value">The session Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForToken WithSessionId(string value);
}

/// <summary>
///     Represents a builder for token.
/// </summary>
public interface IBuilderForToken
{
    /// <summary>
    ///     Sets the token on the builder.
    /// </summary>
    /// <param name="value">The token.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithToken(string value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<StartRequest>
{
    /// <summary>
    ///     Disables partial captions.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional DisablePartialCaptions();

    /// <summary>
    ///     Sets the language code.
    /// </summary>
    /// <param name="value">The language code.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithLanguage(string value);

    /// <summary>
    ///     Sets the maximum duration.
    /// </summary>
    /// <param name="value">The maximum duration.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithMaxDuration(int value);

    /// <summary>
    ///     Sets the Url on the builder.
    /// </summary>
    /// <param name="value">The Url.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithStatusCallbackUrl(Uri value);
}