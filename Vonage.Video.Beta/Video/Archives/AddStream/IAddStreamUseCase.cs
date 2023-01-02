using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Archives.AddStream;

/// <summary>
///     Represents the use case for adding a stream to an archive.
/// </summary>
public interface IAddStreamUseCase
{
    /// <summary>
    ///     Adds the stream included in a composed archive that was started with the streamMode set to "manual".
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    Task<Result<Unit>> AddStreamAsync(AddStreamRequest request);
}