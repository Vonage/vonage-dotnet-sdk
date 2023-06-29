using System;
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

    /// <summary>
    ///     Creates a ParsingFailure from a list of failures.
    /// </summary>
    /// <param name="failures">The failures.</param>
    /// <returns>The parsing failure.</returns>
    public static ParsingFailure FromFailures(ResultFailure[] failures) => new(failures);

    /// <inheritdoc />
    public string GetFailureMessage() => "Parsing failed with the following errors: ";

    /// <inheritdoc />
    public Exception ToException() => new VonageException(this.GetFailureMessage());

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);
}