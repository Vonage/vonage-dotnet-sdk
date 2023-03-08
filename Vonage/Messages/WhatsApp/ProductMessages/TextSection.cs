using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp.ProductMessages;

/// <summary>
///     Represents a text section.
/// </summary>
/// <param name="Text">The text of the section.</param>
/// <param name="Type">The type of the section.</param>
public record TextSection(
    [property: JsonPropertyOrder(1)] string Text,
    [property: JsonPropertyOrder(0)] string Type = null);