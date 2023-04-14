using Vonage.Common.Exceptions;

namespace Vonage.NumberInsights;

public class VonageNumberInsightResponseException : VonageException
{
    public NumberInsightResponseBase Response { get; set; }

    public VonageNumberInsightResponseException(string message) : base(message)
    {
    }
}