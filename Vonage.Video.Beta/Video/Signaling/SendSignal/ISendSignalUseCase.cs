using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Signaling.SendSignal;

/// <summary>
///     Represents a use case for sending a signal.
/// </summary>
public interface ISendSignalUseCase
{
    /// <summary>
    ///     Sends signals to a single participant in an active Vonage Video session.
    /// </summary>
    /// <param name="request">The signal request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    Task<Result<Unit>> SendSignalAsync(Result<SendSignalRequest> request);
}