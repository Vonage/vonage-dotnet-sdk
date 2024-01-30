using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vonage.Common.Serialization;

/// <summary>
///     Represents a custom converter from HttpMethod to Json.
/// </summary>
public class HttpMethodJsonConverter : JsonConverter<HttpMethod>
{
    /// <inheritdoc />
    public override HttpMethod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        new(reader.GetString());

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, HttpMethod value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Method);
}