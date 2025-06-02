#region
using System.Net;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
#endregion

namespace Vonage.SimSwap.Authenticate;

internal record GetTokenRequest(string RequestId) : IVonageRequest
{
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, "oauth2/token")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new StringContent(this.GetUrlEncoded(), Encoding.UTF8, "application/x-www-form-urlencoded");

    private string GetUrlEncoded()
    {
        var builder = new StringBuilder();
        builder.Append("auth_req_id=");
        builder.Append(WebUtility.UrlEncode(this.RequestId));
        builder.Append("&grant_type=urn:openid:params:grant-type:ciba");
        return builder.ToString();
    }
}