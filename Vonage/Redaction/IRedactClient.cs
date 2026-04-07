using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Redaction;

/// <summary>
///     Exposes Redact API features for removing personal data from the Vonage platform to comply with GDPR and similar
///     privacy regulations.
/// </summary>
public interface IRedactClient
{
    /// <summary>
    ///     Redacts a specific transaction record from the Vonage platform.
    /// </summary>
    /// <param name="request">The redaction request containing the transaction ID, product, and type. See <see cref="RedactRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns><c>true</c> if the redaction was successful.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new RedactRequest
    /// {
    ///     Id = "00A0B0C0",
    ///     Product = RedactionProduct.Sms,
    ///     Type = RedactionType.Outbound
    /// };
    /// var success = await client.RedactClient.RedactAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Redact">More examples in the snippets repository</seealso>
    Task<bool> RedactAsync(RedactRequest request, Credentials creds = null);
}