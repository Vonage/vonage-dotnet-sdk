using System.Text.Json.Serialization;

namespace Vonage.SimSwap.Authenticate;

internal record AuthorizeResponse(
    [property: JsonPropertyName("auth_req_id")]
    string RequestId,
    [property: JsonPropertyName("expires_in")]
    string ExpiresIn,
    [property: JsonPropertyName("interval")]
    string Interval)
{
    internal GetTokenRequest BuildGetTokenRequest() => new GetTokenRequest(this.RequestId);
}