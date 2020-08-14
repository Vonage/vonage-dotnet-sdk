using System;
using System.Net;
using System.Net.Http;

namespace Vonage.Request
{
    public class VonageHttpRequestException : HttpRequestException
    {
        public VonageHttpRequestException() { }
        public VonageHttpRequestException(string message) : base(message) { }
        public VonageHttpRequestException(string message, Exception inner) : base(message, inner){}
        public string Json { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
