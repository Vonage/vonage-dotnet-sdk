using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;

namespace Vonage.SubAccounts.TransferNumber;

/// <inheritdoc />
public readonly struct TransferNumberRequest : IVonageRequest
{
    /// <summary>
    ///     Unique primary account ID.
    /// </summary>
    private string ApiKey { get; init; }

    /// <summary>
    ///     The two character country code in ISO 3166-1 alpha-2 format
    /// </summary>
    [JsonPropertyOrder(3)]
    public string Country { get; internal init; }

    /// <summary>
    ///     Account the number is transferred from
    /// </summary>
    [JsonPropertyOrder(0)]
    public string From { get; internal init; }

    /// <summary>
    ///     Number transferred
    /// </summary>
    [JsonPropertyOrder(2)]
    public string Number { get; internal init; }

    /// <summary>
    ///     Account the number is transferred to
    /// </summary>
    [JsonPropertyOrder(1)]
    public string To { get; internal init; }

    /// <summary>
    ///     Initializes a builder for TransferNumberRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForFrom Build() => new TransferNumberRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/accounts/{this.ApiKey}/transfer-number/";

    private StringContent GetRequestContent() => new(JsonSerializer.BuildWithSnakeCase().SerializeObject(this),
        Encoding.UTF8,
        "application/json");

    internal TransferNumberRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};
}