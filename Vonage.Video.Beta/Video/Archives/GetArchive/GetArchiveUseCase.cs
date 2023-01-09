using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video.Archives.Common;

namespace Vonage.Video.Beta.Video.Archives.GetArchive;

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