using System;
using System.Linq;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;

namespace Vonage.Common.Failures;

/// <inheritdoc />
public readonly struct ParsingFailure : IResultFailure
{
    private readonly ResultFailure[] failures;

    private ParsingFailure(ResultFailure[] failures) => this.failures = failures;

    /// <inheritdoc />
    public Type Type => typeof(ParsingFailure);

    /// <inheritdoc />
    public override bool Equals(object obj) =>
        obj is ParsingFailure failure && this.GetHashCode().Equals(failure.GetHashCode());

    /// <inheritdoc />
    public override int GetHashCode() => this.GetFailureMessage().GetHashCode();

    /// <summary>
    ///     Creates a ParsingFailure from a list of failures.
    /// </summary>
    /// <param name="failures">The failures.</param>
    /// <returns>The parsing failure.</returns>
    public static ParsingFailure FromFailures(params ResultFailure[] failures) => new(failures);

    /// <inheritdoc />
    public string GetFailureMessage() =>
        $"Parsing failed with the following errors: {string.Join(", ", this.failures.Select(failure => failure.GetFailureMessage()))}.";

    /// <inheritdoc />
    public Exception ToException() => new VonageException(this.GetFailureMessage());

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);
}