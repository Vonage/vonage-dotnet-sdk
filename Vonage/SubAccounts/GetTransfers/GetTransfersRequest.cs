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

/// <summary>
///     Represents a request to retrieve balance or credit transfers for a primary account within a specified time period.
/// </summary>
[Builder]
public readonly partial struct GetTransfersRequest : IVonageRequest
{
    internal const string BalanceTransfer = "balance-transfers";
    internal const string CreditTransfer = "credit-transfers";
    private const string DateFormat = "yyyy-MM-ddTHH:mm:ssZ";

    private string ApiKey { get; init; }

    private string Endpoint { get; init; }

    /// <summary>
    ///     Sets the end of the retrieval period. If not specified, all transfers until now are returned.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithEndDate(DateTimeOffset.UtcNow)
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<DateTimeOffset> EndDate { get; internal init; }

    /// <summary>
    ///     Sets the start of the retrieval period.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithStartDate(DateTimeOffset.UtcNow.AddDays(-30))
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public DateTimeOffset StartDate { get; internal init; }

    /// <summary>
    ///     Sets a subaccount API key to filter results. Only transfers involving this subaccount will be returned.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithSubAccountKey("bbe6222f")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> SubAccountKey { get; internal init; }

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

    [ValidationRule]
    internal static Result<GetTransfersRequest> VerifySubAccountKey(GetTransfersRequest request) =>
        request.SubAccountKey.Match(key => InputValidation.VerifyNotEmpty(request, key, nameof(request.SubAccountKey)),
            () => request);

    internal GetTransfersRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};

    internal GetTransfersRequest WithEndpoint(string endpoint) => this with {Endpoint = endpoint};
}