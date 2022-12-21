using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Video.Signaling.SendSignals;

/// <inheritdoc />
public class SendSignalsUseCase : ISendSignalsUseCase
{
    private readonly Func<string> generateToken;
    private readonly CustomClient customClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public SendSignalsUseCase(CustomClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.customClient = client;
    }

    /// <inheritdoc />
    public Task<Result<Unit>> SendSignalsAsync(SendSignalsRequest request) =>
        this.customClient.SendAsync(request, this.generateToken());
}