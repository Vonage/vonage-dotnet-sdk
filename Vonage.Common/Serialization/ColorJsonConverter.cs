using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vonage.Common.Serialization;

/// <summary>
///     Represents a custom converter from Color to Json.
/// </summary>
public class ColorJsonConverter : JsonConverter<Color>
{
    /// <inheritdoc />
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value != null
            ? Color.FromName(value)
            : default;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Name);
}