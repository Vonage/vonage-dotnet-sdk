using System;
using System.Threading.Tasks;
using Vonage.Server.Common.Monads;

namespace Vonage.Server.Video.Sessions.GetStreams;

internal class GetStreamsUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal GetStreamsUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<GetStreamsResponse>> GetStreamsAsync(Result<GetStreamsRequest> request) =>
        this.videoHttpClient.SendWithResponseAsync<GetStreamsResponse, GetStreamsRequest>(request,
            this.generateToken());
}