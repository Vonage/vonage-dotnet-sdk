using System;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.NumberInsightV2.FraudCheck;

namespace Vonage.NumberInsightV2;

internal class NumberInsightV2Client : INumberInsightV2Client
{
    public NumberInsightV2Client(VonageHttpClientConfiguration buildConfiguration)
    {
        throw new NotImplementedException();
    }

    public Task<Result<FraudCheckResponse>> PerformFraudCheckAsync(Result<FraudCheckRequest> request) =>
        throw new NotImplementedException();
}