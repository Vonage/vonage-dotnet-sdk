#region
using System;
using System.Linq;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Validation;

/// <summary>
///     Evaluates input against multiple validation rules and collects all failures.
/// </summary>
/// <typeparam name="T">The type of the input being validated.</typeparam>
/// <remarks>
///     Unlike standard validation that stops at the first failure, this evaluator runs all rules
///     and aggregates failures into a <see cref="ParsingFailure" /> for comprehensive error reporting.
/// </remarks>
internal readonly struct InputEvaluation<T>
{
    private readonly T source;

    private InputEvaluation(T source) => this.source = source;

    /// <summary>
    ///     Creates a new evaluation instance for the specified input.
    /// </summary>
    /// <param name="source">The input value to evaluate.</param>
    /// <returns>An <see cref="InputEvaluation{T}" /> ready to apply validation rules.</returns>
    public static InputEvaluation<T> Evaluate(T source) => new InputEvaluation<T>(source);

    /// <summary>
    ///     Applies all validation rules to the input and returns a result.
    /// </summary>
    /// <param name="parsingRules">The validation rules to apply. Each rule returns Success if valid, Failure otherwise.</param>
    /// <returns>
    ///     Success with the original input if all rules pass, or Failure with a <see cref="ParsingFailure" />
    ///     containing all validation errors if any rules fail.
    /// </returns>
    public Result<T> WithRules(params Func<T, Result<T>>[] parsingRules)
    {
        var result = parsingRules.Aggregate(Result<T>.FromSuccess(this.source), (result, rule) => result.Bind(rule));
        return result.IsSuccess ? result : this.ListFailures(parsingRules);
    }

    private Result<T> ListFailures(Func<T, Result<T>>[] parsingRules)
    {
        var copy = this.source;
        var failures = parsingRules
            .Select(rule => rule(copy))
            .Select(result => result.Match(_ => string.Empty, failure => failure.GetFailureMessage()))
            .Where(failure => !string.IsNullOrEmpty(failure))
            .Select(ResultFailure.FromErrorMessage)
            .ToArray();
        return Result<T>.FromFailure(ParsingFailure.FromFailures(failures));
    }
}