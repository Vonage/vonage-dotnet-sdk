#region
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.SubAccounts.TransferAmount;

/// <inheritdoc />
[Builder]
public readonly partial struct TransferAmountRequest : IVonageRequest
{
    internal const string BalanceTransfer = "balance-transfers";
    internal const string CreditTransfer = "credit-transfers";
    private string ApiKey { get; init; }

    private string Endpoint { get; init; }

    /// <summary>
    ///     The amount to be transferred.
    /// </summary>
    [JsonPropertyOrder(2)]
    [Mandatory(2)]
    public decimal Amount { get; internal init; }

    /// <summary>
    ///     Account the credit is transferred from.
    /// </summary>
    [JsonPropertyOrder(0)]
    [Mandatory(0)]
    public string From { get; internal init; }

    /// <summary>
    ///     Reference for the credit transfer
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<string> Reference { get; internal init; }

    /// <summary>
    ///     Account the credit is transferred to.
    /// </summary>
    [JsonPropertyOrder(1)]
    [Mandatory(1)]
    public string To { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/accounts/{this.ApiKey}/{this.Endpoint}")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<TransferAmountRequest> VerifyAmount(TransferAmountRequest amountRequest) =>
        InputValidation.VerifyNotNegative(amountRequest, amountRequest.Amount, nameof(amountRequest.Amount));

    [ValidationRule]
    internal static Result<TransferAmountRequest> VerifyFrom(TransferAmountRequest amountRequest) =>
        InputValidation.VerifyNotEmpty(amountRequest, amountRequest.From, nameof(amountRequest.From));

    [ValidationRule]
    internal static Result<TransferAmountRequest> VerifyTo(TransferAmountRequest amountRequest) =>
        InputValidation.VerifyNotEmpty(amountRequest, amountRequest.To, nameof(amountRequest.To));

    internal TransferAmountRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};

    internal TransferAmountRequest WithEndpoint(string endpoint) => this with {Endpoint = endpoint};
}