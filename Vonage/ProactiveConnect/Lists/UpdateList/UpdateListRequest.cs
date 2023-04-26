using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.ProactiveConnect.Lists.UpdateList;

/// <summary>
///     Represents a request to update a list.
/// </summary>
public readonly struct UpdateListRequest : IVonageRequest
{
    /// <summary>
    ///     Attributes of the list.
    /// </summary>
    [JsonPropertyOrder(4)]
    [JsonConverter(typeof(MaybeJsonConverter<IEnumerable<ListAttribute>>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<IEnumerable<ListAttribute>> Attributes { get; internal init; }

    /// <summary>
    ///     The data source.
    /// </summary>
    [JsonPropertyOrder(5)]
    [JsonPropertyName("datasource")]
    [JsonConverter(typeof(MaybeJsonConverter<ListDataSource>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<ListDataSource> DataSource { get; internal init; }

    /// <summary>
    ///     The description of the resource.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Description { get; internal init; }

    /// <summary>
    ///     Unique identifier for a list.
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; init; }

    /// <summary>
    ///     The name of the resource.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Name { get; internal init; }

    /// <summary>
    ///     Custom strings assigned with a resource - the request allows up to 10 tags, each must be between 1 and 15
    ///     characters.
    /// </summary>
    [JsonPropertyOrder(3)]
    [JsonConverter(typeof(MaybeJsonConverter<IEnumerable<string>>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<IEnumerable<string>> Tags { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForListId Build() => new UpdateListRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Put, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v0.1/bulk/lists/{this.Id}";

    private StringContent GetRequestContent() =>
        new(JsonSerializer.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}