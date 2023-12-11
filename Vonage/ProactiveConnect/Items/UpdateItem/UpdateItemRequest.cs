using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Serialization;

namespace Vonage.ProactiveConnect.Items.UpdateItem;

/// <summary>
///     Represents a request to retrieve an item.
/// </summary>
public readonly struct UpdateItemRequest : IVonageRequest
{
    /// <summary>
    ///     Custom data for the item.
    /// </summary>
    public ReadOnlyDictionary<string, object> Data { get; internal init; }

    /// <summary>
    ///     Unique identifier for the item.
    /// </summary>
    [JsonIgnore]
    public Guid ItemId { get; internal init; }

    /// <summary>
    ///     Unique identifier for the list.
    /// </summary>
    [JsonIgnore]
    public Guid ListId { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForListId Build() => new UpdateItemRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Put, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v0.1/bulk/lists/{this.ListId}/items/{this.ItemId}";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}