#region
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Request;
using Vonage.Test.Common.TestHelpers;
using Xunit;
using TimeProvider = Vonage.Common.TimeProvider;
#endregion

namespace Vonage.Test
{
    public class TestBase
    {
        private const string MockedMethod = "SendAsync";

        protected readonly string ApiKey = Environment.GetEnvironmentVariable("VONAGE_API_KEY") ?? "testkey";
        protected readonly string ApiSecret = Environment.GetEnvironmentVariable("VONAGE_API_Secret") ?? "testSecret";

        protected readonly string AppId = Environment.GetEnvironmentVariable("APPLICATION_ID") ??
                                          "afed99d2-ae38-487c-bb5a-fe2518febd44";

        protected readonly string PrivateKey = Environment.GetEnvironmentVariable("PRIVATE_KEY") ??
                                               TokenHelper.GetKey();

        protected Configuration configuration;

        protected TestBase() => this.configuration = new Configuration();

        protected string ApiUrl =>
            this.configuration.VonageUrls.Nexmo.AbsoluteUri.Substring(0,
                this.configuration.VonageUrls.Nexmo.AbsoluteUri.Length - 1);

        protected string RestUrl =>
            this.configuration.VonageUrls.Rest.AbsoluteUri.Substring(0,
                this.configuration.VonageUrls.Rest.AbsoluteUri.Length - 1);

        protected VonageClient BuildVonageClient(Credentials credentials) =>
            new VonageClient(credentials, this.configuration, new TimeProvider());

        protected VonageClient BuildVonageClient(Credentials credentials, ITimeProvider timeProvider) =>
            new VonageClient(credentials, this.configuration, timeProvider);

        protected Credentials BuildCredentialsForBasicAuthentication() =>
            Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);

        protected Credentials BuildCredentialsForBearerAuthentication() =>
            Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);

        protected void Setup(string uri, Maybe<string> responseContent, string requestContent = null,
            HttpStatusCode expectedCode = HttpStatusCode.OK) =>
            this.Setup(uri,
                responseContent.Map<HttpContent>(content =>
                    new StringContent(content, Encoding.UTF8, "application/json")), expectedCode,
                requestContent);

        protected void Setup(string uri, byte[] responseContent, HttpStatusCode expectedCode = HttpStatusCode.OK) =>
            this.Setup(uri, new StreamContent(new MemoryStream(responseContent)), expectedCode);

        private void Setup(string uri, Maybe<HttpContent> httpContent, HttpStatusCode expectedCode,
            string requestContent = null)
        {
            var expectedResponse = new HttpResponseMessage(expectedCode);
            httpContent.IfSome(some => expectedResponse.Content = some);
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(MockedMethod,
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((actualHttpRequestMessage, _) =>
                {
                    Assert.Equal(uri, actualHttpRequestMessage.RequestUri.AbsoluteUri);
                    if (requestContent == null)
                        return;
                    var actualContent = actualHttpRequestMessage.Content.ReadAsStringAsync().Result;
                    Assert.Equal(requestContent, actualContent);
                })
                .ReturnsAsync(expectedResponse)
                .Verifiable();
            this.configuration.ClientHandler = mockHandler.Object;
        }
    }
}