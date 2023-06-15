using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.SubAccounts.TransferCredit;

/// <inheritdoc />
public readonly struct TransferCreditRequest : IVonageRequest
{
    /// <summary>
    ///     Unique primary account ID.
    /// </summary>
    private string ApiKey { get; init; }

    /// <summary>
    ///     The amount to be transferred.
    /// </summary>
    [JsonPropertyOrder(2)]
    public decimal Amount { get; internal init; }

    /// <summary>
    ///     Account the credit is transferred from.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string From { get; internal init; }

    /// <summary>
    ///     Reference for the credit transfer
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Reference { get; internal init; }

    /// <summary>
    ///     Account the credit is transferred to.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string To { get; internal init; }

    /// <summary>
    ///     Initializes a builder for TransferCreditRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForFrom Build() => new TransferCreditRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/accounts/{this.ApiKey}/credit-transfers/";

    private StringContent GetRequestContent() =>
        new(JsonSerializer.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    internal TransferCreditRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};
}