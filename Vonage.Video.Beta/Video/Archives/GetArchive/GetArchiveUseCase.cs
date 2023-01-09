using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video.Archives.Common;

namespace Vonage.Video.Beta.Video.Archives.GetArchive;

/// <inheritdoc />
internal class GetArchiveUseCase : IGetArchiveUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public GetArchiveUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    /// <inheritdoc />
    public Task<Result<Archive>> GetArchiveAsync(Result<GetArchiveRequest> request) =>
        this.videoHttpClient.SendWithResponseAsync<Archive, GetArchiveRequest>(request, this.generateToken());
}