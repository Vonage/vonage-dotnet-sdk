using Vonage.Request;
using Vonage.Video.Beta.Video.Archives;
using Vonage.Video.Beta.Video.Moderation;
using Vonage.Video.Beta.Video.Sessions;
using Vonage.Video.Beta.Video.Signaling;

namespace Vonage.Video.Beta.Video;

/// <summary>
///     Exposes Video clients.
/// </summary>
public interface IVideoClient
{
    /// <summary>
    ///     Credentials to be used for further sessions.
    /// </summary>
    Credentials Credentials { get; set; }

    /// <summary>
    ///     Client for managing sessions.
    /// </summary>
    ISessionClient SessionClient { get; }

    /// <summary>
    ///     Client for sending signals to participants.
    /// </summary>
    ISignalingClient SignalingClient { get; }

    /// <summary>
    ///     Client for moderating connections.
    /// </summary>
    IModerationClient ModerationClient { get; }

    /// <summary>
    ///     Client for archiving.
    /// </summary>
    IArchiveClient ArchiveClient { get; }
}