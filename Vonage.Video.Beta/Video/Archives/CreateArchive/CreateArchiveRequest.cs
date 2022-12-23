﻿using System.Net.Http;
using System.Net.Http.Headers;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;

namespace Vonage.Video.Beta.Video.Archives.CreateArchive;

/// <summary>
///     Represents a request to creating an archive.
/// </summary>
public readonly struct CreateArchiveRequest : IVideoRequest
{
    private CreateArchiveRequest(string applicationId)
    {
        this.ApplicationId = applicationId;
    }

    /// <summary>
    ///     The application Id.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///     Parses the input into a CreateArchiveRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<CreateArchiveRequest> Parse(string applicationId) =>
        Result<CreateArchiveRequest>
            .FromSuccess(new CreateArchiveRequest(applicationId))
            .Bind(VerifyApplicationId);

    /// <inheritdoc />
    public string GetEndpointPath() => $"/project/{this.ApplicationId}/archive";

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        return httpRequest;
    }

    private static Result<CreateArchiveRequest> VerifyApplicationId(CreateArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));
}