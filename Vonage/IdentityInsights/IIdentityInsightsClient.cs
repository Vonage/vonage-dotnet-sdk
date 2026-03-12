#region
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.IdentityInsights.GetInsights;
#endregion

namespace Vonage.IdentityInsights;

/// <summary>
///     Exposes Identity Insights feature.
/// </summary>
public interface IIdentityInsightsClient
{
    /// <summary>
    ///     Provides multiple phone number insights (e.g., format validation, SIM swap status) in a single request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<GetInsightsResponse>> GetInsightsAsync(Result<GetInsightsRequest> request);
}