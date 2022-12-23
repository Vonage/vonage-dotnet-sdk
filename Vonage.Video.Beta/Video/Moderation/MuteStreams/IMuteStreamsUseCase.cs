using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Moderation.MuteStreams;

/// <summary>
///     Represents the use case for muting streams.
/// </summary>
public interface IMuteStreamsUseCase
{
    /// <summary>
    ///     Forces all streams (except for an optional list of streams) in a session to mute published audio. You can also use
    ///     this method to disable the force mute state of a session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success with the stream information if the operation succeeds, Failure it if fails.</returns>
    Task<Result<MuteStreamsResponse>> MuteStreamSAsync(MuteStreamsRequest request);
}