using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    internal class NumberInsightTest : MockedWebTest
    {
        [Test]
        public void should_send_basic_ni_request()
        {
            SetExpect($"{ApiUrl}/ni/basic/json",
"{\"status\": 0,\"status_message\": \"Success\",\"request_id\": \"ca4f82b6-73aa-43fe-8c52-874fd9ffffff\",\"international_format_number\": \"15555551212\",\"national_format_number\": \"(555) 555-1212\",\"country_code\": \"US\",\"country_code_iso3\": \"USA\",\"country_name\": \"United States of America\",\"country_prefix\": \"1\"}",
$"number=15555551212&api_key={ApiKey}&api_secret={ApiSecret}&");

            var result = NumberInsight.RequestBasic(new NumberInsight.NumberInsightRequest
            {
                number = "15555551212"
            });

            Assert.AreEqual("0", result.status);
            Assert.AreEqual("15555551212", result.international_format_number);
            Assert.AreEqual("(555) 555-1212", result.national_format_number);
        }

        [Test]
        public void should_send_standard_ni_request()
        {
            SetExpect($"{ApiUrl}/ni/standard/json",
"{\"status\": 0,\"status_message\": \"Success\",\"request_id\": \"bcf255a4-047c-4364-89b1-d5cf76ffffff\",\"international_format_number\": \"15555551212\",\"national_format_number\": \"(555) 555-1212\",\"country_code\": \"US\",\"country_code_iso3\": \"USA\",\"country_name\": \"United States of America\",\"country_prefix\": \"1\",\"request_price\": \"0.00500000\",\"remaining_balance\": \"1.1\",\"current_carrier\": {\"network_code\": \"310004\",\"name\": \"Verizon Wireless\",\"country\": \"US\",\"network_type\": \"mobile\"},\"original_carrier\": {\"network_code\": \"310004\",\"name\": \"Verizon Wireless\",\"country\": \"US\",\"network_type\": \"mobile\"}}",
$"number=15555551212&api_key={ApiKey}&api_secret={ApiSecret}&");

            var result = NumberInsight.RequestStandard(new NumberInsight.NumberInsightRequest
            {
                number = "15555551212"
            });

            Assert.AreEqual("0", result.status);
            Assert.AreEqual("15555551212", result.international_format_number);
            Assert.AreEqual("(555) 555-1212", result.national_format_number);
            Assert.AreEqual("Verizon Wireless", result.current_carrier.name);
        }
    }
}