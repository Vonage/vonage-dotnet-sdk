using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.AddStream;

internal class AddStreamUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal AddStreamUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<Unit>> AddStreamAsync(Result<AddStreamRequest> request) =>
        this.VonageHttpClient.SendAsync(request, this.generateToken());
}