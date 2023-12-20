using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;

namespace Vonage.Test.Common.TestHelpers
{
    public class FakeHttpRequestHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode statusCode;
        private Maybe<ExpectedRequest> expectedRequest = Maybe<ExpectedRequest>.None;
        private Maybe<string> responseContent = Maybe<string>.None;
        private Maybe<TimeSpan> responseDelay;
        private readonly Uri baseUri = new("http://fake-host/api");

        private FakeHttpRequestHandler(HttpStatusCode code) => this.statusCode = code;

        public static FakeHttpRequestHandler Build(HttpStatusCode code) => new(code);

        public HttpClient ToHttpClient() => new(this, false)
        {
            BaseAddress = this.baseUri,
        };

        public FakeHttpRequestHandler WithDelay(TimeSpan delay)
        {
            this.responseDelay = delay;
            return this;
        }

        public FakeHttpRequestHandler WithExpectedRequest(ExpectedRequest expected)
        {
            this.expectedRequest = expected;
            return this;
        }

        public FakeHttpRequestHandler WithResponseContent(string content)
        {
            this.responseContent = content;
            return this;
        }

        private void CompareRequests(ReceivedRequest received, ExpectedRequest expected)
        {
            received.RequestUri.Should()
                .Be(new Uri(this.baseUri, expected.RequestUri));
            received.Method.Should().Be(expected.Method);
            received.Content.Should().Be(expected.Content);
        }

        private HttpResponseMessage CreateResponseMessage() =>
            new(this.statusCode)
            {
                Content = new StringContent(this.responseContent.IfNone(string.Empty), Encoding.UTF8,
                    "application/json"),
            };

        private static async Task<Maybe<string>> GetRequestContent(HttpRequestMessage request)
        {
            if (request.Content is null)
            {
                return Maybe<string>.None;
            }

            var content = await request.Content.ReadAsStringAsync();
            return string.IsNullOrWhiteSpace(content) ? Maybe<string>.None : content;
        }

        private static async Task<ReceivedRequest> ParseIncomingRequest(HttpRequestMessage request) =>
            new()
            {
                RequestUri = request.RequestUri,
                Method = request.Method,
                Content = await GetRequestContent(request),
                Headers = request.Headers,
            };

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            await Task.Delay(this.responseDelay.IfNone(TimeSpan.Zero), cancellationToken);
            var incomingRequest = await ParseIncomingRequest(request);
            this.expectedRequest.IfSome(expected => this.CompareRequests(incomingRequest, expected));
            return this.CreateResponseMessage();
        }

        private struct ReceivedRequest
        {
            public Maybe<string> Content { get; set; }
            public HttpRequestHeaders Headers { get; set; }
            public HttpMethod Method { get; set; }
            public Uri RequestUri { get; set; }
        }
    }
}