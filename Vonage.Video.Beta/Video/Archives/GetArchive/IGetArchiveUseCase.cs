using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video.Archives.Common;

namespace Vonage.Video.Beta.Video.Archives.GetArchive;

/// <summary>
///     Represents the use case for retrieving an archive.
/// </summary>
public interface IGetArchiveUseCase
{
    /// <summary>
    ///     Return the archive information of a specific archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    Task<Result<Archive>> GetArchiveAsync(Result<GetArchiveRequest> request);
}