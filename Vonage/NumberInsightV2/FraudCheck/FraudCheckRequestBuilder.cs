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
///     Represents a builder for Phone.
/// </summary>
public interface IBuilderForPhone
{
    /// <summary>
    ///     Sets the Phone.
    /// </summary>
    /// <param name="value">The phone.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithPhone(string value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<FraudCheckRequest>
{
    /// <summary>
    ///     Enables Fraud Score in the response.
    /// </summary>
    /// <returns>The builder.</returns>
    [Obsolete("FraudScore has been sunset on February 3rd 2025. SimSwap is the only available insight.")]
    IBuilderForOptional WithFraudScore();

    /// <summary>
    ///     Enables Sim Swap in the response.
    /// </summary>
    /// <returns>The builder.</returns>
    [Obsolete("FraudScore has been sunset on February 3rd 2025. SimSwap is the only available insight.")]
    IBuilderForOptional WithSimSwap();
}