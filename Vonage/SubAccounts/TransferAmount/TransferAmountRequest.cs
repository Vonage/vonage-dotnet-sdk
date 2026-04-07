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

/// <summary>
///     Represents a request to transfer balance or credit between the primary account and a subaccount.
/// </summary>
[Builder]
public readonly partial struct TransferAmountRequest : IVonageRequest
{
    internal const string BalanceTransfer = "balance-transfers";
    internal const string CreditTransfer = "credit-transfers";
    private string ApiKey { get; init; }

    private string Endpoint { get; init; }

    /// <summary>
    ///     Sets the amount to be transferred. Must be a non-negative value.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithAmount(123.45m)
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(2)]
    [Mandatory(2)]
    public decimal Amount { get; internal init; }

    /// <summary>
    ///     Sets the API key of the account to transfer from.
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
    ///     Sets a reference for the transfer. This reference is added to the audit log.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithReference("Monthly credit allocation")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<string> Reference { get; internal init; }

    /// <summary>
    ///     Sets the API key of the account to transfer to.
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
        .Initialize(HttpMethod.Post, $"/accounts/{this.ApiKey}/{this.Endpoint}")
        .WithContent(this.GetRequestContent())
        .Build();

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