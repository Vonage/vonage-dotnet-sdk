namespace Nexmo.Api.Numbers
{
    [System.Obsolete("The Nexmo.Api.Numbers.NexmoNumberResponseException class is obsolete. " +
        "References to it should be switched to the new Vonage.Numbers.NexmoNumberResponseException class.")]
    public class NexmoNumberResponseException : NexmoException
    {
        public NexmoNumberResponseException(string message) : base(message) {  }
        public NumberTransactionResponse Response { get; set; }
    }
}
