#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.IdentityInsights.GetInsights;
using Vonage.Serialization;
#endregion

namespace Vonage.IdentityInsights;

internal class IdentityInsightsClient : IIdentityInsightsClient
{
    private readonly VonageHttpClient<NetworkApiError> vonageClient;

    internal IdentityInsightsClient(VonageHttpClientConfiguration configuration) =>
        this.vonageClient =
            new VonageHttpClient<NetworkApiError>(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    public Task<Result<GetInsightsResponse>> GetInsightsAsync(Result<GetInsightsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetInsightsRequest, GetInsightsResponse>(request);
}