using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.RemoveStream;

internal class RemoveStreamUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal RemoveStreamUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<Unit>> RemoveStreamAsync(Result<RemoveStreamRequest> request) =>
        this.VonageHttpClient.SendAsync(request, this.generateToken());
}