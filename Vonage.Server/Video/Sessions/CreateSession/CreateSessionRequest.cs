using System.Net.Http;
using System.Text;
using System.Web;
using Vonage.Common.Client;

namespace Vonage.Server.Video.Sessions.CreateSession;

/// <summary>
///     Represents a request for creating a session.
/// </summary>
public readonly struct CreateSessionRequest : IVonageRequest
{
    /// <summary>
    ///     Set to always to have the session archived automatically. With the archiveMode set to manual (the default), you can
    ///     archive the session by calling the REST /archive POST method. If you set the archiveMode to always, you must also
    ///     set the p2p.preference parameter to disabled (the default).
    /// </summary>
    public ArchiveMode ArchiveMode { get; internal init; }

    /// <summary>
    ///     Creates a default request with empty ip address, relayed media mode and manual archive mode.
    /// </summary>
    public static CreateSessionRequest Default => new()
    {
        Location = IpAddress.Empty,
        MediaMode = MediaMode.Relayed,
        ArchiveMode = ArchiveMode.Manual,
    };

    /// <summary>
    ///     The IP address that the Vonage Video APi will use to situate the session in its global network. If no location hint
    ///     is passed in (which is recommended), the session uses a media server based on the location of the first client
    ///     connecting to the session. Pass a location hint in only if you know the general geographic region (and a
    ///     representative IP address) and you think the first client connecting may not be in that region. Specify an IP
    ///     address that is representative of the geographical location for the session.
    /// </summary>
    public IpAddress Location { get; internal init; }

    /// <summary>
    ///     Indicates how streams are sent.
    /// </summary>
    public MediaMode MediaMode { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForLocation Build() => new CreateSessionRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/session/create";

    /// <summary>
    /// Retrieves the encoded Url.
    /// </summary>
    /// <returns>The encoded Url.</returns>
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

    private static string GetMediaPreference(MediaMode mediaMode) =>
        mediaMode == MediaMode.Relayed ? "enabled" : "disabled";

    private StringContent GetRequestContent() =>
        new(this.GetUrlEncoded(), Encoding.UTF8, "application/x-www-form-urlencoded");
}