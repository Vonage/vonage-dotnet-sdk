using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.SubAccounts.GetCreditTransfers;

/// <inheritdoc />
public readonly struct GetCreditTransfersRequest : IVonageRequest
{
    private const string DateFormat = "yyyy-MM-ddTHH:mm:ssZ";

    /// <summary>
    ///     Unique primary account ID.
    /// </summary>
    private string ApiKey { get; init; }

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
    ///     Initializes a builder for GetCreditTransfersRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForStartDate Build() => new GetCreditTransfersRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => QueryHelpers.AddQueryString($"/accounts/{this.ApiKey}/credit-transfers",
        this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string> {{"start_date", this.StartDate.ToString(DateFormat)}};
        this.EndDate.IfSome(value => parameters.Add("end_date", value.ToString(DateFormat)));
        this.SubAccountKey.IfSome(value => parameters.Add("subaccount", value));
        return parameters;
    }

    internal GetCreditTransfersRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};
}