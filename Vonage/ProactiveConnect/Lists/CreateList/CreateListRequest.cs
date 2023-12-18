using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;

namespace Vonage.ProactiveConnect.Lists.CreateList;

/// <summary>
///     Represents a request to create a list.
/// </summary>
public readonly struct CreateListRequest : IVonageRequest
{
    /// <summary>
    ///     Attributes of the list.
    /// </summary>
    [JsonPropertyOrder(4)]
    public IEnumerable<ListAttribute> Attributes { get; internal init; }

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
    ///     The name of the resource.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Name { get; internal init; }

    /// <summary>
    ///     Custom strings assigned with a resource - the request allows up to 10 tags, each must be between 1 and 15
    ///     characters.
    /// </summary>
    [JsonPropertyOrder(3)]
    public IEnumerable<string> Tags { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForName Build() => new CreateListRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/v0.1/bulk/lists";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}