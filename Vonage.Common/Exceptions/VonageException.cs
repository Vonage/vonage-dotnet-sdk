using System;

namespace Vonage.Common.Exceptions
{
    public class VonageException : Exception
    {
        public VonageException(string message) : base(message)
        {
        }
    }
}