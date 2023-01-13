using System;
using System.Threading.Tasks;
using Vonage.Server.Common.Monads;

namespace Vonage.Server.Video.Archives.RemoveStream;

internal class RemoveStreamUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal RemoveStreamUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<Unit>> RemoveStreamAsync(Result<RemoveStreamRequest> request) =>
        this.videoHttpClient.SendAsync(request, this.generateToken());
}