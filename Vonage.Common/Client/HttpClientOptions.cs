namespace Vonage.Common.Client;

/// <summary>
///     Represents options for Vonage's HttpClient.
/// </summary>
/// <param name="Token">The token.</param>
/// <param name="UserAgent">The user agent.</param>
public record HttpClientOptions(string Token, string UserAgent);