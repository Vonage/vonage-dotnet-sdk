using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;

namespace Nexmo.Api.Test.Integration
{
    [TestFixture]
    public class ShortCodeTest
    {
        [Test]
        public void should_initiate_2fa()
        {
            var response = ShortCode.RequestTwoFactorAuth(new ShortCode.TwoFactorAuthRequest
            {
                to = ConfigurationManager.AppSettings["test_number"],
                pin = 1467
            });
            Assert.AreEqual("1", response.message_count);
        }

        [Test]
        public void should_initiate_alert()
        {
            var response = ShortCode.RequestAlert(new ShortCode.AlertRequest
            {
                to = ConfigurationManager.AppSettings["test_number"],
            }, new Dictionary<string, string>
            {
                {"mcount", "xyz123"}
            });
            Assert.AreEqual("1", response.message_count);
        }
    }
}