using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;

namespace Vonage.Video.Archives.DeleteArchive;

/// <summary>
///     Represents a request to delete an archive.
/// </summary>
public readonly struct DeleteArchiveRequest : IVonageRequest, IHasApplicationId, IHasArchiveId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; private init; }

    /// <inheritdoc />
    public Guid ArchiveId { get; private init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static ArchiveRequestBuilder<DeleteArchiveRequest>.IBuilderForApplicationId Build() =>
        ArchiveRequestBuilder<DeleteArchiveRequest>.Build(tuple => new DeleteArchiveRequest
        {
            ApplicationId = tuple.Item1,
            ArchiveId = tuple.Item2,
        });

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Delete, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}";
}