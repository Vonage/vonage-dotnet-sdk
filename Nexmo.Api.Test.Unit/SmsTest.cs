using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nexmo.Api.Test.Unit
{
    [TestClass]
    public class SmsTest : MockedWebTest
    {
        [TestMethod]
        public void should_send_sms()
        {
            SetExpect($"{RestUrl}/sms/json",
"{\"message-count\": \"1\",\"messages\": [{\"to\": \"17775551212\",\"message-id\": \"02000000A3AF32FA\",\"status\": \"0\",\"remaining-balance\": \"7.55560000\",\"message-price\": \"0.00480000\",\"network\": \"310004\"}]}",
$"from=98975&to=17775551212&text=this+is+a+test&api_key={ApiKey}&api_secret={ApiSecret}&");

            var results = SMS.Send(new SMS.SMSRequest
            {
                from = "98975",
                to = "17775551212",
                text = "this is a test"
            });

            Assert.AreEqual("1", results.message_count);
            Assert.AreEqual("17775551212", results.messages[0].to);
            Assert.AreEqual("0", results.messages[0].status);
        }
    }
}