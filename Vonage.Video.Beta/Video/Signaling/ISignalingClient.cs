using Vonage.Video.Beta.Video.Signaling.SendSignal;
using Vonage.Video.Beta.Video.Signaling.SendSignals;

namespace Vonage.Video.Beta.Video.Signaling;

/// <summary>
///     Exposes features for sending and receiving signals.
/// </summary>
public interface ISignalingClient :
    ISendSignalUseCase,
    ISendSignalsUseCase
{
}