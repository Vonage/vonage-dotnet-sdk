using System.Text.Json.Serialization;
using Vonage.Common;

namespace Vonage.Conversations;

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