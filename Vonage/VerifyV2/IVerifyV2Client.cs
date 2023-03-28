using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Sms;

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
    Task<Result<StartVerificationResponse>> StartVerificationAsync(Result<StartSmsVerificationRequest> request);
}