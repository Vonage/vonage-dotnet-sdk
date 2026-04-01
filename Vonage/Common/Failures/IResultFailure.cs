#region
using System;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Failures;

/// <summary>
///     Represents a Failure with an error message.
/// </summary>
/// <example>
///     <code><![CDATA[
/// // Handling failures from Result
/// Result<string> result = GetResult();
/// result.Match(
///     value => Console.WriteLine($"Success: {value}"),
///     failure => Console.WriteLine($"Failed: {failure.GetFailureMessage()}")
/// );
/// ]]></code>
/// </example>
public interface IResultFailure
{
    /// <summary>
    ///     The type of failure.
    /// </summary>
    Type Type { get; }

    /// <summary>
    ///     Returns the error message defined in the failure.
    /// </summary>
    /// <returns>The error message.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// IResultFailure failure = ResultFailure.FromErrorMessage("Invalid input");
    /// string message = failure.GetFailureMessage(); // "Invalid input"
    /// ]]></code>
    /// </example>
    string GetFailureMessage();

    /// <summary>
    ///     Converts the failure to an exception.
    /// </summary>
    /// <returns>The exception.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// IResultFailure failure = ResultFailure.FromErrorMessage("Operation failed");
    /// throw failure.ToException();
    /// ]]></code>
    /// </example>
    Exception ToException();

    /// <summary>
    ///     Converts the failure to a Result with a Failure state.
    /// </summary>
    /// <typeparam name="T">The underlying type of Result.</typeparam>
    /// <returns>A Result with a Failure state.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// IResultFailure failure = ResultFailure.FromErrorMessage("Not found");
    /// Result<User> result = failure.ToResult<User>();
    /// Console.WriteLine(result.IsFailure); // true
    /// ]]></code>
    /// </example>
    Result<T> ToResult<T>();
}