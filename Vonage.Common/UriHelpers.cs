using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;

namespace Vonage.Common;

/// <summary>
///     Exposes helpers methods when working with queries.
/// </summary>
public static class UriHelpers
{
    /// <summary>
    ///     Builds a URI from a base URI and query parameters.
    /// </summary>
    /// <param name="baseUri">The base URI.</param>
    /// <param name="parameters">The query parameters.</param>
    /// <returns>The URI.</returns>
    public static string BuildUri(string baseUri, Dictionary<string, string> parameters)
    {
        var queryParams = string.Join("&", parameters
            .Select(pair => $"{UrlEncoder.Default.Encode(pair.Key)}={UrlEncoder.Default.Encode(pair.Value)}"));
        return queryParams.Length > 0 ? $"{baseUri}?{queryParams}" : baseUri;
    }
}