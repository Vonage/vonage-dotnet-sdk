using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Sessions.GetStreams;

/// <summary>
///     Represents the use case for retrieving streams of a session.
/// </summary>
public interface IGetStreamsUseCase
{
    /// <summary>
    ///     Retrieves information on all Vonage Video streams in a session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A success state with streams if the operation succeeded. A failure state with the error message if it failed.</returns>
    Task<Result<GetStreamsResponse>> GetStreamsAsync(GetStreamsRequest request);
}