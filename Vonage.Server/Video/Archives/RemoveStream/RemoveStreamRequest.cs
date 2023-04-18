using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Archives.RemoveStream;

/// <summary>
///     Represents a request to remove a stream from an archive.
/// </summary>
public readonly struct RemoveStreamRequest : IVonageRequest
{
    private RemoveStreamRequest(Guid applicationId, Guid archiveId, Guid streamId)
    {
        this.ApplicationId = applicationId;
        this.ArchiveId = archiveId;
        this.StreamId = streamId;
    }

    /// <summary>
    ///     The application Id.
    /// </summary>
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The archive Id.
    /// </summary>
    public Guid ArchiveId { get; internal init; }

    /// <summary>
    ///     The stream Id.
    /// </summary>
    public Guid StreamId { get; internal init; }

    /// <summary>
    /// Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new RemoveStreamRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}/streams";

    private StringContent GetRequestContent() =>
        new(
            JsonSerializerBuilder.Build().SerializeObject(new {RemoveStream = this.StreamId}), Encoding.UTF8,
            "application/json");
}