using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.SubAccounts.CreateSubAccount;

/// <inheritdoc />
public readonly struct CreateSubAccountRequest : IVonageRequest
{
    /// <summary>
    ///     Name of the subaccount.
    /// </summary>
    public string Name { get; internal init; }

    /// <summary>
    ///     Secret of the subaccount.
    /// </summary>
    public Maybe<string> Secret { get; internal init; }

    /// <summary>
    ///     Flag showing if balance is shared with primary account.
    /// </summary>
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
    public string GetEndpointPath() => $"/accounts/{this.ApiKey}/subaccounts/";

    private StringContent GetRequestContent()
    {
        var values = new Dictionary<string, object>
        {
            {"name", this.Name},
            {"use_primary_account_balance", this.UsePrimaryAccountBalance},
        };
        this.Secret.IfSome(value => values.Add("secret", value));
        return new StringContent(JsonSerializer.BuildWithSnakeCase().SerializeObject(values), Encoding.UTF8,
            "application/json");
    }

    internal CreateSubAccountRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};

    /// <summary>
    ///     Unique primary account ID.
    /// </summary>
    internal string ApiKey { get; init; }
}