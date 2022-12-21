using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Video.Signaling.SendSignal;

/// <inheritdoc />
public class SendSignalUseCase : ISendSignalUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public SendSignalUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    /// <inheritdoc />
    public Task<Result<Unit>> SendSignalAsync(SendSignalRequest request) =>
        this.videoHttpClient.SendAsync(request, this.generateToken());
}