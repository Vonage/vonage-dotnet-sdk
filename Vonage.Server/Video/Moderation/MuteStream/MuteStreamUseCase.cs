using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Moderation.MuteStream;

internal class MuteStreamUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal MuteStreamUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<MuteStreamResponse>> MuteStreamAsync(Result<MuteStreamRequest> request) =>
        this.VonageHttpClient.SendWithResponseAsync<MuteStreamResponse, MuteStreamRequest>(request,
            this.generateToken());
}