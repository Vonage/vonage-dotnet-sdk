#region
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Common.Serialization;

/// <summary>
///     Represents a custom converter from PhoneNumber to Json.
/// </summary>
/// <example>
///     <code><![CDATA[
/// // Register the converter in serializer options
/// var options = new JsonSerializerOptions();
/// options.Converters.Add(new PhoneNumberJsonConverter());
///
/// // The converter handles serialization/deserialization of PhoneNumber
/// string json = JsonSerializer.Serialize(new PhoneNumber("14155552671"), options);
/// // Produces: "14155552671"
/// ]]></code>
/// </example>
public class PhoneNumberJsonConverter : JsonConverter<PhoneNumber>
{
    /// <inheritdoc />
    public override PhoneNumber Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        PhoneNumber.Parse(reader.GetString()).IfFailure(default(PhoneNumber));

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, PhoneNumber value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Number);
}