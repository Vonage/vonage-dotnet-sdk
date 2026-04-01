#region
using System;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Failures;

/// <inheritdoc />
/// <example>
///     <code><![CDATA[
/// // Create a failure from an error message
/// ResultFailure failure = ResultFailure.FromErrorMessage("Validation failed");
/// Console.WriteLine(failure.GetFailureMessage()); // "Validation failed"
///
/// // Convert to a Result
/// Result<int> result = failure.ToResult<int>();
/// Console.WriteLine(result.IsFailure); // true
/// ]]></code>
/// </example>
public readonly struct ResultFailure : IResultFailure
{
    private readonly string error;
    private ResultFailure(string error) => this.error = error;

    /// <inheritdoc />
    public Type Type => typeof(ResultFailure);

    /// <inheritdoc />
    public string GetFailureMessage() => this.error;

    /// <inheritdoc />
    public Exception ToException() => new VonageException(this.GetFailureMessage());

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);

    /// <summary>
    ///     Creates a failure from an error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>The failure.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// ResultFailure failure = ResultFailure.FromErrorMessage("Email address is invalid");
    /// Console.WriteLine(failure.GetFailureMessage()); // "Email address is invalid"
    /// ]]></code>
    /// </example>
    public static ResultFailure FromErrorMessage(string error) => new ResultFailure(error);
}