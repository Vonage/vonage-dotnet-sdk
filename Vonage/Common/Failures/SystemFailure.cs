#region
using System;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Failures;

/// <inheritdoc />
/// <example>
///     <code><![CDATA[
/// try
/// {
///     // Some operation that might throw
/// }
/// catch (Exception ex)
/// {
///     SystemFailure failure = SystemFailure.FromException(ex);
///     Result<int> result = failure.ToResult<int>();
/// }
/// ]]></code>
/// </example>
public readonly struct SystemFailure : IResultFailure
{
    private readonly Exception exception;
    private SystemFailure(Exception exception) => this.exception = exception;

    /// <inheritdoc />
    public Type Type => typeof(SystemFailure);

    /// <inheritdoc />
    public string GetFailureMessage() => this.exception.Message;

    /// <inheritdoc />
    public Exception ToException() => this.exception;

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);

    /// <summary>
    ///     Creates a failure from an exception.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>The failure.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var exception = new InvalidOperationException("Connection timeout");
    /// SystemFailure failure = SystemFailure.FromException(exception);
    /// Console.WriteLine(failure.GetFailureMessage()); // "Connection timeout"
    /// ]]></code>
    /// </example>
    public static SystemFailure FromException(Exception exception) => new SystemFailure(exception);
}