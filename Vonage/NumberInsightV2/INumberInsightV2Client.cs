using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.NumberInsightV2.FraudCheck;

namespace Vonage.NumberInsightV2;

/// <summary>
///     Exposes Number Insight V2 features.
/// </summary>
public interface INumberInsightV2Client
{
    /// <summary>
    ///     Performs a fraud check request with a phone number.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>A result indicating if the request whether succeeded or failed.</returns>
    Task<Result<FraudCheckResponse>> PerformFraudCheckAsync(Result<FraudCheckRequest> request);
}