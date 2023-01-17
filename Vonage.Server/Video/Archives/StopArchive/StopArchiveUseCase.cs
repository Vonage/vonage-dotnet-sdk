using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Archives.StopArchive;

internal class StopArchiveUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal StopArchiveUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<Archive>> StopArchiveAsync(Result<StopArchiveRequest> request) =>
        this.videoHttpClient.SendWithResponseAsync<Archive, StopArchiveRequest>(request, this.generateToken());
}