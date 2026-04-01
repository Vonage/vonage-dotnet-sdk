#region
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Common.Serialization;

/// <summary>
///     Represents a custom converter from Email to Json.
/// </summary>
/// <example>
///     <code><![CDATA[
/// var options = new JsonSerializerOptions();
/// options.Converters.Add(new EmailJsonConverter());
///
/// // Serializes MailAddress to its string representation
/// var email = MailAddress.Parse("user@example.com").IfFailure(default(MailAddress));
/// string json = JsonSerializer.Serialize(email, options);
/// // Produces: "user@example.com"
/// ]]></code>
/// </example>
public class EmailJsonConverter : JsonConverter<MailAddress>
{
    /// <inheritdoc />
    public override MailAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        MailAddress.Parse(reader.GetString()).IfFailure(default(MailAddress));

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, MailAddress value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Address);
}