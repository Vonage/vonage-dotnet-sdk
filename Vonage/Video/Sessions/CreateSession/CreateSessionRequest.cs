﻿#region
using System.Net;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
#endregion

namespace Vonage.Video.Sessions.CreateSession;

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
    ///     Whether end-to-end encryption is enabled for the session (true) or not (false)
    /// </summary>
    public bool EndToEndEncryption { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForLocation Build() => new CreateSessionRequestBuilder();

    /// <summary>
    ///     Retrieves the encoded Url.
    /// </summary>
    /// <returns>The encoded Url.</returns>
    public string GetUrlEncoded()
    {
        var builder = new StringBuilder();
        builder.Append("location=");
        builder.Append(WebUtility.UrlEncode(this.Location.Address));
        builder.Append("&archiveMode=");
        builder.Append(WebUtility.UrlEncode(this.ArchiveMode.ToString().ToLowerInvariant()));
        builder.Append("&p2p.preference=");
        builder.Append(WebUtility.UrlEncode(GetMediaPreference(this.MediaMode)));
        builder.Append("&e2ee=");
        builder.Append(this.EndToEndEncryption.ToString().ToLowerInvariant());
        return builder.ToString();
    }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, "/session/create")
            .WithContent(this.GetRequestContent())
            .Build();

    private static string GetMediaPreference(MediaMode mediaMode) =>
        mediaMode == MediaMode.Relayed ? "enabled" : "disabled";

    private StringContent GetRequestContent() =>
        new(this.GetUrlEncoded(), Encoding.UTF8, "application/x-www-form-urlencoded");
}