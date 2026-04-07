using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Verify;

/// <summary>
///     Exposes methods for verifying phone numbers using PIN codes sent via SMS or voice calls (Verify v1 API).
/// </summary>
public interface IVerifyClient
{
    /// <summary>
    ///     Generates and sends a verification PIN code to the user's phone number via SMS or voice call.
    /// </summary>
    /// <param name="request">The verification request containing the phone number, brand name, and optional workflow settings.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>
    ///     A <see cref="VerifyResponse"/> containing the request ID to use in subsequent check calls.
    ///     Throws <see cref="VonageVerifyResponseException"/> if the request fails.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new VerifyRequest { Number = "447700900000", Brand = "MyApp" };
    /// var response = await client.VerifyRequestAsync(request);
    /// var requestId = response.RequestId;
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Verify">More examples in the snippets repository</seealso>
    Task<VerifyResponse> VerifyRequestAsync(VerifyRequest request, Credentials creds = null);

    /// <summary>
    ///     Validates the PIN code entered by the user against the verification request.
    /// </summary>
    /// <param name="request">The check request containing the request ID and the user-entered PIN code.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>
    ///     A <see cref="VerifyCheckResponse"/> indicating whether the code was valid and the cost incurred.
    ///     Throws <see cref="VonageVerifyResponseException"/> if the check fails.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new VerifyCheckRequest { RequestId = "abcd1234", Code = "1234" };
    /// var response = await client.VerifyCheckAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Verify">More examples in the snippets repository</seealso>
    Task<VerifyCheckResponse> VerifyCheckAsync(VerifyCheckRequest request, Credentials creds = null);

    /// <summary>
    ///     Retrieves the current status and details of a verification request.
    /// </summary>
    /// <param name="request">The search request containing the request ID to look up.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>
    ///     A <see cref="VerifySearchResponse"/> containing the verification status, events, and check attempts.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new VerifySearchRequest { RequestId = "abcd1234" };
    /// var response = await client.VerifySearchAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Verify">More examples in the snippets repository</seealso>
    Task<VerifySearchResponse> VerifySearchAsync(VerifySearchRequest request, Credentials creds = null);

    /// <summary>
    ///     Controls an in-progress verification request by cancelling it or triggering the next event.
    /// </summary>
    /// <param name="request">The control request containing the request ID and command ("cancel" or "trigger_next_event").</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>
    ///     A <see cref="VerifyControlResponse"/> confirming the command was executed.
    ///     Throws <see cref="VonageVerifyResponseException"/> if the control command fails.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new VerifyControlRequest { RequestId = "abcd1234", Cmd = "cancel" };
    /// var response = await client.VerifyControlAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Verify">More examples in the snippets repository</seealso>
    Task<VerifyControlResponse> VerifyControlAsync(VerifyControlRequest request, Credentials creds = null);

    /// <summary>
    ///     Generates and sends a verification PIN code for PSD2 payment authorization, displaying the payee and amount to the user.
    /// </summary>
    /// <param name="request">The PSD2 request containing the phone number, payee name, and payment amount in Euros.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>
    ///     A <see cref="VerifyResponse"/> containing the request ID to use in subsequent check calls.
    ///     Throws <see cref="VonageVerifyResponseException"/> if the request fails.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new Psd2Request { Number = "447700900000", Payee = "Acme Corp", Amount = 99.99 };
    /// var response = await client.VerifyRequestWithPSD2Async(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Verify">More examples in the snippets repository</seealso>
    Task<VerifyResponse> VerifyRequestWithPSD2Async(Psd2Request request, Credentials creds = null);
}