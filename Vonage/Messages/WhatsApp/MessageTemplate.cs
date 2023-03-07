using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp;

/// <summary>
///     Represents a message template.
/// </summary>
public class MessageTemplate
{
    /// <summary>
    ///     The name of the template. For WhatsApp use your WhatsApp namespace (available via Facebook Business Manager),
    ///     followed by a colon : and the name of the template to use.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Name { get; set; }

    /// <summary>
    ///     The parameters are an array of strings. The first value being {{1}} in the template.
    /// </summary>
    [JsonPropertyOrder(1)]
    public List<object> Parameters { get; set; }
}