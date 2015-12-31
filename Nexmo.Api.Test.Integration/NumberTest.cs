using System.Linq;
using System.Net;
using NUnit.Framework;

namespace Nexmo.Api.Test.Integration
{
    [TestFixture]
    public class NumberTest
    {
        [Test]
        public void should_search_numbers()
        {
            var results = Number.Search(new Number.SearchRequest
            {
                country = "US"
            });
            Assert.Greater(results.count, 1);
            var firstNumber = results.numbers.First();
            Assert.AreEqual("US", firstNumber.country);
            Assert.True(!string.IsNullOrWhiteSpace(firstNumber.msisdn));
            Assert.True(!string.IsNullOrWhiteSpace(firstNumber.type));
            Assert.True(firstNumber.features.Any());
            Assert.Greater(firstNumber.cost, 0);

            // no numbers on 2nd page should be equal to a number on the 1st page (test pagination)
            var nextPage = Number.Search(new Number.SearchRequest
            {
                country = "US",
                index = "2"
            });
            Assert.False(results.numbers.Join(nextPage.numbers, o => o.msisdn, id => id.msisdn, (o, id) => o).Any());
        }

        [Test]
        public void should_buy_then_update_finally_cancel_number()
        {
            // find a number to buy
            const string country = "US";
            var results = Number.Search(new Number.SearchRequest
            {
                country = country
            });
            Assert.Greater(results.count, 1);
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