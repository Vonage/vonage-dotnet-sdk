using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using EnumsNET;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.AccountsNew.ChangeAccountSettings;

/// <summary>
///     Represents a request to update the default webhook callback URLs and settings for an account.
/// </summary>
[Builder]
public readonly partial struct ChangeAccountSettingsRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the default webhook URL for inbound SMS messages.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithInboundSmsCallbackUrl("https://example.com/webhooks/inbound-sms")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> InboundSmsCallbackUrl { get; internal init; }

    /// <summary>
    ///     Sets the default webhook URL for delivery receipts.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithDeliveryReceiptCallbackUrl("https://example.com/webhooks/delivery-receipt")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> DeliveryReceiptCallbackUrl { get; internal init; }

    /// <summary>
    ///     Sets the HTTP method used when making requests to the callback URLs.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithHttpForwardMethod(HttpForwardMethod.PostJson)
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<HttpForwardMethod> HttpForwardMethod { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, "/account/settings")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(this.GetUrlEncoded(), Encoding.UTF8, "application/x-www-form-urlencoded");

    private string GetUrlEncoded()
    {
        var parts = new List<string>();
        this.InboundSmsCallbackUrl.IfSome(v => parts.Add($"moCallBackUrl={System.Net.WebUtility.UrlEncode(v)}"));
        this.DeliveryReceiptCallbackUrl.IfSome(v => parts.Add($"drCallBackUrl={System.Net.WebUtility.UrlEncode(v)}"));
        this.HttpForwardMethod.IfSome(v => parts.Add($"httpForwardMethod={v.AsString(EnumFormat.Description)}"));
        return string.Join("&", parts);
    }
}
