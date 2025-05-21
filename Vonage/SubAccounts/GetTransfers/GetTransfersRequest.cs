using System;
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.SubAccounts.GetTransfers;

/// <inheritdoc />
public readonly struct GetTransfersRequest : IVonageRequest
{
    internal const string BalanceTransfer = "balance-transfers";
    internal const string CreditTransfer = "credit-transfers";
    private const string DateFormat = "yyyy-MM-ddTHH:mm:ssZ";

    private string ApiKey { get; init; }

    private string Endpoint { get; init; }

    /// <summary>
    ///     End of the retrieval period. If absent then all transfers until now is returned.
    /// </summary>
    public Maybe<DateTimeOffset> EndDate { get; internal init; }

    /// <summary>
    ///     Start of the retrieval period.
    /// </summary>
    public DateTimeOffset StartDate { get; internal init; }

    /// <summary>
    ///     Subaccount to filter by.
    /// </summary>
    public Maybe<string> SubAccountKey { get; internal init; }

    /// <summary>
    ///     Initializes a builder for GetTransfersRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForStartDate Build() => new GetTransfersRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => UriHelpers.BuildUri($"/accounts/{this.ApiKey}/{this.Endpoint}",
        this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string> {{"start_date", this.StartDate.ToString(DateFormat)}};
        this.EndDate.IfSome(value => parameters.Add("end_date", value.ToString(DateFormat)));
        this.SubAccountKey.IfSome(value => parameters.Add("subaccount", value));
        return parameters;
    }

    internal GetTransfersRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};

    internal GetTransfersRequest WithEndpoint(string endpoint) => this with {Endpoint = endpoint};
}