using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Sessions.ChangeStreamLayout;
using Vonage.Server.Video.Sessions.CreateSession;
using Vonage.Server.Video.Sessions.GetStream;
using Vonage.Server.Video.Sessions.GetStreams;

namespace Vonage.Server.Video.Sessions;

/// <summary>
///     Represents a client exposing session features.
/// </summary>
public class SessionClient
{
    private readonly CreateSessionUseCase createSessionUseCase;
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///  Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    /// <param name="userAgent">The user agent.</param>
    public SessionClient(HttpClient httpClient, Func<string> tokenGeneration, string userAgent)
    {
        this.vonageClient = new VonageHttpClient(httpClient, JsonSerializerBuilder.Build(),
            new HttpClientOptions(tokenGeneration, userAgent));
        this.createSessionUseCase = new CreateSessionUseCase(this.vonageClient);
    }

    /// <summary>
    ///     Changes how the stream is displayed in the layout of a composed Vonage Video archive.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<Unit>> ChangeStreamLayoutAsync(Result<ChangeStreamLayoutRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Creates a new session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<CreateSessionResponse>> CreateSessionAsync(Result<CreateSessionRequest> request) =>
        this.createSessionUseCase.CreateSessionAsync(request);

    /// <summary>
    ///     Retrieves a stream.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the stream if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<GetStreamResponse>> GetStreamAsync(Result<GetStreamRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetStreamRequest, GetStreamResponse>(request);

    /// <summary>
    ///     Retrieves information on all Vonage Video streams in a session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A success state with streams if the operation succeeded. A failure state with the error message if it failed.</returns>
    public Task<Result<GetStreamsResponse>> GetStreamsAsync(Result<GetStreamsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetStreamsRequest, GetStreamsResponse>(request);
}