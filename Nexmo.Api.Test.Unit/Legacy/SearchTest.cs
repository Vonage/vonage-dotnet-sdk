using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace Nexmo.Api.Test.Unit.Legacy
{
    public class SearchTest : TestBase
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetMessage(bool passCreds)
        {
            //ARRANGE
            var uri = $"{RestUrl}/search/message?id=03000000FFFFFFFF&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = "{\"message-id\":\"03000000FFFFFFFF\",\"account-id\":\"deadbeef\",\"network\":\"310004\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"price\":\"0.00480000\",\"date-received\":\"2015-12-31 14:08:40\",\"status\":\"ACCEPTD\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}";
            Setup(uri: uri, responseContent: expectedResponse);

            //ACT
            var creds = new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            Search.Message msg;
            if (passCreds)
            {
                msg = client.Search.GetMessage("03000000FFFFFFFF", creds);
            }
            else
            {
                msg = client.Search.GetMessage("03000000FFFFFFFF");
            }

            //ASSERT
            Assert.Equal("03000000FFFFFFFF", msg.messageId);
            Assert.Equal("17775551212", msg.from);
            Assert.Equal("17775551213", msg.to);

        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetMessages(bool passCreds)
        {
            //ARRANGE
            var uri = $"{RestUrl}/search/messages?date=2015-12-31&to=17775551213&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = "{\"count\":1,\"items\":[{\"message-id\":\"03000000FFFFFFFF\",\"account-id\":\"deadbeef\",\"network\":\"310004\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"price\":\"0.00480000\",\"date-received\":\"2015-12-31 14:08:40\",\"status\":\"ACCEPTD\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}]}";
            Setup(uri: uri, responseContent: expectedResponse);

            //ACT
            var creds = new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            Search.Messages<Search.Message> msgs;
            if(passCreds)
            {
                msgs = client.Search.GetMessages(new Search.SearchRequest
                {
                    date = "2015-12-31",
                    to = "17775551213"
                }, creds);
            }
            else
            {
                msgs = client.Search.GetMessages(new Search.SearchRequest
                {
                    date = "2015-12-31",
                    to = "17775551213"
                });
            }

            //ASSERT
            Assert.Equal(1, msgs.count);
            var msg = msgs.items[0];
            Assert.Equal("03000000FFFFFFFF", msg.messageId);
            Assert.Equal("17775551212", msg.from);
            Assert.Equal("17775551213", msg.to);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetRejections(bool passCreds)
        {
            //ARRANGE
            var uri = $"{RestUrl}/search/rejections?date=2015-12-31&to=17775551213&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = "{\"count\":1,\"items\":[{\"account-id\":\"deadbeef\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"date-received\":\"2015-12-31 14:08:40\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}]}";
            Setup(uri: uri, responseContent: expectedResponse);

            //ACT
            var creds = new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            Search.Messages<Search.MessageBase> msgs;
            if (passCreds)
            {
                msgs = client.Search.GetRejections(new Search.SearchRequest
                {
                    date = "2015-12-31",
                    to = "17775551213"
                }, creds);
            }
            else
            {
                msgs = client.Search.GetRejections(new Search.SearchRequest
                {
                    date = "2015-12-31",
                    to = "17775551213"
                });
            }
            Assert.Single(msgs.items);
            var msg = msgs.items[0];
            Assert.Equal("17775551212", msg.from);
            Assert.Equal("17775551213", msg.to);
            Assert.Equal("web test", msg.body);
        }
    }
}
