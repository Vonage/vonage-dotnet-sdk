using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;

namespace Vonage.SubAccounts.UpdateSubAccount;

/// <inheritdoc />
public readonly struct UpdateSubAccountRequest : IVonageRequest
{
    /// <summary>
    ///     Unique primary account ID.
    /// </summary>
    private string ApiKey { get; init; }

    /// <summary>
    ///     Name of the subaccount.
    /// </summary>
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     Unique SubAccount ID.
    /// </summary>
    public string SubAccountKey { get; internal init; }

    /// <summary>
    ///     Indicates if the account is suspended.
    /// </summary>
    public Maybe<bool> Suspended { get; internal init; }

    /// <summary>
    ///     Flag showing if balance is shared with primary account.
    /// </summary>
    public Maybe<bool> UsePrimaryAccountBalance { get; internal init; }

    /// <summary>
    ///     Initializes a builder for UpdateSubAccountRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForSubAccountKey Build() => new UpdateSubAccountRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/accounts/{this.ApiKey}/subaccounts/{this.SubAccountKey}";

    private StringContent GetRequestContent()
    {
        var values = new Dictionary<string, object>();
        this.Name.IfSome(value => values.Add("name", value));
        this.UsePrimaryAccountBalance.IfSome(value => values.Add("use_primary_account_balance", value));
        this.Suspended.IfSome(value => values.Add("suspended", value));
        return new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(values), Encoding.UTF8,
            "application/json");
    }

    internal UpdateSubAccountRequest WithApiKey(string primaryAccountKey) => this with {ApiKey = primaryAccountKey};
}