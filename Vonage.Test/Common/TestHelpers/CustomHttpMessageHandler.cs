using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Test.Common.TestHelpers;

/// <summary>
///     Custom message handler for CustomHttpMessageHandler.
/// </summary>
/// <remarks>
///     Compared to the FakeHttpMessageHandler, this one is supposed to handle multiple requests at the same time as the
///     use case contains a sequence of 3 http calls. In that sense, it is closer to a mock.
///     The mapping of request/response is made only on the pair Uri & HttpMethod. Indeed, the upload content is a
///     multipart content, which makes it harder to predict. It makes us lose a bit of reliability in our tests but it's a
///     compromise we made willingly.
/// </remarks>
public class CustomHttpMessageHandler :
    HttpMessageHandler,
    ICustomHandlerExpectsRequest
{
    private readonly ReadOnlyCollection<Mapping> requestMappings;

    public CustomHttpMessageHandler(IEnumerable<Mapping> requestMappings) =>
        this.requestMappings = new ReadOnlyCollection<Mapping>(requestMappings.ToList());

    public Uri BaseUri => new Uri("http://fake-host/api");

    public ICustomHandlerExpectsResponse GivenRequest(ExpectedRequest request) =>
        new CustomHttpMessageHandlerExpectsResponse(this.requestMappings, this.CreateMappingRequest(request));

    public VonageHttpClientConfiguration ToConfiguration(ISpecimenBuilder builder) =>
        new VonageHttpClientConfiguration(
            new HttpClient(this, false) {BaseAddress = this.BaseUri},
            new AuthenticationHeaderValue("Bearer", builder.Create<string>()),
            builder.Create<string>());

    public HttpClient ToHttpClient() => new HttpClient(this, false) {BaseAddress = this.BaseUri};

    public static ICustomHandlerExpectsRequest Build() =>
        new CustomHttpMessageHandler(Enumerable.Empty<Mapping>());

    private MappingRequest CreateMappingRequest(ExpectedRequest request) =>
        new MappingRequest
        {
            Method = request.Method,
            RequestUri = new Uri(this.BaseUri, request.RequestUri),
        };

    private static Func<Mapping, bool> FindMatchingMapping(MappingRequest incomingRequest) =>
        mapping =>
            mapping.Request.RequestUri == incomingRequest.RequestUri &&
            mapping.Request.Method == incomingRequest.Method;

    private static MappingRequest ParseIncomingRequest(HttpRequestMessage request) =>
        new MappingRequest
        {
            RequestUri = request.RequestUri,
            Method = request.Method,
        };

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var incomingRequest = ParseIncomingRequest(request);
        if (!this.requestMappings.Any(FindMatchingMapping(incomingRequest)))
        {
            throw new InvalidOperationException(
                $"Missing mapping for {incomingRequest.Method} {incomingRequest.RequestUri}.");
        }

        var matchingResponse = this.requestMappings.First(FindMatchingMapping(incomingRequest)).Response;
        return Task.FromResult(
            new HttpResponseMessage(matchingResponse.Code)
            {
                Content = new StringContent(matchingResponse.Content.IfNone(string.Empty), Encoding.UTF8,
                    "application/json"),
            });
    }
}

public class CustomHttpMessageHandlerExpectsResponse : ICustomHandlerExpectsResponse
{
    private readonly MappingRequest pendingRequest;
    private readonly ReadOnlyCollection<Mapping> requestMappings;

    public CustomHttpMessageHandlerExpectsResponse(IEnumerable<Mapping> requestMappings,
        MappingRequest pendingRequest)
    {
        this.requestMappings = new ReadOnlyCollection<Mapping>(requestMappings.ToList());
        this.pendingRequest = pendingRequest;
    }

    public ICustomHandlerExpectsRequest RespondWith(MappingResponse response)
    {
        var mappings = this.requestMappings
            .Where(mapping => !mapping.Request.Equals(this.pendingRequest))
            .ToList();
        mappings.Add(new Mapping
        {
            Request = this.pendingRequest,
            Response = response,
        });
        return new CustomHttpMessageHandler(mappings);
    }
}

public interface ICustomHandlerExpectsRequest
{
    Uri BaseUri { get; }
    ICustomHandlerExpectsResponse GivenRequest(ExpectedRequest request);
    VonageHttpClientConfiguration ToConfiguration(ISpecimenBuilder builder);
    HttpClient ToHttpClient();
}

public interface ICustomHandlerExpectsResponse
{
    ICustomHandlerExpectsRequest RespondWith(MappingResponse response);
}

public struct Mapping
{
    public MappingRequest Request { get; set; }
    public MappingResponse Response { get; set; }
}

public struct MappingRequest
{
    public HttpMethod Method { get; set; }
    public Uri RequestUri { get; set; }
}

public struct MappingResponse
{
    public HttpStatusCode Code { get; set; }
    public Maybe<string> Content { get; set; }
}