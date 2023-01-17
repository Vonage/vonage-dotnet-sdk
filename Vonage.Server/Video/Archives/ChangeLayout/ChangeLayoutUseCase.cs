using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.ChangeLayout;

internal class ChangeLayoutUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal ChangeLayoutUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<Unit>> ChangeLayoutAsync(Result<ChangeLayoutRequest> request) =>
        this.videoHttpClient.SendAsync(request, this.generateToken());
}