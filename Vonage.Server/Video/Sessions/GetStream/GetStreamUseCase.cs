using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Sessions.GetStream;

internal class GetStreamUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal GetStreamUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<GetStreamResponse>> GetStreamAsync(Result<GetStreamRequest> request) =>
        this.VonageHttpClient.SendWithResponseAsync<GetStreamResponse, GetStreamRequest>(request, this.generateToken());
}