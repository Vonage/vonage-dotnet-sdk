using System.Threading.Tasks;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Video.Signaling.SendSignals;

public interface ISendSignalsUseCase
{
    Task<Result<Unit>> SendSignalsAsync(SendSignalsRequest request);
}