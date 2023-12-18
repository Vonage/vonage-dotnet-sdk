using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;

namespace Vonage.Conversations.CreateConversation;

/// <inheritdoc />
public readonly struct CreateConversationRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    [JsonPropertyName("callback")]
    [JsonPropertyOrder(5)]
    [JsonConverter(typeof(MaybeJsonConverter<Callback>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Callback> Callback { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("display_name")]
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> DisplayName { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("image_url")]
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Uri> ImageUrl { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("name")]
    [JsonPropertyOrder(0)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("numbers")]
    [JsonPropertyOrder(4)]
    [JsonConverter(typeof(MaybeJsonConverter<IEnumerable<INumber>>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<IEnumerable<INumber>> Numbers { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("properties")]
    [JsonPropertyOrder(3)]
    [JsonConverter(typeof(MaybeJsonConverter<Properties>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Properties> Properties { get; internal init; }

    /// <summary>
    ///     Initializes a builder for CreateConversationRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForOptional Build() => new CreateConversationRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/v1/conversations";

    private StringContent GetRequestContent()
    {
        var serializer = JsonSerializerBuilder.BuildWithSnakeCase();
        var values = new Dictionary<string, object>();
        this.Name.IfSome(value => values.Add("name", value));
        this.DisplayName.IfSome(value => values.Add("display_name", value));
        this.ImageUrl.IfSome(value => values.Add("image_url", value));
        this.Properties.IfSome(value => values.Add("properties", value));
        this.Numbers.IfSome(value => values.Add("numbers", value
            .Select(workflow => workflow.Serialize(serializer))
            .Select(serializedString => serializer.DeserializeObject<dynamic>(serializedString))
            .Select(result => result.IfFailure(default))));
        this.Callback.IfSome(value => values.Add("callback", value));
        return new StringContent(serializer.SerializeObject(values), Encoding.UTF8, "application/json");
    }
}

/// <summary>
///     Represents a contract for a channel number.
/// </summary>
public interface INumber
{
    /// <summary>
    ///     The number type.
    /// </summary>
    string Type { get; }

    /// <summary>
    ///     Serializes the number.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    /// <returns>The serialized number.</returns>
    string Serialize(IJsonSerializer serializer);
}

/// <summary>
/// </summary>
/// <param name="Number"></param>
public record PhoneNumber(
    [property: JsonPropertyName("number")]
    [property: JsonPropertyOrder(1)]
    string Number) : INumber
{
    /// <inheritdoc />
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public string Type => "phone";

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}

/// <summary>
/// </summary>
/// <param name="Uri"></param>
/// <param name="Username"></param>
/// <param name="Password"></param>
public record SipNumber(
    [property: JsonPropertyName("uri")]
    [property: JsonPropertyOrder(1)]
    string Uri,
    [property: JsonPropertyName("username")]
    [property: JsonPropertyOrder(2)]
    string Username,
    [property: JsonPropertyName("password")]
    [property: JsonPropertyOrder(3)]
    string Password) : INumber
{
    /// <inheritdoc />
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public string Type => "sip";

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}

/// <summary>
/// </summary>
/// <param name="User"></param>
public record AppNumber(
    [property: JsonPropertyName("user")]
    [property: JsonPropertyOrder(1)]
    string User) : INumber
{
    /// <inheritdoc />
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public string Type => "app";

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}

/// <summary>
/// </summary>
/// <param name="Uri"></param>
/// <param name="ContentType"></param>
public record WebSocketNumber(
    [property: JsonPropertyName("uri")]
    [property: JsonPropertyOrder(1)]
    string Uri,
    [property: JsonPropertyName("content_type")]
    [property: JsonPropertyOrder(2)]
    string ContentType) : INumber
{
    /// <inheritdoc />
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public string Type => "websocket";

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}

/// <summary>
/// </summary>
/// <param name="Extension"></param>
public record VbcNumber(
    [property: JsonPropertyName("extension")]
    [property: JsonPropertyOrder(1)]
    string Extension) : INumber
{
    /// <inheritdoc />
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public string Type => "vbc";

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}