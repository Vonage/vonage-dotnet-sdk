using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace Nexmo.Api.Test.Unit
{
    public class TestBase
    {
        const string MOCKED_METHOD = "SendAsync";
        protected string ApiUrl = Configuration.Instance.Settings["appSettings:Nexmo.Url.Api"];
        protected string RestUrl = Configuration.Instance.Settings["appSettings:Nexmo.Url.Rest"];
        protected string ApiKey = Environment.GetEnvironmentVariable("NEXMO_API_KEY")??"testKey";
        protected string ApiSecret = Environment.GetEnvironmentVariable("NEXMO_API_Secret") ?? "testSecret";
        public void Setup(string uri, string responseContent, string requestContent = null, HttpStatusCode expectedCode = HttpStatusCode.OK)
        {
            typeof(Configuration).GetField("_client", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Configuration.Instance, null);
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(MOCKED_METHOD,
                ItExpr.Is<HttpRequestMessage>(
                    x => 
                    string.Equals(x.RequestUri.AbsoluteUri, uri, StringComparison.OrdinalIgnoreCase) && 
                    (requestContent == null) || 
                    (string.Equals(x.Content.ReadAsStringAsync().Result, requestContent, StringComparison.OrdinalIgnoreCase))),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = expectedCode,
                    Content = new StringContent(responseContent)
                })
                .Verifiable();
            Configuration.Instance.ClientHandler = mockHandler.Object;
            
        }
    }
}
