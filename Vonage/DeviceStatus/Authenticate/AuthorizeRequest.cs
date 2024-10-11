using System.Net;
using System.Net.Http;
using System.Text;
using Vonage.Common;
using Vonage.Common.Client;

namespace Vonage.DeviceStatus.Authenticate;

internal record AuthorizeRequest(PhoneNumber Number, string Scope) : IVonageRequest
{
    public string GetEndpointPath() => "oauth2/bc-authorize";
    
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();
    
    private string GetUrlEncoded()
    {
        var builder = new StringBuilder();
        builder.Append("login_hint=tel:");
        builder.Append(WebUtility.UrlEncode(this.Number.NumberWithInternationalIndicator));
        builder.Append("&scope=openid");
        builder.Append(WebUtility.UrlEncode($" {this.Scope}"));
        return builder.ToString();
    }
    
    private StringContent GetRequestContent() =>
        new StringContent(this.GetUrlEncoded(), Encoding.UTF8, "application/x-www-form-urlencoded");
}