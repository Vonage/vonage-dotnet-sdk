using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Archives.DeleteArchive;

/// <summary>
///     Represents a request to delete an archive.
/// </summary>
public readonly struct DeleteArchiveRequest : IVonageRequest
{
    private DeleteArchiveRequest(Guid applicationId, Guid archiveId)
    {
        this.ApplicationId = applicationId;
        this.ArchiveId = archiveId;
    }

    /// <summary>
    ///     The application Id.
    /// </summary>
    public Guid ApplicationId { get; }

    /// <summary>
    ///     The archive Id.
    /// </summary>
    public Guid ArchiveId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Delete, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}";

    /// <summary>
    ///     Parses the input into a DeleteArchiveRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="archiveId">The archive Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<DeleteArchiveRequest> Parse(Guid applicationId, Guid archiveId) =>
        Result<DeleteArchiveRequest>
            .FromSuccess(new DeleteArchiveRequest(applicationId, archiveId))
            .Bind(VerifyApplicationId)
            .Bind(VerifyArchiveId);

    private static Result<DeleteArchiveRequest> VerifyApplicationId(DeleteArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<DeleteArchiveRequest> VerifyArchiveId(DeleteArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(ArchiveId));
}