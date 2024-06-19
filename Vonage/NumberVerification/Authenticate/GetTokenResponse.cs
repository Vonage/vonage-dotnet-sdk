using System.Text.Json.Serialization;

namespace Vonage.NumberVerification.Authenticate;

internal record GetTokenResponse(
    [property: JsonPropertyName("access_token")]
    string AccessToken,
    [property: JsonPropertyName("token_type")]
    string TokenType,
    [property: JsonPropertyName("expires_in")]
    int ExpiresIn);