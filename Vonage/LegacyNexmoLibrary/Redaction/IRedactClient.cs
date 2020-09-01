using Nexmo.Api.Request;

namespace Nexmo.Api.Redaction
{
    [System.Obsolete("The Nexmo.Api.Redaction.IRedactClient class is obsolete. " +
        "References to it should be switched to the new Vonage.Redaction.IRedactClient class.")]
    public interface IRedactClient
    {
        /// <summary>
        /// Redact a specific message
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        bool Redact(RedactRequest request, Credentials creds = null);
    }
}