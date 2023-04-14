using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Redaction;

public interface IRedactClient
{
    /// <summary>
    /// Redact a specific message
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<bool> RedactAsync(RedactRequest request, Credentials creds = null);

    /// <summary>
    /// Redact a specific message
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    bool Redact(RedactRequest request, Credentials creds = null);
}