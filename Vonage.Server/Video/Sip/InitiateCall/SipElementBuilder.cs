using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Sip.InitiateCall;

/// <summary>
///     Represents a builder for SipElement.
/// </summary>
public class SipElementBuilder
{
    private bool hasEncryptedMedia;
    private bool hasVideo;
    private bool hasForceMute;
    private Maybe<SipElement.SipAuthentication> authentication;
    private Maybe<SipElement.SipHeader> customHeaderKey;
    private string from;
    private readonly string uri;

    private SipElementBuilder(string uri) => this.uri = uri;

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <param name="uri">The SIP uri.</param>
    /// <returns>The builder.</returns>
    public static SipElementBuilder Build(string uri) => new(uri);

    /// <summary>
    ///     Creates a request.
    /// </summary>
    /// <returns>The request if validation succeeded, a failure if it failed.</returns>
    public Result<SipElement> Create() =>
        Result<SipElement>.FromSuccess(new SipElement
            {
                Authentication = this.authentication,
                Uri = this.uri,
                HasVideo = this.hasVideo,
                Headers = this.customHeaderKey,
                HasEncryptedMedia = this.hasEncryptedMedia,
                HasForceMute = this.hasForceMute,
                From = this.from,
            })
            .Bind(VerifyUri);

    /// <summary>
    ///     Indicates the transmitted media must be encrypted.
    /// </summary>
    /// <returns>The builder.</returns>
    public SipElementBuilder EnableEncryptedMedia()
    {
        this.hasEncryptedMedia = true;
        return this;
    }

    /// <summary>
    ///     Indicates the SIP endpoint observes force mute moderation.
    /// </summary>
    /// <returns>The builder.</returns>
    public SipElementBuilder EnableForceMute()
    {
        this.hasForceMute = true;
        return this;
    }

    /// <summary>
    ///     Indicates the SIP call will include video.
    /// </summary>
    /// <returns>The builder.</returns>
    public SipElementBuilder EnableVideo()
    {
        this.hasVideo = true;
        return this;
    }

    /// <summary>
    ///     Specifies the authentication credentials to be used in the SIP Invite Request.
    /// </summary>
    /// <param name="sipAuthentication">The authentication.</param>
    /// <returns>The builder.</returns>
    public SipElementBuilder WithAuthentication(SipElement.SipAuthentication sipAuthentication)
    {
        this.authentication = sipAuthentication;
        return this;
    }

    /// <summary>
    ///     Specifies the number that will be sent to the SIP number as the caller.
    /// </summary>
    /// <param name="value">The number.</param>
    /// <returns>The builder.</returns>
    public SipElementBuilder WithFrom(string value)
    {
        this.from = value;
        return this;
    }

    /// <summary>
    ///     Specifies a custom header to be added to the SIP Invite Request.
    /// </summary>
    /// <param name="value">The custom header.</param>
    /// <returns>The builder.</returns>
    public SipElementBuilder WithHeaderKey(string value)
    {
        this.customHeaderKey = new SipElement.SipHeader(value);
        return this;
    }

    private static Result<SipElement> VerifyUri(SipElement request) =>
        InputValidation.VerifyNotEmpty(request, request.Uri, nameof(request.Uri));
}