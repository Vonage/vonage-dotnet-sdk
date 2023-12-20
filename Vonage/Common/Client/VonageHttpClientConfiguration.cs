using System.Net.Http;
using System.Net.Http.Headers;
using Vonage.Common.Monads;

namespace Vonage.Common.Client;

/// <summary>
///     Represents the configuration for all Vonage Clients.
/// </summary>
/// <param name="HttpClient">HttpClient to used for further connections.</param>
/// <param name="AuthenticationHeader">AuthenticationHeader to be used for further connections.</param>
/// <param name="UserAgent">Value to be used in the user-agent header of each request.</param>
public record VonageHttpClientConfiguration(
    HttpClient HttpClient,
    Result<AuthenticationHeaderValue> AuthenticationHeader,
    string UserAgent);