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
    /// <param name="configuration">The custom configuration.</param>
    /// <param name="serializer">The serializer.</param>
    public VonageHttpClient(VonageHttpClientConfiguration configuration, IJsonSerializer serializer)
    {
        this.client = configuration.HttpClient;
        this.jsonSerializer = serializer;
        this.userAgent = configuration.UserAgent;
        this.requestOptions = configuration.AuthenticationHeader
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
        await SendRequest(request, this.BuildHttpRequestMessage, this.SendRequest, this.ParseFailure<Unit>,
                CreateSuccessResult)
            .ConfigureAwait(false);

    /// <summary>
    ///     Sends a HttpRequest without Authorization and UserAgent headers.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public async Task<Result<Unit>> SendWithoutHeadersAsync<T>(Result<T> request) where T : IVonageRequest =>
        await SendRequest(request, value => value.BuildRequestMessage(), this.SendRequest, this.ParseFailure<Unit>,
            CreateSuccessResult).ConfigureAwait(false);

    /// <summary>
    ///     Sends a HttpRequest and returns the raw content.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <typeparam name="TRequest">Type of the request.</typeparam>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public async Task<Result<string>> SendWithRawResponseAsync<TRequest>(Result<TRequest> request)
        where TRequest : IVonageRequest =>
        await SendRequest(request, this.BuildHttpRequestMessage,
            this.SendRequest,
            this.ParseFailure<string>,
            responseData => responseData.Content).ConfigureAwait(false);

    /// <summary>
    ///     Sends a HttpRequest and parses the response.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public async Task<Result<TResponse>> SendWithResponseAsync<TRequest, TResponse>(Result<TRequest> request)
        where TRequest : IVonageRequest =>
        await SendRequest(request, this.BuildHttpRequestMessage,
            this.SendRequest,
            this.ParseFailure<TResponse>,
            this.ParseSuccess<TResponse>).ConfigureAwait(false);

    private Result<HttpRequestMessage> BuildHttpRequestMessage<T>(T value) where T : IVonageRequest =>
        this.requestOptions
            .Map(options => value
                .BuildRequestMessage()
                .WithAuthenticationHeader(options.AuthenticationHeader)
                .WithUserAgent(options.UserAgent));

    private HttpFailure CreateFailureResult(HttpStatusCode code, string responseContent) =>
        this.jsonSerializer
            .DeserializeObject<TError>(responseContent)
            .Match(
                success => success.ToFailure() with {Code = code, Json = responseContent},
                failure => HttpFailure.From(code, failure.GetFailureMessage(), responseContent));

    private static HttpFailure CreateFailureResult(HttpStatusCode code) => HttpFailure.From(code);

    private static Result<Unit> CreateSuccessResult(ResponseData response) => Result<Unit>.FromSuccess(Unit.Default);

    private static async Task<ResponseData> ExtractResponseData(HttpResponseMessage response) =>
        new ResponseData(response.StatusCode, response.IsSuccessStatusCode,
            await response.Content.ReadAsStringAsync().ConfigureAwait(false));

    private Result<T> ParseFailure<T>(ResponseData response) =>
        MaybeExtensions.FromNonEmptyString(response.Content)
            .Match(value => this.CreateFailureResult(response.Code, value), () => CreateFailureResult(response.Code))
            .ToResult<T>();

    private Result<T> ParseSuccess<T>(ResponseData response) =>
        this.jsonSerializer
            .DeserializeObject<T>(response.Content)
            .Match(Result<T>.FromSuccess, Result<T>.FromFailure);

    private static async Task<Result<TResponse>> SendRequest<TRequest, TResponse>(
        Result<TRequest> request,
        Func<TRequest, Result<HttpRequestMessage>> httpRequestConversion,
        Func<HttpRequestMessage, Task<HttpResponseMessage>> sendRequest,
        Func<ResponseData, Result<TResponse>> failure,
        Func<ResponseData, Result<TResponse>> success) =>
        await request
            .Bind(httpRequestConversion)
            .MapAsync(sendRequest)
            .MapAsync(ExtractResponseData)
            .Bind(response => !response.IsSuccessStatusCode ? failure(response) : success(response))
            .ConfigureAwait(false);

    private Task<HttpResponseMessage> SendRequest(HttpRequestMessage request) => this.client.SendAsync(request);

    private sealed record ResponseData(HttpStatusCode Code, bool IsSuccessStatusCode, string Content);

    private sealed record HttpClientOptions(AuthenticationHeaderValue AuthenticationHeader, string UserAgent);
}