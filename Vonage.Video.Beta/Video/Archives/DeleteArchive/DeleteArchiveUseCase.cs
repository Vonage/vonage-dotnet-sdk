using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Archives.DeleteArchive;

internal class DeleteArchiveUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal DeleteArchiveUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<Unit>> DeleteArchiveAsync(Result<DeleteArchiveRequest> request) =>
        this.videoHttpClient.SendAsync(request, this.generateToken());
}