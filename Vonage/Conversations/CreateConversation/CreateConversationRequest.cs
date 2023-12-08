using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.Conversations.CreateConversation;

/// <inheritdoc />
public readonly struct CreateConversationRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<Callback>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Callback> Callback { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> DisplayName { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Uri> ImageUrl { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<IEnumerable<INumber>>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<IEnumerable<INumber>> Numbers { get; internal init; }

    /// <summary>
    /// </summary>
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

    private StringContent GetRequestContent() => new(JsonSerializer.BuildWithSnakeCase().SerializeObject(this),
        Encoding.UTF8, "application/json");
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
public record PhoneNumber(string Number) : INumber
{
    /// <inheritdoc />
    public string Type => "phone";

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}

/// <summary>
/// </summary>
/// <param name="Uri"></param>
/// <param name="Username"></param>
/// <param name="Password"></param>
public record SipNumber(string Uri, string Username, string Password) : INumber
{
    /// <inheritdoc />
    public string Type => "sip";

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}

/// <summary>
/// </summary>
/// <param name="User"></param>
public record AppNumber(string User) : INumber
{
    /// <inheritdoc />
    public string Type => "app";

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}

/// <summary>
/// </summary>
/// <param name="Uri"></param>
/// <param name="ContentType"></param>
public record WebSocketNumber(string Uri, string ContentType) : INumber
{
    /// <inheritdoc />
    public string Type => "websocket";

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}

/// <summary>
/// </summary>
/// <param name="Extension"></param>
public record VbcNumber(string Extension) : INumber
{
    /// <inheritdoc />
    public string Type => "vbc";

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}