using Vonage.Common.Exceptions;

namespace Vonage.Numbers
{
    public class VonageNumberResponseException : VonageException
    {
        public NumberTransactionResponse Response { get; set; }

        public VonageNumberResponseException(string message) : base(message)
        {
        }
    }
}