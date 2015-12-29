using System;
using System.Configuration;
using Moq;
using Nexmo.Api.Request;
using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    internal class MockedWebTest
    {
        protected Mock<IHttpWebRequestFactory> _mock;
        protected Mock<IHttpWebRequest> _request;

        protected string RestUrl = ConfigurationManager.AppSettings["Nexmo.Url.Rest"];
        protected string ApiKey = ConfigurationManager.AppSettings["Nexmo.api_key"];
        protected string ApiSecret = ConfigurationManager.AppSettings["Nexmo.api_secret"];

        [SetUp]
        public void Setup()
        {
            _mock = new Mock<IHttpWebRequestFactory>();
            _request = new Mock<IHttpWebRequest>();
            _mock.Setup(x => x.CreateHttp(It.IsAny<Uri>()))
                .Returns<Uri>(r => _request.Object);
            ApiRequest.WebRequestFactory = _mock.Object;
        }
    }
}
