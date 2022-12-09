using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Video.Session.CreateSession;

public struct CreateSessionRequest
{
    public const string IncompatibleMediaAndArchive =
        "A session with always archive mode must also have the routed media mode.";

    private CreateSessionRequest(IpAddress ipAddress, MediaMode mediaMode, ArchiveMode archiveMode)
    {
    }

    public static Result<CreateSessionRequest> Parse(string location, MediaMode mediaMode, ArchiveMode archiveMode) =>
        IpAddress
            .Parse(location)
            .Bind(ipAddress => Parse(ipAddress, mediaMode, archiveMode));

    public static Result<CreateSessionRequest>
        Parse(IpAddress ipAddress, MediaMode mediaMode, ArchiveMode archiveMode) =>
        AreMediaAndArchiveCompatible(mediaMode, archiveMode)
            ? Result<CreateSessionRequest>.FromSuccess(new CreateSessionRequest(ipAddress, mediaMode, archiveMode))
            : Result<CreateSessionRequest>.FromFailure(ResultFailure.FromErrorMessage(IncompatibleMediaAndArchive));

    private static bool AreMediaAndArchiveCompatible(MediaMode mediaMode, ArchiveMode archiveMode) =>
        archiveMode == ArchiveMode.Manual || mediaMode == MediaMode.Routed;
}