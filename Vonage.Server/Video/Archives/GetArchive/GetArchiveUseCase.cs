using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Archives.GetArchive;

internal class GetArchiveUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal GetArchiveUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<Archive>> GetArchiveAsync(Result<GetArchiveRequest> request) =>
        this.videoHttpClient.SendWithResponseAsync<Archive, GetArchiveRequest>(request, this.generateToken());
}