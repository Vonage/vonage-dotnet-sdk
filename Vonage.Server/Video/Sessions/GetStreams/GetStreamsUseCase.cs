using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Sessions.GetStreams;

internal class GetStreamsUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal GetStreamsUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<GetStreamsResponse>> GetStreamsAsync(Result<GetStreamsRequest> request) =>
        this.VonageHttpClient.SendWithResponseAsync<GetStreamsResponse, GetStreamsRequest>(request,
            this.generateToken());
}