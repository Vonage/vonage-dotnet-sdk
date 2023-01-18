using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Archives.StopArchive;

internal class StopArchiveUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal StopArchiveUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<Archive>> StopArchiveAsync(Result<StopArchiveRequest> request) =>
        this.VonageHttpClient.SendWithResponseAsync<Archive, StopArchiveRequest>(request, this.generateToken());
}