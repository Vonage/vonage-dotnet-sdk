using System.Collections.Generic;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.NumberInsightV2.FraudCheck;

internal class FraudCheckRequestBuilder : IBuilderForPhone, IBuilderForOptional
{
    private readonly HashSet<string> insights = new();
    private Result<PhoneNumber> phone;

    /// <inheritdoc />
    public Result<FraudCheckRequest> Create() =>
        this.phone.Match(this.ToSuccess, ToFailure)
            .Map(InputEvaluation<FraudCheckRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyInsights));

    /// <inheritdoc />
    public IBuilderForOptional WithFraudScore()
    {
        this.insights.Add("fraud_score");
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithPhone(string value)
    {
        this.phone = PhoneNumber.Parse(value);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithSimSwap()
    {
        this.insights.Add("sim_swap");
        return this;
    }

    private static Result<FraudCheckRequest> ToFailure(IResultFailure failure) =>
        Result<FraudCheckRequest>.FromFailure(
            ParsingFailure.FromFailures(ResultFailure.FromErrorMessage(failure.GetFailureMessage())));

    private Result<FraudCheckRequest> ToSuccess(PhoneNumber number) =>
        Result<FraudCheckRequest>.FromSuccess(new FraudCheckRequest
        {
            Phone = number, Insights = this.insights,
        });

    private static Result<FraudCheckRequest> VerifyInsights(FraudCheckRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Insights, nameof(request.Insights));
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
    IBuilderForOptional WithFraudScore();

    /// <summary>
    ///     Enables Sim Swap in the response.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithSimSwap();
}