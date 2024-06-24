using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server;

namespace Vonage.Video.ExperienceComposer.Start;

internal struct StartRequestBuilder :
    IBuilderForApplicationId,
    IBuilderForName,
    IBuilderForResolution,
    IBuilderForToken,
    IBuilderForUrl,
    IBuilderForSessionId,
    IBuilderForOptional
{
    private Guid applicationId = Guid.Empty;
    private string sessionId = string.Empty;
    private string token = string.Empty;
    private int maxDuration = 7200;
    private string name = string.Empty;
    private RenderResolution resolution = RenderResolution.StandardDefinitionLandscape;
    private Uri url;

    public StartRequestBuilder()
    {
    }

    public IBuilderForSessionId WithApplicationId(Guid value) => this with {applicationId = value};

    public Result<StartRequest> Create() => Result<StartRequest>.FromSuccess(new StartRequest
        {
            ApplicationId = this.applicationId,
            SessionId = this.sessionId,
            Token = this.token,
            Resolution = this.resolution,
            Url = this.url,
            MaxDuration = this.maxDuration,
            Properties = new StartProperties(this.name),
        })
        .Map(InputEvaluation<StartRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(
            VerifyName,
            VerifyMinimumDuration,
            VerifyMaximumDuration,
            VerifyToken,
            VerifyApplicationId,
            VerifySessionId));

    public IBuilderForOptional WithMaxDuration(int value) => this with {maxDuration = value};

    public IBuilderForOptional WithName(string value) => this with {name = value};

    public IBuilderForName WithResolution(RenderResolution value) => this with {resolution = value};

    public IBuilderForUrl WithToken(string value) => this with {token = value};

    public IBuilderForResolution WithUrl(Uri value) => this with {url = value};

    public IBuilderForToken WithSessionId(string value) => this with {sessionId = value};

    private static Result<StartRequest> VerifyApplicationId(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<StartRequest> VerifySessionId(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    private static Result<StartRequest> VerifyToken(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Token, nameof(request.Token));

    private static Result<StartRequest> VerifyName(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Properties.Name, nameof(request.Properties.Name));

    private static Result<StartRequest> VerifyMinimumDuration(StartRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.MaxDuration, 60, nameof(request.MaxDuration));

    private static Result<StartRequest> VerifyMaximumDuration(StartRequest request) =>
        InputValidation.VerifyLowerOrEqualThan(request, request.MaxDuration, 36000, nameof(request.MaxDuration));
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
    IBuilderForUrl WithToken(string value);
}

/// <summary>
///     Represents a builder for Url.
/// </summary>
public interface IBuilderForUrl
{
    /// <summary>
    ///     Sets the Url on the builder.
    /// </summary>
    /// <param name="value">The Url.</param>
    /// <returns>The builder.</returns>
    IBuilderForResolution WithUrl(Uri value);
}

/// <summary>
///     Represents a builder for resolution.
/// </summary>
public interface IBuilderForResolution
{
    /// <summary>
    ///     Sets the resolution on the builder.
    /// </summary>
    /// <param name="value">The resolution.</param>
    /// <returns>The builder.</returns>
    IBuilderForName WithResolution(RenderResolution value);
}

/// <summary>
///     Represents a builder for name.
/// </summary>
public interface IBuilderForName
{
    /// <summary>
    ///     Sets the name on the builder.
    /// </summary>
    /// <param name="value">The name.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithName(string value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<StartRequest>
{
    /// <summary>
    ///     Sets the maximum duration on the builder.
    /// </summary>
    /// <param name="value">The maximum duration.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithMaxDuration(int value);
}