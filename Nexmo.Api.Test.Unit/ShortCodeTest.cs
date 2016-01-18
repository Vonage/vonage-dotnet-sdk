using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Moq;
using Nexmo.Api.Request;
using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    internal class ShortCodeTest : MockedWebTest
    {
        [Test]
        public void should_initiate_2fa()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"message-count\":\"1\",\"messages\":[{\"message-id\":\"02000000AE70FFFF\",\"to\":\"15555551212\",\"remaining-balance\":7.546,\"message-price\":0.0048,\"ok\":true,\"status\":\"0\",\"msisdn\":\"15555551212\",\"network\":\"US-FIXED\",\"messageId\":\"02000000AE70FFFF\",\"remainingBalance\":7.546,\"messagePrice\":0.0048}]}")));
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var request = new ShortCode.TwoFactorAuthRequest
            {
                to = "15555551212",
                pin = 1247
            };
            var response = ShortCode.RequestTwoFactorAuth(request);

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/sc/us/2fa/json?to={1}&pin={2}&api_key={3}&api_secret={4}&", RestUrl, request.to, request.pin, ApiKey, ApiSecret))),
                Times.Once);
            Assert.AreEqual("1", response.message_count);
            Assert.AreEqual("15555551212", response.messages.First().to);
        }

        [Test]
        public void should_initiate_alert()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"message-count\":\"1\",\"messages\":[{\"message-id\":\"02000000AE70FFFF\",\"to\":\"15555551212\",\"remaining-balance\":7.546,\"message-price\":0.0048,\"ok\":true,\"status\":\"0\",\"msisdn\":\"15555551212\",\"network\":\"US-FIXED\",\"messageId\":\"02000000AE70FFFF\",\"remainingBalance\":7.546,\"messagePrice\":0.0048}]}")));
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var request = new ShortCode.AlertRequest
            {
                to = "15555551212"
            };
            var customValues = new Dictionary<string, string>
            {
                {"mcount", "xyz123"}
            };
            var response = ShortCode.RequestAlert(request, customValues);

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/sc/us/alert/json?to={1}&api_key={2}&api_secret={3}&mcount={4}&", RestUrl, request.to, ApiKey, ApiSecret, customValues["mcount"]))),
                Times.Once);
            Assert.AreEqual("1", response.message_count);
            Assert.AreEqual("15555551212", response.messages.First().to);
        }
    }
}