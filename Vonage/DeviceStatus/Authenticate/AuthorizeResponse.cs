using System.Text.Json.Serialization;

namespace Vonage.DeviceStatus.Authenticate;

internal record AuthorizeResponse(
    [property: JsonPropertyName("auth_req_id")]
    string RequestId,
    [property: JsonPropertyName("expires_in")]
    int ExpiresIn,
    [property: JsonPropertyName("interval")]
    int Interval)
{
    internal GetTokenRequest BuildGetTokenRequest() => new GetTokenRequest(this.RequestId);
}