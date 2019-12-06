using System.Web;
using Xunit;

namespace Nexmo.Api.UnitTest
{
    public class SMS_test : TestBase
    {      

        [Fact]
        public void TestSmsRequest()
        {

            // ARRANGE
            var restUrl = "https://rest.nexmo.com";
            var expectedUri = $"{restUrl}/sms/json";            
            var responseContent = "{\"message-count\": \"1\",\"messages\": [{\"to\": \"17775551212\",\"message-id\": \"02000000A3AF32FA\",\"status\": \"0\",\"remaining-balance\": \"7.55560000\",\"message-price\": \"0.00480000\",\"network\": \"310004\"}]}";
            var from = "98975";
            var to = "17775551212";
            var text = "this is a test";
            var requestContent = $"from={from}&to={to}&text={HttpUtility.UrlEncode(text)}&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(uri: expectedUri, responseContent: responseContent, requestContent: requestContent);

            // ACT
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var response = client.SMS.Send(new SMS.SMSRequest() { to = to, from = from, text = text });

            //ASSERT
            Assert.True(response.messages.Count == 1);
        }
    }
}
