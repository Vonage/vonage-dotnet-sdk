using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Signaling.SendSignals;

/// <summary>
///     Represents a use case for sending signals.
/// </summary>
public interface ISendSignalsUseCase
{
    /// <summary>
    ///     Sends signals to all participants in an active Vonage Video session.
    /// </summary>
    /// <param name="request">The signal request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    Task<Result<Unit>> SendSignalsAsync(SendSignalsRequest request);
}