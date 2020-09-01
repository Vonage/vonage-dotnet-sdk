namespace Nexmo.Api.Verify
{
    [System.Obsolete("The Nexmo.Api.Verify.NexmoVerifyResponseException class is obsolete. " +
        "References to it should be switched to the new Vonage.Verify.NexmoVerifyResponseException class.")]
    public class NexmoVerifyResponseException : NexmoException
    {
        public NexmoVerifyResponseException(string message) : base(message) { }

        public VerifyResponseBase Response {get;set;}
    }
}
