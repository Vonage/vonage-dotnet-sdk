using Vonage.Common.Exceptions;

namespace Vonage.Messaging;

/// <summary>
///     Exception thrown when an SMS send request fails or returns a non-zero status code.
///     Check the <see cref="Response"/> property for detailed error information.
/// </summary>
public class VonageSmsResponseException : VonageException
{
    /// <summary>
    ///     Gets or sets the full SMS response that caused the exception.
    ///     Contains error details in <see cref="SendSmsResponse.Messages"/>.
    /// </summary>
    public SendSmsResponse Response { get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="VonageSmsResponseException"/> class.
    /// </summary>
    /// <param name="message">The error message describing the failure.</param>
    public VonageSmsResponseException(string message) : base(message)
    {
    }
}