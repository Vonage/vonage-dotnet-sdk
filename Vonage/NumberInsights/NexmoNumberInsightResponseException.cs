using Vonage.Common.Exceptions;

namespace Vonage.NumberInsights;

/// <summary>
///     Exception thrown when a Number Insight API request fails with a non-zero status code.
/// </summary>
public class VonageNumberInsightResponseException : VonageException
{
    /// <summary>
    ///     The original API response that caused this exception, containing the error status and details.
    /// </summary>
    public NumberInsightResponseBase Response { get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="VonageNumberInsightResponseException"/> class.
    /// </summary>
    /// <param name="message">The error message describing the failure.</param>
    public VonageNumberInsightResponseException(string message) : base(message)
    {
    }
}