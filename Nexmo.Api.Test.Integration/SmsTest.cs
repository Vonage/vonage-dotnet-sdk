using NUnit.Framework;

namespace Nexmo.Api.Test.Integration
{
    [TestFixture]
    public class SmsTest
    {
        [Test]
        public void should_send_sms()
        {
            var results = SMS.SendSMS(new SMS.SMSRequest
            {
                from = "15555551212",
                to = "17775551212",
                text = "this is a test"
            });
            Assert.AreEqual("17775551212", results.messages[0].to);
            Assert.AreEqual("0", results.messages[0].status);
        }
    }
}