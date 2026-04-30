using System.Text.Json.Serialization;

namespace Vonage.ApplicationsNew;

/// <summary>
///     Carries a public key when creating or updating an application.
/// </summary>
public record ApplicationKeys(
    [property: JsonPropertyName("public_key")] string PublicKey);
