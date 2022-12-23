using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Archives.DeleteArchive;

/// <summary>
///     Represents the use case for deleting an archive.
/// </summary>
public interface IDeleteArchiveUseCase
{
    /// <summary>
    ///     Deletes the specified archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    Task<Result<Unit>> DeleteArchiveAsync(DeleteArchiveRequest request);
}