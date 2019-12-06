using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
namespace Nexmo.Api.UnitTest
{
    public class ShortCodeTest : TestBase
    {
        [Fact]
        public void Initiate2FA()
        {
            //ARRANGE
            var request = new ShortCode.TwoFactorAuthRequest
            {
                to = "15555551212",
                pin = 1247
            };
            var uri = $"{RestUrl}/sc/us/2fa/json?to={request.to}&pin={request.pin}&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = "{\"message-count\":\"1\",\"messages\":[{\"message-id\":\"02000000AE70FFFF\",\"to\":\"15555551212\",\"remaining-balance\":7.546,\"message-price\":0.0048,\"ok\":true,\"status\":\"0\",\"msisdn\":\"15555551212\",\"network\":\"US-FIXED\",\"messageId\":\"02000000AE70FFFF\",\"remainingBalance\":7.546,\"messagePrice\":0.0048}]}";
            Setup(uri: uri, responseContent: expectedResponse);
            //ACT
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var response = client.ShortCode.RequestTwoFactorAuth(request);

            //ASSERT
            Assert.Equal("1", response.message_count);
            Assert.Equal("15555551212", response.messages.First().to);
        }

        [Fact]
        public void InitiateAlert()
        {
            //ARRANGE
            var request = new ShortCode.AlertRequest
            {
                to = "15555551212"
            };
            var customValues = new Dictionary<string, string>
            {
                {"mcount", "xyz123"}
            };
            var uri = $"{RestUrl}/sc/us/alert/json?to={request.to}&api_key={ApiKey}&api_secret={ApiSecret}&mcount={customValues["mcount"]}&";
            var ExpectedResponse = "{\"message-count\":\"1\",\"messages\":[{\"message-id\":\"02000000AE70FFFF\",\"to\":\"15555551212\",\"remaining-balance\":7.546,\"message-price\":0.0048,\"ok\":true,\"status\":\"0\",\"msisdn\":\"15555551212\",\"network\":\"US-FIXED\",\"messageId\":\"02000000AE70FFFF\",\"remainingBalance\":7.546,\"messagePrice\":0.0048}]}";
            Setup(uri: uri, responseContent: ExpectedResponse);

            //ACT
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var response = client.ShortCode.RequestAlert(request, customValues);

            Assert.Equal("1", response.message_count);
            Assert.Equal("15555551212", response.messages.First().to);
        }
    }
}
