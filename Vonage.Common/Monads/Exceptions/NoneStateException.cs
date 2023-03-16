using System;

namespace Vonage.Common.Monads.Exceptions;

/// <summary>
///     Represents errors that occurs when a Maybe is in a None state.
/// </summary>
public class NoneStateException : Exception
{
    /// <summary>
    ///     Creates a NoneStateException.
    /// </summary>
    public NoneStateException()
        : base("State is None.")
    {
    }
}