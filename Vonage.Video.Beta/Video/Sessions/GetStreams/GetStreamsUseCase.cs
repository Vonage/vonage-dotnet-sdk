using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Video.Sessions.GetStreams;

/// <inheritdoc />
public class GetStreamsUseCase : IGetStreamsUseCase
{
    private readonly Func<string> generateToken;
    private readonly CustomClient customClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public GetStreamsUseCase(CustomClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.customClient = client;
    }

    /// <inheritdoc />
    public Task<Result<GetStreamsResponse>> GetStreamsAsync(GetStreamsRequest request) =>
        this.customClient.SendWithResponseAsync<GetStreamsResponse>(request, this.generateToken());
}