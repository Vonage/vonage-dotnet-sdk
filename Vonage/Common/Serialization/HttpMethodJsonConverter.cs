#region
using System;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json;
#endregion

namespace Vonage.Common.Serialization;

/// <summary>
///     Represents a custom converter from HttpMethod to Json for System.Text.Json.
/// </summary>
/// <example>
///     <code><![CDATA[
/// var options = new JsonSerializerOptions();
/// options.Converters.Add(new HttpMethodJsonConverter());
///
/// string json = JsonSerializer.Serialize(HttpMethod.Post, options);
/// // Produces: "POST"
/// ]]></code>
/// </example>
public class HttpMethodJsonConverter : System.Text.Json.Serialization.JsonConverter<HttpMethod>
{
    /// <inheritdoc />
    public override HttpMethod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        new HttpMethod(reader.GetString());

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, HttpMethod value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Method);
}

/// <summary>
///     Represents a custom converter from HttpMethod to Json for Newtonsoft.Json.
/// </summary>
/// <example>
///     <code><![CDATA[
/// var settings = new JsonSerializerSettings();
/// settings.Converters.Add(new NewtonsoftHttpMethodConverter());
///
/// string json = JsonConvert.SerializeObject(HttpMethod.Get, settings);
/// // Produces: "GET"
/// ]]></code>
/// </example>
public class NewtonsoftHttpMethodConverter : Newtonsoft.Json.JsonConverter<HttpMethod>
{
    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, HttpMethod value, Newtonsoft.Json.JsonSerializer serializer)
    {
        writer.WriteValue(value.Method);
    }

    /// <inheritdoc />
    public override HttpMethod ReadJson(JsonReader reader, Type objectType, HttpMethod existingValue,
        bool hasExistingValue,
        Newtonsoft.Json.JsonSerializer serializer) => new HttpMethod(reader.Value.ToString());
}