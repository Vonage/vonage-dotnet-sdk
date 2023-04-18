using System;
using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.Server.Video.Archives.GetArchive;

/// <summary>
///     Represents a request to retrieve an archive.
/// </summary>
public readonly struct GetArchiveRequest : IVonageRequest
{
    /// <summary>
    ///     The application Id.
    /// </summary>
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The archive Id.
    /// </summary>
    public Guid ArchiveId { get; internal init; }

    /// <summary>
    /// Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new GetArchiveRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}";
}