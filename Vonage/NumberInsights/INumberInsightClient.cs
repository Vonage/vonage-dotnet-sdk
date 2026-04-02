using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.NumberInsights;

/// <summary>
///     Exposes Number Insight API features for retrieving information about phone numbers including carrier,
///     validity, roaming status, and caller identity.
/// </summary>
public interface INumberInsightClient
{
    /// <summary>
    ///     Retrieves basic information about a phone number including country and formatting details.
    ///     This is the fastest and lowest-cost lookup option.
    /// </summary>
    /// <param name="request">The request containing the phone number to look up. See <see cref="BasicNumberInsightRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="BasicInsightResponse"/> containing country, carrier prefix, and number formatting information.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new BasicNumberInsightRequest { Number = "447700900000" };
    /// var response = await client.NumberInsightClient.GetNumberInsightBasicAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/NumberInsights">More examples in the snippets repository</seealso>
    Task<BasicInsightResponse> GetNumberInsightBasicAsync(BasicNumberInsightRequest request, Credentials creds = null);

    /// <summary>
    ///     Retrieves standard information about a phone number including carrier, porting, and roaming details.
    ///     Provides more detail than basic lookup at additional cost.
    /// </summary>
    /// <param name="request">The request containing the phone number and optional CNAM lookup flag. See <see cref="StandardNumberInsightRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="StandardInsightResponse"/> containing carrier, porting status, roaming information, and optionally caller identity.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new StandardNumberInsightRequest { Number = "447700900000", Cnam = true };
    /// var response = await client.NumberInsightClient.GetNumberInsightStandardAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/NumberInsights">More examples in the snippets repository</seealso>
    Task<StandardInsightResponse> GetNumberInsightStandardAsync(StandardNumberInsightRequest request,
        Credentials creds = null);

    /// <summary>
    ///     Retrieves advanced information about a phone number including validity and reachability.
    ///     This synchronous method waits for the response. For high-volume usage, consider <see cref="GetNumberInsightAsynchronousAsync"/> to avoid timeouts.
    /// </summary>
    /// <param name="request">The request containing the phone number and lookup options. See <see cref="AdvancedNumberInsightRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>An <see cref="AdvancedInsightsResponse"/> containing validity, reachability, and all standard insight data.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new AdvancedNumberInsightRequest { Number = "447700900000" };
    /// var response = await client.NumberInsightClient.GetNumberInsightAdvancedAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/NumberInsights">More examples in the snippets repository</seealso>
    Task<AdvancedInsightsResponse> GetNumberInsightAdvancedAsync(AdvancedNumberInsightRequest request,
        Credentials creds = null);

    /// <summary>
    ///     Retrieves advanced information about a phone number asynchronously via webhook callback.
    ///     Recommended for production use to avoid timeouts on complex lookups.
    /// </summary>
    /// <param name="request">The request containing the phone number and callback URL. See <see cref="AdvancedNumberInsightAsynchronousRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>An <see cref="AdvancedInsightsAsynchronousResponse"/> confirming the request was accepted. Full results are delivered to the callback URL.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = new AdvancedNumberInsightAsynchronousRequest
    /// {
    ///     Number = "447700900000",
    ///     Callback = "https://example.com/webhooks/insight"
    /// };
    /// var response = await client.NumberInsightClient.GetNumberInsightAsynchronousAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/NumberInsights">More examples in the snippets repository</seealso>
    Task<AdvancedInsightsAsynchronousResponse> GetNumberInsightAsynchronousAsync(
        AdvancedNumberInsightAsynchronousRequest request,
        Credentials creds = null);
}