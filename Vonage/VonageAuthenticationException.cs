using System;

namespace Vonage
{
    public class VonageAuthenticationException : Exception
    {
        public VonageAuthenticationException(string message) : base(message) { }
    }
}