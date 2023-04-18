using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Archives.StopArchive;

/// <summary>
///     Represents a request to stop an archive.
/// </summary>
public readonly struct StopArchiveRequest : IVonageRequest, IHasApplicationId, IHasArchiveId
{
    private StopArchiveRequest(Guid applicationId, Guid archiveId)
    {
        this.ApplicationId = applicationId;
        this.ArchiveId = archiveId;
    }

    /// <inheritdoc />
    public Guid ApplicationId { get; }

    /// <inheritdoc />
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
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(BuilderExtensions.VerifyArchiveId);
}