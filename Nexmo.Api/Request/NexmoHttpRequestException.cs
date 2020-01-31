using System;
using System.Net;
using System.Net.Http;

namespace Nexmo.Api.Request
{
    public class NexmoHttpRequestException : HttpRequestException
    {
        public NexmoHttpRequestException() { }
        public NexmoHttpRequestException(string message) : base(message) { }
        public NexmoHttpRequestException(string message, Exception inner) : base(message, inner){}
        public string Json { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
