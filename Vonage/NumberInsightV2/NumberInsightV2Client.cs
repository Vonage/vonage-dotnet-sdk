using System;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.NumberInsightV2.FraudCheck;

namespace Vonage.NumberInsightV2;

internal class NumberInsightV2Client : INumberInsightV2Client
{
    private readonly VonageHttpClient vonageClient;

    public NumberInsightV2Client(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient(configuration, JsonSerializer.BuildWithSnakeCase());

    public Task<Result<FraudCheckResponse>> PerformFraudCheckAsync(Result<FraudCheckRequest> request) =>
        throw new NotImplementedException();
}