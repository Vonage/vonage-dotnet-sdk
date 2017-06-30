using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nexmo.Api.Test.Unit
{
    [TestClass]
    public class MockedWebTestSetup
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Configuration.Instance.ClientHandler = new FakeClientHandler();
        }
    }

    public class MockedWebTest
    {
        protected string ApiUrl = Configuration.Instance.Settings["appSettings:Nexmo.Url.Api"];
        protected string RestUrl = Configuration.Instance.Settings["appSettings:Nexmo.Url.Rest"];
        protected string ApiKey = Configuration.Instance.Settings["appSettings:Nexmo.api_key"];
        protected string ApiSecret = Configuration.Instance.Settings["appSettings:Nexmo.api_secret"];

        protected void SetExpect(string uri, string response, string content = "")
        {
            var fakeClientHandler = (FakeClientHandler)Configuration.Instance.ClientHandler;
            fakeClientHandler.ExpectedUris.Push(uri);
            fakeClientHandler.ExpectedResponses.Push(response);
            fakeClientHandler.ExpectedContent.Push(content);
        }

        protected void SetExpectStatus(HttpStatusCode code)
        {
            var fakeClientHandler = (FakeClientHandler)Configuration.Instance.ClientHandler;
            fakeClientHandler.ExpectedStatusCodes.Push(code);
        }
    }

    public class FakeClientHandler : HttpClientHandler
    {
        internal Stack<string> ExpectedUris { get; } = new Stack<string>();
        internal Stack<string> ExpectedContent { get; } = new Stack<string>();
        internal Stack<HttpStatusCode> ExpectedStatusCodes { get; } = new Stack<HttpStatusCode>();
        internal Stack<string> ExpectedResponses { get; } = new Stack<string>();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uri = ExpectedUris.Pop();
            var responseContent = ExpectedResponses.Pop();
            var requestContent = string.Empty;
            if (ExpectedContent.Any())
            {
                requestContent = ExpectedContent.Pop();
            }

            if (!string.Equals(request.RequestUri.AbsoluteUri, uri, StringComparison.OrdinalIgnoreCase))
                return AssertFail();

            if (!string.IsNullOrEmpty(requestContent))
            {
                var contentReadTask = request.Content.ReadAsStringAsync();
                contentReadTask.Wait(cancellationToken);
                if (!string.Equals(contentReadTask.Result, requestContent, StringComparison.OrdinalIgnoreCase))
                {
                    return AssertFail();
                }
            }

            var responseCode = HttpStatusCode.OK;
            if (ExpectedStatusCodes.Any())
            {
                responseCode = ExpectedStatusCodes.Pop();
            }
            var response = new HttpResponseMessage(responseCode)
            {
                Content = new StringContent(responseContent)
            };
            return Task.FromResult(response);
        }

        private static Task<HttpResponseMessage> AssertFail()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            return Task.FromResult(response);
        }
    }
}