using System.Text.Json.Serialization;

namespace Vonage.Applications;

/// <summary>
///     Represents the cryptographic keys returned with an application. The private key is only included when the
///     application is first created or updated.
/// </summary>
public record ApplicationResponseKeys(
    [property: JsonPropertyName("public_key")] string PublicKey,
    [property: JsonPropertyName("private_key")] string PrivateKey);
