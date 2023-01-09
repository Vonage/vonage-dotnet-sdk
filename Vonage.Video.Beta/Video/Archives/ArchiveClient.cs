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

/// <summary>
///     Represents a client exposing archiving features.
/// </summary>
public class ArchiveClient
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

    /// <summary>
    ///     Retrieves all archives from an application.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A success state with archives if the operation succeeded. A failure state with the error message if it failed.</returns>
    public Task<Result<GetArchivesResponse>> GetArchivesAsync(Result<GetArchivesRequest> request) =>
        this.getArchivesUseCase.GetArchivesAsync(request);

    /// <summary>
    ///     Return the archive information of a specific archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Archive>> GetArchiveAsync(Result<GetArchiveRequest> request) =>
        this.getArchiveUseCase.GetArchiveAsync(request);

    /// <summary>
    ///     Creates a new archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Archive>> CreateArchiveAsync(Result<CreateArchiveRequest> request) =>
        this.createArchiveUseCase.CreateArchiveAsync(request);

    /// <summary>
    ///     Deletes the specified archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Unit>> DeleteArchiveAsync(Result<DeleteArchiveRequest> request) =>
        this.deleteArchiveUseCase.DeleteArchiveAsync(request);

    /// <summary>
    ///     Stops an archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Archive>> StopArchiveAsync(Result<StopArchiveRequest> request) =>
        this.stopArchiveUseCase.StopArchiveAsync(request);

    /// <summary>
    ///     Changes the layout type of a composed archive while it is being recorded.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Unit>> ChangeLayoutAsync(Result<ChangeLayoutRequest> request) =>
        this.changeLayoutUseCase.ChangeLayoutAsync(request);

    /// <summary>
    ///     Adds the stream included in a composed archive that was started with the streamMode set to "manual".
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Unit>> AddStreamAsync(Result<AddStreamRequest> request) =>
        this.addStreamUseCase.AddStreamAsync(request);

    /// <summary>
    ///     Removes the stream included in a composed archive that was started with the streamMode set to "manual".
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Unit>> RemoveStreamAsync(Result<RemoveStreamRequest> request) =>
        this.removeStreamUseCase.RemoveStreamAsync(request);
}