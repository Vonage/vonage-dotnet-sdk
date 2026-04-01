#region
using System;
using System.Net.Http;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Client;

/// <summary>
///     Fluent builder for constructing <see cref="HttpRequestMessage"/> instances for Vonage API requests.
/// </summary>
/// <remarks>
///     This builder simplifies the creation of HTTP requests with proper method, URI, and content configuration.
/// </remarks>
/// <example>
/// <code><![CDATA[
/// var request = VonageRequestBuilder
///     .Initialize(HttpMethod.Post, "/v1/messages")
///     .WithContent(new StringContent(json, Encoding.UTF8, "application/json"))
///     .Build();
/// ]]></code>
/// </example>
public class VonageRequestBuilder
{
    private readonly HttpRequestMessage request;
    private Maybe<HttpContent> requestContent = Maybe<HttpContent>.None;

    private VonageRequestBuilder(HttpMethod httpMethod, Uri uri) =>
        this.request = new HttpRequestMessage(httpMethod, uri);

    /// <summary>
    ///     Builds and returns the configured <see cref="HttpRequestMessage" />.
    /// </summary>
    /// <returns>The configured HTTP request message ready to be sent.</returns>
    public HttpRequestMessage Build()
    {
        this.requestContent.IfSome(content => this.request.Content = content);
        return this.request;
    }

    /// <summary>
    ///     Initializes a new request builder with the specified HTTP method and relative endpoint URI.
    /// </summary>
    /// <param name="method">The HTTP method (GET, POST, PUT, DELETE, etc.).</param>
    /// <param name="endpointUri">The relative URI path for the API endpoint.</param>
    /// <returns>A new <see cref="VonageRequestBuilder" /> instance.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var builder = VonageRequestBuilder.Initialize(HttpMethod.Get, "/v1/accounts");
    /// ]]></code>
    /// </example>
    public static VonageRequestBuilder Initialize(HttpMethod method, string endpointUri) =>
        new(method, new Uri(endpointUri, UriKind.Relative));

    /// <summary>
    ///     Initializes a new request builder with the specified HTTP method and URI.
    /// </summary>
    /// <param name="method">The HTTP method (GET, POST, PUT, DELETE, etc.).</param>
    /// <param name="uri">The URI for the API endpoint.</param>
    /// <returns>A new <see cref="VonageRequestBuilder" /> instance.</returns>
    public static VonageRequestBuilder Initialize(HttpMethod method, Uri uri) => new(method, uri);

    /// <summary>
    ///     Sets the HTTP content (body) for the request.
    /// </summary>
    /// <param name="content">The HTTP content to include in the request. If null, no content is set.</param>
    /// <returns>This builder instance for method chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// builder.WithContent(new StringContent("{\"text\":\"Hello\"}", Encoding.UTF8, "application/json"));
    /// ]]></code>
    /// </example>
    public VonageRequestBuilder WithContent(HttpContent content)
    {
        if (content != null)
        {
            this.requestContent = content;
        }

        return this;
    }
}