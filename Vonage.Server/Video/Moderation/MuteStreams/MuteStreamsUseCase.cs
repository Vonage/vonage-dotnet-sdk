using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Moderation.MuteStreams;

internal class MuteStreamsUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal MuteStreamsUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<MuteStreamsResponse>> MuteStreamsAsync(Result<MuteStreamsRequest> request) =>
        this.VonageHttpClient.SendWithResponseAsync<MuteStreamsResponse, MuteStreamsRequest>(request,
            this.generateToken());
}