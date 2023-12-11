using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Serialization;

namespace Vonage.ProactiveConnect.Items.CreateItem;

/// <summary>
///     Represents a request to create an item.
/// </summary>
public readonly struct CreateItemRequest : IVonageRequest
{
    /// <summary>
    ///     Custom data for the item.
    /// </summary>
    public ReadOnlyDictionary<string, object> Data { get; internal init; }

    /// <summary>
    ///     Unique identifier for a list.
    /// </summary>
    [JsonIgnore]
    public Guid ListId { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForListId Build() => new CreateItemRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v0.1/bulk/lists/{this.ListId}/items";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}