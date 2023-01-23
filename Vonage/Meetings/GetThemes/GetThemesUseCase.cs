using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Meetings.GetThemes;

internal class GetThemesUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient httpClient;

    internal GetThemesUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.httpClient = client;
    }

    internal async Task<Result<Theme[]>> GetThemesAsync() =>
        await this.httpClient.SendWithResponseAsync<Theme[], GetThemesRequest>(
            GetThemesRequest.Default,
            this.generateToken());
}