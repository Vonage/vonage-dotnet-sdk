using System;
using System.IO;
using System.Text;
using Moq;
using Nexmo.Api.Request;
using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    internal class NumberInsightTest : MockedWebTest
    {
        [Test]
        public void should_send_basic_ni_request()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"status\": 0,\"status_message\": \"Success\",\"request_id\": \"ca4f82b6-73aa-43fe-8c52-874fd9ffffff\",\"international_format_number\": \"15555551212\",\"national_format_number\": \"(555) 555-1212\",\"country_code\": \"US\",\"country_code_iso3\": \"USA\",\"country_name\": \"United States of America\",\"country_prefix\": \"1\"}")));
            var postDataStream = new MemoryStream();
            _request.Setup(e => e.GetRequestStream()).Returns(postDataStream);
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var result = NumberInsight.RequestBasic(new NumberInsight.NumberInsightBasicRequest
            {
                number = "15555551212"
            });

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/number/format/json", ApiUrl))),
                Times.Once);
            postDataStream.Position = 0;
            var sr = new StreamReader(postDataStream);
            var postData = sr.ReadToEnd();
            Assert.AreEqual(string.Format("number=15555551212&api_key={0}&api_secret={1}&", ApiKey, ApiSecret), postData);

            Assert.AreEqual("0", result.status);
            Assert.AreEqual("15555551212", result.international_format_number);
            Assert.AreEqual("(555) 555-1212", result.national_format_number);
        }

        [Test]
        public void should_send_standard_ni_request()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"status\": 0,\"status_message\": \"Success\",\"request_id\": \"bcf255a4-047c-4364-89b1-d5cf76ffffff\",\"international_format_number\": \"15555551212\",\"national_format_number\": \"(555) 555-1212\",\"country_code\": \"US\",\"country_code_iso3\": \"USA\",\"country_name\": \"United States of America\",\"country_prefix\": \"1\",\"request_price\": \"0.00500000\",\"remaining_balance\": \"1.1\",\"current_carrier\": {\"network_code\": \"310004\",\"name\": \"Verizon Wireless\",\"country\": \"US\",\"network_type\": \"mobile\"},\"original_carrier\": {\"network_code\": \"310004\",\"name\": \"Verizon Wireless\",\"country\": \"US\",\"network_type\": \"mobile\"}}")));
            var postDataStream = new MemoryStream();
            _request.Setup(e => e.GetRequestStream()).Returns(postDataStream);
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var result = NumberInsight.RequestStandard(new NumberInsight.NumberInsightBasicRequest
            {
                number = "15555551212"
            });

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/number/lookup/json", ApiUrl))),
                Times.Once);
            postDataStream.Position = 0;
            var sr = new StreamReader(postDataStream);
            var postData = sr.ReadToEnd();
            Assert.AreEqual(string.Format("number=15555551212&api_key={0}&api_secret={1}&", ApiKey, ApiSecret), postData);

            Assert.AreEqual("0", result.status);
            Assert.AreEqual("15555551212", result.international_format_number);
            Assert.AreEqual("(555) 555-1212", result.national_format_number);
            Assert.AreEqual("Verizon Wireless", result.current_carrier.name);
        }
    }
}