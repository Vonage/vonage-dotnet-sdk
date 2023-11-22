using System;
using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.VerifyV2.VerifyCode;

namespace Vonage.VerifyV2.StartVerification;

/// <summary>
///     Represents a response of a StartVerification request.
/// </summary>
/// <param name="RequestId">The Id of the request.</param>
/// <param name="CheckUrl">URL for Silent Auth Verify workflow completion (only shows if using Silent Auth).</param>
public record StartVerificationResponse(
    [property: JsonPropertyOrder(0)] Guid RequestId,
    [property: JsonPropertyOrder(1)]
    [property: JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    Maybe<Uri> CheckUrl)
{
    /// <summary>
    ///     Builds a VerifyCodeRequest based on the current request.
    /// </summary>
    /// <param name="verificationCode">The verification code.</param>
    /// <returns>A request to verify the code for the current process.</returns>
    public Result<VerifyCodeRequest> BuildVerificationRequest(string verificationCode) => VerifyCodeRequest.Build()
        .WithRequestId(this.RequestId).WithCode(verificationCode).Create();
}