using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.GetThemes;

internal class GetThemesUseCase
{
    private readonly VonageHttpClient httpClient;

    internal GetThemesUseCase(VonageHttpClient client) => this.httpClient = client;

    internal async Task<Result<Theme[]>> GetThemesAsync() =>
        await this.httpClient.SendWithResponseAsync<Theme[], GetThemesRequest>(
            GetThemesRequest.Default);
}