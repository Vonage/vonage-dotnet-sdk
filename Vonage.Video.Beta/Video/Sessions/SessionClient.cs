using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video.Sessions.ChangeStreamLayout;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using Vonage.Video.Beta.Video.Sessions.GetStreams;

namespace Vonage.Video.Beta.Video.Sessions;

/// <inheritdoc />
public class SessionClient : ISessionClient
{
    private readonly ChangeStreamLayoutUseCase changeStreamLayoutUseCase;
    private readonly CreateSessionUseCase createSessionUseCase;
    private readonly GetStreamsUseCase getStreamsUseCase;
    private readonly GetStreamUseCase getStreamUseCase;

    /// <summary>
    ///  Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    public SessionClient(HttpClient httpClient, Func<string> tokenGeneration)
    {
        this.createSessionUseCase = new CreateSessionUseCase(new VideoHttpClient(httpClient), tokenGeneration);
        this.getStreamUseCase = new GetStreamUseCase(new VideoHttpClient(httpClient), tokenGeneration);
        this.getStreamsUseCase = new GetStreamsUseCase(new VideoHttpClient(httpClient), tokenGeneration);
        this.changeStreamLayoutUseCase =
            new ChangeStreamLayoutUseCase(new VideoHttpClient(httpClient), tokenGeneration);
    }

    /// <inheritdoc />
    public Task<Result<CreateSessionResponse>> CreateSessionAsync(Result<CreateSessionRequest> request) =>
        this.createSessionUseCase.CreateSessionAsync(request);

    /// <inheritdoc />
    public Task<Result<GetStreamResponse>> GetStreamAsync(Result<GetStreamRequest> request) =>
        this.getStreamUseCase.GetStreamAsync(request);

    /// <inheritdoc />
    public Task<Result<GetStreamsResponse>> GetStreamsAsync(Result<GetStreamsRequest> request) =>
        this.getStreamsUseCase.GetStreamsAsync(request);

    /// <inheritdoc />
    public Task<Result<Unit>> ChangeStreamLayoutAsync(Result<ChangeStreamLayoutRequest> request) =>
        this.changeStreamLayoutUseCase.ChangeStreamLayoutAsync(request);
}