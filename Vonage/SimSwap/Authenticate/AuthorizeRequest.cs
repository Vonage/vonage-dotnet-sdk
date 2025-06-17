#region
using System.Net;
using System.Net.Http;
using System.Text;
using Vonage.Common;
using Vonage.Common.Client;
#endregion

namespace Vonage.SimSwap.Authenticate;

internal record AuthorizeRequest(PhoneNumber Number, string Scope) : IVonageRequest
{
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, "oauth2/bc-authorize")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new StringContent(this.GetUrlEncoded(), Encoding.UTF8, "application/x-www-form-urlencoded");

    private string GetUrlEncoded()
    {
        var builder = new StringBuilder();
        builder.Append("login_hint=tel:");
        builder.Append(WebUtility.UrlEncode(this.Number.NumberWithInternationalIndicator));
        builder.Append("&scope=openid");
        builder.Append(WebUtility.UrlEncode($" {this.Scope}"));
        return builder.ToString();
    }
}