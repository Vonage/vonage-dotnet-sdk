#region
using System;
#endregion

namespace Vonage.Common.Exceptions;

/// <summary>
///     Represents a Vonage-specific exception.
/// </summary>
/// <example>
///     <code><![CDATA[
/// throw new VonageException("Operation failed due to invalid configuration");
/// ]]></code>
/// </example>
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