using System;
using System.Net.Http;
using Vonage.Common.Monads;

namespace Vonage.Common.Client;

/// <summary>
///     Dedicated HttpRequest builder for VonageRequests.
/// </summary>
public class VonageRequestBuilder
{
    private readonly HttpRequestMessage request;
    private Maybe<HttpContent> requestContent = Maybe<HttpContent>.None;

    private VonageRequestBuilder(HttpMethod httpMethod, Uri uri) =>
        this.request = new HttpRequestMessage(httpMethod, uri);

    public HttpRequestMessage Build()
    {
        this.requestContent.IfSome(content => this.request.Content = content);
        return this.request;
    }

    public static VonageRequestBuilder Initialize(HttpMethod method, string endpointUri) =>
        new(method, new Uri(endpointUri, UriKind.Relative));

    public static VonageRequestBuilder Initialize(HttpMethod method, Uri uri) => new(method, uri);

    public VonageRequestBuilder WithContent(HttpContent content)
    {
        if (content != null)
        {
            this.requestContent = content;
        }

        return this;
    }
}