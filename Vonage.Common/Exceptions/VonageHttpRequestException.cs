using System;
using System.Net;
using System.Net.Http;

namespace Vonage.Common.Exceptions
{
    public class VonageHttpRequestException : HttpRequestException
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Json { get; set; }

        public VonageHttpRequestException()
        {
        }

        public VonageHttpRequestException(string message) : base(message)
        {
        }

        public VonageHttpRequestException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}