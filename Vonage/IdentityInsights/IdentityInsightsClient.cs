#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.IdentityInsights.GetInsights;
using Vonage.Request;
using Vonage.Serialization;
#endregion

namespace Vonage.IdentityInsights;

internal class IdentityInsightsClient : IIdentityInsightsClient
{
    private readonly Configuration configuration;
    private readonly Credentials credentials;
    private readonly VonageUrls.Region region;
    private readonly VonageHttpClient<NetworkApiError> vonageClient;

    internal IdentityInsightsClient(
        Credentials credentials,
        Configuration configuration,
        VonageUrls.Region region = VonageUrls.Region.US)
    {
        this.credentials = credentials;
        this.configuration = configuration;
        this.region = region;
        this.vonageClient = new VonageHttpClient<NetworkApiError>(this.BuildClientConfiguration(),
            JsonSerializerBuilder.BuildWithSnakeCase());
    }

    public Task<Result<GetInsightsResponse>> GetInsightsAsync(Result<GetInsightsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetInsightsRequest, GetInsightsResponse>(request);

    public IIdentityInsightsClient WithEuRegion() =>
        new IdentityInsightsClient(this.credentials, this.configuration, VonageUrls.Region.EU);

    private VonageHttpClientConfiguration BuildClientConfiguration() =>
        new VonageHttpClientConfiguration(
            this.configuration.BuildHttpClientForRegion(this.region),
            this.credentials.GetAuthenticationHeader(),
            this.credentials.GetUserAgent());
}