using System;

namespace Vonage
{
    public class VonageException : Exception
    {
        public VonageException(string message) : base(message) { }
    }
}
