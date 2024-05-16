using Vonage.Common;
using Vonage.Common.Monads;

namespace Vonage.SimSwap.Authenticate;

/// <summary>
///     Represents a request to authenticate towards SimSwap API.
/// </summary>
public readonly struct AuthenticateRequest
{
    /// <summary>
    ///     Parses the input into an AuthenticateRequest.
    /// </summary>
    /// <param name="number">The phone number.</param>
    /// <returns>Success if the input matches all requirements. Failure otherwise.</returns>
    public static Result<AuthenticateRequest> Parse(string number) =>
        PhoneNumber.Parse(number).Map(phoneNumber => new AuthenticateRequest
        {
            PhoneNumber = phoneNumber,
        });
    
    /// <summary>
    ///     Subscriber number in E.164 format (starting with country code). Optionally prefixed with '+'.
    /// </summary>
    public PhoneNumber PhoneNumber { get; private init; }
    
    internal AuthorizeRequest BuildAuthorizeRequest() => new AuthorizeRequest(this.PhoneNumber);
}