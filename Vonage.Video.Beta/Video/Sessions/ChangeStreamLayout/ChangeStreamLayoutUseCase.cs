using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Sessions.ChangeStreamLayout;

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