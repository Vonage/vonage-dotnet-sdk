using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vonage.Common.Serialization;

/// <summary>
///     Represents a custom converter from Email to Json.
/// </summary>
public class EmailJsonConverter : JsonConverter<MailAddress>
{
    /// <inheritdoc />
    public override MailAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        MailAddress.Parse(reader.GetString()).IfFailure(default(MailAddress));

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, MailAddress value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Address);
}