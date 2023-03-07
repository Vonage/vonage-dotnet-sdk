using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a WhatsApp configuration.
/// </summary>
public class MessageWhatsApp
{
    /// <summary>
    ///     The BCP 47 language of the template. Vonage will translate the BCP 47 format to the WhatsApp equivalent. For
    ///     examples en-GB will be auto-translate to en_GB.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Locale { get; set; }

    /// <summary>
    ///     Policy for resolving what language template to use. Please note that WhatsApp deprecated the fallback policy in
    ///     January of 2020,
    ///     all requests carrying a fallback policy will be rejected with a 400 error. As of right now the only valid choice is
    ///     deterministic
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Policy { get; set; }
}