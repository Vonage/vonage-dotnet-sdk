using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Meetings.GetDialNumbers;

internal class GetDialNumbersUseCase
{
    private readonly VonageHttpClient httpClient;

    internal GetDialNumbersUseCase(VonageHttpClient client) => this.httpClient = client;

    internal async Task<Result<GetDialNumbersResponse[]>> GetDialNumbersAsync() =>
        await this.httpClient.SendWithResponseAsync<GetDialNumbersResponse[], GetDialNumbersRequest>(
            GetDialNumbersRequest.Default);
}