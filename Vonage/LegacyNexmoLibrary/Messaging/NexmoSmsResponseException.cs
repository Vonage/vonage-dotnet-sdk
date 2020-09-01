namespace Nexmo.Api.Messaging
{
    [System.Obsolete("The Nexmo.Api.Messaging.NexmoSmsResponseException class is obsolete. " +
        "References to it should be switched to the new Vonage.Messaging.NexmoSmsResponseException class.")]
    public class NexmoSmsResponseException : NexmoException
    {
        public NexmoSmsResponseException(string message) : base(message) { }

        public SendSmsResponse Response { get; set; }
    }
}
