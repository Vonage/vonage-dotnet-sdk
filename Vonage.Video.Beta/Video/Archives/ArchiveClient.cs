using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video.Archives.AddStream;
using Vonage.Video.Beta.Video.Archives.ChangeLayout;
using Vonage.Video.Beta.Video.Archives.Common;
using Vonage.Video.Beta.Video.Archives.CreateArchive;
using Vonage.Video.Beta.Video.Archives.DeleteArchive;
using Vonage.Video.Beta.Video.Archives.GetArchive;
using Vonage.Video.Beta.Video.Archives.GetArchives;
using Vonage.Video.Beta.Video.Archives.RemoveStream;
using Vonage.Video.Beta.Video.Archives.StopArchive;

namespace Vonage.Video.Beta.Video.Archives;

/// <inheritdoc />
public class ArchiveClient : IArchiveClient
{
    private readonly GetArchivesUseCase getArchivesUseCase;
    private readonly GetArchiveUseCase getArchiveUseCase;
    private readonly CreateArchiveUseCase createArchiveUseCase;
    private readonly DeleteArchiveUseCase deleteArchiveUseCase;
    private readonly StopArchiveUseCase stopArchiveUseCase;
    private readonly ChangeLayoutUseCase changeLayoutUseCase;
    private readonly AddStreamUseCase addStreamUseCase;
    private readonly RemoveStreamUseCase removeStreamUseCase;

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
        this.stopArchiveUseCase = new StopArchiveUseCase(new VideoHttpClient(httpClient), tokenGeneration);
        this.changeLayoutUseCase =
            new ChangeLayoutUseCase(new VideoHttpClient(httpClient), tokenGeneration);
        this.addStreamUseCase = new AddStreamUseCase(new VideoHttpClient(httpClient), tokenGeneration);
        this.removeStreamUseCase = new RemoveStreamUseCase(new VideoHttpClient(httpClient), tokenGeneration);
    }

    /// <inheritdoc />
    public Task<Result<GetArchivesResponse>> GetArchivesAsync(Result<GetArchivesRequest> request) =>
        this.getArchivesUseCase.GetArchivesAsync(request);

    /// <inheritdoc />
    public Task<Result<Archive>> GetArchiveAsync(Result<GetArchiveRequest> request) =>
        this.getArchiveUseCase.GetArchiveAsync(request);

    /// <inheritdoc />
    public Task<Result<Archive>> CreateArchiveAsync(Result<CreateArchiveRequest> request) =>
        this.createArchiveUseCase.CreateArchiveAsync(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteArchiveAsync(Result<DeleteArchiveRequest> request) =>
        this.deleteArchiveUseCase.DeleteArchiveAsync(request);

    /// <inheritdoc />
    public Task<Result<Archive>> StopArchiveAsync(Result<StopArchiveRequest> request) =>
        this.stopArchiveUseCase.StopArchiveAsync(request);

    /// <inheritdoc />
    public Task<Result<Unit>> ChangeLayoutAsync(Result<ChangeLayoutRequest> request) =>
        this.changeLayoutUseCase.ChangeLayoutAsync(request);

    /// <inheritdoc />
    public Task<Result<Unit>> AddStreamAsync(Result<AddStreamRequest> request) =>
        this.addStreamUseCase.AddStreamAsync(request);

    /// <inheritdoc />
    public Task<Result<Unit>> RemoveStreamAsync(Result<RemoveStreamRequest> request) =>
        this.removeStreamUseCase.RemoveStreamAsync(request);
}