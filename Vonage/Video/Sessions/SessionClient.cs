using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.Sessions.ChangeStreamLayout;
using Vonage.Video.Sessions.CreateSession;
using Vonage.Video.Sessions.GetStream;
using Vonage.Video.Sessions.GetStreams;

namespace Vonage.Video.Sessions;

/// <summary>
///     Represents a client exposing session features.
/// </summary>
public class SessionClient
{
    private readonly CreateSessionUseCase createSessionUseCase;
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    public SessionClient(VonageHttpClientConfiguration configuration)
    {
        this.vonageClient =
            new VonageHttpClient(configuration, JsonSerializerBuilder.BuildWithCamelCase());
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