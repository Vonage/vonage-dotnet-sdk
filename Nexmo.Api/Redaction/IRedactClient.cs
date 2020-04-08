using Nexmo.Api.Request;

namespace Nexmo.Api.Redaction
{
    public interface IRedactClient
    {
        bool Redact(RedactRequest request, Credentials creds = null);
    }
}