using Vonage.Request;
using Vonage.Video.Archives;
using Vonage.Video.Broadcast;
using Vonage.Video.Moderation;
using Vonage.Video.Sessions;
using Vonage.Video.Signaling;
using Vonage.Video.Sip;

namespace Vonage.Video;

/// <summary>
///     Exposes Video clients.
/// </summary>
public interface IVideoClient
{
    /// <summary>
    ///     Client for archiving.
    /// </summary>
    ArchiveClient ArchiveClient { get; }

    /// <summary>
    ///     Client for live streaming and broadcasting.
    /// </summary>
    BroadcastClient BroadcastClient { get; }

    /// <summary>
    ///     Credentials to be used for further sessions.
    /// </summary>
    Credentials Credentials { get; set; }

    /// <summary>
    ///     Client for moderating connections.
    /// </summary>
    ModerationClient ModerationClient { get; }

    /// <summary>
    ///     Client for managing sessions.
    /// </summary>
    SessionClient SessionClient { get; }

    /// <summary>
    ///     Client for sending signals to participants.
    /// </summary>
    SignalingClient SignalingClient { get; }

    /// <summary>
    ///     Clients for managing SIP calls in a video session.
    /// </summary>
    SipClient SipClient { get; }
}