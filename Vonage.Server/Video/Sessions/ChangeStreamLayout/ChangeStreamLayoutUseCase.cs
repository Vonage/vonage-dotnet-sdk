using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Sessions.ChangeStreamLayout;

internal class ChangeStreamLayoutUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal ChangeStreamLayoutUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<Unit>> ChangeStreamLayoutAsync(Result<ChangeStreamLayoutRequest> request) =>
        this.videoHttpClient.SendAsync(request, this.generateToken());
}