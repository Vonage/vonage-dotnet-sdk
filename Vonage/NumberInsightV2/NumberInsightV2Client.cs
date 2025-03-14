﻿#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.NumberInsightV2.FraudCheck;
using Vonage.Serialization;
#endregion

namespace Vonage.NumberInsightV2;

internal class NumberInsightV2Client : INumberInsightV2Client
{
    private readonly VonageHttpClient<StandardApiError> vonageClient;

    public NumberInsightV2Client(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient<StandardApiError>(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    public Task<Result<FraudCheckResponse>> PerformFraudCheckAsync(Result<FraudCheckRequest> request) =>
        this.vonageClient.SendWithResponseAsync<FraudCheckRequest, FraudCheckResponse>(request);
}