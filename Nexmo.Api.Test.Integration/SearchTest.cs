using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nexmo.Api.Test.Integration
{
    [TestClass]
    public class SearchTest
    {
        [TestMethod]
        public void should_get_message()
        {
            var msg = Search.GetMessage("03000000FFFFFFFF");
            Assert.AreEqual("03000000FFFFFFFF", msg.messageId);
            Assert.AreEqual("17775551212", msg.from);
            Assert.AreEqual("2015-12-31 14:08:40", msg.dateReceived);
        }

        [TestMethod]
        public void should_get_messages()
        {
            var msgs = Search.GetMessages(new Search.SearchRequest
            {
                date = "2015-12-31",
                to = Configuration.Instance.Settings["nexmo_number"]
            });

            Assert.AreEqual(1, msgs.count);
        }

        [TestMethod]
        public void should_get_rejections()
        {
            var msgs = Search.GetRejections(new Search.SearchRequest
            {
                date = "2015-12-31",
                to = Configuration.Instance.Settings["nexmo_number"]
            });

            Assert.AreEqual(1, msgs.count);
        }
    }
}