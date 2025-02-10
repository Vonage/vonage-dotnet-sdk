using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Handy.DotNETCoreCompatibility.ColourTranslations;
using Vonage.Common.Monads;

namespace Vonage.Common.Serialization;

/// <summary>
///     Represents a custom converter from Color to Json.
/// </summary>
public class ColorJsonConverter : JsonConverter<Color>
{
    /// <inheritdoc />
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        GetValue(reader)
            .Map(TranslateToArgb)
            .Map(TranslateToColor)
            .IfNone(default(Color));

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) =>
        writer.WriteStringValue(ToHex(value));

    private static Maybe<string> GetValue(Utf8JsonReader reader) => reader.GetString() ?? Maybe<string>.None;

    private static string ToHex(Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";

    private static ARGB TranslateToArgb(string value) => HTMLColorTranslator.Translate(value);

    private static Color TranslateToColor(ARGB value) => Color.FromArgb(value.A, value.R, value.G, value.B);
}