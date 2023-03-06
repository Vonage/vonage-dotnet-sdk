using System;

namespace Vonage.Common.Exceptions
{
    public class VonageAuthenticationException : Exception
    {
        public VonageAuthenticationException(string message) : base(message)
        {
        }
    }
}