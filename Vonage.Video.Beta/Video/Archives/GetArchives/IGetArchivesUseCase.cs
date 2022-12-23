using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Archives.GetArchives;

/// <summary>
/// Represents the use case for retrieving archives.
/// </summary>
public interface IGetArchivesUseCase
{
    /// <summary>
    ///     Retrieves all archives from an application.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A success state with archives if the operation succeeded. A failure state with the error message if it failed.</returns>
    Task<Result<GetArchivesResponse>> GetArchivesAsync(GetArchivesRequest request);
}