using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.ChangeLayout;

internal class ChangeLayoutUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal ChangeLayoutUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<Unit>> ChangeLayoutAsync(Result<ChangeLayoutRequest> request) =>
        this.VonageHttpClient.SendAsync(request, this.generateToken());
}