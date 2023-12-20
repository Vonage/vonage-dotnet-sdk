using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vonage.Common.Serialization;

/// <summary>
///     Represents a custom converter from PhoneNumber to Json.
/// </summary>
public class PhoneNumberJsonConverter : JsonConverter<PhoneNumber>
{
    /// <inheritdoc />
    public override PhoneNumber Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        PhoneNumber.Parse(reader.GetString()).IfFailure(default(PhoneNumber));

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, PhoneNumber value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Number);
}