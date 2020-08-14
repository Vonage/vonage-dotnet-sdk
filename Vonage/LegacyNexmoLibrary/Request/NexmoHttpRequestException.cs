using System;
using System.Net;
using System.Net.Http;

namespace Nexmo.Api.Request
{
    [System.Obsolete("The Nexmo.Api.Request.NexmoHttpRequestException class is obsolete. " +
        "References to it should be switched to the new Vonage.Request.NexmoHttpRequestException class.")]
    public class NexmoHttpRequestException : HttpRequestException
    {
        public NexmoHttpRequestException() { }
        public NexmoHttpRequestException(string message) : base(message) { }
        public NexmoHttpRequestException(string message, Exception inner) : base(message, inner){}
        public string Json { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
