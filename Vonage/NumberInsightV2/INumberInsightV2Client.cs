using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.NumberInsightV2.FraudCheck;

namespace Vonage.NumberInsightV2;

/// <summary>
///     Exposes Number Insight V2 API features for fraud detection and phone number verification.
/// </summary>
public interface INumberInsightV2Client
{
    /// <summary>
    ///     Performs a fraud check on a phone number to detect potential fraud risk and SIM swap activity.
    /// </summary>
    /// <param name="request">The fraud check request containing the phone number and requested insights. Use <see cref="FraudCheckRequest.Build"/> to create.</param>
    /// <returns>
    ///     Success with a <see cref="FraudCheckResponse"/> containing fraud score and/or SIM swap data,
    ///     or failure with error details.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = FraudCheckRequest.Build()
    ///     .WithPhone("447700900000")
    ///     .Create();
    /// var response = await client.NumberInsightV2Client.PerformFraudCheckAsync(request);
    /// ]]></code>
    /// </example>
    Task<Result<FraudCheckResponse>> PerformFraudCheckAsync(Result<FraudCheckRequest> request);
}