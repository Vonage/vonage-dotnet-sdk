using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Archives.ChangeLayout;

/// <summary>
///     Represents the use case for changing the layout of an archive.
/// </summary>
public interface IChangeLayoutUseCase
{
    /// <summary>
    ///     Changes the layout type of a composed archive while it is being recorded.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    Task<Result<Unit>> ChangeLayoutAsync(ChangeLayoutRequest request);
}