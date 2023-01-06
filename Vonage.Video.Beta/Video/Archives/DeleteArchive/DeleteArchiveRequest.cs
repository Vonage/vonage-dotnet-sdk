using System.Net.Http;
using System.Net.Http.Headers;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;

namespace Vonage.Video.Beta.Video.Archives.DeleteArchive;

/// <summary>
///     Represents a request to delete an archive.
/// </summary>
public readonly struct DeleteArchiveRequest : IVideoRequest
{
    private DeleteArchiveRequest(string applicationId, string archiveId)
    {
        this.ApplicationId = applicationId;
        this.ArchiveId = archiveId;
    }

    /// <summary>
    ///     The application Id.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///     The archive Id.
    /// </summary>
    public string ArchiveId { get; }

    /// <summary>
    ///     Parses the input into a DeleteArchiveRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="archiveId">The archive Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<DeleteArchiveRequest> Parse(string applicationId, string archiveId) =>
        Result<DeleteArchiveRequest>
            .FromSuccess(new DeleteArchiveRequest(applicationId, archiveId))
            .Bind(VerifyApplicationId)
            .Bind(VerifyArchiveId);

    /// <inheritdoc />
    public string GetEndpointPath() => $"/project/{this.ApplicationId}/archive/{this.ArchiveId}";

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        return httpRequest;
    }

    private static Result<DeleteArchiveRequest> VerifyApplicationId(DeleteArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<DeleteArchiveRequest> VerifyArchiveId(DeleteArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(ArchiveId));
}