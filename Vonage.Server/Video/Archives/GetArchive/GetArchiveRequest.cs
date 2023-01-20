using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Server.Video.Archives.GetArchive;

/// <summary>
///     Represents a request to retrieve an archive.
/// </summary>
public readonly struct GetArchiveRequest : IVonageRequest
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

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token) =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .WithAuthorizationToken(token)
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}";

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

    private static Result<GetArchiveRequest> VerifyApplicationId(GetArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<GetArchiveRequest> VerifyArchiveId(GetArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(ArchiveId));
}