using Newtonsoft.Json;

namespace Vonage.Verify;

/// <summary>
///     Base class for verification requests, containing common properties shared by <see cref="VerifyRequest"/> and <see cref="Psd2Request"/>.
/// </summary>
public class VerifyRequestBase
{
    /// <summary>
    ///     The mobile or landline phone number to verify. Unless you are setting country explicitly, this number must be in E.164 format (e.g., "447700900000").
    /// </summary>
    [JsonProperty("number")]
    public string Number { get; set; }

    /// <summary>
    ///     The two-character country code (ISO 3166-1 alpha-2) to use for formatting the phone number. If you do not provide number in international format, specify this to let Vonage format the number correctly.
    /// </summary>
    [JsonProperty("country")]
    public string Country { get; set; }

    /// <summary>
    ///     The length of the verification code to generate. Must be either 4 or 6 digits.
    /// </summary>
    [JsonProperty("code_length")]
    public int CodeLength { get; set; }

    /// <summary>
    ///     The language code (BCP-47 format) for the SMS or text-to-speech message. By default, the language matches the phone number's country. Examples: "en-us", "es-es", "fr-fr".
    /// </summary>
    [JsonProperty("lg")]
    public string Lg { get; set; }

    /// <summary>
    ///     How long the generated verification code is valid for, in seconds. Must be an integer multiple of <see cref="NextEventWait"/>. If not, it defaults to equal <see cref="NextEventWait"/>.
    /// </summary>
    [JsonProperty("pin_expiry")]
    public int PinExpiry { get; set; }

    /// <summary>
    ///     The wait time in seconds between attempts to deliver the verification code. Must be between 60 and 900 seconds.
    /// </summary>
    [JsonProperty("next_event_wait")]
    public int NextEventWait { get; set; }
}