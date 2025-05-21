using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;

namespace Vonage.SubAccounts.CreateSubAccount;

/// <inheritdoc />
public readonly struct CreateSubAccountRequest : IVonageRequest
{
    /// <summary>
    ///     Unique primary account ID.
    /// </summary>
    private string ApiKey { get; init; }

    /// <summary>
    ///     Name of the subaccount.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Name { get; internal init; }

    /// <summary>
    ///     Secret of the subaccount.
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Secret { get; internal init; }

    /// <summary>
    ///     Flag showing if balance is shared with primary account.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonPropertyName("use_primary_account_balance")]
    public bool UsePrimaryAccountBalance { get; internal init; }

    /// <summary>
    ///     Initializes a builder for CreateRoomRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForName Build() => new CreateSubAccountRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/accounts/{this.ApiKey}/subaccounts";

    private StringContent GetRequestContent() => new(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
        Encoding.UTF8,
        "application/json");

    internal CreateSubAccountRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};
}