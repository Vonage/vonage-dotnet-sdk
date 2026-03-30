#region
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.IdentityInsights.GetInsights;
#endregion

namespace Vonage.IdentityInsights;

/// <summary>
///     Client for retrieving identity insights about phone numbers including format validation,
///     carrier information, and SIM swap detection.
/// </summary>
public interface IIdentityInsightsClient
{
    /// <summary>
    ///     Retrieves multiple phone number insights (format validation, carrier information, SIM swap status) in a single request.
    /// </summary>
    /// <param name="request">The request containing the phone number and desired insights.</param>
    /// <returns>
    ///     Success with a <see cref="GetInsightsResponse"/> containing the requested insights,
    ///     or Failure if the request is invalid or the API call fails.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetInsightsRequest.Build()
    ///     .WithPhoneNumber("+14155552671")
    ///     .WithFormat()
    ///     .WithSimSwap(new SimSwapRequest(24))
    ///     .Create();
    /// var response = await client.GetInsightsAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/IdentityInsights">More examples in the snippets repository</seealso>
    Task<Result<GetInsightsResponse>> GetInsightsAsync(Result<GetInsightsRequest> request);

    /// <summary>
    ///     Returns a new client configured to use the Identity Insights API in the EU region.
    /// </summary>
    /// <returns>A new <see cref="IIdentityInsightsClient"/> instance targeting the EU region.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var euClient = client.WithEuRegion();
    /// var response = await euClient.GetInsightsAsync(request);
    /// ]]></code>
    /// </example>
    IIdentityInsightsClient WithEuRegion();
}