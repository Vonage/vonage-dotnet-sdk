using System;
using Vonage.Common.Monads;

namespace Vonage.Common.Failures;

/// <inheritdoc />
public readonly struct SystemFailure : IResultFailure
{
    private readonly Exception exception;
    private SystemFailure(Exception exception) => this.exception = exception;

    /// <inheritdoc />
    public Type Type => typeof(SystemFailure);

    /// <summary>
    ///     Creates a failure from an exception.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>The failure.</returns>
    public static SystemFailure FromException(Exception exception) => new(exception);

    /// <inheritdoc />
    public string GetFailureMessage() => this.exception.Message;

    /// <inheritdoc />
    public Exception ToException() => this.exception;

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);
}