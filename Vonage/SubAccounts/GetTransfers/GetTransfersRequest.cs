#region
using System;
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.SubAccounts.GetTransfers;

/// <inheritdoc />
[Builder]
public readonly partial struct GetTransfersRequest : IVonageRequest
{
    internal const string BalanceTransfer = "balance-transfers";
    internal const string CreditTransfer = "credit-transfers";
    private const string DateFormat = "yyyy-MM-ddTHH:mm:ssZ";

    private string ApiKey { get; init; }

    private string Endpoint { get; init; }

    /// <summary>
    ///     End of the retrieval period. If absent then all transfers until now is returned.
    /// </summary>
    [Optional]
    public Maybe<DateTimeOffset> EndDate { get; internal init; }

    /// <summary>
    ///     Start of the retrieval period.
    /// </summary>
    [Mandatory(0, nameof(VerifySubAccountKey))]
    public DateTimeOffset StartDate { get; internal init; }

    /// <summary>
    ///     Subaccount to filter by.
    /// </summary>
    [Optional]
    public Maybe<string> SubAccountKey { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, UriHelpers.BuildUri($"/accounts/{this.ApiKey}/{this.Endpoint}",
            this.GetQueryStringParameters()))
        .Build();

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string> {{"start_date", this.StartDate.ToString(DateFormat)}};
        this.EndDate.IfSome(value => parameters.Add("end_date", value.ToString(DateFormat)));
        this.SubAccountKey.IfSome(value => parameters.Add("subaccount", value));
        return parameters;
    }

    internal static Result<GetTransfersRequest> VerifySubAccountKey(GetTransfersRequest request) =>
        request.SubAccountKey.Match(key => InputValidation.VerifyNotEmpty(request, key, nameof(request.SubAccountKey)),
            () => request);

    internal GetTransfersRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};

    internal GetTransfersRequest WithEndpoint(string endpoint) => this with {Endpoint = endpoint};
}