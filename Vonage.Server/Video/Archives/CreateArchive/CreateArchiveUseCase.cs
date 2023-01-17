using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Archives.CreateArchive;

internal class CreateArchiveUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal CreateArchiveUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<Archive>> CreateArchiveAsync(Result<CreateArchiveRequest> request) =>
        this.videoHttpClient.SendWithResponseAsync<Archive, CreateArchiveRequest>(request, this.generateToken());
}