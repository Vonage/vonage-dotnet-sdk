using System;
using Vonage.Common.Failures;

namespace Vonage.Common.Monads.Exceptions;

/// <summary>
///     Represents errors that occurs when a Result is in a Failure state.
/// </summary>
[Serializable]
public class FailureStateException : Exception
{
    /// <summary>
    ///     Gets the failure value of the Result.
    /// </summary>
    public IResultFailure Failure { get; }

    /// <summary>
    ///     Creates a FailureStateException.
    /// </summary>
    /// <param name="failure">The failure value of the Result.</param>
    public FailureStateException(IResultFailure failure)
        : base("State is Failure.") =>
        this.Failure = failure;
}