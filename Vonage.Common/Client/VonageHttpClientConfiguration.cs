using System;
using System.Net.Http;

namespace Vonage.Common.Client;

/// <summary>
///     Represents the configuration for all Vonage Clients.
/// </summary>
/// <param name="HttpClient">HttpClient to used for further connections.</param>
/// <param name="TokenGeneration">Function used for generating a token.</param>
/// <param name="UserAgent">Value to be used in the user-agent header of each request.</param>
public record VonageHttpClientConfiguration(HttpClient HttpClient, Func<string> TokenGeneration, string UserAgent);