using Vonage.Common.Exceptions;

namespace Vonage.Numbers;

/// <summary>
///     Exception thrown when a Numbers API transaction fails with a non-success status code.
/// </summary>
public class VonageNumberResponseException : VonageException
{
    /// <summary>
    ///     The original API response that caused this exception, containing the error code and details.
    /// </summary>
    public NumberTransactionResponse Response { get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="VonageNumberResponseException"/> class.
    /// </summary>
    /// <param name="message">The error message describing the failure.</param>
    public VonageNumberResponseException(string message) : base(message)
    {
    }
}