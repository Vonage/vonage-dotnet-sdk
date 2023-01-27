using System.Net.Http;
using System.Text;
using System.Web;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Server.Video.Sessions.Common;

namespace Vonage.Server.Video.Sessions.CreateSession;

/// <summary>
///     Represents a request for creating a session.
/// </summary>
public readonly struct CreateSessionRequest : IVonageRequest
{
    /// <summary>
    ///     The endpoint for creating a session.
    /// </summary>
    private const string CreateSessionEndpoint = "/session/create";

    /// <summary>
    ///     Indicates media mode and archive mode are incompatible.
    /// </summary>
    public const string IncompatibleMediaAndArchive =
        "A session with always archive mode must also have the routed media mode.";

    private CreateSessionRequest(IpAddress location, MediaMode mediaMode, ArchiveMode archiveMode)
    {
        this.Location = location;
        this.MediaMode = mediaMode;
        this.ArchiveMode = archiveMode;
    }

    /// <summary>
    ///     Set to always to have the session archived automatically. With the archiveModeset to manual (the default), you can archive the session by calling the REST /archive POST method. If you set the archiveMode to always, you must also set the p2p.preference parameter to disabled (the default).
    /// </summary>
    public ArchiveMode ArchiveMode { get; }

    /// <summary>
    ///     Creates a default request with empty ip address, relayed media mode and manual archive mode.
    /// </summary>
    public static CreateSessionRequest Default => new(IpAddress.Empty, MediaMode.Relayed, ArchiveMode.Manual);

    /// <summary>
    ///     The IP address that the Vonage Video APi will use to situate the session in its global network. If no location hint is passed in (which is recommended), the session uses a media server based on the location of the first client connecting to the session. Pass a location hint in only if you know the general geographic region (and a representative IP address) and you think the first client connecting may not be in that region. Specify an IP address that is representative of the geographical location for the session.
    /// </summary>
    public IpAddress Location { get; }

    /// <summary>
    ///     Indicates how streams are sent.
    /// </summary>
    public MediaMode MediaMode { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/session/create";

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public string GetUrlEncoded()
    {
        var builder = new StringBuilder();
        builder.Append("location=");
        builder.Append(HttpUtility.UrlEncode(this.Location.Address));
        builder.Append("&archiveMode=");
        builder.Append(HttpUtility.UrlEncode(this.ArchiveMode.ToString().ToLowerInvariant()));
        builder.Append("&p2p.preference=");
        builder.Append(HttpUtility.UrlEncode(GetMediaPreference(this.MediaMode)));
        return builder.ToString();
    }

    /// <summary>
    ///     Parses the provided input.
    /// </summary>
    /// <param name="location">The IP address that the Vonage Video APi will use to situate the session in its global network. If no location hint is passed in (which is recommended), the session uses a media server based on the location of the first client connecting to the session. Pass a location hint in only if you know the general geographic region (and a representative IP address) and you think the first client connecting may not be in that region. Specify an IP address that is representative of the geographical location for the session.</param>
    /// <param name="mediaMode">Indicates how streams are sent.</param>
    /// <param name="archiveMode">Set to always to have the session archived automatically. With the archiveModeset to manual (the default), you can archive the session by calling the REST /archive POST method. If you set the archiveMode to always, you must also set the p2p.preference parameter to disabled (the default).</param>
    /// <returns>Success if the parsing operation succeeded, Failure if it failed.</returns>
    public static Result<CreateSessionRequest> Parse(string location, MediaMode mediaMode, ArchiveMode archiveMode) =>
        IpAddress
            .Parse(location)
            .Bind(ipAddress => Parse(ipAddress, mediaMode, archiveMode));

    /// <summary>
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <param name="mediaMode"></param>
    /// <param name="archiveMode"></param>
    /// <returns></returns>
    public static Result<CreateSessionRequest>
        Parse(IpAddress ipAddress, MediaMode mediaMode, ArchiveMode archiveMode) =>
        AreMediaAndArchiveCompatible(mediaMode, archiveMode)
            ? Result<CreateSessionRequest>.FromSuccess(new CreateSessionRequest(ipAddress, mediaMode, archiveMode))
            : Result<CreateSessionRequest>.FromFailure(ResultFailure.FromErrorMessage(IncompatibleMediaAndArchive));

    private static bool AreMediaAndArchiveCompatible(MediaMode mediaMode, ArchiveMode archiveMode) =>
        archiveMode == ArchiveMode.Manual || mediaMode == MediaMode.Routed;

    private static string GetMediaPreference(MediaMode mediaMode) =>
        mediaMode == MediaMode.Relayed ? "enabled" : "disabled";

    private StringContent GetRequestContent() =>
        new(this.GetUrlEncoded(), Encoding.UTF8, "application/x-www-form-urlencoded");
}