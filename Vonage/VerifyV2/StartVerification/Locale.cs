using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vonage.VerifyV2.StartVerification;

/// <summary>
///     Represents a language locale for verification request messages. The locale determines the language used in SMS and voice messages. Has no effect on Silent Auth channels.
/// </summary>
public readonly struct Locale
{
    /// <summary>
    ///     German (Germany) locale.
    /// </summary>
    public static Locale DeDe => new("de-de");

    /// <summary>
    ///     English (United Kingdom) locale.
    /// </summary>
    public static Locale EnGb => new("en-gb");

    /// <summary>
    ///     English (United States) locale. This is the default locale.
    /// </summary>
    public static Locale EnUs => new("en-us");

    /// <summary>
    ///     Spanish (Spain) locale.
    /// </summary>
    public static Locale EsEs => new("es-es");

    /// <summary>
    ///     Spanish (Mexico) locale.
    /// </summary>
    public static Locale EsMx => new("es-mx");

    /// <summary>
    ///     Spanish (United States) locale.
    /// </summary>
    public static Locale EsUs => new("es-us");

    /// <summary>
    ///     French (France) locale.
    /// </summary>
    public static Locale FrFr => new("fr-fr");

    /// <summary>
    ///     Hindi (India) locale.
    /// </summary>
    public static Locale HiIn => new("hi-in");

    /// <summary>
    ///     Indonesian (Indonesia) locale.
    /// </summary>
    public static Locale IdId => new("id-id");

    /// <summary>
    ///     Italian (Italy) locale.
    /// </summary>
    public static Locale ItIt => new("it-it");

    /// <summary>
    ///     Japanese (Japan) locale.
    /// </summary>
    public static Locale JaJp => new("ja-jp");

    /// <summary>
    ///     The IETF BCP 47 language tag (e.g., "en-us", "de-de").
    /// </summary>
    public string Language { get; }

    /// <summary>
    ///     Portuguese (Brazil) locale.
    /// </summary>
    public static Locale PtBr => new("pt-br");

    /// <summary>
    ///     Portuguese (Portugal) locale.
    /// </summary>
    public static Locale PtPt => new("pt-pt");

    /// <summary>
    ///     Russian (Russia) locale.
    /// </summary>
    public static Locale RuRu => new("ru-ru");

    internal Locale(string language) => this.Language = language;

    /// <summary>
    ///     Creates a Locale from a string value. Use this for locales not predefined as static properties.
    /// </summary>
    /// <param name="value">The locale value in IETF BCP 47 format (e.g., "ko-kr" for Korean).</param>
    /// <returns>A Locale instance.</returns>
    public static implicit operator Locale(string value) => new(value);

    /// <summary>
    ///     Converts the Locale to its string representation.
    /// </summary>
    /// <param name="value">The locale.</param>
    /// <returns>The IETF BCP 47 language tag.</returns>
    public static implicit operator string(Locale value) => value.Language;
}

/// <summary>
///     Represents a custom converter from Locale description to Json.
/// </summary>
public class LocaleJsonConverter : JsonConverter<Locale>
{
    /// <inheritdoc />
    public override Locale Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        new(reader.GetString());

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Locale value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Language);
}