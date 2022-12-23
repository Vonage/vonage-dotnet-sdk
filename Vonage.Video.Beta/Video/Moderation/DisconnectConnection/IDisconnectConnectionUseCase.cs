using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Moderation.DisconnectConnection;

/// <summary>
///     Represents the use case for disconnecting a connection.
/// </summary>
public interface IDisconnectConnectionUseCase
{
    /// <summary>
    ///     Disconnects a connection.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    Task<Result<Unit>> DisconnectConnectionAsync(DisconnectConnectionRequest request);
}