using System;

namespace Vonage.VerifyV2.StartVerification;

/// <summary>
///     Represents a response of a StartVerification request.
/// </summary>
/// <param name="RequestId">The Id of the request.</param>
public record StartVerificationResponse(Guid RequestId);