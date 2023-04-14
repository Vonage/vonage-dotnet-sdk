using Vonage.Common.Exceptions;

namespace Vonage.Verify;

public class VonageVerifyResponseException : VonageException
{
    public VerifyResponseBase Response { get; set; }

    public VonageVerifyResponseException(string message) : base(message)
    {
    }
}