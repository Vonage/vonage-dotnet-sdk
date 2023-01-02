using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using EnumsNET;

namespace Vonage.Video.Beta.Video.Archives.Common;

/// <summary>
///     Represents a custom converter from LayoutType to Json.
/// </summary>
public class LayoutTypeConverter : JsonConverter<LayoutType>
{
    /// <inheritdoc />
    public override bool HandleNull => true;

    /// <inheritdoc />
    public override LayoutType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value != null
            ? Enums.Parse<LayoutType>(value, false, EnumFormat.Description)
            : default;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, LayoutType value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.AsString(EnumFormat.Description));
}