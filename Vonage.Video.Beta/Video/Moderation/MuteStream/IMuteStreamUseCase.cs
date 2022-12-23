using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Moderation.MuteStream;

/// <summary>
///     Represents the use case for muting a stream.
/// </summary>
public interface IMuteStreamUseCase
{
    /// <summary>
    ///   Mutes a specific publisher stream
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success with the stream information if the operation succeeds, Failure it if fails.</returns>
    Task<Result<MuteStreamResponse>> MuteStreamAsync(MuteStreamRequest request);
}