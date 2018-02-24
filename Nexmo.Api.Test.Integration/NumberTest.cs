using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nexmo.Api.Test.Integration
{
    [TestClass]
    public class NumberTest
    {
        [TestMethod]
        public void should_search_numbers()
        {
            var results = Number.Search(new Number.SearchRequest
            {
                country = "US"
            });
	        Assert.IsTrue(results.count > 1);
            var firstNumber = results.numbers.First();
            Assert.AreEqual("US", firstNumber.country);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(firstNumber.msisdn));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(firstNumber.type));
            Assert.IsTrue(firstNumber.features.Any());
            Assert.IsTrue(firstNumber.cost > 0);

            // no numbers on 2nd page should be equal to a number on the 1st page (test pagination)
            var nextPage = Number.Search(new Number.SearchRequest
            {
                country = "US",
                index = "2"
            });
            Assert.IsFalse(results.numbers.Join(nextPage.numbers, o => o.msisdn, id => id.msisdn, (o, id) => o).Any());
        }

        [TestMethod]
        public void should_buy_then_update_finally_cancel_number()
        {
            // find a number to buy
            const string country = "US";
            var results = Number.Search(new Number.SearchRequest
            {
                country = country
            });
            Assert.IsTrue(results.count > 1);
            var firstNumber = results.numbers.First().msisdn;

            // buy it
            Number.Buy(country, firstNumber);

            // try to update
            Number.Update(new Number.NumberUpdateCommand
            {
                country = country,
                msisdn = firstNumber,
                moHttpUrl = "https://test.test.com/mo",
                moSmppSysType = "inbound"
            });

            // cancel it
            Number.Cancel(country, firstNumber);
        }
    }
}