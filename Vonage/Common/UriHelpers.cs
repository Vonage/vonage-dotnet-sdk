#region
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
#endregion

namespace Vonage.Common;

/// <summary>
///     Provides helper methods for building URIs with query parameters.
/// </summary>
public static class UriHelpers
{
    /// <summary>
    ///     Builds a URI by appending URL-encoded query parameters to a base URI.
    /// </summary>
    /// <param name="baseUri">The base URI without query string.</param>
    /// <param name="parameters">The query parameters to append. Keys and values will be URL-encoded.</param>
    /// <returns>The complete URI with query string, or just the base URI if parameters is empty.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var uri = UriHelpers.BuildUri("https://api.example.com/search", new Dictionary<string, string>
    /// {
    ///     ["query"] = "hello world",
    ///     ["page"] = "1"
    /// });
    /// // Returns: "https://api.example.com/search?query=hello%20world&page=1"
    /// ]]></code>
    /// </example>
    public static string BuildUri(string baseUri, Dictionary<string, string> parameters)
    {
        var queryParams = string.Join("&", parameters
            .Select(pair => $"{UrlEncoder.Default.Encode(pair.Key)}={UrlEncoder.Default.Encode(pair.Value)}"));
        return queryParams.Length > 0 ? $"{baseUri}?{queryParams}" : baseUri;
    }
}