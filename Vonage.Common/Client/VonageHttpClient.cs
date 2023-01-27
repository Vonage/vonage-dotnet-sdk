using System.Net;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Common.Client;

/// <summary>
///     Represents a custom http client for Vonage's APIs.
/// </summary>
public class VonageHttpClient
{
    private readonly Func<string> tokenGeneration;
    private readonly HttpClient client;
    private readonly IJsonSerializer jsonSerializer;

    /// <summary>
    ///     Creates a custom Http Client for Vonage purposes.
    /// </summary>
    /// <param name="httpClient">The http client.</param>
    /// <param name="serializer">The serializer.</param>
    /// <param name="tokenGeneration">The token generation operation.</param>
    public VonageHttpClient(HttpClient httpClient, IJsonSerializer serializer, Func<string> tokenGeneration)
    {
        this.client = httpClient;
        this.jsonSerializer = serializer;
        this.tokenGeneration = tokenGeneration;
    }

    /// <summary>
    ///     Sends a HttpRequest.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public async Task<Result<Unit>> SendAsync<T>(Result<T> request) where T : IVonageRequest =>
        await request
            .Map(this.BuildHttpRequestMessage)
            .MapAsync(value => this.client.SendAsync(value))
            .BindAsync(value => MatchResponse(value, this.ParseFailure<Unit>, CreateSuccessResult));

    /// <summary>
    ///     Sends a HttpRequest and parses the response.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public async Task<Result<TResponse>> SendWithResponseAsync<TResponse, TRequest>(Result<TRequest> request)
        where TRequest : IVonageRequest =>
        await request
            .Map(this.BuildHttpRequestMessage)
            .MapAsync(value => this.client.SendAsync(value))
            .BindAsync(value => MatchResponse(value, this.ParseFailure<TResponse>, this.ParseSuccess<TResponse>));

    private HttpRequestMessage BuildHttpRequestMessage<T>(T value) where T : IVonageRequest =>
        value.BuildRequestMessage().WithAuthorization(this.tokenGeneration());

    private Result<T> CreateFailureResult<T>(HttpStatusCode code, string responseContent) =>
        this.jsonSerializer
            .DeserializeObject<ErrorResponse>(responseContent)
            .Map(parsedError => HttpFailure.From(code, parsedError.Message))
            .Bind(failure => Result<T>.FromFailure(failure));

    private static Result<T> CreateFailureResult<T>(HttpStatusCode code) =>
        Result<T>.FromFailure(HttpFailure.From(code));

    private static Task<Result<Unit>> CreateSuccessResult(HttpResponseMessage response) =>
        Task.FromResult(Result<Unit>.FromSuccess(Unit.Default));

    private static Task<Result<T>> MatchResponse<T>(
        HttpResponseMessage response,
        Func<HttpResponseMessage, Task<Result<T>>> failure,
        Func<HttpResponseMessage, Task<Result<T>>> success) =>
        !response.IsSuccessStatusCode ? failure(response) : success(response);

    private async Task<Result<T>> ParseFailure<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return string.IsNullOrWhiteSpace(responseContent)
            ? CreateFailureResult<T>(response.StatusCode)
            : this.CreateFailureResult<T>(response.StatusCode, responseContent);
    }

    private async Task<Result<T>> ParseSuccess<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return this.jsonSerializer
            .DeserializeObject<T>(responseContent)
            .Match(Result<T>.FromSuccess, Result<T>.FromFailure);
    }
}