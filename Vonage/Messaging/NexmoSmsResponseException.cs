using Vonage.Common.Exceptions;

namespace Vonage.Messaging
{
    public class VonageSmsResponseException : VonageException
    {
        public SendSmsResponse Response { get; set; }

        public VonageSmsResponseException(string message) : base(message)
        {
        }
    }
}