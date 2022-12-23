using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Archives.StopArchive;

/// <summary>
///     Represents the use case for stopping an archive.
/// </summary>
public interface IStopArchiveUseCase
{
    /// <summary>
    ///     Stops an archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    Task<Result<Archive>> StopArchiveAsync(StopArchiveRequest request);
}