using System;

namespace Vonage.Common.Monads.Exceptions;

/// <summary>
///     Represents errors that occurs when a Result is in a Success state.
/// </summary>
public class SuccessStateException<T> : Exception
{
    /// <summary>
    ///     Gets the success value of the Result.
    /// </summary>
    public T Success { get; }

    /// <summary>
    ///     Creates a SuccessStateException.
    /// </summary>
    /// <param name="success">The success value of the Result.</param>
    public SuccessStateException(T success)
        : base("State is Success.") =>
        this.Success = success;
}