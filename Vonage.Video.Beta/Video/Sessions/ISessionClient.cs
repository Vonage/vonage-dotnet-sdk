using Vonage.Video.Beta.Video.Sessions.ChangeStreamLayout;
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
    IGetStreamsUseCase,
    IChangeStreamLayoutUseCase
{
}