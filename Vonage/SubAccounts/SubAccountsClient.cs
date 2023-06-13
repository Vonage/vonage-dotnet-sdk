using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.SubAccounts.GetSubAccounts;

namespace Vonage.SubAccounts;

/// <inheritdoc />
public class SubAccountsClient : ISubAccountsClient
{
    private readonly string apiKey;
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    /// <param name="apiKey">The account Id.</param>
    public SubAccountsClient(VonageHttpClientConfiguration configuration, string apiKey)
    {
        this.vonageClient = new VonageHttpClient(configuration, JsonSerializer.BuildWithSnakeCase());
        this.apiKey = apiKey;
    }

    /// <inheritdoc />
    public async Task<Result<GetSubAccountsResponse>> GetSubaccounts() =>
        await this.vonageClient
            .SendWithResponseAsync<GetSubAccountsRequest, EmbeddedResponse<GetSubAccountsResponse>>(
                GetSubAccountsRequest.Build(this.apiKey))
            .Map(value => value.Content);
}