using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.NumberInsightV2.FraudCheck;

public struct FraudCheckRequest : IVonageRequest
{
    public static Result<FraudCheckRequest> Build()
    {
        throw new NotImplementedException();
    }

    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    public string GetEndpointPath() => throw new NotImplementedException();
}