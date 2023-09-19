using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Numbers;

/// <inheritdoc />
public class NumbersClient : INumbersClient
{
    private readonly Configuration configuration;
    private const string SuccessStatusCode = "200";

    /// <summary>
    ///     Gets or sets credentials to be used in further requests.
    /// </summary>
    public Credentials Credentials { get; set; }

    /// <summary>
    ///     Constructor for NumbersClients.
    /// </summary>
    /// <param name="credentials">Credentials to be used in further requests.</param>
    public NumbersClient(Credentials credentials = null)
    {
        this.Credentials = credentials;
        this.configuration = Configuration.Instance;
    }

    internal NumbersClient(Credentials credentials, Configuration configuration)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
    }

    /// <inheritdoc />
    public NumberTransactionResponse BuyANumber(NumberTransactionRequest request, Credentials creds = null)
    {
        var response = new ApiRequest(creds ?? this.Credentials, this.configuration)
            .DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    $"/number/buy?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
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
        var response = await new ApiRequest(creds ?? this.Credentials, this.configuration)
            .DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    $"/number/buy?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
                request,
                false
            );
        ValidateNumbersResponse(response);
        return response;
    }

    /// <inheritdoc />
    public NumberTransactionResponse CancelANumber(NumberTransactionRequest request, Credentials creds = null)
    {
        var response = new ApiRequest(creds ?? this.Credentials, this.configuration)
            .DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    $"/number/cancel?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
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
        var response = await new ApiRequest(creds ?? this.Credentials, this.configuration)
            .DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    $"/number/cancel?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
                request,
                false
            );
        ValidateNumbersResponse(response);
        return response;
    }

    /// <inheritdoc />
    public NumbersSearchResponse GetAvailableNumbers(NumberSearchRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParameters<NumbersSearchResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/search"),
            AuthType.Query,
            request
        );

    /// <inheritdoc />
    public Task<NumbersSearchResponse> GetAvailableNumbersAsync(NumberSearchRequest request,
        Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParametersAsync<NumbersSearchResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/number/search"),
            AuthType.Query,
            request
        );

    /// <inheritdoc />
    public NumbersSearchResponse GetOwnedNumbers(NumberSearchRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParameters<NumbersSearchResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/account/numbers"),
            AuthType.Query,
            request
        );

    /// <inheritdoc />
    public Task<NumbersSearchResponse>
        GetOwnedNumbersAsync(NumberSearchRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoGetRequestWithQueryParametersAsync<NumbersSearchResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/account/numbers"),
            AuthType.Query,
            request
        );

    /// <inheritdoc />
    public NumberTransferResponse TransferANumber(NumberTransferRequest request, string apiKey,
        Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoRequestWithJsonContent<NumberTransferResponse>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/transfer-number"),
            request,
            AuthType.Basic
        );

    /// <inheritdoc />
    public Task<NumberTransferResponse> TransferANumberAsync(NumberTransferRequest request, string apiKey,
        Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials, this.configuration).DoRequestWithJsonContentAsync<NumberTransferResponse>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/accounts/{apiKey}/transfer-number"),
            request,
            AuthType.Basic
        );

    /// <inheritdoc />
    public NumberTransactionResponse UpdateANumber(UpdateNumberRequest request, Credentials creds = null)
    {
        var response = new ApiRequest(creds ?? this.Credentials, this.configuration)
            .DoPostRequestUrlContentFromObject<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    $"/number/update?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
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
        var response = await new ApiRequest(creds ?? this.Credentials, this.configuration)
            .DoPostRequestUrlContentFromObjectAsync<NumberTransactionResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest,
                    $"/number/update?{FormatQueryStringCredentials(creds ?? this.Credentials)}"),
                request,
                false
            );
        ValidateNumbersResponse(response);
        return response;
    }

    private static string FormatQueryStringCredentials(Credentials credentials) =>
        $"api_key={credentials.ApiKey}&api_secret={credentials.ApiSecret}";

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