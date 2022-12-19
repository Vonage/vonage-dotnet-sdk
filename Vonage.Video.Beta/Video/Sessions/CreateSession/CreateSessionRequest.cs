using System.Text;
using System.Web;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;

namespace Vonage.Video.Beta.Video.Sessions.CreateSession;

/// <summary>
///     Represents a request for creating a session.
/// </summary>
public readonly struct CreateSessionRequest
{
    /// <summary>
    ///     The endpoint for creating a session.
    /// </summary>
    public const string CreateSessionEndpoint = "/session/create";

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
    ///     Defines how archiving is configured for the session.
    /// </summary>
    public ArchiveMode ArchiveMode { get; }

    /// <summary>
    ///     Defines how media will be transmitted on the session.
    /// </summary>
    public MediaMode MediaMode { get; }

    /// <summary>
    ///     The ip address.
    /// </summary>
    public IpAddress Location { get; }

    /// <summary>
    ///     Creates a default request with empty ip address, relayed media mode and manual archive mode.
    /// </summary>
    public static CreateSessionRequest Default => new(IpAddress.Empty, MediaMode.Relayed, ArchiveMode.Manual);

    /// <summary>
    ///     Parses the provided input.
    /// </summary>
    /// <param name="location">The ip address.</param>
    /// <param name="mediaMode">The media mode.</param>
    /// <param name="archiveMode">The archive mode.</param>
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

    private static string GetMediaPreference(MediaMode mediaMode) =>
        mediaMode == MediaMode.Relayed ? "enabled" : "disabled";
}