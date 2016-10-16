using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    internal class SearchTest : MockedWebTest
    {
        [Test]
        public void should_get_message()
        {
            SetExpect($"{RestUrl}/search/message?id=03000000FFFFFFFF&api_key={ApiKey}&api_secret={ApiSecret}&",
"{\"message-id\":\"03000000FFFFFFFF\",\"account-id\":\"deadbeef\",\"network\":\"310004\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"price\":\"0.00480000\",\"date-received\":\"2015-12-31 14:08:40\",\"status\":\"ACCEPTD\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}");

            var msg = Search.GetMessage("03000000FFFFFFFF");

            Assert.AreEqual("03000000FFFFFFFF", msg.messageId);
            Assert.AreEqual("17775551212", msg.from);
            Assert.AreEqual("17775551213", msg.to);
        }

        [Test]
        public void should_get_messages()
        {
            SetExpect($"{RestUrl}/search/messages?date=2015-12-31&to=17775551213&api_key={ApiKey}&api_secret={ApiSecret}&",
"{\"count\":1,\"items\":[{\"message-id\":\"03000000FFFFFFFF\",\"account-id\":\"deadbeef\",\"network\":\"310004\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"price\":\"0.00480000\",\"date-received\":\"2015-12-31 14:08:40\",\"status\":\"ACCEPTD\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}]}");

            var msgs = Search.GetMessages(new Search.SearchRequest
            {
                date = "2015-12-31",
                to = "17775551213"
            });

            Assert.AreEqual(1, msgs.count);
            var msg = msgs.items[0];
            Assert.AreEqual("03000000FFFFFFFF", msg.messageId);
            Assert.AreEqual("17775551212", msg.from);
            Assert.AreEqual("17775551213", msg.to);
        }

        [Test]
        public void should_get_rejections()
        {
            SetExpect($"{RestUrl}/search/rejections?date=2015-12-31&to=17775551213&api_key={ApiKey}&api_secret={ApiSecret}&",
"{\"count\":1,\"items\":[{\"account-id\":\"deadbeef\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"date-received\":\"2015-12-31 14:08:40\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}]}");

            var msgs = Search.GetRejections(new Search.SearchRequest
            {
                date = "2015-12-31",
                to = "17775551213"
            });

            Assert.AreEqual(1, msgs.count);
            var msg = msgs.items[0];
            Assert.AreEqual("17775551212", msg.from);
            Assert.AreEqual("17775551213", msg.to);
            Assert.AreEqual("web test", msg.body);
        }
    }
}