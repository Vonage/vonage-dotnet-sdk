using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Archives.AddStream;

internal class AddStreamUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal AddStreamUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<Unit>> AddStreamAsync(Result<AddStreamRequest> request) =>
        this.videoHttpClient.SendAsync(request, this.generateToken());
}