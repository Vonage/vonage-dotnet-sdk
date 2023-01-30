using System;

namespace Vonage.Common.Client;

/// <summary>
///     Represents options for Vonage's HttpClient.
/// </summary>
/// <param name="TokenGeneration">The token generation operation.</param>
/// <param name="UserAgent">The user agent.</param>
public record HttpClientOptions(Func<string> TokenGeneration, string UserAgent);