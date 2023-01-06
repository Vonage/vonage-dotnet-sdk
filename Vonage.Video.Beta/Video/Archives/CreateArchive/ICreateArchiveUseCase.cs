using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Archives.CreateArchive;

/// <summary>
///     Represents the use case for creating an archive.
/// </summary>
public interface ICreateArchiveUseCase
{
    /// <summary>
    ///     Creates a new archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    Task<Result<Archive>> CreateArchiveAsync(Result<CreateArchiveRequest> request);
}