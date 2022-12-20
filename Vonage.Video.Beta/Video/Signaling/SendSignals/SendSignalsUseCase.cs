using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Video.Signaling.SendSignals;

public class SendSignalsUseCase : ISendSignalsUseCase
{
    public Task<Result<Unit>> SendSignalsAsync(SendSignalsRequest request) => throw new NotImplementedException();
}