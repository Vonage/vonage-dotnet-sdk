﻿using System.Net.Http;
using System.Net.Http.Headers;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;

namespace Vonage.Video.Beta.Video.Archives.GetArchive;

/// <summary>
///     Represents a request to retrieve an archive.
/// </summary>
public readonly struct GetArchiveRequest : IVideoRequest
{
    private GetArchiveRequest(string applicationId, string archiveId)
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
    ///     Parses the input into a GetStreamRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="archiveId">The archive Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetArchiveRequest> Parse(string applicationId, string archiveId) =>
        Result<GetArchiveRequest>
            .FromSuccess(new GetArchiveRequest(applicationId, archiveId))
            .Bind(VerifyApplicationId)
            .Bind(VerifyArchiveId);

    /// <inheritdoc />
    public string GetEndpointPath() => $"/project/{this.ApplicationId}/archive/{this.ArchiveId}";

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        return httpRequest;
    }

    private static Result<GetArchiveRequest> VerifyApplicationId(GetArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<GetArchiveRequest> VerifyArchiveId(GetArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(ArchiveId));
}