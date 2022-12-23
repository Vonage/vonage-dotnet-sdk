using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video.Archives.CreateArchive;
using Vonage.Video.Beta.Video.Archives.DeleteArchive;
using Vonage.Video.Beta.Video.Archives.GetArchive;
using Vonage.Video.Beta.Video.Archives.GetArchives;

namespace Vonage.Video.Beta.Video.Archives;

/// <inheritdoc />
public class ArchiveClient : IArchiveClient
{
    private readonly GetArchivesUseCase getArchivesUseCase;
    private readonly GetArchiveUseCase getArchiveUseCase;
    private readonly CreateArchiveUseCase createArchiveUseCase;
    private readonly DeleteArchiveUseCase deleteArchiveUseCase;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    public ArchiveClient(HttpClient httpClient, Func<string> tokenGeneration)
    {
        this.getArchivesUseCase = new GetArchivesUseCase(new VideoHttpClient(httpClient), tokenGeneration);
        this.getArchiveUseCase = new GetArchiveUseCase(new VideoHttpClient(httpClient), tokenGeneration);
        this.createArchiveUseCase = new CreateArchiveUseCase(new VideoHttpClient(httpClient), tokenGeneration);
        this.deleteArchiveUseCase = new DeleteArchiveUseCase(new VideoHttpClient(httpClient), tokenGeneration);
    }

    /// <inheritdoc />
    public Task<Result<GetArchivesResponse>> GetArchivesAsync(GetArchivesRequest request) =>
        this.getArchivesUseCase.GetArchivesAsync(request);

    /// <inheritdoc />
    public Task<Result<Archive>> GetArchiveAsync(GetArchiveRequest request) =>
        this.getArchiveUseCase.GetArchiveAsync(request);

    /// <inheritdoc />
    public Task<Result<Archive>> CreateArchiveAsync(CreateArchiveRequest request) =>
        this.createArchiveUseCase.CreateArchiveAsync(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteArchiveAsync(DeleteArchiveRequest request) =>
        this.deleteArchiveUseCase.DeleteArchiveAsync(request);
}