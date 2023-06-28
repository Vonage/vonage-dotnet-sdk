using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.SubAccounts.GetSubAccounts;

internal readonly struct GetSubAccountsRequest : IVonageRequest
{
    private readonly string apiKey;

    private GetSubAccountsRequest(string apiKey) => this.apiKey = apiKey;

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/accounts/{this.apiKey}/subaccounts";

    internal static GetSubAccountsRequest Build(string apiKey) => new(apiKey);
}