using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.VerifyCode;

namespace Vonage.VerifyV2;

/// <summary>
///     Exposes VerifyV2 features.
/// </summary>
public interface IVerifyV2Client
{
    /// <summary>
    ///     Requests a verification to be sent to a user.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The response.</returns>
    Task<Result<StartVerificationResponse>> StartVerificationAsync<T>(Result<T> request)
        where T : IStartVerificationRequest;

    /// <summary>
    ///     Allows a code to be checked against an existing Verification request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Unit>> VerifyCodeAsync(Result<VerifyCodeRequest> request);
}