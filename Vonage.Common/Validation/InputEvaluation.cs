using System;
using System.Linq;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Common.Validation;

public readonly struct InputEvaluation<T> where T : IVonageRequest
{
    private readonly T request;
    private InputEvaluation(T request) => this.request = request;

    public static InputEvaluation<T> Evaluate(T request) => new(request);

    public Result<T> WithRules(params Func<T, Result<T>>[] parsingRules)
    {
        var copy = this.request;
        var failures = parsingRules
            .Select(rule => rule(copy))
            .Select(result => result.Match(_ => string.Empty, failure => failure.GetFailureMessage()))
            .Where(failure => !string.IsNullOrEmpty(failure))
            .Select(ResultFailure.FromErrorMessage)
            .ToArray();
        return failures.Length == 0
            ? Result<T>.FromSuccess(this.request)
            : Result<T>.FromFailure(ParsingFailure.FromFailures(failures));
    }
}