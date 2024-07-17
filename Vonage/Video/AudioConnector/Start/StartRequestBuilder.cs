#region
using System;
using System.Collections.Generic;
using System.Linq;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Video.AudioConnector.Start;

internal struct StartRequestBuilder : IBuilderForApplicationId, IBuilderForOptional, IBuilderForToken,
    IBuilderForSessionId, IBuilderForUri
{
    private Guid applicationId = Guid.Empty;
    private string sessionId = string.Empty;
    private string token = string.Empty;
    private Uri uri;
    private readonly HashSet<string> streams = new HashSet<string>();
    private readonly Dictionary<string, string> headers = new Dictionary<string, string>();
    private SupportedAudioRates audioRate = SupportedAudioRates.AUDIO_RATE_8000Hz;

    public StartRequestBuilder()
    {
    }

    public IBuilderForSessionId WithApplicationId(Guid value) => this with {applicationId = value};

    public Result<StartRequest> Create() => Result<StartRequest>.FromSuccess(new StartRequest
        {
            ApplicationId = this.applicationId,
            SessionId = this.sessionId,
            Token = this.token,
            WebSocket = new WebSocket(this.uri, this.streams.ToArray(), this.headers, this.audioRate),
        })
        .Map(InputEvaluation<StartRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(
            VerifyToken,
            VerifyApplicationId,
            VerifySessionId));

    public IBuilderForUri WithToken(string value) => this with {token = value};

    public IBuilderForToken WithSessionId(string value) => this with {sessionId = value};

    public IBuilderForOptional WithUrl(Uri value) => this with {uri = value};

    private static Result<StartRequest> VerifyApplicationId(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<StartRequest> VerifySessionId(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    private static Result<StartRequest> VerifyToken(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Token, nameof(request.Token));

    public IBuilderForOptional WithStream(string value)
    {
        this.streams.Add(value);
        return this;
    }

    public IBuilderForOptional WithHeader(KeyValuePair<string, string> header)
    {
        if (!this.headers.ContainsKey(header.Key))
        {
            this.headers.Add(header.Key, header.Value);
        }

        return this;
    }

    public IBuilderForOptional WithAudioRate(SupportedAudioRates value) => this with {audioRate = value};
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
    IBuilderForUri WithToken(string value);
}

/// <summary>
///     Represents a builder for Uri.
/// </summary>
public interface IBuilderForUri
{
    /// <summary>
    ///     Sets the Url on the builder.
    /// </summary>
    /// <param name="value">The Url.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithUrl(Uri value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<StartRequest>
{
    /// <summary>
    ///     Adds a stream to include iin the WebSocket audio.
    /// </summary>
    /// <param name="value">The stream Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithStream(string value);

    /// <summary>
    ///     Adds a custom header.
    /// </summary>
    /// <param name="header">The custom header.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithHeader(KeyValuePair<string, string> header);

    /// <summary>
    ///     Sets the audio rate on the builder.
    /// </summary>
    /// <param name="value">The audio sampling rate.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithAudioRate(SupportedAudioRates value);
}