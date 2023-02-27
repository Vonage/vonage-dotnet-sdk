using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Vonage.Common.Monads;

namespace Vonage.Common.Test.TestHelpers
{
    public class FakeHttpRequestHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode statusCode;
        private Maybe<string> responseContent = Maybe<string>.None;

        private FakeHttpRequestHandler(HttpStatusCode code) => this.statusCode = code;

        public ReceivedRequest Request { get; private set; }

        public static FakeHttpRequestHandler Build(HttpStatusCode code) => new(code);

        public HttpClient ToHttpClient(Uri baseUri) => new(this, false)
        {
            BaseAddress = baseUri,
        };

        public FakeHttpRequestHandler WithResponseContent(string content)
        {
            this.responseContent = content;
            return this;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var content = await request.Content.ReadAsStringAsync();
            this.Request = new ReceivedRequest
            {
                RequestUri = request.RequestUri,
                Method = request.Method,
                Content = string.IsNullOrWhiteSpace(content) ? Maybe<string>.None : content,
                Headers = request.Headers,
            };
            var response = new HttpResponseMessage(this.statusCode)
            {
                Content = new StringContent(this.responseContent.IfNone(string.Empty), Encoding.UTF8,
                    "application/json"),
            };
            return response;
        }

        public struct ReceivedRequest
        {
            public Maybe<string> Content { get; set; }
            public HttpRequestHeaders Headers { get; set; }
            public HttpMethod Method { get; set; }
            public Uri RequestUri { get; set; }
        }
    }
}