using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Serialization;
using Vonage.Server;

namespace Vonage.Video.Archives.ChangeLayout;

/// <summary>
///     Represents a request to change the layout of an archive.
/// </summary>
public readonly struct ChangeLayoutRequest : IVonageRequest, IHasApplicationId, IHasArchiveId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    public Guid ArchiveId { get; internal init; }

    /// <summary>
    ///     The layout to apply of the archive.
    /// </summary>
    public Layout Layout { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new ChangeLayoutRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Put, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}/layout";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this.Layout),
            Encoding.UTF8,
            "application/json");
}