using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Archives.RemoveStream;

/// <summary>
///     Represents a request to remove a stream from an archive.
/// </summary>
public readonly struct RemoveStreamRequest : IVonageRequest, IHasApplicationId, IHasArchiveId, IHasStreamId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; private init; }

    /// <inheritdoc />
    public Guid ArchiveId { get; private init; }

    /// <inheritdoc />
    public Guid StreamId { get; private init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static StreamRequestBuilder<RemoveStreamRequest>.IBuilderForApplicationId Build() =>
        StreamRequestBuilder<RemoveStreamRequest>.Build(tuple => new RemoveStreamRequest
        {
            ApplicationId = tuple.Item1,
            ArchiveId = tuple.Item2,
            StreamId = tuple.Item3,
        });

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