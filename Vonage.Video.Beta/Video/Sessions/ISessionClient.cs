using Vonage.Request;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using Vonage.Video.Beta.Video.Sessions.GetStreams;

namespace Vonage.Video.Beta.Video.Sessions;

/// <summary>
///     Exposes features for managing sessions.
/// </summary>
public interface ISessionClient :
    ICreateSessionUseCase,
    IGetStreamUseCase,
    IGetStreamsUseCase
{
    /// <summary>
    ///     The credentials that will be used for further connections.
    /// </summary>
    Credentials Credentials { get; }
}