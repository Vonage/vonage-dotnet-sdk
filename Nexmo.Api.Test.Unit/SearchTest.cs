using System;
using System.IO;
using System.Text;
using Moq;
using Nexmo.Api.Request;
using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    internal class SearchTest : MockedWebTest
    {
        [Test]
        public void should_get_message()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"message-id\":\"03000000FFFFFFFF\",\"account-id\":\"deadbeef\",\"network\":\"310004\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"price\":\"0.00480000\",\"date-received\":\"2015-12-31 14:08:40\",\"status\":\"ACCEPTD\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}")));
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var msg = Search.GetMessage("03000000FFFFFFFF");

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/search/message?id=03000000FFFFFFFF&api_key={1}&api_secret={2}&", RestUrl, ApiKey, ApiSecret))),
                Times.Once);
            Assert.AreEqual("03000000FFFFFFFF", msg.messageId);
            Assert.AreEqual("17775551212", msg.from);
            Assert.AreEqual("17775551213", msg.to);
        }

        [Test]
        public void should_get_messages()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"count\":1,\"items\":[{\"message-id\":\"03000000FFFFFFFF\",\"account-id\":\"deadbeef\",\"network\":\"310004\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"price\":\"0.00480000\",\"date-received\":\"2015-12-31 14:08:40\",\"status\":\"ACCEPTD\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}]}")));
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var msgs = Search.GetMessages(new Search.SearchRequest
            {
                date = "2015-12-31",
                to = "17775551213"
            });

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/search/messages?date=2015-12-31&to=17775551213&api_key={1}&api_secret={2}&", RestUrl, ApiKey, ApiSecret))),
                Times.Once);

            Assert.AreEqual(1, msgs.count);
            var msg = msgs.items[0];
            Assert.AreEqual("03000000FFFFFFFF", msg.messageId);
            Assert.AreEqual("17775551212", msg.from);
            Assert.AreEqual("17775551213", msg.to);
        }

        [Test]
        public void should_get_rejections()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"count\":1,\"items\":[{\"account-id\":\"deadbeef\",\"from\":\"17775551212\",\"to\":\"17775551213\",\"body\":\"web test\",\"date-received\":\"2015-12-31 14:08:40\",\"error-code\":\"1\",\"error-code-label\":\"Unknown\",\"type\":\"MT\"}]}")));
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var msgs = Search.GetRejections(new Search.SearchRequest
            {
                date = "2015-12-31",
                to = "17775551213"
            });

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/search/rejections?date=2015-12-31&to=17775551213&api_key={1}&api_secret={2}&", RestUrl, ApiKey, ApiSecret))),
                Times.Once);

            Assert.AreEqual(1, msgs.count);
            var msg = msgs.items[0];
            Assert.AreEqual("17775551212", msg.from);
            Assert.AreEqual("17775551213", msg.to);
            Assert.AreEqual("web test", msg.body);
        }
    }
}