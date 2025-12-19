#region
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Client;

internal class VonageHttpClient<TError> where TError : IApiError
{
    private readonly HttpClient client;
    private readonly IJsonSerializer jsonSerializer;
    private readonly Result<HttpClientOptions> requestOptions;
    private readonly string userAgent;

    /// <summary>
    ///     Creates a custom Http Client for Vonage purposes.
    /// </summary>
    /// <param name="clientConfiguration">The custom configuration.</param>
    /// <param name="jsonSerializer">The serializer.</param>
    public VonageHttpClient(VonageHttpClientConfiguration clientConfiguration, IJsonSerializer jsonSerializer)
    {
        this.client = clientConfiguration.HttpClient;
        this.jsonSerializer = jsonSerializer;
        this.userAgent = clientConfiguration.UserAgent;
        this.requestOptions = clientConfiguration.AuthenticationHeader
            .Map(header =>
                new HttpClientOptions(header, UserAgentProvider.GetFormattedUserAgent(this.userAgent)));
    }

    internal VonageHttpClient<T> WithDifferentHeader<T>(Result<AuthenticationHeaderValue> header) where T : IApiError =>
        new VonageHttpClient<T>(new VonageHttpClientConfiguration(this.client, header, this.userAgent),
            this.jsonSerializer);

    /// <summary>
    ///     Sends a HttpRequest.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public async Task<Result<Unit>> SendAsync<T>(Result<T> request) where T : IVonageRequest =>
        await ProcessRequest(request,
                this.BuildHttpRequestMessageWithHeaders,
                this.SendHttpRequest,
                this.ParseResponseWhenFailure<Unit>,
                _ => CreateEmptySuccess())
            .ConfigureAwait(false);

    /// <summary>
    ///     Sends a HttpRequest without Authorization and UserAgent headers.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public async Task<Result<Unit>> SendWithoutHeadersAsync<T>(Result<T> request) where T : IVonageRequest =>
        await ProcessRequest(request,
                value => value.BuildRequestMessage(),
                this.SendHttpRequest,
                this.ParseResponseWhenFailure<Unit>,
                _ => CreateEmptySuccess())
            .ConfigureAwait(false);

    /// <summary>
    ///     Sends a HttpRequest and returns the raw content.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <typeparam name="TRequest">Type of the request.</typeparam>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public async Task<Result<string>> SendWithRawResponseAsync<TRequest>(Result<TRequest> request)
        where TRequest : IVonageRequest =>
        await ProcessRequest(request,
                this.BuildHttpRequestMessageWithHeaders,
                this.SendHttpRequest,
                this.ParseResponseWhenFailure<string>,
                responseData => responseData.Content)
            .ConfigureAwait(false);

    /// <summary>
    ///     Sends a HttpRequest and parses the response.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public async Task<Result<TResponse>> SendWithResponseAsync<TRequest, TResponse>(Result<TRequest> request)
        where TRequest : IVonageRequest =>
        await ProcessRequest(request,
            this.BuildHttpRequestMessageWithHeaders,
            this.SendHttpRequest,
            this.ParseResponseWhenFailure<TResponse>,
            this.ParseResponseWhenSuccess<TResponse>).ConfigureAwait(false);

    private Result<HttpRequestMessage> BuildHttpRequestMessageWithHeaders<T>(T request) where T : IVonageRequest =>
        this.requestOptions
            .Map(options => request
                .BuildRequestMessage()
                .WithAuthenticationHeader(options.AuthenticationHeader)
                .WithUserAgent(options.UserAgent));

    private static Result<Unit> CreateEmptySuccess() => Result<Unit>.FromSuccess(Unit.Default);

    private static async Task<HttpResponseData> ExtractResponse(HttpResponseMessage response) =>
        new HttpResponseData(response.StatusCode, response.IsSuccessStatusCode,
            await response.Content.ReadAsStringAsync().ConfigureAwait(false));

    private Result<T> ParseResponseWhenFailure<T>(HttpResponseData httpResponse) =>
        httpResponse.ToHttpFailure(this.jsonSerializer).ToResult<T>();

    private Result<T> ParseResponseWhenSuccess<T>(HttpResponseData httpResponse) =>
        this.jsonSerializer.DeserializeObject<T>(httpResponse.Content)
            .Match(Result<T>.FromSuccess, Result<T>.FromFailure);

    private static async Task<Result<TResponse>> ProcessRequest<TRequest, TResponse>(
        Result<TRequest> request,
        Func<TRequest, Result<HttpRequestMessage>> createHttpRequest,
        Func<HttpRequestMessage, Task<HttpResponseMessage>> sendHttpRequest,
        Func<HttpResponseData, Result<TResponse>> parseWhenFailure,
        Func<HttpResponseData, Result<TResponse>> parseWhenSuccess) =>
        await request
            .Bind(createHttpRequest)
            .MapAsync(sendHttpRequest)
            .MapAsync(ExtractResponse)
            .Bind(response => response.IsSuccessStatusCode ? parseWhenSuccess(response) : parseWhenFailure(response))
            .ConfigureAwait(false);

    private Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage request) => this.client.SendAsync(request);

    private sealed record HttpResponseData(HttpStatusCode Code, bool IsSuccessStatusCode, string Content)
    {
        public HttpFailure ToHttpFailure(IJsonSerializer jsonSerializer) =>
            MaybeExtensions.FromNonEmptyString(this.Content)
                .ToResult()
                .Map(_ => this.ToHttpFailureWithContent(jsonSerializer))
                .IfFailure(_ => HttpFailure.From(this.Code));

        private HttpFailure ToHttpFailureWithContent(IJsonSerializer jsonSerializer) =>
            jsonSerializer
                .DeserializeObject<TError>(this.Content)
                .Match(success => success.ToFailure() with {Code = this.Code, Json = this.Content},
                    failure => HttpFailure.From(this.Code, failure.GetFailureMessage(), this.Content));
    }

    private sealed record HttpClientOptions(AuthenticationHeaderValue AuthenticationHeader, string UserAgent);
}