using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Sessions.CreateSession;

/// <summary>
///     Represents the use case for creating a session.
/// </summary>
public interface ICreateSessionUseCase
{
    /// <summary>
    ///     Creates a new session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    Task<Result<CreateSessionResponse>> CreateSessionAsync(CreateSessionRequest request);
}