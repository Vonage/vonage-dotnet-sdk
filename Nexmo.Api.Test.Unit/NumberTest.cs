using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nexmo.Api.Test.Unit
{
    [TestClass]
    public class NumberTest : MockedWebTest
    {
        [TestMethod]
        public void should_search_numbers()
        {
            SetExpect($"{RestUrl}/number/search/?country=US&api_key={ApiKey}&api_secret={ApiSecret}&",
"{\"count\":177,\"numbers\":[{\"country\":\"US\",\"msisdn\":\"15102694548\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568490\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568491\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568492\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568973\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"}]}");

            var results = Number.Search(new Number.SearchRequest
            {
                country = "US"
            });

            Assert.AreEqual(177, results.count);
            Assert.AreEqual(5, results.numbers.Count());
        }

        [TestMethod]
        public void should_buy_number()
        {
            SetExpect($"{RestUrl}/number/buy",
"{\"error-code\":\"200\",\"error-code-label\":\"success\"}",
$"country=US&msisdn=17775551212&api_key={ApiKey}&api_secret={ApiSecret}&");

            var result = Number.Buy("US", "17775551212");

            Assert.AreEqual("200", result.ErrorCode);
        }

        [TestMethod]
        public void should_update_number()
        {
            SetExpect($"{RestUrl}/number/update",
"{\"error-code\":\"200\",\"error-code-label\":\"success\"}",
$"country=US&msisdn=17775551212&moHttpUrl=https%3a%2f%2ftest.test.com%2fmo&moSmppSysType=inbound&api_key={ApiKey}&api_secret={ApiSecret}&");

            var result = Number.Update(new Number.NumberUpdateCommand
            {
                country = "US",
                msisdn = "17775551212",
                moHttpUrl = "https://test.test.com/mo",
                moSmppSysType = "inbound"
            });

            Assert.AreEqual("200", result.ErrorCode);
        }

        [TestMethod]
        public void should_cancel_number()
        {
            SetExpect($"{RestUrl}/number/cancel",
"{\"error-code\":\"200\",\"error-code-label\":\"success\"}",
$"country=US&msisdn=17775551212&api_key={ApiKey}&api_secret={ApiSecret}&");

            var result = Number.Cancel("US", "17775551212");

            Assert.AreEqual("200", result.ErrorCode);
        }
    }
}