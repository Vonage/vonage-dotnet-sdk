using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.DeleteArchive;

internal class DeleteArchiveUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient VonageHttpClient;

    internal DeleteArchiveUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.VonageHttpClient = client;
    }

    internal Task<Result<Unit>> DeleteArchiveAsync(Result<DeleteArchiveRequest> request) =>
        this.VonageHttpClient.SendAsync(request, this.generateToken());
}