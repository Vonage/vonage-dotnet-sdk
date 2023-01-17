using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.GetArchives;

internal class GetArchivesUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    internal GetArchivesUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    internal Task<Result<GetArchivesResponse>> GetArchivesAsync(Result<GetArchivesRequest> request) =>
        this.videoHttpClient.SendWithResponseAsync<GetArchivesResponse, GetArchivesRequest>(request,
            this.generateToken());
}