using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Sessions.GetStreams;

/// <inheritdoc />
public class GetStreamsUseCase : IGetStreamsUseCase
{
    private readonly Func<string> generateToken;
    private readonly VideoHttpClient videoHttpClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public GetStreamsUseCase(VideoHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.videoHttpClient = client;
    }

    /// <inheritdoc />
    public Task<Result<GetStreamsResponse>> GetStreamsAsync(GetStreamsRequest request) =>
        this.videoHttpClient.SendWithResponseAsync<GetStreamsResponse>(request, this.generateToken());
}