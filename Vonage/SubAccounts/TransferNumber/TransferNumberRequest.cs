#region
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.SubAccounts.TransferNumber;

/// <inheritdoc />
[Builder]
public readonly partial struct TransferNumberRequest : IVonageRequest
{
    private const int CountryLength = 2;

    /// <summary>
    ///     Unique primary account ID.
    /// </summary>
    private string ApiKey { get; init; }

    /// <summary>
    ///     The two character country code in ISO 3166-1 alpha-2 format
    /// </summary>
    [JsonPropertyOrder(3)]
    [Mandatory(3)]
    public string Country { get; internal init; }

    /// <summary>
    ///     Account the number is transferred from
    /// </summary>
    [JsonPropertyOrder(0)]
    [Mandatory(0)]
    public string From { get; internal init; }

    /// <summary>
    ///     Number transferred
    /// </summary>
    [JsonPropertyOrder(2)]
    [Mandatory(2)]
    public string Number { get; internal init; }

    /// <summary>
    ///     Account the number is transferred to
    /// </summary>
    [JsonPropertyOrder(1)]
    [Mandatory(1)]
    public string To { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/accounts/{this.ApiKey}/transfer-number";

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<TransferNumberRequest> VerifyCountry(TransferNumberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Country, nameof(request.Country))
            .Bind(VerifyCountryLength);

    internal static Result<TransferNumberRequest> VerifyCountryLength(TransferNumberRequest request) =>
        InputValidation.VerifyLength(request, request.Country, CountryLength, nameof(request.Country));

    [ValidationRule]
    internal static Result<TransferNumberRequest> VerifyFrom(TransferNumberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.From, nameof(request.From));

    [ValidationRule]
    internal static Result<TransferNumberRequest> VerifyNumber(TransferNumberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Number, nameof(request.Number));

    [ValidationRule]
    internal static Result<TransferNumberRequest> VerifyTo(TransferNumberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.To, nameof(request.To));

    internal TransferNumberRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};
}