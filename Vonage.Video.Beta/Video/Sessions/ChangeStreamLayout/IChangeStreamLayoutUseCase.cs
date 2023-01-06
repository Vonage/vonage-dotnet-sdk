using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Sessions.ChangeStreamLayout;

/// <summary>
///     Represents a use case for changing a stream layout.
/// </summary>
public interface IChangeStreamLayoutUseCase
{
    /// <summary>
    ///     Changes how the stream is displayed in the layout of a composed Vonage Video archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    Task<Result<Unit>> ChangeStreamLayoutAsync(Result<ChangeStreamLayoutRequest> request);
}