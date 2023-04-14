using System;
using Vonage.Common.Monads;

namespace Vonage.Common.Failures;

/// <summary>
///     Represents a Failure with an error message.
/// </summary>
public interface IResultFailure
{
    /// <summary>
    ///     Returns the error message defined in the failure.
    /// </summary>
    /// <returns>The error message.</returns>
    string GetFailureMessage();

    /// <summary>
    ///     Converts the failure to an exception.
    /// </summary>
    /// <returns>The exception.</returns>
    Exception ToException();

    /// <summary>
    ///     Converts the failure to a Result with a Failure state.
    /// </summary>
    /// <typeparam name="T">The underlying type of Result.</typeparam>
    /// <returns>A Result with a Failure state.</returns>
    Result<T> ToResult<T>();
}