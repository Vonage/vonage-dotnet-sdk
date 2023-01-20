using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Archives.RemoveStream;

/// <summary>
///     Represents a request to remove a stream from an archive.
/// </summary>
public readonly struct RemoveStreamRequest : IVonageRequest
{
    private RemoveStreamRequest(string applicationId, string archiveId, string streamId)
    {
        this.ApplicationId = applicationId;
        this.ArchiveId = archiveId;
        this.StreamId = streamId;
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
    ///     The stream Id.
    /// </summary>
    public string StreamId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token) =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
            .WithAuthorizationToken(token)
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}/streams";

    /// <summary>
    ///     Parses the input into a RemoveStreamRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="archiveId">The archive Id.</param>
    /// <param name="streamId">The stream Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<RemoveStreamRequest> Parse(string applicationId, string archiveId, string streamId) =>
        Result<RemoveStreamRequest>
            .FromSuccess(new RemoveStreamRequest(applicationId, archiveId, streamId))
            .Bind(VerifyApplicationId)
            .Bind(VerifyArchiveId)
            .Bind(VerifyStreamId);

    private StringContent GetRequestContent() =>
        new(
            JsonSerializerBuilder.Build().SerializeObject(new {RemoveStream = this.StreamId}), Encoding.UTF8,
            "application/json");

    private static Result<RemoveStreamRequest> VerifyApplicationId(RemoveStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<RemoveStreamRequest> VerifyArchiveId(RemoveStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(ArchiveId));

    private static Result<RemoveStreamRequest> VerifyStreamId(RemoveStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(StreamId));
}