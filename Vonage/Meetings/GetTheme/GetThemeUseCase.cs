using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.GetTheme;

internal class GetThemeUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient httpClient;

    internal GetThemeUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.httpClient = client;
    }

    internal async Task<Result<Theme>> GetThemeAsync(Result<GetThemeRequest> request) =>
        await this.httpClient.SendWithResponseAsync<Theme, GetThemeRequest>(request, this.generateToken());
}