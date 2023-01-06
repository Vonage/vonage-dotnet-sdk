using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Archives.RemoveStream;

/// <summary>
///     Represents the use case for removing a stream from an archive.
/// </summary>
public interface IRemoveStreamUseCase
{
    /// <summary>
    ///     Removes the stream included in a composed archive that was started with the streamMode set to "manual".
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    Task<Result<Unit>> RemoveStreamAsync(Result<RemoveStreamRequest> request);
}