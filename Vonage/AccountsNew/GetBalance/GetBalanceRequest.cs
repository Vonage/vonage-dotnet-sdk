using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.AccountsNew.GetBalance;

internal readonly struct GetBalanceRequest : IVonageRequest
{
    internal static readonly GetBalanceRequest Default = new();

    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, "/account/get-balance")
        .Build();
}
