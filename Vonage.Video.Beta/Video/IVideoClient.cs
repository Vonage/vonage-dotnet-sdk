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
    SessionClient SessionClient { get; }

    /// <summary>
    ///     Client for sending signals to participants.
    /// </summary>
    SignalingClient SignalingClient { get; }

    /// <summary>
    ///     Client for moderating connections.
    /// </summary>
    ModerationClient ModerationClient { get; }

    /// <summary>
    ///     Client for archiving.
    /// </summary>
    ArchiveClient ArchiveClient { get; }
}