#region
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using EnumsNET;
#endregion

namespace Vonage.Common.Serialization;

/// <summary>
///     Represents a custom converter from Enum description to Json.
/// </summary>
/// <typeparam name="T">Type of the enum.</typeparam>
/// <example>
///     <code><![CDATA[
/// // Given an enum with Description attributes:
/// // public enum Status
/// // {
/// //     [Description("pending")]
/// //     Pending,
/// //     [Description("completed")]
/// //     Completed
/// // }
///
/// var options = new JsonSerializerOptions();
/// options.Converters.Add(new EnumDescriptionJsonConverter<Status>());
///
/// string json = JsonSerializer.Serialize(Status.Completed, options);
/// // Produces: "completed" (uses the Description attribute value)
/// ]]></code>
/// </example>
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