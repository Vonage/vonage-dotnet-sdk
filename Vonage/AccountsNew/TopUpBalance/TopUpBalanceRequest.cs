using System.Net;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.AccountsNew.TopUpBalance;

/// <summary>
///     Represents a request to top up an account balance when auto-reload is enabled.
/// </summary>
[Builder]
public readonly partial struct TopUpBalanceRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the transaction reference from when auto-reload was enabled on the account.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithTransactionReference("8ef2447e69604f642ae59363aa5f781b")
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public string TransactionReference { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, "/account/top-up")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(this.GetUrlEncoded(), Encoding.UTF8, "application/x-www-form-urlencoded");

    private string GetUrlEncoded() => $"trx={WebUtility.UrlEncode(this.TransactionReference)}";

    [ValidationRule]
    internal static Result<TopUpBalanceRequest> VerifyTransactionReference(TopUpBalanceRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.TransactionReference,
            nameof(request.TransactionReference));
}
