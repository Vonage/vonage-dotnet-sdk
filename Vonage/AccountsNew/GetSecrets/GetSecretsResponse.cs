using System.Text.Json.Serialization;
using Vonage.Common;

namespace Vonage.AccountsNew.GetSecrets;

/// <summary>
///     Represents the paginated list of API secrets for an account.
/// </summary>
public record GetSecretsResponse(
    [property: JsonPropertyName("_links")] HalLinks Links,
    [property: JsonPropertyName("_embedded")] GetSecretsEmbedded Embedded);

/// <summary>
///     Represents the embedded secrets payload within a <see cref="GetSecretsResponse"/>.
/// </summary>
public record GetSecretsEmbedded(
    [property: JsonPropertyName("secrets")] SecretInfo[] Secrets);
