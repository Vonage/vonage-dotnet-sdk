using System.Collections.Generic;
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
                to = Configuration.Instance.Settings["test_number"],
                pin = 1467
            });
            Assert.AreEqual("1", response.message_count);
        }

        [Test]
        public void should_initiate_alert()
        {
            var response = ShortCode.RequestAlert(new ShortCode.AlertRequest
            {
                to = Configuration.Instance.Settings["test_number"],
            }, new Dictionary<string, string>
            {
                {"mcount", "xyz123"}
            });
            Assert.AreEqual("1", response.message_count);
        }
    }
}