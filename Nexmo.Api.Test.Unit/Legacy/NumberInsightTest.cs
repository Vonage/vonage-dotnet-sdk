using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nexmo.Api.Test.Unit
{
    public class NumberInsightTest : TestBase
    {
        [Fact]
        public void SendBasicNiRequest()
        {
            //ARRANGE
            var expectedUri = $"{ApiUrl}/ni/basic/json";
            var expectedRequestContent = $"number=15555551212&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = "{\"status\": 0,\"status_message\": \"Success\",\"request_id\": \"ca4f82b6-73aa-43fe-8c52-874fd9ffffff\",\"international_format_number\": \"15555551212\",\"national_format_number\": \"(555) 555-1212\",\"country_code\": \"US\",\"country_code_iso3\": \"USA\",\"country_name\": \"United States of America\",\"country_prefix\": \"1\"}";
            Setup(uri: expectedUri, requestContent: expectedRequestContent, responseContent: expectedResponseContent);

            //ACT
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var response = client.NumberInsight.RequestBasic(new NumberInsight.NumberInsightRequest
            {
                Number = "15555551212"
            });

            //ASSERT
            Assert.Equal("0", response.Status);
            Assert.Equal("15555551212", response.InternationalFormatNumber);
            Assert.Equal("(555) 555-1212", response.NationalFormatNumber);
        }

        [Fact]
        public void SendStandardNiRequest()
        {
            //ARRANGE
            var expectedUri = $"{ApiUrl}/ni/standard/json";
            var expectedRequestContent = $"number=15555551212&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = "{\"status\": 0,\"status_message\": \"Success\",\"request_id\": \"bcf255a4-047c-4364-89b1-d5cf76ffffff\",\"international_format_number\": \"15555551212\",\"national_format_number\": \"(555) 555-1212\",\"country_code\": \"US\",\"country_code_iso3\": \"USA\",\"country_name\": \"United States of America\",\"country_prefix\": \"1\",\"request_price\": \"0.00500000\",\"remaining_balance\": \"1.1\",\"current_carrier\": {\"network_code\": \"310004\",\"name\": \"Verizon Wireless\",\"country\": \"US\",\"network_type\": \"mobile\"},\"original_carrier\": {\"network_code\": \"310004\",\"name\": \"Verizon Wireless\",\"country\": \"US\",\"network_type\": \"mobile\"}}";

            Setup(uri: expectedUri, requestContent: expectedRequestContent, responseContent: expectedResponseContent);

            //ACT
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var result = client.NumberInsight.RequestStandard(new NumberInsight.NumberInsightRequest
            {
                Number = "15555551212"
            });

            //ASSERT
            Assert.Equal("0", result.Status);
            Assert.Equal("15555551212", result.InternationalFormatNumber);
            Assert.Equal("(555) 555-1212", result.NationalFormatNumber);
            Assert.Equal("Verizon Wireless", result.CurrentCarrier.Name);

        }
    }
}
