using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Sessions.GetStream;

internal class GetStreamUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal GetStreamUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<GetStreamResponse>> GetStreamAsync(Result<GetStreamRequest> request) =>
        this.videoHttpClient.SendWithResponseAsync<GetStreamResponse, GetStreamRequest>(request, this.generateToken());
}