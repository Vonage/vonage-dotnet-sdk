using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Common.Test;

namespace Vonage.Test.Unit.Meetings.UpdateThemeLogo
{
    public class UpdateThemeLogoMessageHandler :
        HttpMessageHandler,
        UpdateThemeLogoMessageHandler.IExpectRequest,
        UpdateThemeLogoMessageHandler.IExpectResponse
    {
        private readonly Dictionary<ExpectedRequest, Maybe<ExpectedResponse>> map =
            new Dictionary<ExpectedRequest, Maybe<ExpectedResponse>>();

        private Maybe<ExpectedRequest> pendingRequest = Maybe<ExpectedRequest>.None;

        public Uri BaseUri => new Uri("http://fake-host/api");

        public static IExpectRequest Build() => new UpdateThemeLogoMessageHandler();

        public IExpectResponse GivenRequest(ExpectedRequest request)
        {
            var newRequest = new ExpectedRequest
            {
                Content = request.Content,
                Method = request.Method,
                RequestUri = new Uri(this.BaseUri, request.RequestUri),
            };
            this.pendingRequest = newRequest;
            if (this.map.ContainsKey(newRequest))
            {
                this.map.Remove(newRequest);
            }

            this.map.Add(newRequest, Maybe<ExpectedResponse>.None);
            return this;
        }

        public IExpectRequest RespondWith(ExpectedResponse response)
        {
            var pair = this.map.First(v =>
                v.Key.Equals(this.pendingRequest.IfNone(() => throw new InvalidOperationException())));
            this.map[pair.Key] = response;
            this.pendingRequest = Maybe<ExpectedRequest>.None;
            return this;
        }

        public HttpClient ToHttpClient() => new HttpClient(this, false)
        {
            BaseAddress = this.BaseUri,
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

        private static async Task<ExpectedRequest> ParseIncomingRequest(HttpRequestMessage request) =>
            new ExpectedRequest
            {
                RequestUri = request.RequestUri,
                Method = request.Method,
                Content = await GetRequestContent(request),
            };

        public interface IExpectRequest
        {
            Uri BaseUri { get; }
            IExpectResponse GivenRequest(ExpectedRequest request);
            HttpClient ToHttpClient();
        }

        public interface IExpectResponse
        {
            IExpectRequest RespondWith(ExpectedResponse response);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var incomingRequest = await ParseIncomingRequest(request);
            var a = this.map
                .FirstOrDefault(pair =>
                    pair.Key.RequestUri == request.RequestUri && pair.Key.Method == incomingRequest.Method);
            var b = a.Value.IfNone(() => throw new InvalidOperationException("Missing map."));
            return new HttpResponseMessage(b.Code)
            {
                Content = new StringContent(b.Content.IfNone(string.Empty), Encoding.UTF8,
                    "application/json"),
            };
        }

        public struct ExpectedResponse
        {
            public HttpStatusCode Code { get; set; }
            public Maybe<string> Content { get; set; }
        }
    }
}