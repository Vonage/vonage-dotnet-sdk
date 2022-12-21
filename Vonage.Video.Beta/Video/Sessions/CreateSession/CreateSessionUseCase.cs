using System;
using System.Linq;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;

namespace Vonage.Video.Beta.Video.Sessions.CreateSession;

/// <inheritdoc />
public class CreateSessionUseCase : ICreateSessionUseCase
{
    private readonly Func<string> generateToken;
    private readonly CustomClient customClient;

    /// <summary>
    ///     Creates a new instance of use case.
    /// </summary>
    /// <param name="client">Custom Http Client to used for further connections.</param>
    /// <param name="generateToken">Function used for generating a token.</param>
    public CreateSessionUseCase(CustomClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.customClient = client;
    }

    /// <inheritdoc />
    public async Task<Result<CreateSessionResponse>> CreateSessionAsync(CreateSessionRequest request)
    {
        var result =
            await this.customClient.SendWithResponseAsync<CreateSessionResponse[]>(request, this.generateToken());
        return result.Bind(GetFirstSessionIfAvailable);
    }

    private static Result<CreateSessionResponse> GetFirstSessionIfAvailable(CreateSessionResponse[] sessions) =>
        sessions.Any()
            ? sessions.First()
            : Result<CreateSessionResponse>.FromFailure(
                ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
}