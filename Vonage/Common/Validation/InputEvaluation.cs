using System;
using System.Linq;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Common.Validation;

internal readonly struct InputEvaluation<T>
{
    private readonly T source;
    private InputEvaluation(T source) => this.source = source;
    
    public static InputEvaluation<T> Evaluate(T source) => new InputEvaluation<T>(source);
    
    public Result<T> WithRules(params Func<T, Result<T>>[] parsingRules)
    {
        var copy = this.source;
        var failures = parsingRules
            .Select(rule => rule(copy))
            .Select(result => result.Match(_ => string.Empty, failure => failure.GetFailureMessage()))
            .Where(failure => !string.IsNullOrEmpty(failure))
            .Select(ResultFailure.FromErrorMessage)
            .ToArray();
        return failures.Length == 0
            ? Result<T>.FromSuccess(this.source)
            : Result<T>.FromFailure(ParsingFailure.FromFailures(failures));
    }
}