using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Vonage.Utility;

/// <summary>
///     Utility class to parse Webhook results.
/// </summary>
public class WebhookParser
{
    /// <summary>
    ///     Parses query string parameters into the given type. Converts the string pairs into a dictionary,
    ///     serializes them to JSON, and deserializes the JSON into <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The target type to deserialize the webhook payload into.</typeparam>
    /// <param name="requestData">The query string key/value pairs from the inbound HTTP request.</param>
    /// <returns>The deserialized webhook payload.</returns>
    public static T ParseQuery<T>(IEnumerable<KeyValuePair<string, StringValues>> requestData)
    {
        var dict = requestData.ToDictionary(x => x.Key, x => x.Value.ToString());
        var json = JsonConvert.SerializeObject(dict);
        return JsonConvert.DeserializeObject<T>(json);
    }
    
    /// <summary>
    ///     Parses query string parameters into the given type. Use this overload when the query data is exposed
    ///     as plain string values (legacy ASP.NET / HttpUtility) rather than ASP.NET Core <see cref="StringValues"/>.
    /// </summary>
    /// <typeparam name="T">The target type to deserialize the webhook payload into.</typeparam>
    /// <param name="requestData">The query string key/value pairs from the inbound HTTP request.</param>
    /// <returns>The deserialized webhook payload.</returns>
    public static T ParseQueryNameValuePairs<T>(IEnumerable<KeyValuePair<string, string>> requestData)
    {
        var dict = requestData.ToDictionary(x => x.Key, x => x.Value);
        var json = JsonConvert.SerializeObject(dict);
        return JsonConvert.DeserializeObject<T>(json);
    }
    
    /// <summary>
    ///     Parses a URL-encoded form body into the given type. Uses Newtonsoft.Json — abnormally named fields
    ///     should be decorated with <c>JsonPropertyAttribute</c>.
    /// </summary>
    /// <typeparam name="T">The target type to deserialize the webhook payload into.</typeparam>
    /// <param name="contentString">A URL-encoded form body of the form <c>key1=value1&amp;key2=value2</c>.</param>
    /// <returns>The deserialized webhook payload.</returns>
    public static T ParseUrlFormString<T>(string contentString)
    {
        var splitParameters = contentString.Split('&');
        var contentDictionary = splitParameters.Select(param => param.Split('='))
            .ToDictionary(split => split[0], split => WebUtility.UrlDecode(split[1]));
        var json = JsonConvert.SerializeObject(contentDictionary);
        return JsonConvert.DeserializeObject<T>(json);
    }
    
    /// <summary>
    ///     Synchronous wrapper around <see cref="ParseWebhookAsync{T}(Stream, string)"/>. Meant to be called from
    ///     ASP.NET Core MVC with only the body content of the inbound request.
    /// </summary>
    /// <typeparam name="T">The target type to deserialize the webhook payload into.</typeparam>
    /// <param name="content">The request body stream containing the webhook payload.</param>
    /// <param name="contentType">
    ///     The content type of the request. Must contain <c>application/json</c> or
    ///     <c>application/x-www-form-urlencoded</c>.
    /// </param>
    /// <returns>The deserialized webhook payload.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="contentType"/> is not supported.</exception>
    public static T ParseWebhook<T>(Stream content, string contentType) =>
        ParseWebhookAsync<T>(content, contentType).Result;
    
    /// <summary>
    ///     Synchronous wrapper around <see cref="ParseWebhookAsync{T}(HttpRequestMessage)"/>. Meant to be called
    ///     from legacy ASP.NET Web API where the request is exposed as an <see cref="HttpRequestMessage"/>.
    /// </summary>
    /// <typeparam name="T">The target type to deserialize the webhook payload into.</typeparam>
    /// <param name="request">The inbound request whose content carries the webhook payload.</param>
    /// <returns>The deserialized webhook payload.</returns>
    /// <exception cref="ArgumentException">Thrown when the request content type is not supported.</exception>
    public static T ParseWebhook<T>(HttpRequestMessage request) => ParseWebhookAsync<T>(request).Result;
    
    /// <summary>
    ///     Parses the request body stream into the given type. Intended to be called from ASP.NET Core MVC / API
    ///     handlers where the webhook payload arrives in the body of the inbound request.
    /// </summary>
    /// <typeparam name="T">The target type to deserialize the webhook payload into.</typeparam>
    /// <param name="content">The request body stream containing the webhook payload.</param>
    /// <param name="contentType">
    ///     The content type of the request. Must contain <c>application/json</c> or
    ///     <c>application/x-www-form-urlencoded</c>.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     Thrown if <paramref name="contentType"/> does not contain <c>application/json</c> or
    ///     <c>application/x-www-form-urlencoded</c>.
    /// </exception>
    /// <returns>A task that resolves to the deserialized webhook payload.</returns>
    /// <example>
    /// <code><![CDATA[
    /// // ASP.NET Core MVC controller
    /// [HttpPost("webhooks/inbound")]
    /// public async Task<IActionResult> Inbound()
    /// {
    ///     var payload = await WebhookParser.ParseWebhookAsync<InboundSms>(
    ///         Request.Body,
    ///         Request.ContentType);
    ///     // ... handle payload
    ///     return Ok();
    /// }
    /// ]]></code>
    /// </example>
    public static async Task<T> ParseWebhookAsync<T>(Stream content, string contentType)
    {
        if (contentType.Contains("application/json"))
        {
            using var reader = new StreamReader(content);
            var json = await reader.ReadToEndAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(json);
        }
        
        if (contentType.Contains("application/x-www-form-urlencoded"))
        {
            using var reader = new StreamReader(content);
            var contentString = await reader.ReadToEndAsync().ConfigureAwait(false);
            return ParseUrlFormString<T>(contentString);
        }
        
        throw new ArgumentException("Invalid Content Type");
    }
    
    /// <summary>
    ///     Parses the content of an <see cref="HttpRequestMessage"/> into the given type. Intended to be called
    ///     from legacy ASP.NET Web API handlers.
    /// </summary>
    /// <typeparam name="T">The target type to deserialize the webhook payload into.</typeparam>
    /// <param name="request">The inbound request whose content carries the webhook payload.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown if the request content type does not contain <c>application/json</c> or
    ///     <c>application/x-www-form-urlencoded</c>.
    /// </exception>
    /// <returns>A task that resolves to the deserialized webhook payload.</returns>
    /// <example>
    /// <code><![CDATA[
    /// // Legacy ASP.NET Web API controller
    /// public async Task<IHttpActionResult> Inbound(HttpRequestMessage request)
    /// {
    ///     var payload = await WebhookParser.ParseWebhookAsync<InboundSms>(request);
    ///     // ... handle payload
    ///     return Ok();
    /// }
    /// ]]></code>
    /// </example>
    public static async Task<T> ParseWebhookAsync<T>(HttpRequestMessage request)
    {
        if (request.Content.Headers.GetValues("Content-Type").First().Contains("application/json"))
        {
            var json = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(json);
        }
        
        if (request.Content.Headers.GetValues("Content-Type").First().Contains("application/x-www-form-urlencoded"))
        {
            var contentString = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            return ParseUrlFormString<T>(contentString);
        }
        
        throw new ArgumentException("Invalid Content Type");
    }
}