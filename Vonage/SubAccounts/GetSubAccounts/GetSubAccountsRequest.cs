#region
using System.Net.Http;
using Vonage.Common.Client;
#endregion

namespace Vonage.SubAccounts.GetSubAccounts;

internal readonly struct GetSubAccountsRequest : IVonageRequest
{
    private readonly string apiKey;

    private GetSubAccountsRequest(string apiKey) => this.apiKey = apiKey;

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, $"/accounts/{this.apiKey}/subaccounts")
        .Build();

    internal static GetSubAccountsRequest Build(string apiKey) => new(apiKey);
}