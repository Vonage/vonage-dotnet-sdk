using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using EnumsNET;

namespace Vonage.Server.Common.Serialization;

/// <summary>
///     Represents a custom converter from Enum description to Json.
/// </summary>
/// <typeparam name="T">Type of the enum.</typeparam>
public class EnumDescriptionJsonConverter<T> : JsonConverter<T> where T : struct, Enum
{
    /// <inheritdoc />
    public override bool HandleNull => true;

    /// <inheritdoc />
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value != null
            ? Enums.Parse<T>(value, false, EnumFormat.Description)
            : default;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.AsString(EnumFormat.Description));
}