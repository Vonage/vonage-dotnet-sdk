using Vonage.Request;
using Vonage.Video.Beta.Video.Sessions;

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
}