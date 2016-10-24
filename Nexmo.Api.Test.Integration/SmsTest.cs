using NUnit.Framework;

namespace Nexmo.Api.Test.Integration
{
    [TestFixture]
    public class SmsTest
    {
        [Test]
        public void should_send_sms()
        {
            var results = SMS.Send(new SMS.SMSRequest
            {
                from = Configuration.Instance.Settings["nexmo_number"],
                to = Configuration.Instance.Settings["test_number"],
                text = "this is a test"
            });
            Assert.AreEqual(Configuration.Instance.Settings["test_number"], results.messages[0].to);
            Assert.AreEqual("0", results.messages[0].status);
        }
    }
}