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

/// <summary>
///     Represents a request to transfer a phone number from one account to another within the primary account's hierarchy.
/// </summary>
[Builder]
public readonly partial struct TransferNumberRequest : IVonageRequest
{
    private const int CountryLength = 2;

    private string ApiKey { get; init; }

    /// <summary>
    ///     Sets the two-character country code in ISO 3166-1 alpha-2 format (e.g., "GB", "US").
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCountry("GB")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(3)]
    [Mandatory(3)]
    public string Country { get; internal init; }

    /// <summary>
    ///     Sets the API key of the account to transfer the number from.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithFrom("7c9738e6")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(0)]
    [Mandatory(0)]
    public string From { get; internal init; }

    /// <summary>
    ///     Sets the phone number to transfer.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithNumber("447700900000")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(2)]
    [Mandatory(2)]
    public string Number { get; internal init; }

    /// <summary>
    ///     Sets the API key of the account to transfer the number to.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithTo("ad6dc56f")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(1)]
    [Mandatory(1)]
    public string To { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/accounts/{this.ApiKey}/transfer-number")
        .WithContent(this.GetRequestContent())
        .Build();

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