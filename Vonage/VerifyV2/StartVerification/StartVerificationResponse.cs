using System;
using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.VerifyV2.VerifyCode;

namespace Vonage.VerifyV2.StartVerification;

/// <summary>
///     Represents the response from starting a verification request, containing the request identifier and optional Silent Auth check URL.
/// </summary>
/// <param name="RequestId">The unique identifier (UUID) of the verification request. Use this ID for subsequent operations like <see cref="IVerifyV2Client.VerifyCodeAsync"/> or <see cref="IVerifyV2Client.CancelAsync"/>.</param>
/// <param name="CheckUrl">The URL for Silent Authentication completion. Only returned when the first workflow is Silent Auth and the network supports it. The user's device must access this URL via cellular data to complete verification.</param>
public record StartVerificationResponse(
    [property: JsonPropertyOrder(0)] Guid RequestId,
    [property: JsonPropertyOrder(1)]
    [property: JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    Maybe<Uri> CheckUrl)
{
    /// <summary>
    ///     Creates a <see cref="VerifyCodeRequest"/> to validate the PIN code entered by the user.
    /// </summary>
    /// <param name="verificationCode">The PIN code provided by the user.</param>
    /// <returns>A <see cref="VerifyCodeRequest"/> ready to submit to <see cref="IVerifyV2Client.VerifyCodeAsync"/>.</returns>
    public Result<VerifyCodeRequest> BuildVerificationRequest(string verificationCode) => VerifyCodeRequest.Build()
        .WithRequestId(this.RequestId).WithCode(verificationCode).Create();
}