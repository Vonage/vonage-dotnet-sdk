using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using EnumsNET;

namespace Vonage.Video.Beta.Video.Archives.Common;

/// <summary>
///     Represents a custom converter from RenderResolution to Json.
/// </summary>
public class RenderResolutionConverter : JsonConverter<RenderResolution>
{
    /// <inheritdoc />
    public override bool HandleNull => true;

    public override RenderResolution Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value != null
            ? Enums.Parse<RenderResolution>(value, false, EnumFormat.Description)
            : default;
    }

    public override void Write(Utf8JsonWriter writer, RenderResolution value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.AsString(EnumFormat.Description));
}