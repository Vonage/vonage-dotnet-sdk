using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Sessions.GetStream;

/// <summary>
///     Represents the use case for retrieving a stream.
/// </summary>
public interface IGetStreamUseCase
{
    /// <summary>
    ///     Retrieves a stream.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the stream if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    Task<Result<GetStreamResponse>> GetStreamAsync(GetStreamRequest request);
}