using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video;

/// <summary>
///     Represents a custom http client for Vonage's APIs.
/// </summary>
internal class VideoHttpClient
{
    private readonly HttpClient client;
    private readonly JsonSerializer jsonSerializer;

    /// <summary>
    ///     Creates a custom Http Client.
    /// </summary>
    /// <param name="httpClient">The http client.</param>
    internal VideoHttpClient(HttpClient httpClient)
    {
        this.client = httpClient;
        this.jsonSerializer = new JsonSerializer();
    }

    private Task<HttpResponseMessage> SendRequestAsync(IVideoRequest request, string token) =>
        this.client.SendAsync(request.BuildRequestMessage(token));

    private async Task<Result<T>> ParseSuccess<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return this.jsonSerializer
            .DeserializeObject<T>(responseContent)
            .Match(Result<T>.FromSuccess, Result<T>.FromFailure);
    }

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

    private Result<T> CreateFailureResult<T>(HttpStatusCode code, string responseContent) =>
        this.jsonSerializer
            .DeserializeObject<ErrorResponse>(responseContent)
            .Map(parsedError => HttpFailure.From(code, parsedError.Message))
            .Bind(failure => Result<T>.FromFailure(failure));

    private static Result<T> CreateFailureResult<T>(HttpStatusCode code) =>
        Result<T>.FromFailure(HttpFailure.From(code));

    private static Task<Result<Unit>> CreateSuccessResult(HttpResponseMessage response) =>
        Task.FromResult(Result<Unit>.FromSuccess(Unit.Default));

    /// <summary>
    ///     Sends a HttpRequest and parses the response.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <param name="token">The token to use for authentication.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    internal async Task<Result<TResponse>> SendWithResponseAsync<TResponse, TRequest>(Result<TRequest> request,
        string token) where TRequest : IVideoRequest =>
        await request
            .MapAsync(value => this.SendRequestAsync(value, token))
            .BindAsync(value => MatchResponse(value, this.ParseFailure<TResponse>, this.ParseSuccess<TResponse>));

    /// <summary>
    ///     Sends a HttpRequest.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <param name="token">The token to use for authentication.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    internal async Task<Result<Unit>> SendAsync<T>(Result<T> request, string token) where T : IVideoRequest =>
        await request
            .MapAsync(value => this.SendRequestAsync(value, token))
            .BindAsync(value =>
                MatchResponse(value, this.ParseFailure<Unit>, CreateSuccessResult));
}