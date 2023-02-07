using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Archives.StopArchive;

/// <summary>
///     Represents a request to stop an archive.
/// </summary>
public readonly struct StopArchiveRequest : IVonageRequest
{
    private StopArchiveRequest(Guid applicationId, Guid archiveId)
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
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}/stop";

    /// <summary>
    ///     Parses the input into a StopArchiveRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="archiveId">The archive Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<StopArchiveRequest> Parse(Guid applicationId, Guid archiveId) =>
        Result<StopArchiveRequest>
            .FromSuccess(new StopArchiveRequest(applicationId, archiveId))
            .Bind(VerifyApplicationId)
            .Bind(VerifyArchiveId);

    private static Result<StopArchiveRequest> VerifyApplicationId(StopArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<StopArchiveRequest> VerifyArchiveId(StopArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(ArchiveId));
}