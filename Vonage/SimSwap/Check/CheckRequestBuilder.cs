using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.SimSwap.Check;

internal class CheckRequestBuilder : IBuilderForPhoneNumber, IBuilderForOptional
{
    private const int MaximumPeriod = 2400;
    private const int MinimumPeriod = 1;
    private const int DefaultPeriod = 240;
    private string number;
    private int period = DefaultPeriod;
    
    /// <inheritdoc />
    public Result<CheckRequest> Create() =>
        PhoneNumber.Parse(this.number)
            .Map(phoneNumber => new CheckRequest
            {
                PhoneNumber = phoneNumber,
                Period = this.period,
            })
            .Map(InputEvaluation<CheckRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyAgeMinimumPeriod, VerifyMaximumPeriod));
    
    /// <inheritdoc />
    public IVonageRequestBuilder<CheckRequest> WithPeriod(int value) => new CheckRequestBuilder
    {
        number = this.number,
        period = value,
    };
    
    /// <inheritdoc />
    public IBuilderForOptional WithPhoneNumber(string value) => new CheckRequestBuilder
    {
        number = value,
        period = this.period,
    };
    
    private static Result<CheckRequest> VerifyAgeMinimumPeriod(CheckRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.Period, MinimumPeriod, nameof(request.Period));
    
    private static Result<CheckRequest> VerifyMaximumPeriod(CheckRequest request) =>
        InputValidation.VerifyLowerOrEqualThan(request, request.Period, MaximumPeriod, nameof(request.Period));
}

/// <summary>
///     Represents a builder for PhoneNumber.
/// </summary>
public interface IBuilderForPhoneNumber
{
    /// <summary>
    ///     Sets the phone number on the builder.
    /// </summary>
    /// <param name="value">The phone number.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithPhoneNumber(string value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<CheckRequest>
{
    /// <summary>
    ///     Sets the period on the builder.
    /// </summary>
    /// <param name="value">The period in hours to be checked for SIM swap.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<CheckRequest> WithPeriod(int value);
}