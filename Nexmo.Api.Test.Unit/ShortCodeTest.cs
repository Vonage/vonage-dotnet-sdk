using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    internal class ShortCodeTest : MockedWebTest
    {
        [Test]
        public void should_initiate_2fa()
        {
            var request = new ShortCode.TwoFactorAuthRequest
            {
                to = "15555551212",
                pin = 1247
            };

            SetExpect($"{RestUrl}/sc/us/2fa/json?to={request.to}&pin={request.pin}&api_key={ApiKey}&api_secret={ApiSecret}&",
"{\"message-count\":\"1\",\"messages\":[{\"message-id\":\"02000000AE70FFFF\",\"to\":\"15555551212\",\"remaining-balance\":7.546,\"message-price\":0.0048,\"ok\":true,\"status\":\"0\",\"msisdn\":\"15555551212\",\"network\":\"US-FIXED\",\"messageId\":\"02000000AE70FFFF\",\"remainingBalance\":7.546,\"messagePrice\":0.0048}]}");

            var response = ShortCode.RequestTwoFactorAuth(request);

            Assert.AreEqual("1", response.message_count);
            Assert.AreEqual("15555551212", response.messages.First().to);
        }

        [Test]
        public void should_initiate_alert()
        {
            var request = new ShortCode.AlertRequest
            {
                to = "15555551212"
            };
            var customValues = new Dictionary<string, string>
            {
                {"mcount", "xyz123"}
            };

            SetExpect($"{RestUrl}/sc/us/alert/json?to={request.to}&api_key={ApiKey}&api_secret={ApiSecret}&mcount={customValues["mcount"]}&",
"{\"message-count\":\"1\",\"messages\":[{\"message-id\":\"02000000AE70FFFF\",\"to\":\"15555551212\",\"remaining-balance\":7.546,\"message-price\":0.0048,\"ok\":true,\"status\":\"0\",\"msisdn\":\"15555551212\",\"network\":\"US-FIXED\",\"messageId\":\"02000000AE70FFFF\",\"remainingBalance\":7.546,\"messagePrice\":0.0048}]}");

            var response = ShortCode.RequestAlert(request, customValues);

            Assert.AreEqual("1", response.message_count);
            Assert.AreEqual("15555551212", response.messages.First().to);
        }
    }
}