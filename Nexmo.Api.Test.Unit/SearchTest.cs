using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace Nexmo.Api.Test.Unit
{
    public class SearchTest : TestBase
    {
        [Fact]
        public void GetMessage()
        {
            //ARRANGE
            var uri = $"{RestUrl}/search/message?id=03000000FFFFFFFF&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = "{\"message-id\":\"03000000FFFFFFFF\",\"account-id\":\"deadbeef\",\"network\":\"310004\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"price\":\"0.00480000\",\"date-received\":\"2015-12-31 14:08:40\",\"status\":\"ACCEPTD\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}";
            Setup(uri: uri, responseContent: expectedResponse);

            //ACT
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var msg = client.Search.GetMessage("03000000FFFFFFFF");

            //ASSERT
            Assert.Equal("03000000FFFFFFFF", msg.messageId);
            Assert.Equal("17775551212", msg.from);
            Assert.Equal("17775551213", msg.to);

        }

        [Fact]
        public void GetMessages()
        {
            //ARRANGE
            var uri = $"{RestUrl}/search/messages?date=2015-12-31&to=17775551213&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = "{\"count\":1,\"items\":[{\"message-id\":\"03000000FFFFFFFF\",\"account-id\":\"deadbeef\",\"network\":\"310004\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"price\":\"0.00480000\",\"date-received\":\"2015-12-31 14:08:40\",\"status\":\"ACCEPTD\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}]}";
            Setup(uri: uri, responseContent: expectedResponse);

            //ACT
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var msgs = client.Search.GetMessages(new Search.SearchRequest
            {
                date = "2015-12-31",
                to = "17775551213"
            });

            //ASSERT
            Assert.Equal(1, msgs.count);
            var msg = msgs.items[0];
            Assert.Equal("03000000FFFFFFFF", msg.messageId);
            Assert.Equal("17775551212", msg.from);
            Assert.Equal("17775551213", msg.to);
        }

        [Fact]
        public void GetRejections()
        {
            //ARRANGE
            var uri = $"{RestUrl}/search/rejections?date=2015-12-31&to=17775551213&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = "{\"count\":1,\"items\":[{\"account-id\":\"deadbeef\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"date-received\":\"2015-12-31 14:08:40\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}]}";
            Setup(uri: uri, responseContent: expectedResponse);

            //ACT
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var msgs = client.Search.GetRejections(new Search.SearchRequest
            {
                date = "2015-12-31",
                to = "17775551213"
            });
            Assert.Single(msgs.items);
            var msg = msgs.items[0];
            Assert.Equal("17775551212", msg.from);
            Assert.Equal("17775551213", msg.to);
            Assert.Equal("web test", msg.body);
        }
    }
}
