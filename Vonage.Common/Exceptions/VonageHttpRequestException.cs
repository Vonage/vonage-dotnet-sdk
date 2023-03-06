using System;
using System.Net;
using System.Net.Http;

namespace Vonage.Common.Exceptions
{
    /// <summary>
    ///     Represents an issue when processing an HttpRequest.
    /// </summary>
    public class VonageHttpRequestException : HttpRequestException
    {
        /// <summary>
        ///     The response status code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; init; }

        /// <summary>
        ///     The response body content.
        /// </summary>
        public string Json { get; init; }

        /// <summary>
        ///     Creates a VonageHttpRequestException.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public VonageHttpRequestException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Creates a VonageHttpRequestException.
        /// </summary>
        /// <param name="inner">The inner exception.</param>
        public VonageHttpRequestException(Exception inner) : base(inner.Message, inner)
        {
        }
    }
}