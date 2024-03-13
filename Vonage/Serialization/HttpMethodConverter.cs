using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace Vonage.Serialization;

/// <summary>
///     Converter from HttpMethod to Json.
/// </summary>
public class HttpMethodConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType) => objectType == typeof(HttpMethod);

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return new HttpMethod((string) reader.Value);
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is null || !this.CanConvert(value.GetType()))
            writer.WriteNull();
        else
            writer.WriteValue(((HttpMethod) value).Method);
    }
}