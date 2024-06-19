using System.Text.Json.Serialization;

namespace Vonage.NumberVerification.Verify;

internal record VerifyResponse(
    [property: JsonPropertyName("devicePhoneNumberVerified")]
    bool Verified);