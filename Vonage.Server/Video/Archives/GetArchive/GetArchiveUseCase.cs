using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Archives.GetArchive;

internal class GetArchiveUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal GetArchiveUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<Archive>> GetArchiveAsync(Result<GetArchiveRequest> request) =>
        this.VonageHttpClient.SendWithResponseAsync<Archive, GetArchiveRequest>(request, this.generateToken());
}