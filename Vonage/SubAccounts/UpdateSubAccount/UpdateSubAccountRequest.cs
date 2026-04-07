#region
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
#endregion

namespace Vonage.SubAccounts.UpdateSubAccount;

/// <summary>
///     Represents a request to update the properties of an existing subaccount.
/// </summary>
public readonly struct UpdateSubAccountRequest : IVonageRequest
{
    private string ApiKey { get; init; }

    /// <summary>
    ///     The new name for the subaccount; limited to 80 characters.
    /// </summary>
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     The unique API key of the subaccount to update.
    /// </summary>
    public string SubAccountKey { get; internal init; }

    /// <summary>
    ///     Indicates whether the subaccount should be suspended.
    /// </summary>
    public Maybe<bool> Suspended { get; internal init; }

    /// <summary>
    ///     Indicates whether the subaccount shares the primary account's balance.
    /// </summary>
    public Maybe<bool> UsePrimaryAccountBalance { get; internal init; }

    /// <summary>
    ///     Initializes a builder for creating an <see cref="UpdateSubAccountRequest"/>.
    /// </summary>
    /// <returns>The builder to configure the subaccount update.</returns>
    public static IBuilderForSubAccountKey Build() => new UpdateSubAccountRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(new HttpMethod("PATCH"), $"/accounts/{this.ApiKey}/subaccounts/{this.SubAccountKey}")
        .WithContent(this.GetRequestContent())
        .Build();

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