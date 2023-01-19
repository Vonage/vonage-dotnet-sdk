using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Archives.CreateArchive;

internal class CreateArchiveUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal CreateArchiveUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<Archive>> CreateArchiveAsync(Result<CreateArchiveRequest> request) =>
        this.VonageHttpClient.SendWithResponseAsync<Archive, CreateArchiveRequest>(request, this.generateToken());
}