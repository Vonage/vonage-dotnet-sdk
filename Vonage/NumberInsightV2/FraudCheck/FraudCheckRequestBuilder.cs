#region
using System;
using System.Collections.Generic;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.NumberInsightV2.FraudCheck;

internal struct FraudCheckRequestBuilder : IBuilderForPhone, IBuilderForOptional
{
    private readonly HashSet<string> insights = ["sim_swap"];
    private Result<PhoneNumber> phone = default;

    public FraudCheckRequestBuilder()
    {
    }

    /// <inheritdoc />
    public Result<FraudCheckRequest> Create() =>
        this.phone.BiMap(this.ToRequest, ToParsingFailure)
            .Map(InputEvaluation<FraudCheckRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules());

    /// <inheritdoc />
    [Obsolete("FraudScore has been sunset on February 3rd 2025. SimSwap is the only available insight.")]
    public IBuilderForOptional WithFraudScore() => this;

    /// <inheritdoc />
    [Obsolete("FraudScore has been sunset on February 3rd 2025. SimSwap is the only available insight.")]
    public IBuilderForOptional WithSimSwap() => this;

    /// <inheritdoc />
    public IBuilderForOptional WithPhone(string value) => this with {phone = PhoneNumber.Parse(value)};

    private static IResultFailure ToParsingFailure(IResultFailure failure) =>
        ParsingFailure.FromFailures(ResultFailure.FromErrorMessage(failure.GetFailureMessage()));

    private FraudCheckRequest ToRequest(PhoneNumber number) =>
        new FraudCheckRequest
        {
            Phone = number, Insights = this.insights,
        };
}

/// <summary>
///     Builder interface for setting the phone number in a <see cref="FraudCheckRequest"/>.
/// </summary>
public interface IBuilderForPhone
{
    /// <summary>
    ///     Sets the phone number to check for fraud risk.
    /// </summary>
    /// <param name="value">The phone number in E.164 format without leading + or 00 (e.g., "447700900000").</param>
    /// <returns>The builder for configuring optional parameters.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithPhone("447700900000")
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithPhone(string value);
}

/// <summary>
///     Builder interface for configuring optional insight types in a <see cref="FraudCheckRequest"/>.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<FraudCheckRequest>
{
    /// <summary>
    ///     Includes fraud score analysis in the response, providing a risk score (0-100) and recommended action.
    /// </summary>
    /// <returns>The builder for further configuration or request creation.</returns>
    [Obsolete("FraudScore has been sunset on February 3rd 2025. SimSwap is the only available insight.")]
    IBuilderForOptional WithFraudScore();

    /// <summary>
    ///     Includes SIM swap detection in the response, indicating if the SIM was changed in the last 7 days.
    /// </summary>
    /// <returns>The builder for further configuration or request creation.</returns>
    [Obsolete("FraudScore has been sunset on February 3rd 2025. SimSwap is the only available insight.")]
    IBuilderForOptional WithSimSwap();
}