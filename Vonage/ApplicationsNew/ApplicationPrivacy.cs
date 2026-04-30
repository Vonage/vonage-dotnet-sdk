using System.Text.Json.Serialization;

namespace Vonage.ApplicationsNew;

/// <summary>
///     Represents the privacy settings for a Vonage application.
/// </summary>
public record ApplicationPrivacy(
    [property: JsonPropertyName("improve_ai")] bool ImproveAi);
