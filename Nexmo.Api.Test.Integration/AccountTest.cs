using NUnit.Framework;

namespace Nexmo.Api.Test.Integration
{
    [TestFixture]
    public class AccountTest
    {
        [Test]
        public void should_get_account_balance()
        {
            var balance = Account.GetBalance();
            Assert.AreEqual(.43d, balance);
        }

        [Test]
        public void should_get_pricing()
        {
            var pricing = Account.GetPricing("US");
            Assert.AreEqual("US-FIXED", pricing.networks[0].code);
            Assert.AreEqual("United States of America Landline", pricing.networks[0].network);
        }
    }
}