#region
using System;
using System.Linq;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Validation;

internal readonly struct InputEvaluation<T>
{
    private readonly T source;
    private InputEvaluation(T source) => this.source = source;

    public static InputEvaluation<T> Evaluate(T source) => new InputEvaluation<T>(source);

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