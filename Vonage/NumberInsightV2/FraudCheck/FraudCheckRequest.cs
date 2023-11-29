using System;
using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.NumberInsightV2.FraudCheck;

public struct FraudCheckRequest : IVonageRequest
{
    public static IBuilderForPhone Build()
    {
        throw new NotImplementedException();
    }

    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    public string GetEndpointPath() => throw new NotImplementedException();
}