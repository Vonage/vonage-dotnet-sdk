using System;

namespace Vonage.Common.Exceptions;

/// <summary>
///     Represents a Vonage-specific exception.
/// </summary>
public class VonageException : Exception
{
    /// <summary>
    ///     Creates a VonageException.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public VonageException(string message) : base(message)
    {
    }
}