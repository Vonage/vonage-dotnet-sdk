using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Meetings.GetApplicationThemes;

internal class GetApplicationThemesUseCase
{
    private readonly Func<string> generateToken;
    private readonly VonageHttpClient httpClient;

    internal GetApplicationThemesUseCase(VonageHttpClient client, Func<string> generateToken)
    {
        this.generateToken = generateToken;
        this.httpClient = client;
    }

    internal async Task<Result<Theme[]>> GetApplicationThemesAsync() =>
        await this.httpClient.SendWithResponseAsync<Theme[], GetApplicationThemesRequest>(
            GetApplicationThemesRequest.Default,
            this.generateToken());
}