using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Serialization;

namespace Vonage.Video.Archives.AddStream;

/// <summary>
///     Represents a request to add a stream to an archive.
/// </summary>
public readonly struct AddStreamRequest : IVonageRequest, IHasApplicationId, IHasArchiveId, IHasStreamId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    public Guid ArchiveId { get; internal init; }

    /// <summary>
    ///     Whether the composed archive should include the stream's audio (true, the default) or not (false).
    /// </summary>
    public bool HasAudio { get; internal init; }

    /// <summary>
    ///     Whether the composed archive should include the stream's video (true, the default) or not (false).
    /// </summary>
    public bool HasVideo { get; internal init; }

    /// <inheritdoc />
    public Guid StreamId { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new AddStreamRequestBuilder();

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
            JsonSerializerBuilder.Build()
                .SerializeObject(new {AddStream = this.StreamId, this.HasAudio, this.HasVideo}),
            Encoding.UTF8,
            "application/json");
}