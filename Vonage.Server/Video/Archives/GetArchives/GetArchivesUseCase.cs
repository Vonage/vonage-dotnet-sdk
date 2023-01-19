using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.GetArchives;

internal class GetArchivesUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal GetArchivesUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<GetArchivesResponse>> GetArchivesAsync(Result<GetArchivesRequest> request) =>
        this.VonageHttpClient.SendWithResponseAsync<GetArchivesResponse, GetArchivesRequest>(request,
            this.generateToken());
}