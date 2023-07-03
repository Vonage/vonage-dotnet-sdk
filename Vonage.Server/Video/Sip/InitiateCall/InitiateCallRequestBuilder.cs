using System;
using System.Collections.Generic;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Sip.InitiateCall;

internal class InitiateCallRequestBuilder :
    IBuilderForApplicationId,
    IBuilderForToken,
    IBuilderForSessionId,
    IBuilderForSipUri,
    IBuilderForOptionalSip
{
    private bool hasEncryptedMedia;
    private bool hasForceMute;
    private bool hasVideo;
    private Guid applicationId;
    private Maybe<Dictionary<string, string>> headers;
    private Maybe<SipElement.SipAuthentication> authentication;
    private Maybe<string> from;
    private string token;
    private string sessionId;
    private Uri uri;

    /// <inheritdoc />
    public Result<InitiateCallRequest> Create() =>
        Result<InitiateCallRequest>.FromSuccess(new InitiateCallRequest
            {
                ApplicationId = this.applicationId,
                Token = this.token,
                SessionId = this.sessionId,
                Sip = new SipElement
                {
                    Uri = this.uri,
                    Authentication = this.authentication,
                    From = this.from,
                    Headers = this.headers,
                    HasVideo = this.hasVideo,
                    HasEncryptedMedia = this.hasEncryptedMedia,
                    HasForceMute = this.hasForceMute,
                },
            })
            .Map(InputEvaluation<InitiateCallRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyApplicationId, VerifySessionId, VerifyToken));

    /// <inheritdoc />
    public IBuilderForOptionalSip EnableEncryptedMedia()
    {
        this.hasEncryptedMedia = true;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptionalSip EnableForceMute()
    {
        this.hasForceMute = true;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptionalSip EnableVideo()
    {
        this.hasVideo = true;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForSessionId WithApplicationId(Guid value)
    {
        this.applicationId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptionalSip WithAuthentication(SipElement.SipAuthentication sipAuthentication)
    {
        this.authentication = sipAuthentication;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptionalSip WithFrom(string value)
    {
        this.from = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptionalSip WithHeader(string key, string value)
    {
        var dictionary = this.headers.IfNone(new Dictionary<string, string>());
        dictionary[key] = value;
        this.headers = dictionary;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForToken WithSessionId(string value)
    {
        this.sessionId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptionalSip WithSipUri(Uri value)
    {
        this.uri = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForSipUri WithToken(string value)
    {
        this.token = value;
        return this;
    }

    private static Result<InitiateCallRequest> VerifyApplicationId(InitiateCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<InitiateCallRequest> VerifySessionId(InitiateCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    private static Result<InitiateCallRequest> VerifyToken(InitiateCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Token, nameof(request.Token));
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
///     Represents a builder that allows to set the Token.
/// </summary>
public interface IBuilderForToken
{
    /// <summary>
    ///     Sets the Token on the builder.
    /// </summary>
    /// <param name="value">The token.</param>
    /// <returns>The builder.</returns>
    IBuilderForSipUri WithToken(string value);
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
    IBuilderForToken WithSessionId(string value);
}

/// <summary>
///     Represents a builder that allows to set the Sip uri.
/// </summary>
public interface IBuilderForSipUri
{
    /// <summary>
    ///     Sets the Sip uri on the builder.
    /// </summary>
    /// <param name="value">The uri.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptionalSip WithSipUri(Uri value);
}

/// <summary>
///     Represents a builder for optional sip values.
/// </summary>
public interface IBuilderForOptionalSip : IVonageRequestBuilder<InitiateCallRequest>
{
    /// <summary>
    ///     Indicates the transmitted media must be encrypted.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptionalSip EnableEncryptedMedia();

    /// <summary>
    ///     Indicates the SIP endpoint observes force mute moderation.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptionalSip EnableForceMute();

    /// <summary>
    ///     Indicates the SIP call will include video.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptionalSip EnableVideo();

    /// <summary>
    ///     Specifies the authentication credentials to be used in the SIP Invite Request.
    /// </summary>
    /// <param name="sipAuthentication">The authentication.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptionalSip WithAuthentication(SipElement.SipAuthentication sipAuthentication);

    /// <summary>
    ///     Specifies the number that will be sent to the SIP number as the caller.
    /// </summary>
    /// <param name="value">The number.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptionalSip WithFrom(string value);

    /// <summary>
    ///     Specifies a custom header to be added to the SIP Invite Request.
    /// </summary>
    /// <param name="key">The header key.</param>
    /// <param name="value">The header value.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptionalSip WithHeader(string key, string value);
}