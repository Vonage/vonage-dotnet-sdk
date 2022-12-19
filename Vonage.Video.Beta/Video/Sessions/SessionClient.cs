using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using Vonage.Video.Beta.Video.Sessions.GetStreams;
using Vonage.Voice;

namespace Vonage.Video.Beta.Video.Sessions;

/// <inheritdoc />
public class SessionClient : ISessionClient
{
    private readonly CreateSessionUseCase createSessionUseCase;
    private readonly GetStreamsUseCase getStreamsUseCase;
    private readonly GetStreamUseCase getStreamUseCase;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="credentials">Credentials to be used for further connections.</param>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGenerator">Generator for authentication tokens.</param>
    public SessionClient(Credentials credentials, HttpClient httpClient, ITokenGenerator tokenGenerator)
    {
        this.Credentials = credentials;
        this.createSessionUseCase = new CreateSessionUseCase(this.Credentials, httpClient, tokenGenerator);
        this.getStreamUseCase = new GetStreamUseCase(this.Credentials, httpClient, tokenGenerator);
        this.getStreamsUseCase = new GetStreamsUseCase(this.Credentials, httpClient, tokenGenerator);
    }

    /// <inheritdoc />
    public Credentials Credentials { get; }

    /// <inheritdoc />
    public Task<Result<CreateSessionResponse>> CreateSessionAsync(CreateSessionRequest request) =>
        this.createSessionUseCase.CreateSessionAsync(request);

    /// <inheritdoc />
    public Task<Result<GetStreamResponse>> GetStreamAsync(GetStreamRequest request) =>
        this.getStreamUseCase.GetStreamAsync(request);

    /// <inheritdoc />
    public Task<Result<GetStreamsResponse>> GetStreamsAsync(GetStreamsRequest request) =>
        this.getStreamsUseCase.GetStreamsAsync(request);
}