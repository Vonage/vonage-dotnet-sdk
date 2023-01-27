using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Archives.AddStream;
using Vonage.Server.Video.Archives.ChangeLayout;
using Vonage.Server.Video.Archives.Common;
using Vonage.Server.Video.Archives.CreateArchive;
using Vonage.Server.Video.Archives.DeleteArchive;
using Vonage.Server.Video.Archives.GetArchive;
using Vonage.Server.Video.Archives.GetArchives;
using Vonage.Server.Video.Archives.RemoveStream;
using Vonage.Server.Video.Archives.StopArchive;

namespace Vonage.Server.Video.Archives;

/// <summary>
///     Represents a client exposing archiving features.
/// </summary>
public class ArchiveClient
{
    private readonly AddStreamUseCase addStreamUseCase;
    private readonly ChangeLayoutUseCase changeLayoutUseCase;
    private readonly CreateArchiveUseCase createArchiveUseCase;
    private readonly DeleteArchiveUseCase deleteArchiveUseCase;
    private readonly GetArchivesUseCase getArchivesUseCase;
    private readonly GetArchiveUseCase getArchiveUseCase;
    private readonly RemoveStreamUseCase removeStreamUseCase;
    private readonly StopArchiveUseCase stopArchiveUseCase;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    public ArchiveClient(HttpClient httpClient, Func<string> tokenGeneration)
    {
        var client = new VonageHttpClient(httpClient, JsonSerializerBuilder.Build(), tokenGeneration);
        this.getArchivesUseCase = new GetArchivesUseCase(client, tokenGeneration);
        this.getArchiveUseCase = new GetArchiveUseCase(client, tokenGeneration);
        this.createArchiveUseCase = new CreateArchiveUseCase(client, tokenGeneration);
        this.deleteArchiveUseCase = new DeleteArchiveUseCase(client, tokenGeneration);
        this.stopArchiveUseCase = new StopArchiveUseCase(client, tokenGeneration);
        this.changeLayoutUseCase = new ChangeLayoutUseCase(client, tokenGeneration);
        this.addStreamUseCase = new AddStreamUseCase(client, tokenGeneration);
        this.removeStreamUseCase = new RemoveStreamUseCase(client, tokenGeneration);
    }

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
    ///     Retrieves all archives from an application.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A success state with archives if the operation succeeded. A failure state with the error message if it failed.</returns>
    public Task<Result<GetArchivesResponse>> GetArchivesAsync(Result<GetArchivesRequest> request) =>
        this.getArchivesUseCase.GetArchivesAsync(request);

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
}