namespace Nexmo.Api.NumberInsights
{
    [System.Obsolete("The Nexmo.Api.NumberInsights.NexmoNumberInsightResponseException class is obsolete. " +
        "References to it should be switched to the new Vonage.NumberInsights.NexmoNumberInsightResponseException class.")]
    public class NexmoNumberInsightResponseException : NexmoException
    {
        public NexmoNumberInsightResponseException(string message) : base(message) { }

        public NumberInsightResponseBase Response { get; set; }
    }
}
