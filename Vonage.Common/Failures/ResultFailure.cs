using System;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;

namespace Vonage.Common.Failures;

/// <inheritdoc />
public readonly struct ResultFailure : IResultFailure
{
    private readonly string error;
    private ResultFailure(string error) => this.error = error;

    /// <summary>
    ///     Creates a failure from an error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>The failure.</returns>
    public static ResultFailure FromErrorMessage(string error) => new(error);

    /// <inheritdoc />
    public string GetFailureMessage() => this.error;

    /// <inheritdoc />
    public Exception ToException() => new VonageException(this.error);

    /// <summary>
    ///     Creates a result failure from an error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <typeparam name="T">Type of the result.</typeparam>
    /// <returns>The result.</returns>
    public static Result<T> ToResult<T>(string error) => Result<T>.FromFailure(FromErrorMessage(error));
}