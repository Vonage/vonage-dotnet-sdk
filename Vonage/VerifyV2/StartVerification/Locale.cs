using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vonage.VerifyV2.StartVerification;

/// <summary>
///     Represents a language for verification requests.
/// </summary>
public readonly struct Locale
{
    /// <summary>
    ///     The de-de locale.
    /// </summary>
    public static Locale DeDe => new("de-de");

    /// <summary>
    ///     The en-gb locale.
    /// </summary>
    public static Locale EnGb => new("en-gb");

    /// <summary>
    ///     The en-us locale.
    /// </summary>
    public static Locale EnUs => new("en-us");

    /// <summary>
    ///     The es-es locale.
    /// </summary>
    public static Locale EsEs => new("es-es");

    /// <summary>
    ///     The es-mx locale.
    /// </summary>
    public static Locale EsMx => new("es-mx");

    /// <summary>
    ///     The es-us locale.
    /// </summary>
    public static Locale EsUs => new("es-us");

    /// <summary>
    ///     The fr-fr locale.
    /// </summary>
    public static Locale FrFr => new("fr-fr");

    /// <summary>
    ///     The hi-in locale.
    /// </summary>
    public static Locale HiIn => new("hi-in");

    /// <summary>
    ///     The id-id locale.
    /// </summary>
    public static Locale IdId => new("id-id");

    /// <summary>
    ///     The it-it locale.
    /// </summary>
    public static Locale ItIt => new("it-it");

    /// <summary>
    ///     The ja-jp locale.
    /// </summary>
    public static Locale JaJp => new("ja-jp");

    /// <summary>
    ///     The language.
    /// </summary>
    public string Language { get; }

    /// <summary>
    ///     The pt-br locale.
    /// </summary>
    public static Locale PtBr => new("pt-br");

    /// <summary>
    ///     The pt-pt locale.
    /// </summary>
    public static Locale PtPt => new("pt-pt");

    /// <summary>
    ///     The ru-ru locale.
    /// </summary>
    public static Locale RuRu => new("ru-ru");

    internal Locale(string language) => this.Language = language;

    /// <summary>
    ///     Implicit conversion from string to Locale.
    /// </summary>
    /// <param name="value">The locale value.</param>
    /// <returns>The locale.</returns>
    public static implicit operator Locale(string value) => new(value);

    /// <summary>
    ///     Implicit conversion from Locale to string.
    /// </summary>
    /// <param name="value">The locale.</param>
    /// <returns>The locale value.</returns>
    public static implicit operator string(Locale value) => value.Language;
}

/// <summary>
///     Represents a custom converter from Locale description to Json.
/// </summary>
public class LocaleJsonConverter : JsonConverter<Locale>
{
    /// <inheritdoc />
    public override Locale Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value != null
            ? new Locale(value)
            : default;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Locale value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Language);
}