using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;

namespace Vonage.Numbers;

/// <inheritdoc />
public class NumbersClient : INumbersClient
{
    private const string SuccessStatusCode = "200";
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

    /// <summary>
    ///     Constructor for NumbersClients.
    /// </summary>
    /// <param name="credentials">Credentials to be used in further requests.</param>
    public NumbersClient(Credentials credentials = null)
    {
        this.Credentials = credentials;
        this.configuration = Configuration.Instance;
    }

    internal NumbersClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
        this.timeProvider = timeProvider;
    }

    /// <summary>
    ///     Gets or sets credentials to be used in further requests.
    /// </summary>
    public Credentials Credentials { get; set; }

    /// <inheritdoc />
    [Obsolete("Favor asynchronous version instead.")]
    public NumberTransactionResponse BuyANumber(NumberTransactionRequest request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    this.configuration, $"/number/buy?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
                request,
                false
            );
        ValidateNumbersResponse(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<NumberTransactionResponse> BuyANumberAsync(NumberTransactionRequest request,
        Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    this.configuration, $"/number/buy?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
                request,
                false
            );
        ValidateNumbersResponse(response);
        return response;
    }

    /// <inheritdoc />
    [Obsolete("Favor asynchronous version instead.")]
    public NumberTransactionResponse CancelANumber(NumberTransactionRequest request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    this.configuration, $"/number/cancel?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
                request,
                false
            );
        ValidateNumbersResponse(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<NumberTransactionResponse> CancelANumberAsync(NumberTransactionRequest request,
        Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    this.configuration, $"/number/cancel?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
                request,
                false
            );
        ValidateNumbersResponse(response);
        return response;
    }

    /// <inheritdoc />
    [Obsolete("Favor asynchronous version instead.")]
    public NumbersSearchResponse GetAvailableNumbers(NumberSearchRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParameters<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, this.configuration, "/number/search"),
                AuthType.Query,
                request
            );

    /// <inheritdoc />
    public Task<NumbersSearchResponse> GetAvailableNumbersAsync(NumberSearchRequest request,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, this.configuration, "/number/search"),
                AuthType.Query,
                request
            );

    /// <inheritdoc />
    [Obsolete("Favor asynchronous version instead.")]
    public NumbersSearchResponse GetOwnedNumbers(NumberSearchRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParameters<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, this.configuration, "/account/numbers"),
                AuthType.Query,
                request
            );

    /// <inheritdoc />
    public Task<NumbersSearchResponse>
        GetOwnedNumbersAsync(NumberSearchRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<NumbersSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, this.configuration, "/account/numbers"),
                AuthType.Query,
                request
            );

    /// <inheritdoc />
    [Obsolete("Favor asynchronous version instead.")]
    public NumberTransferResponse TransferANumber(NumberTransferRequest request, string apiKey,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContent<NumberTransferResponse>(
                HttpMethod.Post,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration,
                    $"/accounts/{apiKey}/transfer-number"),
                request,
                AuthType.Basic
            );

    /// <inheritdoc />
    public Task<NumberTransferResponse> TransferANumberAsync(NumberTransferRequest request, string apiKey,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<NumberTransferResponse>(
                HttpMethod.Post,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration,
                    $"/accounts/{apiKey}/transfer-number"),
                request,
                AuthType.Basic
            );

    /// <inheritdoc />
    [Obsolete("Favor asynchronous version instead.")]
    public NumberTransactionResponse UpdateANumber(UpdateNumberRequest request, Credentials creds = null)
    {
        var response = ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    this.configuration, $"/number/update?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
                request,
                false
            );
        ValidateNumbersResponse(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<NumberTransactionResponse> UpdateANumberAsync(UpdateNumberRequest request,
        Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    this.configuration, $"/number/update?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
                request,
                false
            );
        ValidateNumbersResponse(response);
        return response;
    }

    private static string FormatQueryStringCredentials(Credentials credentials) =>
        $"api_key={credentials.ApiKey}&api_secret={credentials.ApiSecret}";

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;

    private static void ValidateNumbersResponse(NumberTransactionResponse response)
    {
        if (response.ErrorCode != SuccessStatusCode)
        {
            throw new VonageNumberResponseException(
                    $"Number Transaction failed with error code:{response.ErrorCode} and label {response.ErrorCodeLabel}")
                {Response = response};
        }
    }
}