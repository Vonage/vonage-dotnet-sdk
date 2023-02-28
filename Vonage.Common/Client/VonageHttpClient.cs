﻿using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Common.Client;

/// <summary>
///     Represents a custom http client for Vonage's APIs.
/// </summary>
public class VonageHttpClient
{
    private readonly HttpClient client;
    private readonly HttpClientOptions options;
    private readonly IJsonSerializer jsonSerializer;
    private readonly string userAgent;

    /// <summary>
    ///     Creates a custom Http Client for Vonage purposes.
    /// </summary>
    /// <param name="httpClient">The http client.</param>
    /// <param name="serializer">The serializer.</param>
    /// <param name="options">The options.</param>
    public VonageHttpClient(HttpClient httpClient, IJsonSerializer serializer, HttpClientOptions options)
    {
        this.client = httpClient;
        this.jsonSerializer = serializer;
        this.options = options;
        this.userAgent = GetFormattedUserAgent(this.options.UserAgent);
    }

    /// <summary>
    ///     Creates a custom Http Client for Vonage purposes.
    /// </summary>
    /// <param name="configuration">The custom configuration.</param>
    /// <param name="serializer">The serializer.</param>
    public VonageHttpClient(VonageHttpClientConfiguration configuration, IJsonSerializer serializer)
    {
        this.client = configuration.HttpClient;
        this.jsonSerializer = serializer;
        this.options = new HttpClientOptions(configuration.TokenGeneration, configuration.UserAgent);
        this.userAgent = GetFormattedUserAgent(this.options.UserAgent);
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
    ///     Sends a HttpRequest without Authorization and UserAgent headers.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public async Task<Result<Unit>> SendWithoutHeadersAsync<T>(Result<T> request) where T : IVonageRequest =>
        await request
            .Map(value => value.BuildRequestMessage())
            .MapAsync(value => this.client.SendAsync(value))
            .BindAsync(value => MatchResponse(value, this.ParseFailure<Unit>, CreateSuccessResult));

    /// <summary>
    ///     Sends a HttpRequest and parses the response.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public async Task<Result<TResponse>> SendWithResponseAsync<TRequest, TResponse>(Result<TRequest> request)
        where TRequest : IVonageRequest =>
        await request
            .Map(this.BuildHttpRequestMessage)
            .MapAsync(value => this.client.SendAsync(value))
            .BindAsync(value => MatchResponse(value, this.ParseFailure<TResponse>, this.ParseSuccess<TResponse>));

    private HttpRequestMessage BuildHttpRequestMessage<T>(T value) where T : IVonageRequest =>
        value.BuildRequestMessage()
            .WithAuthorization(this.options.TokenGeneration())
            .WithUserAgent(this.userAgent);

    private Result<T> CreateFailureResult<T>(HttpStatusCode code, string responseContent) =>
        this.jsonSerializer
            .DeserializeObject<ErrorResponse>(responseContent)
            .Map(parsedError => HttpFailure.From(code, parsedError.Message))
            .Bind(failure => Result<T>.FromFailure(failure));

    private static Result<T> CreateFailureResult<T>(HttpStatusCode code) =>
        Result<T>.FromFailure(HttpFailure.From(code));

    private static Task<Result<Unit>> CreateSuccessResult(HttpResponseMessage response) =>
        Task.FromResult(Result<Unit>.FromSuccess(Unit.Default));

    private static string GetFormattedUserAgent(string userAgent)
    {
#if NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1
        var languageVersion = RuntimeInformation.FrameworkDescription
            .Replace(" ", string.Empty)
            .Replace("/", string.Empty)
            .Replace(":", string.Empty)
            .Replace(";", string.Empty)
            .Replace("_", string.Empty)
            .Replace("(", string.Empty)
            .Replace(")", string.Empty);
#else
        var languageVersion = System.Diagnostics.FileVersionInfo
            .GetVersionInfo(typeof(int).Assembly.Location)
            .ProductVersion;
#endif
        var libraryVersion = typeof(VonageHttpClient)
            .GetTypeInfo()
            .Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            .InformationalVersion;
        return $"vonage-dotnet/{libraryVersion} dotnet/{languageVersion} {userAgent}".Trim();
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

    private async Task<Result<T>> ParseSuccess<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return this.jsonSerializer
            .DeserializeObject<T>(responseContent)
            .Match(Result<T>.FromSuccess, Result<T>.FromFailure);
    }
}